 using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

    /// <summary>
    /// Summary description for KeggApi
    /// </summary>
    public class KeggApi
    {
        private List<string[]> genes;
        private List<string[]> pathways;
        private List<string> coveredPathways;
        private string findGeneStr;
        private string linkPathwayStr;
        private static string getImageStr; // map05169/image";
        private string getGenesCountStr;

        public KeggApi()
        {
            genes = new List<string[]>();
            pathways = new List<string[]>();
            coveredPathways = new List<string>();
            findGeneStr = "http://rest.kegg.jp/find/genes/";
            linkPathwayStr = "http://rest.kegg.jp/link/pathway/";
            getImageStr = "http://rest.kegg.jp/get/";
            getGenesCountStr = "http://rest.kegg.jp/link/hsa/";
        }
        public List<string[]> Pathways
        {
            get { return pathways; }
            set { pathways = value; }
        }
        public List<string[]> Genes
        {
            get { return genes; }
            set { genes = value; }
        }

        public bool findGenes(string geneName)
        {
            #region serach gene in DB
                try
                {

                    Connection newCon = new Connection();
                    string query = String.Format("select * from kegggenes WHERE description like '%{0}%'", geneName);
                    MySqlCommand command = new MySqlCommand(query, newCon.conn);
                    MySqlDataReader dr = command.ExecuteReader();

                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            if (Regex.IsMatch(dr["description"].ToString(), string.Format(@"\b{0}\b", geneName)))
                            {
                                string[] tmp = new string[3];
                                tmp[0] = geneName;
                                tmp[1] = dr["geneId"].ToString();
                                tmp[2] = dr["description"].ToString();
                                genes.Add(tmp);
                            }
                        }
                        return true;
                    }
                    newCon.conn.Close();
                }
                catch(Exception ex)
                {
                    return false;
                }
            #endregion
            int counter=0;
            HttpWebRequest req = WebRequest.Create(findGeneStr + geneName) as HttpWebRequest;
            try
            {
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", resp.StatusCode);
                        throw new ApplicationException(message);
                    }
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    while (reader.Peek() >= 0)
                    {
                        string line = reader.ReadLine();
                        if (line.StartsWith("hsa", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] parts = line.Split('\t');
                            if (Regex.IsMatch((parts[1].Split(';'))[0], string.Format(@"\b{0}\b", geneName)))
                            {
                                string[] tmp = new string[3];
                                tmp[0] = geneName;
                                tmp[1] = parts[0];
                                tmp[2] = (parts[1].Split(';'))[0];
                                genes.Add(tmp);
                            }
                            counter++;
                        }
                    }
                }
                if (counter == 0)
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool linkGenesToPathway()
        {
            int counter = 0;
            string parameters = String.Empty;
            foreach (string[] gene in genes)
            {
                if (genes[genes.Count - 1][1] == gene[1])
                    parameters += gene[1];
                else
                    parameters += gene[1] + "+";
            }
            HttpWebRequest req = WebRequest.Create(linkPathwayStr + parameters) as HttpWebRequest;
            try
            {
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", resp.StatusCode);
                        throw new ApplicationException(message);
                    }
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    while (reader.Peek() >= 0)
                    {
                        string line = reader.ReadLine();
                        if (line.StartsWith("hsa", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] parts = line.Split('\t');
                            string[] tmp = new string[2];
                            tmp[0] = genes.Find(p => p[1] == parts[0])[0];
                            tmp[1] = parts[1];
                            pathways.Add(tmp);
                        }
                        counter++;
                    }
                    if (counter == 0)
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static Image getImage(string pathway)
        {
            HttpWebRequest req = WebRequest.Create(getImageStr + pathway + "/image") as HttpWebRequest;
            Image result = null;
            try
            {
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", resp.StatusCode);
                        throw new ApplicationException(message);
                    }
                    Stream reader = resp.GetResponseStream();
                    result = Image.FromStream(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public int genesCountInPathway(string pathway)
        {
            #region get genes count from DB
            try
            {
                Connection newCon = new Connection();
                string query = String.Format("select genesCount from keggpathways WHERE pathId = '{0}'", pathway.Replace("hsa","map"));
                MySqlCommand command = new MySqlCommand(query, newCon.conn);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    string count = dr["genesCount"].ToString();
                    return (Convert.ToInt32(count));
                }
                newCon.conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
            int genesCount = 0;
            HttpWebRequest req = WebRequest.Create(getGenesCountStr + pathway) as HttpWebRequest;
            try
            {
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", resp.StatusCode);
                        throw new ApplicationException(message);
                    }
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    while (reader.Peek() >= 0)
                    {
                        reader.ReadLine();
                        genesCount++;
                    }
                }
                return genesCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<string> getGenes(string pathway)
        {
            coveredPathways.Clear();
            List<string> tmp = new List<string>();
            try
            {      
                foreach (string[] s in this.Pathways)
                {
                    if (pathway == s[1] && (coveredPathways.Find(p=>p == s[0]))==null)
                    {
                        tmp.Add(s[0]);
                        coveredPathways.Add(s[0]);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tmp;
        }
        public string getPathwayName(string pathId)
        {
            #region serach pathway in DB
            try
            {
                Connection newCon = new Connection();
                string query = String.Format("select name from keggpathways WHERE pathId = '{0}'", pathId);
                MySqlCommand command = new MySqlCommand(query, newCon.conn);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    string name = dr["name"].ToString().Replace("- Homo sapiens (human)", "");
                    return name;
                }
                newCon.conn.Close();
            }
            catch (Exception ex)
            {
                return "";
            }
            #endregion
            pathId = pathId.Remove(0, 8); // path:hsa04310 => 04310
            HttpWebRequest req = WebRequest.Create("http://rest.kegg.jp/find/pathway/" + pathId) as HttpWebRequest;
            try
            {
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", resp.StatusCode);
                        throw new ApplicationException(message);
                    }
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    string line = reader.ReadLine();
                    if (line.StartsWith("path", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string[] parts = line.Split('\t');
                        return (parts[1].Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public void clear()
        {
            genes.Clear();
            pathways.Clear();
            coveredPathways.Clear();
        }
       
    }


