using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Ferreteria.models
{
    class ProductModel
    {
        private int id;
        private string name;
        private float buyPrice;
        private float salePrice;
        private int amount;
        private int category;
        private DateTime creationDate;
        private DateTime updateDate;

        public ProductModel(string name,float buyPrice,float salePrice,int amount,int Category)
        {
            //this.id = 1;
            this.name = name;
            this.buyPrice = buyPrice;
            this.salePrice = salePrice;
            this.amount = amount;
            //this.category = Category;
        }

        //public int Id {
        //    get { return id; } 
        //    set{ id = value; }
        //}
        public string Name {  
            get { return name; } 
            set{ name = value; }
        }
        public float BuyPrice {
            get { return buyPrice; } 
            set { buyPrice = value; } }
        public float SalePrice {
            get {  return salePrice; } 
            set { salePrice = value; } 
        }
        //public int Category { 
        //    get { return category; }  
        //    set { category = value; }
        //} 
        public DateTime CreationDate { 
            get { return creationDate; }
            set { creationDate = value; } 
        }
        public DateTime UpdateDate { 
            get {   return updateDate; } 
            set { updateDate = value; } 
        }
     }
}
