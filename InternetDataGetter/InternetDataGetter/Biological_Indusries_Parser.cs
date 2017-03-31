using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InternetDataGetter
{
    public class BiologicalIndusriesParser : WebsiteParser
    {
               
        public static void Parse(Uri uri)
        {
            HtmlDocument entirePage = DataGetter.GetHtmlpage(uri);
            //string productName = GetProductName(entirePage, "//h2[@class='product-name']");
            //string description = GetDescription(entirePage, "//meta[@name='description']");
            specifications Specifications = GetSpecifications(entirePage, "//td[@class='data']");
        }

        private static string GetProductName(HtmlDocument page, string element)
        {
            List<string> elements = new List<string>();
            elements.Add(element);
            List<KeyValuePair<string, HtmlNodeCollection>> productName = DataGetter.GetDataByXPATH(page, elements);
            HtmlNode productNameNode = productName[0].Value[0];
            return productNameNode.InnerText;
        }

        private static string GetDescription(HtmlDocument page, string element)
        {
            List<string> elements = new List<string>();
            elements.Add(element);
            List<KeyValuePair<string, HtmlNodeCollection>> description = DataGetter.GetDataByXPATH(page, elements);
            HtmlNode descriptionNode = description[0].Value[0];
            string descriptionStr = descriptionNode.OuterHtml;
            descriptionStr = descriptionStr.Replace("\n", "");
            string[] list_description = descriptionStr.Split('=');        
            descriptionStr = list_description[2];
            descriptionStr = descriptionStr.Replace(">", "");
            return descriptionStr.Trim();

        }

        private struct specifications
        {
            public string form;
            public string storage_conditions;
            public string quality_control;
        }

        private static specifications GetSpecifications(HtmlDocument page, string element)
        {

            specifications specifications = new specifications();
            List<string> elements = new List<string>();
            elements.Add(element);
            List<KeyValuePair<string, HtmlNodeCollection>> htmlSpecifications = DataGetter.GetDataByXPATH(page, elements);
            HtmlNode Form = htmlSpecifications[0].Value[0];
            HtmlNode storageConditions = htmlSpecifications[0].Value[1];
            HtmlNode qualityControl = htmlSpecifications[0].Value[2];
            specifications.form = Form.InnerText; ;
            specifications.storage_conditions = storageConditions.InnerText;
            specifications.quality_control = qualityControl.InnerText;
            return specifications;

        }

        public override HtmlDocument GetMainPage(Uri uri)
        {
            HtmlDocument mainPage = null;

            if (uri != null)
            {
                mainPage = DataGetter.GetHtmlpage(uri);
            }

            return mainPage;
        }

        public override List<Uri> GetCategories(HtmlDocument page)
        {
            List<string> ids = new List<string>();
            string id = "//a[@href]"; // define id for raw-categories retrieval
            ids.Add(id); // add id to ids list because 'GetDataByXPATH' can only get string list
                         
            // allocate string lists for each category
            List<string> str_Categories = new List<string>();
            List<string> str_CellCulture = new List<string>();
            List<string> str_StemCellResearch = new List<string>();
            List<string> str_HumanCytogenetics = new List<string>();
            List<string> str_AOI = new List<string>();
            List<string> str_PopularBrands = new List<string>();
            List<string> str_Mycoplasma = new List<string>();
            List<string> str_MolecularBiology = new List<string>();
            List<string> str_LabInstruments = new List<string>();

            // search for that id in page (BI home page)
            List<KeyValuePair<string, HtmlNodeCollection>> raw_categories = DataGetter.GetDataByXPATH(page, ids);

            // extract all product-categories from HtmlDocument
            for (int i=0; i< raw_categories[0].Value.Count(); i++)
            {
                
                if (raw_categories[0].Value[i].OuterHtml.Contains("products"))
                {
                    string[] temp_str = raw_categories[0].Value[i].OuterHtml.Split('"');
                    str_Categories.Add(temp_str[1]);
                }
            }

            // divide into product-sub-categories
            for (int i=0; i<str_Categories.Count(); i++)
            {
                if (str_Categories[i].Contains("cell-culture"))
                {
                    str_CellCulture.Add(str_Categories[i]);
                }
                else if (str_Categories[i].Contains("stem-cell-research"))
                {
                    str_StemCellResearch.Add(str_Categories[i]);
                }
                else if (str_Categories[i].Contains("human-cytogenetics"))
                {
                    str_HumanCytogenetics.Add(str_Categories[i]);
                }
                else if (str_Categories[i].Contains("areas-of-interest"))
                {
                    str_AOI.Add(str_Categories[i]);
                }
                else if (str_Categories[i].Contains("popular-brands"))
                {
                    str_PopularBrands.Add(str_Categories[i]);
                }
                else if (str_Categories[i].Contains("mycoplasma"))
                {
                    str_Mycoplasma.Add(str_Categories[i]);
                }
                else if (str_Categories[i].Contains("molecular-biology"))
                {
                    str_MolecularBiology.Add(str_Categories[i]);
                }
                else if (str_Categories[i].Contains("lab-instruments"))
                {
                    str_LabInstruments.Add(str_Categories[i]);
                }
            }
            
            List<Uri> uri = new List<Uri>();
            return uri;
        }

        public override List<Product> GetProducts(Uri category)
        {
            List<Product> products = new List<Product>();
            return products;
        }

        public override Product GetProduct(Uri product)
        {
            throw new NotImplementedException();
        }

    }
}
