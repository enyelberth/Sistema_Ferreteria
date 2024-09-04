using System;

namespace Sistema_Ferreteria.Models
{
    public class ProductModel
    {
        // Campos y propiedades
        public string Id { get; set; } 
        public string Name { get; set; }
        public float BuyPrice { get; set; }
        public float SalePrice { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; } 
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        // Constructor
        public ProductModel(string id, string name, float buyPrice, float salePrice, int amount, string category)
        {
            Id = id; 
            Name = name;
            BuyPrice = buyPrice;
            SalePrice = salePrice;
            Amount = amount;
            Category = category;
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        // Constructor sin parámetros
        public ProductModel() { }
    }
}