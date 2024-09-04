using Sistema_Ferreteria.Controllers;
using Sistema_Ferreteria.Views.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Sistema_Ferreteria.Views
{
    public partial class ProductTable : Form
    {
        ProductoController controller = new ProductoController();
        CategoryController categoryController = new CategoryController();
        authController authController = new authController();

        public ProductTable()
        {
            InitializeComponent();
            if (authController.checkAuth())
            {
                button5.Text = "Generar Inventario";
            }
            else
            {
                button5.Text = "Autenticarse";
            }
        }

        //variables

        private string editingID;


        //funciones 

        public void loadProducts()
        {
            var categories = categoryController.GetCategories();
            var categoryDictionary = categories.ToDictionary(c => c.Id, c => c.Name);

            var products = controller.GetProducts();
            

            dataGridView1.Rows.Clear();

            foreach (var product in products)
            {
                
                string categoryName = categoryDictionary.TryGetValue(product.Category, out var name) ? name : "Desconocida";
               

                dataGridView1.Rows.Add(
                    product.Id,
                    product.Name,
                    product.BuyPrice,
                    product.SalePrice,
                    product.Amount,
                    categoryName,
                    product.CreationDate.ToShortDateString(),
                    product.UpdateDate.ToShortDateString()
                );
            }
        }

        //hacer focus en el data grid
        private void SetFocusOnDataGridViewRow(string id)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == id) 
                {
                    dataGridView1.CurrentCell = row.Cells[0]; 
                    row.Selected = true; 
                    dataGridView1.Focus(); 
                    break; 
                }
            }
        }

        private void SaveInventory()
        {
            var categories = categoryController.GetCategories();
            var products = controller.GetProducts();

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







        //eventos

        //categorias
        private void button6_Click(object sender, EventArgs e)
        {

            if (!authController.checkAuth())
            {
                MessageBox.Show("Para Realizar esta accion necesita autenticacion");
                return;
            }

            CategoryTable categoryTable = new CategoryTable();
            categoryTable.ShowDialog();
            loadProducts();
            
        }

        //iniciar sesion/guardar archivo
        private void button5_Click(object sender, EventArgs e)
        {
            if (authController.checkAuth())
            {
                SaveInventory();
            }
            else
            {
                Login login = new Login();
                login.ShowDialog();
                if (authController.checkAuth())
                {
                    button5.Text = "Generar Inventario";
                }
            }
        }

        //crear producto
        private void button1_Click(object sender, EventArgs e)
        {
            if (!authController.checkAuth())
            {
                MessageBox.Show("Para Realizar esta accion necesita autenticacion");
                return;
            }
            ProductForm productForm = new ProductForm();
            productForm.ShowDialog();
            loadProducts();

        }

        //buscar producto
        private void button3_Click(object sender, EventArgs e)
        {
            SearchProductTable searchProductTable = new SearchProductTable();
            searchProductTable.setSearch(textBox1.Text);
            searchProductTable.ShowDialog();
            loadProducts();
        }

        //1 click en datagridview
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                editingID = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
 
                if (e.ColumnIndex == 0)
                {
                    var cellValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                    if (cellValue != null)
                    {
                        textBox2.Text = cellValue.ToString();
                    }
                    else
                    {
                        textBox2.Text = string.Empty; 
                    }
                }
            }
        }

        //doble click datagridview
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (!authController.checkAuth())
            {
                MessageBox.Show("Para Realizar esta accion necesita autenticacion");
                return;
            }

            if (e.RowIndex >= 0)
            {
                EditProductForm editProductForm = new EditProductForm();
                editProductForm.setEdit(editingID);
                editProductForm.ShowDialog();
                loadProducts();
            }

            
        }

        //reducir cantidad
        private void button4_Click(object sender, EventArgs e)
        {
            if (!authController.checkAuth())
            {
                MessageBox.Show("Para Realizar esta accion necesita autenticacion");
                return;
            }

            if (int.TryParse(textBox2.Text, out int currentValue))
            {
                currentValue -= 1;
                textBox2.Text = currentValue.ToString();
                controller.DecreaseQuantity(editingID, 1);
                loadProducts();
                SetFocusOnDataGridViewRow(editingID);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto.");
            }
        }


        //aumentar cantidad
        private void button2_Click(object sender, EventArgs e)
        {
            if (!authController.checkAuth())
            {
                MessageBox.Show("Para Realizar esta accion necesita autenticacion");
                return;
            }

            if (int.TryParse(textBox2.Text, out int currentValue))
            {
                currentValue += 1;
                textBox2.Text = currentValue.ToString();
                controller.IncreaseQuantity(editingID, 1);
                loadProducts();
                SetFocusOnDataGridViewRow(editingID);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto.");
            }
        }



        //cerrar ventana
        private void ProductTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            
            
            Application.Exit();
        }

        


        //abrir ventana
        private void ProductTable_Load(object sender, EventArgs e)
        {
            loadProducts();
        }

        
    }
}
