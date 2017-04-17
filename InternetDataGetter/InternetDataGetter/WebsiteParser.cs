using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InternetDataGetter
{
    public class Product
    {
        public string Name;
    }

    public abstract class WebsiteParser
    {
        public Uri address;

        public abstract HtmlDocument GetPage(Uri uri);
        public abstract List<Uri> GetCategories(HtmlDocument document);
        public abstract List<Product> GetProducts(Uri category);
        public abstract Product GetProduct(Uri product);
    }
}
