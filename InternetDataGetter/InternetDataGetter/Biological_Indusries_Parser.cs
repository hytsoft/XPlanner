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
        public BiologicalIndusriesParser(string BI_home)
        {
            Uri home_uri = new Uri(BI_home);
            base.address = home_uri;
        }

        public static void Parse(Uri uri)
        {
            HtmlDocument entirePage = DataGetter.GetHtmlpage(uri);
        }

        private static string GetProductName(HtmlDocument page)
        {
            List<string> ids = new List<string>();
            string id = "//div[@class='product-name']";
            ids.Add(id);
            List<KeyValuePair<string, HtmlNodeCollection>> rawProductName = DataGetter.GetDataByXPATH(page, ids);
            HtmlNode rawProductNameNode = rawProductName[0].Value[0];
            string productName = rawProductNameNode.InnerText.Trim();
            return productName;
        }

        private static string GetDescription(HtmlDocument page)
        {
            string id = "//meta[@name='description']";
            List<string> ids = new List<string>();
            ids.Add(id);
            List<KeyValuePair<string, HtmlNodeCollection>> description = DataGetter.GetDataByXPATH(page, ids);
             HtmlNode descriptionNode = description[0].Value[0];
            string descriptionStr = descriptionNode.OuterHtml;
            string[] description_splt = descriptionStr.Split('"');
            descriptionStr = description_splt[3];
            string[] description_splt2 = descriptionStr.Split(';');
            int last_idx = description_splt2.Count();
            descriptionStr = description_splt2[last_idx-1];
            return descriptionStr.Trim();
        }

        private static string GetSpecifications(HtmlDocument page)
        {
            string id = "//table[@id='product-attribute-specs-table']";
            List<string> ids = new List<string>();
            ids.Add(id);
            List<KeyValuePair<string, HtmlNodeCollection>> htmlSpecifications = DataGetter.GetDataByXPATH(page, ids);
            string specs = htmlSpecifications[0].Value[0].InnerText.Trim();
            return specs;
        }

        private static string GetShortDesc(HtmlDocument page)
        {
            string id = "";
            List<string> ids = new List<string>();
            ids.Add(id);
            List<KeyValuePair<string, HtmlNodeCollection>> htmlShortDesc = DataGetter.GetDataByXPATH(page, ids);
            string ShortDesc = htmlShortDesc[0].Value[0].InnerText.Trim();
            return ShortDesc;
        }

        public override HtmlDocument GetPage(Uri uri)
        {
            HtmlDocument Page = null;

            if (uri != null)
            {
                Page = DataGetter.GetHtmlpage(uri);
            }

            return Page;
        }

        public override List<Uri> GetCategories(HtmlDocument page)
        {
            List<string> ids = new List<string>();
            string id = "//a[@href]"; // define id for raw-categories retrieval
            ids.Add(id); // add id to ids list because 'GetDataByXPATH' can only get string list
                         
            // allocate string lists for each category
            List<string> str_Categories = new List<string>();
            List<Uri> Categories = new List<Uri>();

            // search for that id in page (BI home page)
            List<KeyValuePair<string, HtmlNodeCollection>> raw_categories = DataGetter.GetDataByXPATH(page, ids);

            // extract all product-categories from HtmlDocument
            for (int i=0; i< raw_categories[0].Value.Count(); i++)
            {
             
                if (raw_categories[0].Value[i].OuterHtml.Contains("http"))
                { 
                    if (raw_categories[0].Value[i].OuterHtml.Contains("products"))
                    {
                        string[] temp_str = raw_categories[0].Value[i].OuterHtml.Split('"');
                        str_Categories.Add(temp_str[1]);
                    }
                }
            }
            

            // divide into product-sub-categories
            for (int i=0; i<str_Categories.Count(); i++)
            {
                Uri curr_uri = new Uri(str_Categories[i]);
                string curr_str = str_Categories[i];
                if (CountChar(curr_str, '/') > 5)
                {

                    if (curr_str.Contains("cell-culture"))
                    {
                        Categories.Add(curr_uri);
                    }
                    else if (str_Categories[i].Contains("stem-cell-research"))
                    {
                        Categories.Add(curr_uri);
                    }
                    else if (str_Categories[i].Contains("human-cytogenetics"))
                    {
                        Categories.Add(curr_uri);
                    }
                    else if (str_Categories[i].Contains("areas-of-interest"))
                    {
                        Categories.Add(curr_uri);
                    }
                    else if (str_Categories[i].Contains("popular-brands"))
                    {
                        Categories.Add(curr_uri);
                    }
                    else if (str_Categories[i].Contains("mycoplasma"))
                    {
                        Categories.Add(curr_uri);
                    }
                    else if (str_Categories[i].Contains("molecular-biology"))
                    {
                        Categories.Add(curr_uri);
                    }
                    else if (str_Categories[i].Contains("lab-instruments"))
                    {
                        Categories.Add(curr_uri);
                    }

                }
            }

            return Categories;
        }

        public override List<Product> GetProducts(Uri category)
        {
            // allocate products-uri list
            List<Uri> products_uris = new List<Uri>();

            // get html page of current category
            HtmlDocument productsPage = GetPage(category);

            // allocate ids for products bulk
            List<string> ids = new List<string>();
            string id = "//div[@class='grid-container']"; ids.Add(id);
            List<KeyValuePair<string, HtmlNodeCollection>> raw_product_bulk = DataGetter.GetDataByXPATH(productsPage, ids);

            // check whether there are 3rd-order sub-category(ies)
            if (raw_product_bulk != null) 
            {
                // 3rd sub-category bulk is always the 2nd value's InnerHtml 
                string subcat_bulk = raw_product_bulk[0].Value[2].InnerHtml;

                List<string> subcat_bulk_sorted = parse_3rd_subcat(subcat_bulk);
                List<string> prod_ids = new List<string>();

                for (int i = 0; i < subcat_bulk_sorted.Count(); i++)
                {
                    string currLine = subcat_bulk_sorted[i];
                    Uri currUri = new Uri(currLine);
                    products_uris = get_products_uris(currUri);
                    for (int j = 0; j<products_uris.Count(); j++)
                    {
                        Uri currProdUri = products_uris[j];
                        Product product = GetProduct(currProdUri);
                    }
                }
            }
            else //(final sub-category-order reached)
            {
                List<string> prod_ids = new List<string>();
                string prod_id = "//ul[@class='products-list hover-effect']";
                prod_ids.Add(prod_id);
                List<KeyValuePair<string, HtmlNodeCollection>> products_bulk = DataGetter.GetDataByXPATH(productsPage, prod_ids);
            }

            List<Product> products = new List<Product>();
            
            return products;
        }

        public override Product GetProduct(Uri product)
        {
            HtmlDocument productPage = GetPage(product);
            //string GeneralDescription = getGeneralDescription(productPage);
            List<HtmlDocument> product_types = getProductTypes(productPage);
            if (product_types!=null)
            {
                for (int i = 0; i < product_types.Count(); i++)
                {
                    
                }
            }
            else
            {
                string product_name = GetProductName(productPage);
                string description = GetDescription(productPage);
                string specs = GetSpecifications(productPage);
            }

            return null;
        }

        public int CountChar(string str, char ch)
        {
            int counter = 0;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i].Equals(ch))
                    {
                        counter++;
                    }      
            }

            return counter;
        }

        public List<string> parse_3rd_subcat(string subcat)
        {
            string[] subcat_raw = subcat.Split('<');
            List<string> subcat_sorted = new List<string>();
            for (int i = 0; i < subcat_raw.Count(); i++)
            {
                string currLine = subcat_raw[i];
                if (currLine.Contains("http"))
                {
                    string[] currLine_splt = currLine.Split('"');
                    subcat_sorted.Add(currLine_splt[1]);
                }
            }

            return subcat_sorted;
        }

        public List<Uri> get_products_uris(Uri uri)
        {
            List<string> prod_ids = new List<string>();
            List<Uri> products_uris = new List<Uri>();
            string prod_id = "//ul[@class='products-list hover-effect']";
            prod_ids.Add(prod_id);
            HtmlDocument prodPage = GetPage(uri);
            List<KeyValuePair<string, HtmlNodeCollection>> products_bulk_raw = DataGetter.GetDataByXPATH(prodPage, prod_ids);

            // proceed if 'products_bulk_raw' is-NOT-empty
            int res_count = products_bulk_raw.Count();
            if (res_count == 1)
            {
                string products_bulk = products_bulk_raw[0].Value[0].InnerHtml;
                string[] products_bulk_splt = products_bulk.Split('\n');
                for (int j = 0; j < products_bulk_splt.Count(); j++)
                {
                    if (products_bulk_splt[j].Contains("product-name"))
                    {

                        string currProd = products_bulk_splt[j];
                        string[] currProdComponents = currProd.Split('"');
                        Uri product_uri = new Uri(currProdComponents[3]);
                        products_uris.Add(product_uri);
                    }
                }
            }

            return products_uris;
        }

        public List<HtmlDocument> getProductTypes(HtmlDocument productPage)
        {
            string id = "//tr";
            List<string> ids = new List<string>();
            ids.Add(id);
            List<KeyValuePair<string, HtmlNodeCollection>> raw_types = DataGetter.GetDataByXPATH(productPage, ids);


            return null;
        }

    }
}
