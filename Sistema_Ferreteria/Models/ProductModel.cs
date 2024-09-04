using System;

namespace Sistema_Ferreteria.Models // Asegúrate de que el nombre del espacio de nombres sea correcto
{
    public class ProductModel
    {
        // Campos privados
        private string id;
        private string name;
        private float buyPrice;
        private float salePrice;
        private int amount;
        private int category;
        private DateTime creationDate;
        private DateTime updateDate;

        // Constructor
        public ProductModel(string id, string name, float buyPrice, float salePrice, int amount, int category)
        {
            this.id = id;
            this.name = name;
            this.buyPrice = buyPrice;
            this.salePrice = salePrice;
            this.amount = amount;
            this.category = category; 
            this.creationDate = DateTime.Now.Date; 
            this.updateDate = DateTime.Now.Date; 
        }

        // Propiedades públicas
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public float BuyPrice
        {
            get { return buyPrice; }
            set { buyPrice = value; }
        }

        public float SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public int Category
        {
            get { return category; }
            set { category = value; }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
    }
}