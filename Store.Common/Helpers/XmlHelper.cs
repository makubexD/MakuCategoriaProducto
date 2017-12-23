using Store.DTO.ProductXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Store.Common.Helpers
{
    public class XmlHelper
    {
        private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"products.xml");

        public static void Insert(List<ProducXmlDto> products)
        {
            var oProducts = GetAll();
            if (oProducts != null)
                oProducts.AddRange(products);
            else
                oProducts = products;
            Save(oProducts);
        }

        private  static void Save(List<ProducXmlDto> products)
        {
            var writer = new XmlSerializer(typeof(List<ProducXmlDto>));
            var file = File.Create(path);
            writer.Serialize(file, products);
            file.Close();
        }

        public static bool GetByCode(string code)
        {
            var products = GetAll();
            if (products == null)
                return false;
            return products.Any(r => r.Code == code);
        }

        public static List<ProducXmlDto> GetAll()
        {
            List<ProducXmlDto> products = null;
            if (File.Exists(path))
            {
                var serializer = new XmlSerializer(typeof(List<ProducXmlDto>));
                var reader = new StreamReader(path);
                products = (List<ProducXmlDto>)serializer.Deserialize(reader);
                reader.Close();
            }
            return products;
        }

        public static List<ProducXmlDto> Search(string filterValue)
        {
            var oProducts = GetAll();
            if (oProducts != null)
                return oProducts.Where(r => r.Code.Contains(filterValue) || r.Name.Contains(filterValue)).ToList();
            return oProducts;
        }

        public static void Remove(string code)
        {
            var oProducts = GetAll();
            if (oProducts != null)
            {
                var product = oProducts.FirstOrDefault(r => r.Code==code);
                oProducts.Remove(product);
                Save(oProducts);
            }
        }

        public static void Update(string code, ProducXmlDto dto)
        {
            var oProducts = GetAll();
            if (oProducts != null)
            {
                var product = oProducts.FirstOrDefault(r => r.Code == code);
                if (product != null)
                {
                    product.Name = dto.Name;
                    product.CategoryCode = dto.CategoryCode;
                    product.Price = dto.Price;
                    product.Stock = dto.Stock;
                    Save(oProducts);
                }
                
            }
        }
    }
}
