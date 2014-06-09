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

        private string[] getPatway(string pathId)
        {
            return pathways.Find(p => p[1] == pathId);
        }


        public string findGenes(string[] geneName)
        {
           Connection newCon = new Connection();
            string notfoundGenes = String.Empty;
            MySqlCommand command = new MySqlCommand("", newCon.conn);
            

            foreach (string gene in geneName)
            {
                if (gene == "") continue;

                if (findGenesFromDb(gene, command)) continue;
                else if (findGenesFromKeggDB(gene)) continue;
                else notfoundGenes += gene + " ";
            }

            newCon.conn.Close();
            return notfoundGenes;

          
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
                            string[] tmp = new string[4];
                            tmp[0] = genes.Find(p => p[1] == parts[0])[0];
                            tmp[1] = parts[1];
                            tmp[2] = getPathwayName(tmp[1]);
                            tmp[3] = genesCountInPathway(tmp[1]).ToString();

                            string[] ptwy = getPatway(tmp[1]);

                            if (ptwy != null)
                            {
                                if (!ptwy[0].Contains(tmp[0]))
                                    ptwy[0] += "," + tmp[0];
                            }
                            else
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
            int gene_count;

            if ((gene_count = pathwayGeneCountDb(pathway)) != -1) return gene_count;
            else if ((gene_count = pathwayGeneCountKeggDb(pathway)) != -1) return gene_count;
            else return 0;
           
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
            string patw_name = String.Empty;

            if ((patw_name = getPathwayNameDb(pathId)) != String.Empty) return patw_name;
            else if ((patw_name = getPathwayNameKeggDb(pathId)) != String.Empty) return patw_name;
            else return String.Empty;
            
        }
        public void clear()
        {
            genes.Clear();
            pathways.Clear();
            coveredPathways.Clear();
        }



        #region KeggDB
        private bool findGenesFromKeggDB(string geneName)
        {
            int counter = 0;
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
        private int pathwayGeneCountKeggDb(string pathway)
        {
            int ret = -1; int genesCount = 0;
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
                ret = genesCount;
            }
            catch (Exception ex)
            {
                ret = -1;
            }

            return ret;
        }
        private string getPathwayNameKeggDb(string pathId)
        {
            string ret = String.Empty;
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
                        ret = (parts[1].Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                ret = String.Empty;
            }

            return ret;
        }
        #endregion

        #region LocalDB
        private bool findGenesFromDb(string geneName, MySqlCommand command)
        {
            bool ret = false;
            #region serach gene in DB
            try
            {
                string query = String.Format("select * from kegggenes WHERE description like '%{0}%'", geneName);
                command.CommandText = query;
                MySqlDataReader dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
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
                    dr.Close();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                return ret;
            }

            return ret;

            #endregion
        }
        public int pathwayGeneCountDb(string pathway)
        {
            int ret = -1;
            Connection newCon = new Connection();
            try
            {
                string query = String.Format("select genesCount from keggpathways WHERE pathId = '{0}'", pathway.Replace("hsa", "map"));
                MySqlCommand command = new MySqlCommand(query, newCon.conn);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    string count = dr["genesCount"].ToString();
                    newCon.conn.Close();
                    ret = (Convert.ToInt32(count));
                }
                newCon.conn.Close();
            }
            catch (Exception ex)
            {
                newCon.conn.Close();
                ret = -1;
            }

            return ret;

        }
        private string getPathwayNameDb(string pathId)
        {
            string ret = String.Empty;
            Connection newCon = new Connection();
            try
            {
                string query = String.Format("select name from keggpathways WHERE pathId = '{0}'", pathId);
                MySqlCommand command = new MySqlCommand(query, newCon.conn);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    string name = dr["name"].ToString().Replace("- Homo sapiens (human)", "");
                    newCon.conn.Close();
                    ret = name;
                }
                newCon.conn.Close();
            }
            catch (Exception ex)
            {
                newCon.conn.Close();
                ret = String.Empty;
            }

            return ret;
        }
        #endregion







    }


