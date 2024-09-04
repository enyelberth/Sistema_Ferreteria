using Newtonsoft.Json;
using Sistema_Ferreteria.Controllers;
using Sistema_Ferreteria.Models; // Asegúrate de incluir los modelos correctos
using System;
using System.Collections.Generic;
using System.IO; // Añadir esto para usar File
using System.Windows.Forms;

namespace Sistema_Ferreteria.Views.UI
{
    public partial class LoadArchive : Form
    {
        public LoadArchive()
        {
            InitializeComponent();
        }

        private void SaveInventory()
        {
            CategoryController categoryController = new CategoryController();
            ProductoController productoController = new ProductoController();

            var categories = categoryController.GetCategories();
            var products = productoController.GetProducts();

            var jsonData = new
            {
                categories,
                products
            };


            string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


            DateTime date = DateTime.UtcNow;


            string formattedDate = date.ToString("yyyy-MM-dd");


            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, $"{formattedDate}-inventario.json");

            try
            {

                File.WriteAllText(filePath, json);
                MessageBox.Show($"{formattedDate}-inventario.json creado en el escritorio");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el archivo: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Title = "Selecciona un archivo de inventario";
                openFileDialog.Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    if (Path.GetExtension(openFileDialog.FileName).ToLower() != ".json")
                    {
                        MessageBox.Show("Por favor selecciona un archivo con extensión .json", "Error de archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        CategoryController categoryController = new CategoryController();
                        ProductoController productoController = new ProductoController();

                        
                        string json = File.ReadAllText(openFileDialog.FileName);

                        
                        var jsonData = JsonConvert.DeserializeObject<JsonData>(json);

                        


                        foreach (var category in jsonData.Categories) 
                        {
                            
                            categoryController.addCategoryWid(category.Id,category.Name, category.CreationDate, category.UpdateDate); 
                        }

                       
                        foreach (var product in jsonData.Products)
                        {
                            productoController.AddProduct(product);
                        }

                        
                        ProductTable productTable = new ProductTable();

                        productTable.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar el archivo: {ex.Message}");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductTable productTable = new ProductTable();

            productTable.Show();
            this.Hide();
        }

        private void LoadArchive_FormClosing(object sender, FormClosingEventArgs e)
        {
            authController authController = new authController();


            if (authController.checkAuth())
            {
                var result = MessageBox.Show("¿Deseas guardar el inventario antes de salir?",
                                           "Guardar Inventario",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveInventory();

                }
                else if (result == DialogResult.No)
                {

                }
            }
        }
    }


    public class JsonData
    {
        public List<Category> Categories { get; set; } 
        public List<ProductModel> Products { get; set; } 
    }
}