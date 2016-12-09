﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Globalization;

namespace InternetDataGetter
{
    public class TableData
    {
        public string left;
        public string right;
    }

    public class IngredientData
    {
        public string Name;
        public double Amount;
        public string Unit;
    }

    public class Description
    {
        public List<IngredientData> Components;
        public string Application;
        public string FeaturesBenefits;
        public string GeneralDescription;
        public string Packaging;
        public string Reconstitution;

    }

    class SigmaAldrichParser
    {
        public static List<TableData> ParseDetailProperties(HtmlNode node)
        {
            string nodesText;
            List<TableData> tableData = new List<TableData>();

            HtmlNodeCollection table = node.SelectNodes("//table");

            foreach (HtmlNode currNode in table)
            {
                HtmlNodeCollection tableBody = currNode.SelectNodes("//tbody");


                foreach (HtmlNode tableRow in tableBody)
                {
                    HtmlNodeCollection tableRows = tableRow.SelectNodes("tr");

                    if (tableRows != null)
                    {
                        foreach (HtmlNode row in tableRows)
                        {

                            HtmlNodeCollection cells = row.SelectNodes("th|td");
                            if (cells != null)
                            {
                                foreach (HtmlNode cell in cells)
                                {
                                    string tmp = WebUtility.HtmlDecode(row.InnerText).Replace("\n", "").Replace("\r", "").Replace("\t", "").Trim();

                                    if (tmp != "")
                                    {
                                        KeyValuePair<string, string> pair = GetTableRowData(tmp);

                                        if (!tableData.Exists(x => x.left == pair.Key && x.right == pair.Value))
                                        {
                                            tableData.Add(new TableData() { left = pair.Key, right = pair.Value });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return tableData;
        }

        public static Description ParseDescription(List<KeyValuePair<string, HtmlNodeCollection>> nodes, List<string> headers)
        {
            string nodesText;
            List<IngredientData> componenstData = new List<IngredientData>();
            Description description = new Description();

            foreach (KeyValuePair<string, HtmlNodeCollection> nodeCollection in nodes)
            {
                foreach (HtmlNode currNode in nodeCollection.Value)
                {
                    //need to get the paragraph closest to h4
                    HtmlNode nearestParagraph = null;
                    string foundheader = "";

                    for (int i = 0; i < currNode.ChildNodes.Count; i++)
                    {
                        HtmlNode currChild = currNode.ChildNodes[i];
                        string value = "";
                        int currChildIndex = -1;
                        if (currChild.Name == "h4")
                        {
                            currChildIndex = i;
                            value = currChild.InnerText;

                            bool isExists = IsExistsInList(headers, value, out foundheader);

                            if (isExists && foundheader != "")
                            {
                                nearestParagraph = GetNearestElement(currNode, "p", currChildIndex);

                                if (nearestParagraph != null)
                                {
                                    switch (foundheader)
                                    {
                                        case "Components":
                                            componenstData = ExtractComponents(nearestParagraph);
                                            description.Components = componenstData;
                                            break;
                                        case "Application":
                                            description.Application = nearestParagraph.InnerText.Trim();
                                            break;
                                        case "Features and Benefits":
                                            description.FeaturesBenefits = nearestParagraph.InnerText.Trim();
                                            break;
                                        case "General description":
                                            description.GeneralDescription = nearestParagraph.InnerText.Trim();
                                            break;
                                        case "Packaging":
                                            description.Packaging = nearestParagraph.InnerText.Trim();
                                            break;
                                        case "Reconstitution":
                                            description.Reconstitution = nearestParagraph.InnerText.Trim();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                        }
                    }


                }
            }

            return description;
        }

        private static List<IngredientData> ExtractComponents(HtmlNode nearestParagraph)
        {
            List<IngredientData> ingredientsData = new List<IngredientData>();
            string ingredients = nearestParagraph.InnerText.Trim();
            string[] parts = ingredients.Split(',');
            string firstIngredient = "";

            if (parts[0].Contains("Ingredients"))
            {
                ingredientsData = ExtractComponentsMethod1(nearestParagraph);
            }
            else
            {
                ingredientsData = ExtractComponentsMethod2(nearestParagraph);
            }

            return ingredientsData;
        }

        private static List<IngredientData> ExtractComponentsMethod1(HtmlNode nearestParagraph)
        {
            IngredientData tmp = new IngredientData();
            List<IngredientData> ingredientsData = new List<IngredientData>();

            string ingredients = nearestParagraph.InnerText.Trim();
            string[] parts = ingredients.Split(',');
            string firstIngredient = "";

            if (parts[0].Contains("Ingredients"))
            {
                string header = parts[0].Substring(0, 17);
                firstIngredient = parts[0].Substring(17, parts[0].Length - 1 - 16);
            }

            tmp.Name = firstIngredient;

            int partsCount = parts.Count();
            for (int i = 1; i < partsCount; i++)
            {
                string str = parts[i].Trim();

                KeyValuePair<double, string> num = ExtractNumberFromString(str);

                tmp.Amount = num.Key;
                tmp.Unit = "g/L";
                ingredientsData.Add(tmp);

                tmp = new IngredientData();
                tmp.Name = num.Value;
            }

            return ingredientsData;
        }

        private static List<IngredientData> ExtractComponentsMethod2(HtmlNode nearestParagraph)
        {
            List<IngredientData> ingredientsData = new List<IngredientData>();
            int flagStart = 0;
            string currComponent = "";
            int charCounter = 0;

            string ingredients = nearestParagraph.InnerText.Trim();

            for (int i = 0; i < ingredients.Length; i++)
            {
                if (Char.IsNumber(ingredients[i]))
                {
                    if (flagStart == 0)
                    {
                        flagStart = 1;
                        currComponent += ingredients[i];
                    }
                    else
                    {
                        if (charCounter > 0)
                        {
                            //this is the end of a component
                            //Handle component
                            string[] componentParts = currComponent.Split(' ');

                            string name = "";
                            if (componentParts.Count() > 3)
                            {
                                for (int j = 2; j < componentParts.Count(); j++)
                                {
                                    name += " " + componentParts[j];
                                }
                            }

                            ingredientsData.Add(new IngredientData() { Amount = double.Parse(componentParts[0]), Name = (name != "" ? name : componentParts[2]), Unit = componentParts[1] });
                            flagStart = 0;
                            currComponent = "";
                            currComponent += ingredients[i];
                            charCounter = 0;
                        }
                        else
                        {
                            currComponent += ingredients[i];
                        }
                    }
                }
                else
                {
                    charCounter++;
                    currComponent += ingredients[i];

                    if (i + 1 == ingredients.Length)
                    {
                        string[] componentParts = currComponent.Split(' ');

                        string name = "";
                        if (componentParts.Count() > 3)
                        {
                            for (int j = 2; j < componentParts.Count(); j++)
                            {
                                name += " " + componentParts[j];
                            }
                        }

                        ingredientsData.Add(new IngredientData() { Amount = double.Parse(componentParts[0]), Name = (name != "" ? name : componentParts[2]), Unit = componentParts[1] });
                    }
                }
            }

            return ingredientsData;
        }

        private static KeyValuePair<string, string> GetTableRowData(string row)
        {
            int position = GetStartPositionOfLargestWhiteSpacesSequence(row);
            string firstVal = row.Substring(0, position);
            string secondVal = row.Substring(position + 1, row.Length - (position + 1)).Trim();

            return new KeyValuePair<string, string>(firstVal, secondVal);
        }

        private static HtmlNode GetNearestElement(HtmlNode node, string element, int index)
        {
            HtmlNode retVal = null;

            for (int i = index + 1; i < node.ChildNodes.Count; i++)
            {
                HtmlNode currNode = node.ChildNodes[i];
                if (currNode.Name == element)
                {
                    retVal = currNode;
                    break;
                }
            }

            return retVal;
        }

        private static bool IsExistsInList(List<string> list, string data, out string found_data)
        {
            bool isExists = false;
            found_data = "";

            foreach (string item in list)
            {
                if (data.Contains(item))
                {
                    found_data = item;
                    isExists = true;
                    break;
                }
            }

            return isExists;
        }

        private static int GetStartPositionOfLargestWhiteSpacesSequence(string str)
        {
            int counter = 0;
            int currLargest = 0;
            int retVal = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    if (i + 1 < str.Length)
                    {
                        if (str[i + 1] == ' ')
                        {
                            counter++;
                        }
                        else
                        {
                            if (counter > currLargest)
                            {
                                currLargest = counter;
                                retVal = i - counter;
                            }
                            counter = 0;
                        }
                    }
                }
                else
                {
                    if (counter > currLargest)
                    {
                        currLargest = counter;
                        retVal = i - counter;
                    }
                    counter = 0;
                }
            }

            return retVal;
        }

        private static KeyValuePair<double, string> ExtractNumberFromString(string str)
        {
            int numDots = 0;
            string number = "";
            int i = 0;

            for (i = 0; i < str.Length; i++)
            {
                if (Char.IsNumber(str[i]))
                {
                    number += str[i].ToString();
                }
                else
                {
                    if (str[i] == '.')
                    {
                        if (numDots > 0)
                        {
                            break;
                        }
                        else
                        {
                            number += str[i].ToString();
                            numDots++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            double parsedNumber = double.Parse(number) * 1.00; //Convert.ToDouble(number);
            string data = str.Substring(i, str.Length - i);

            return new KeyValuePair<double, string>(Math.Round(parsedNumber, 2), data);
        }

        public static List<Description> GetAllProductsForCategory(string category, int num_products_to_get, int pages_to_get)
        {
            string categoryUri = GetCategoryUri(category);
            int numProductsToGet = 0;

            List<string> paginationUri = GetCategoryPaginationUrls(categoryUri, pages_to_get);

            List<string> productsUris = GetProductsUri(paginationUri, num_products_to_get);

            List<Description> products = new List<Description>();
            List<string> elements = new List<string>();
            List<string> headers = new List<string>();
            headers.Add("Components");
            headers.Add("Application");
            headers.Add("Features and Benefits");
            headers.Add("General description");
            headers.Add("Packaging");
            headers.Add("Reconstitution");

            elements.Add("//div[@class='descriptionContent']");

            if (num_products_to_get == 0)
            {
                //get all products
                numProductsToGet = productsUris.Count;
            }

            for (int i = 0; i < num_products_to_get; i++)
            {
                List<KeyValuePair<string, HtmlNodeCollection>> dataDescription = DataGetter.GetDataByXPATH(new Uri(productsUris[i]), elements);
                Description description = SigmaAldrichParser.ParseDescription(dataDescription, headers);

                products.Add(description);

                System.Threading.Thread.Sleep((int)DataGetter.GetRandomNumber(5.0, 15.0) * 1000);
            }

            return products;
        }

        private static string GetCategoryUri(string category)
        {
            string retVal = "";

            switch (category)
            {
                case "AnalyticalStandards":
                    retVal = SigmaAldrichConstants.AnalyticalStandards;
                    break;
                case "GasChromatography":
                    retVal = SigmaAldrichConstants.GasChromatography;
                    break;
                case "Spectroscopy":
                    retVal = SigmaAldrichConstants.Spectroscopy;
                    break;
                case "SamplePrepPurification":
                    retVal = SigmaAldrichConstants.SamplePrepPurification;
                    break;
                case "Titration":
                    retVal = SigmaAldrichConstants.Titration;
                    break;
                case "AcidsBases":
                    retVal = SigmaAldrichConstants.AcidsBases;
                    break;
                case "CatalystInorganics":
                    retVal = SigmaAldrichConstants.CatalystInorganics;
                    break;
                case "ChemicalSynthesis":
                    retVal = SigmaAldrichConstants.ChemicalSynthesis;
                    break;
                case "HeteroCyclicBuildingBlocks":
                    retVal = SigmaAldrichConstants.HeteroCyclicBuildingBlocks;
                    break;
                case "OrganicBuildingBlocks":
                    retVal = SigmaAldrichConstants.OrganicBuildingBlocks;
                    break;
                case "OrganoMettalics":
                    retVal = SigmaAldrichConstants.OrganoMettalics;
                    break;
                case "Salts":
                    retVal = SigmaAldrichConstants.Salts;
                    break;
                case "Solvents":
                    retVal = SigmaAldrichConstants.Solvents;
                    break;
                case "StableIsotopes":
                    retVal = SigmaAldrichConstants.StableIsotopes;
                    break;
                case "Antibodies":
                    retVal = SigmaAldrichConstants.Antibodies;
                    break;
                case "BiochemicalsReagents":
                    retVal = SigmaAldrichConstants.BiochemicalsReagents;
                    break;
                case "BiologicalBuffers":
                    retVal = SigmaAldrichConstants.BiologicalBuffers;
                    break;
                case "CellBiology":
                    retVal = SigmaAldrichConstants.CellBiology;
                    break;
                case "CellCulture":
                    retVal = SigmaAldrichConstants.CellCulture;
                    break;
                case "EnzymesInhibitorsSubstraits":
                    retVal = SigmaAldrichConstants.EnzymesInhibitorsSubstraits;
                    break;
                case "HistologyHematology":
                    retVal = SigmaAldrichConstants.HistologyHematology;
                    break;
                case "Microbiology":
                    retVal = SigmaAldrichConstants.Microbiology;
                    break;
                case "MolecularBiology":
                    retVal = SigmaAldrichConstants.MolecularBiology;
                    break;
                case "PCRAmplification":
                    retVal = SigmaAldrichConstants.PCRAmplification;
                    break;
                case "SynthetichBiology":
                    retVal = SigmaAldrichConstants.SynthetichBiology;
                    break;
                default:
                    break;
            }

            return retVal;
        }

        public static List<string> GetProductURL()
        {
            return new List<string>();
        }

        public static List<string> GetCategoryPaginationUrls(string category_url, int pages_to_get )
        {
            int numPagesToGet = pages_to_get;
            HtmlDocument doc = DataGetter.GetHtmlpage(new Uri(category_url));

            //infer page name from 2nd page link
            List<string> liElements = new List<string>();
            liElements.Add("searchResultsPagination");
            List<KeyValuePair<string, HtmlNode>> paginationElements = DataGetter.GetDataByID(doc, "div", liElements);

            List<string> liElements1 = new List<string>();
            liElements1.Add("pg2");
            List<KeyValuePair<string, HtmlNode>> page2 = DataGetter.GetDataByID(doc, "li", liElements1);
            string page2Link = ExtractLinkFromHtml(page2[0].Value, "href", "'>");

            
            HtmlNodeCollection divNodeChildren = paginationElements[paginationElements.Count - 1].Value.ChildNodes;
            HtmlNode divNodeChildren1 = divNodeChildren.First(x => x.Name == "ul");
            HtmlNode lastPage = divNodeChildren1.ChildNodes[divNodeChildren1.ChildNodes.Count - 2];
            string lastPageLink = ExtractLinkFromHtml(lastPage, "href", "'>");
            int lastPageNumber = ExtractNumberFromString(lastPageLink, "page=");

            //Now build the pagination links...
            string[] pageParts = page2Link.Split(new string[] { "page=" }, StringSplitOptions.None);
            //pageParts[0] = pageParts[0].Substring(1);
            pageParts[1] = RemoveNumberFromStartOfString(pageParts[1]);
            //pageParts[1] = pageParts[1].Substring(0, pageParts[1].Length - 1);
            List<string> paginationLinks = new List<string>();

            if (numPagesToGet == 0)
            {
                //get all products
                numPagesToGet = lastPageNumber;
            }

            for (int i = 0; i < numPagesToGet; i++)
            {
                paginationLinks.Add(SigmaAldrichConstants.SigmaAldrichMain + pageParts[0] + "page=" + (i + 1) + pageParts[1]);
            }

            return paginationLinks;
        }

        private static string ExtractLinkFromHtml(HtmlNode node, string starting_str, string ending_str)
        {
            string link = WebUtility.HtmlDecode(node.InnerHtml);
            int linkkStartLocation = link.IndexOf(starting_str/*"href"*/, 0);
            int linkEndLocation = link.IndexOf(ending_str/*"'>"*/, linkkStartLocation);
            link = link.Substring(linkkStartLocation + starting_str.Length + 2, linkEndLocation - (linkkStartLocation + starting_str.Length + 2));

            return link;
        }

        private static int ExtractNumberFromString(string str, string look_for)
        {
            int startLocation = str.IndexOf(look_for) + look_for.Length;
            string number = "";

            for (int i = startLocation; i < str.Length; i++)
            {
                if (char.IsNumber(str[i]))
                {
                    number += str[i];
                }
                else
                {
                    break;
                }
            }

            return int.Parse(number);
        }

        private static string RemoveNumberFromStartOfString(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsNumber(str[i]))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            string tmp = str.Substring(count);

            return tmp;
        }

        private static List<string> GetProductsUri(List<string> pagination_uri, int num_products_to_get)
        {
            //go over all product pages and extract product url's
            List<string> productsLinks = new List<string>();

            int numProductsToGet = num_products_to_get;

            if (numProductsToGet == 0)
            {
                numProductsToGet = pagination_uri.Count;
            }

            for (int i = 0; i < numProductsToGet; i++)
            {
                HtmlDocument dataPage = DataGetter.GetHtmlpage(new Uri (pagination_uri[i]));

                //now get all product url's from page
                List<string> elements = new List<string>();
                elements.Add("//li[@class='productNumberValue']");
                List<KeyValuePair<string, HtmlNodeCollection>> data = DataGetter.GetDataByXPATH(dataPage, elements);

                HtmlNodeCollection productsNodes = data[0].Value;

                for (int j = 0; j < numProductsToGet && j < productsNodes.Count; j++)
                {
                    string link = productsNodes[j].InnerHtml;
                    string extractedLink = ExtractLinkFromHtml(productsNodes[j], "href=", "\">");
                    productsLinks.Add(SigmaAldrichConstants.SigmaAldrichMain + "/" + extractedLink);
                }

                //foreach (HtmlNode node in productsNodes)
                //{
                //    string link = node.InnerHtml;
                //    string extractedLink = ExtractLinkFromHtml(node, "href=", "\">");
                //    productsLinks.Add(SigmaAldrichConstants.SigmaAldrichMain + "/" + extractedLink);
                //}

                if (productsLinks.Count < numProductsToGet)
                {
                    System.Threading.Thread.Sleep((int)DataGetter.GetRandomNumber(5.0, 15.0) * 1000); 
                }
                else
                {
                    break;
                }
            }

            return productsLinks;
        }
    }
}