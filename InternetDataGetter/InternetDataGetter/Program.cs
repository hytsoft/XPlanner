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
            BiologicalIndusriesParser BI_website_parser = new BiologicalIndusriesParser(); //create new class instance to use abstract methods one by one
            string BILink = "http://www.bioind.com/israel/"; //define main link to BI home page
            Uri BI_uri = new Uri(BILink); //create uri for page retrieval
            List<Uri> Categories = new List<Uri>(); //create list of category uris

            HtmlDocument mainPage = BI_website_parser.GetMainPage(BI_uri); // get BI home page as HtmlDocument

            Categories = BI_website_parser.GetCategories(mainPage); // get all category uris
            
            //List<WebsiteParser> websites = new list<WebsiteParser>;

            //websites.Add(BI_website_parser);
        }
    }
}
