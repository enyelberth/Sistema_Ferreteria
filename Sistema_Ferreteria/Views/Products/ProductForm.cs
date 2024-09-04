using Sistema_Ferreteria.Controllers;
using Sistema_Ferreteria.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_Ferreteria.Views
{
    public partial class ProductForm : Form
    {
        CategoryController categoryController = new CategoryController();

        public ProductForm()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = categoryController.GetCategories();
            comboBox1.Items.Clear();

            foreach (var category in categories)
            {
                comboBox1.Items.Add(new { Text = category.Name, Value = category.Id });
            }

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
        }

        private void handleSubmit()
        {
            if (
                !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrWhiteSpace(textBox5.Text) &&
                comboBox1.SelectedItem != null
                )
            {
                if (
                    float.TryParse(textBox3.Text, out float value3) &&
                    float.TryParse(textBox4.Text, out float value4) &&
                    int.TryParse(textBox5.Text, out int value5)
                    )
                {
                    // Cambiar int a Guid
                    string categoryId = ((dynamic)comboBox1.SelectedItem).Value;

                    var newProduct = new ProductModel(
                        textBox1.Text,
                        textBox2.Text,
                        value3,
                        value4,
                        value5,
                        categoryId // Aquí pasamos un Guid
                    );

                    ProductoController controller = new ProductoController();
                    controller.AddProduct(newProduct);

                    cleanTextboxes();
                }
                else
                {
                    MessageBox.Show("Los campos de precios y cantidad deben ser números.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, completa todos los campos.");
            }
        }

        private void cleanTextboxes()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
        }

        // Eventos

        // Crear producto
        private void button1_Click(object sender, EventArgs e)
        {
            handleSubmit();
            this.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y el punto decimal
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox3_KeyPress(sender, e); // Llama al mismo método para evitar duplicación de código
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox3_KeyPress(sender, e); // Llama al mismo método para evitar duplicación de código
        }
    }
}