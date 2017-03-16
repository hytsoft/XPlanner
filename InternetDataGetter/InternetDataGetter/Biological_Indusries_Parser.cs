using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InternetDataGetter
{
    public static class BiologicalIndusriesParser
    {
        //string ProductName = GetProductName(uri, elements);

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
            List<KeyValuePair<string, HtmlNodeCollection>> htmlspecifications = DataGetter.GetDataByXPATH(page, elements);
            HtmlNode Form = htmlspecifications[0].Value[0];
            HtmlNode Storage_Conditions = htmlspecifications[0].Value[1];
            HtmlNode Quality_Control = htmlspecifications[0].Value[2];
            specifications.form = Form.InnerText; ;
            specifications.storage_conditions = Storage_Conditions.InnerText;
            specifications.quality_control = Quality_Control.InnerText;
            return specifications;

        }
    }
}
