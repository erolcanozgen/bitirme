 using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net;
    using System.IO;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

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

        public void findGenes(string geneName)
        {
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
                            if (Regex.IsMatch(parts[1], string.Format(@"\b{0}\b", geneName)))
                            {
                                string[] tmp = new string[2];
                                tmp[0] = geneName;
                                tmp[1] = parts[0];
                                genes.Add(tmp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void linkGenesToPathway()
        {
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
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            List<string> tmp = new List<string>();
            try
            {      
                foreach (string[] s in this.Pathways)
                {
                    if (pathway == s[1] && (coveredPathways.Find(p=>p == pathway))==null)
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
       
    }


