using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace InternetDataGetter
{

    /*
    What's the idea ?
    I want to be able to get a webpage or a part of it, and save it in the form of a csv file for later extraction.

    What do i need ?
    1.Be able to get the webpage
    2.Search the webpage for the wanted element\s
    3.Save the data to file as csv
    4.Create some randomization code to prevent the site from blocking me...
        */
    class Program
    {
        static void Main(string[] args)
        {
            string uri = @"http://www.sigmaaldrich.com/catalog/product/sigma/y2377";
            //string uri = @"http://www.sigmaaldrich.com/catalog/product/sial/a3340";
            //string uri = @"http://www.sigmaaldrich.com/catalog/product/aldrich/330108";
            //string uri = @"http://www.sigmaaldrich.com/israel.html";

            List<string> elements = new List<string>();

            //elements.Add("//div");
            //elements.Add("contentStage clearfix");

            List<string> headers = new List<string>();
            headers.Add("Components");
            headers.Add("Application");
            headers.Add("Features and Benefits");
            headers.Add("General description");
            headers.Add("Packaging");
            headers.Add("Reconstitution");

            elements.Add("//div[@class='descriptionContent']");


            //List<KeyValuePair<string, HtmlNodeCollection>> dataDescription = DataGetter.GetDataByXPATH(new Uri(uri), elements);
            //Description description = SigmaAldrichParser.ParseDescription(dataDescription, headers);

            //elements.Clear();
            //elements.Add("productDetailProperties");
            //List<KeyValuePair<string, HtmlNode>> data = dg.GetData(new Uri(uri), elements);
            //List<TableData> properties =  SigmaAldrichParser.ParseDetailProperties(data[0].Value);

            //List<Description> products = SigmaAldrichParser.GetAllProductsForCategory("AcidsBases", 20, 1);
            //SigmaAldrichParser.GetCategoryPaginationUrls(SigmaAldrichConstants.AcidsBases);

            Product p = SigmaAldrichParser.GetProduct(@"http://www.sigmaaldrich.com/catalog/product/sial/62915?lang=en&region=IL");
            SigmaAldrichParser.WriteProductDataToCSVFile("data.csv", p);

        }
    }
}
