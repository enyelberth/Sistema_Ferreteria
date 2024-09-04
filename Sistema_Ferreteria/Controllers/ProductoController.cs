using Sistema_Ferreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_Ferreteria.Controllers
{
    public class ProductoController
    {
        private static List<ProductModel> productList = new List<ProductModel>();

        // Crear (Agregar producto)
        public void AddProduct(ProductModel product)
        {
            
            if (productList.Any(p => p.Id == product.Id))
            {
                MessageBox.Show($"El producto con ID {product.Id} ya existe.");
                return;
            }

            product.CreationDate = DateTime.Now;
            product.UpdateDate = DateTime.Now;

            productList.Add(product);
        }

        // Leer (Obtener todos los productos)
        public List<ProductModel> GetProducts()
        {
            return productList;
        }

        // Leer (Obtener un producto por ID)
        public ProductModel GetProductById(string id)
        {
            return productList.FirstOrDefault(p => p.Id == id);
        }

        // Actualizar (Modificar producto)
        public bool UpdateProduct(string id, ProductModel updatedProduct)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.BuyPrice = updatedProduct.BuyPrice;
                product.SalePrice = updatedProduct.SalePrice;
                product.Amount = updatedProduct.Amount;
                product.Category = updatedProduct.Category; // Actualiza la categoría
                product.UpdateDate = DateTime.Now;
                return true;
            }
            return false;
        }

        // Eliminar producto
        public bool DeleteProduct(string id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                productList.Remove(product);
                return true;
            }
            return false;
        }

        // Aumentar cantidad
        public bool IncreaseQuantity(string id, int amount)
        {
            var product = GetProductById(id);
            if (product != null && amount > 0)
            {
                product.Amount += amount;
                product.UpdateDate = DateTime.Now;
                return true;
            }
            return false;
        }

        // Disminuir cantidad
        public bool DecreaseQuantity(string id, int amount)
        {
            var product = GetProductById(id);
            if (product != null && amount > 0 && product.Amount >= amount)
            {
                product.Amount -= amount;
                product.UpdateDate = DateTime.Now;
                return true;
            }
            return false;
        }

       
    }
}