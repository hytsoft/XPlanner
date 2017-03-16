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

            //List<string> elements = new List<string>();

            //elements.Add("//div");
            //elements.Add("contentStage clearfix");

            //List<string> headers = new List<string>();
            //headers.Add("Components");
            //headers.Add("Application");
            //headers.Add("Features and Benefits");
            //headers.Add("General description");
            //headers.Add("Packaging");
            //headers.Add("Reconstitution");

            //elements.Add("//div[@class='descriptionContent']");


            //List<KeyValuePair<string, HtmlNodeCollection>> dataDescription = DataGetter.GetDataByXPATH(new Uri(uri), elements);
            //Description description = SigmaAldrichParser.ParseDescription(dataDescription, headers);

            //elements.Clear();
            //elements.Add("productDetailProperties");
            //List<KeyValuePair<string, HtmlNode>> data = dg.GetData(new Uri(uri), elements);
            //List<TableData> properties =  SigmaAldrichParser.ParseDetailProperties(data[0].Value);

            //List<Description> products = SigmaAldrichParser.GetAllProductsForCategory("AcidsBases", 20, 1);
            //SigmaAldrichParser.GetCategoryPaginationUrls(SigmaAldrichConstants.AcidsBases);
            
            //Product p = SigmaAldrichParser.GetProduct(@"http://www.sigmaaldrich.com/catalog/product/sial/d3060");
            //SigmaAldrichParser.WriteProductDataToCSVFile("data.csv", p);

            //start from page 529
            #region GetManyProducts
            //string category = "MolecularBiology";
            //string categoryUri = SigmaAldrichParser.GetCategoryUri(category);
            //List<string> paginationUri = SigmaAldrichParser.GetCategoryPaginationUrls(categoryUri, 0);

            //Console.WriteLine(string.Format("{0}: Found {1} product pages for category : {2}", DateTime.Now, paginationUri.Count, category));

            //for (int i = 0; i < paginationUri.Count; i++)
            //{
            //    List<string> productsUris = SigmaAldrichParser.GetProductsUri(paginationUri[i]);

            //    List<Product> products = new List<Product>();
            //    List<string> elements = new List<string>();
            //    List<string> headers = new List<string>();
            //    headers.Add("Components");
            //    headers.Add("Application");
            //    headers.Add("Features and Benefits");
            //    headers.Add("General description");
            //    headers.Add("Packaging");
            //    headers.Add("Reconstitution");
            //    headers.Add("Other Notes");
            //    headers.Add("Legal Information");
            //    headers.Add("Caution");
            //    headers.Add("Biochem/physiol Actions");
            //    headers.Add("Preparation Note");

            //    elements.Add("//div[@class='descriptionContent']");

            //    Console.WriteLine(string.Format("{0}: Found {1} products on page {2} for category {3}", DateTime.Now, productsUris.Count, i + 1, category));

            //    for (int j = 0; j < productsUris.Count; j++)
            //    {
            //        //List<KeyValuePair<string, HtmlNodeCollection>> dataDescription = DataGetter.GetDataByXPATH(new Uri(productsUris[i]), elements);
            //        //Description description = SigmaAldrichParser.ParseDescription(dataDescription, headers);

            //        //products.Add(description);

            //        //products.Add(SigmaAldrichParser.GetProduct(productsUris[i]));

            //        //SigmaAldrichParser.GetProduct(productsUris[i]
            //        Product p = SigmaAldrichParser.GetProduct(productsUris[i]);
            //        SigmaAldrichParser.WriteProductDataToCSVFile(category + ".csv", p);

            //        System.Threading.Thread.Sleep((int)DataGetter.GetRandomNumber(5.0, 15.0) * 1000);
            //        Console.WriteLine(string.Format("{0}: Successfully wrote product {1}/{2}: {3}", DateTime.Now, j + 1 , productsUris.Count, p.Name));
            //    } 
            //}
            #endregion

            #region getProductsStarting from page X
            //string category = "MolecularBiology";
            //string categoryUri = SigmaAldrichParser.GetCategoryUri(category);
            //List<string> paginationUri = SigmaAldrichParser.GetCategoryPaginationUrls(categoryUri, 0);

            //Console.WriteLine(string.Format("{0}: Found {1} product pages for category : {2}", DateTime.Now, paginationUri.Count, category));
            //int desiredPage = 1798;
            //for (int i = desiredPage; i < paginationUri.Count; i++)
            //{
            //    List<string> productsUris = SigmaAldrichParser.GetProductsUri(paginationUri[i]);

            //    List<Product> products = new List<Product>();
            //    List<string> elements = new List<string>();
            //    List<string> headers = new List<string>();
            //    headers.Add("Components");
            //    headers.Add("Application");
            //    headers.Add("Features and Benefits");
            //    headers.Add("General description");
            //    headers.Add("Packaging");
            //    headers.Add("Reconstitution");
            //    headers.Add("Other Notes");
            //    headers.Add("Legal Information");
            //    headers.Add("Caution");
            //    headers.Add("Biochem/physiol Actions");
            //    headers.Add("Preparation Note");

            //    elements.Add("//div[@class='descriptionContent']");

            //    Console.WriteLine(string.Format("{0}: Found {1} products on page {2}/{3} for category {4}", DateTime.Now, productsUris.Count, i + 1, paginationUri.Count, category));

            //    for (int j = 0; j < productsUris.Count; j++)
            //    {
            //        //List<KeyValuePair<string, HtmlNodeCollection>> dataDescription = DataGetter.GetDataByXPATH(new Uri(productsUris[i]), elements);
            //        //Description description = SigmaAldrichParser.ParseDescription(dataDescription, headers);

            //        //products.Add(description);

            //        //products.Add(SigmaAldrichParser.GetProduct(productsUris[i]));

            //        //SigmaAldrichParser.GetProduct(productsUris[i]
            //        Product p = SigmaAldrichParser.GetProduct(productsUris[j]);
            //        SigmaAldrichParser.WriteProductDataToCSVFile(category + ".csv", p);

            //        System.Threading.Thread.Sleep((int)DataGetter.GetRandomNumber(5.0, 15.0) * 1000);
            //        Console.WriteLine(string.Format("{0}: Successfully wrote product {1}/{2}: {3}", DateTime.Now, j + 1, productsUris.Count, p.Name));
            //    }
            //}
            #endregion

            Uri test_uri = new Uri("http://www.bioind.com/bio-pure-human-serum-albumin-hsa/");
            BiologicalIndusriesParser.Parse(test_uri);

        }
    }
}
