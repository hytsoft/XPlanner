using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.IO;

namespace InternetDataGetter
{
    public static class DataGetter
    {
        public static List<KeyValuePair<string, HtmlNode>> GetDataByID(Uri uri, string element_tag, List<string> element_ids)
        {
            WebClient client = new WebClient();
            List<KeyValuePair<string, HtmlNode>> data = new List<KeyValuePair<string, HtmlNode>>();

            byte[] byteArray = client.DownloadData(uri);

            //Convert Byte Array into Stram
            Stream stream = new MemoryStream(byteArray);

            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream);
            
            foreach (string element in element_ids)
            {
                HtmlNode node = doc.DocumentNode.Descendants(element_tag).FirstOrDefault(x => x.Id == element);
                //IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes(element);

                if (node != null)
                {
                    data.Add(new KeyValuePair<string, HtmlNode> (element, node));
                }
            }

            return data;
        }

        public static List<KeyValuePair<string, HtmlNode>> GetDataByID(HtmlDocument html_doc, string element_tag, List<string> element_ids)
        {
            List<KeyValuePair<string, HtmlNode>> data = new List<KeyValuePair<string, HtmlNode>>();

            foreach (string element in element_ids)
            {
                HtmlNode node = html_doc.DocumentNode.Descendants(element_tag).FirstOrDefault(x => x.Id == element);
                //IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes(element);

                if (node != null)
                {
                    data.Add(new KeyValuePair<string, HtmlNode>(element, node));
                }
            }

            return data;
        }

        public static List<KeyValuePair<string, HtmlNodeCollection>> GetDataByXPATH(Uri uri, List<string> element_ids)
        {
            WebClient client = new WebClient();
            List<KeyValuePair<string, HtmlNodeCollection>> data = new List<KeyValuePair<string, HtmlNodeCollection>>();

            byte[] byteArray = client.DownloadData(uri);

            //Convert Byte Array into Stram
            Stream stream = new MemoryStream(byteArray);

            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream);

            foreach (string element in element_ids)
            {
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(element);
                //IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes(element);

                if (nodes != null)
                {
                    data.Add(new KeyValuePair<string, HtmlNodeCollection>(element, nodes));
                }
            }

            return data;
        }

        public static List<KeyValuePair<string, HtmlNodeCollection>> GetDataByXPATH(HtmlDocument html_doc, List<string> element_ids)
        {
            List<KeyValuePair<string, HtmlNodeCollection>> data = new List<KeyValuePair<string, HtmlNodeCollection>>();

            foreach (string element in element_ids)
            {
                HtmlNodeCollection nodes = html_doc.DocumentNode.SelectNodes(element);
                //IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes(element);

                if (nodes != null)
                {
                    data.Add(new KeyValuePair<string, HtmlNodeCollection>(element, nodes));
                }
            }

            return data;
        }

        public static HtmlDocument GetHtmlpage(Uri uri)
        {
            WebClient client = new WebClient();
            List<KeyValuePair<string, HtmlNode>> data = new List<KeyValuePair<string, HtmlNode>>();

            byte[] byteArray = client.DownloadData(uri);

            //Convert Byte Array into Stram
            Stream stream = new MemoryStream(byteArray);

            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream);

            return doc;
        }

        public static void SaveDataToCSVFile(List<string> data, string filename)
        {
            string line = "";
            for (int i=0; i < data.Count - 1; i++)
            {
                line += data[i] + ",";
            }
            line += data[data.Count - 1];

            using (StreamWriter objWriter = new StreamWriter(filename))
            {
                objWriter.Write(line);
            }
        }

        public static double GetRandomNumber(double minimum, double maximum)   
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
