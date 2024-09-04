using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Sistema_Ferreteria.Models;

namespace Sistema_Ferreteria.Controllers
{
    internal class CategoryController
    {
        private static List<Category> categories = new List<Category>();

        // Constructor
        public CategoryController()
        {
        }

        // Agregar una nueva categoría
        public void AddCategory(string name)
        {
            var newCategory = new Category(name);
            categories.Add(newCategory);
        }

        public void addCategoryWid(string id, string name, DateTime creationDate, DateTime updatedDate)
        {
            var newCategory = new Category( id,  name,  creationDate,  updatedDate);
            
            categories.Add(newCategory);
        }

        // Actualizar una categoría
        public bool UpdateCategory(string id, string newName)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                category.Update(newName);
                return true;
            }
            return false;
        }

        // Obtener todas las categorías
        public List<Category> GetCategories()
        {
            return categories;
        }

        // Obtener una categoría por ID
        public Category GetCategoryById(string id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        // Eliminar una categoría
        public bool DeleteCategory(string id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                categories.Remove(category);
                return true;
            }
            return false;
        }
    }
}