using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace InternetDataGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            string BI_Link = "http://www.bioind.com/"; //define main link to BI home page
            BiologicalIndusriesParser BI_website_parser = new BiologicalIndusriesParser(BI_Link); //create new class instance to use abstract methods one by one
            List<WebsiteParser> websites = new List<WebsiteParser>();
            websites.Add(BI_website_parser);


            WebsiteParserManager wbm = new WebsiteParserManager(websites);

            wbm.GetProducts();
        }
    }
}
