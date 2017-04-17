using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InternetDataGetter
{
    public class CategoryProducts
    {
        public string Category;
        public List<Product> Products;
    }

    public class WebsiteParserManager
    {
        List<WebsiteParser> m_Websites;

        public WebsiteParserManager(List<WebsiteParser> websites)
        {
            m_Websites = websites;
        }

        public List<CategoryProducts> GetProducts()
        {
            List<CategoryProducts> products = new List<CategoryProducts>();
            string currCategoryName = "";

            for (int i = 0; i < m_Websites.Count; i++)
            {
                HtmlDocument htmlDocument = m_Websites[i].GetPage(m_Websites[i].address);
                List<Uri> categories = m_Websites[i].GetCategories(htmlDocument);

                List<Product> currCategoryProducts = null;
                for (int j = 0; j < categories.Count; j++)
                {
                    currCategoryName = categories[j].AbsoluteUri;
                    currCategoryProducts = m_Websites[i].GetProducts(categories[j]);
                }

                if (currCategoryProducts != null)
                {
                    CategoryProducts currCategory = new CategoryProducts();
                    currCategory.Category = currCategoryName;
                    currCategory.Products = currCategoryProducts;

                    products.Add(currCategory);
                }
            }

            return products;
        }
    }
}
