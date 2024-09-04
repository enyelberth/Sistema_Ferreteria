using Sistema_Ferreteria.Controllers;
using Sistema_Ferreteria.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_Ferreteria.Views
{
    public partial class EditProductForm : Form
    {
        private ProductoController controller;
        private CategoryController categoryController;

        private string editingId; // Cambiar a Guid

        public EditProductForm()
        {
            InitializeComponent();
            controller = new ProductoController();
            categoryController = new CategoryController();
            LoadCategories(); // Cargar categorías al inicializar
        }

        private void LoadCategories()
        {
            // Obtener todas las categorías y llenar el ComboBox
            var categories = categoryController.GetCategories();
            comboBox1.Items.Clear();

            foreach (var category in categories)
            {
                comboBox1.Items.Add(new { Text = category.Name, Value = category.Id });
            }

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
        }

        public void setEdit(string id) // Cambiar el tipo de id a Guid
        {
            editingId = id;
            var product = controller.GetProductById(id.ToString()); // Asegúrate de que GetProductById acepte un Guid

            if (product != null)
            {
                textBox2.Text = product.Name;
                textBox3.Text = product.BuyPrice.ToString();
                textBox4.Text = product.SalePrice.ToString();
                textBox5.Text = product.Amount.ToString();

                // Seleccionar la categoría en el ComboBox
                var selectedCategory = comboBox1.Items
                    .Cast<dynamic>()
                    .FirstOrDefault(c => c.Value == product.Category);
                if (selectedCategory != null)
                {
                    comboBox1.SelectedItem = selectedCategory;
                }
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }
        }

        private void handleSubmit()
        {
            // Verificar que los TextBox no estén vacíos
            if (
                !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrWhiteSpace(textBox5.Text) &&
                comboBox1.SelectedItem != null // Verificar selección de categoría
            )
            {
                if (
                    float.TryParse(textBox3.Text, out float buyPrice) &&
                    float.TryParse(textBox4.Text, out float salePrice) &&
                    int.TryParse(textBox5.Text, out int amount)
                )
                {
                    // Obtener el ID de la categoría seleccionada como Guid
                    string categoryId = ((dynamic)comboBox1.SelectedItem).Value;

                    var updatedProduct = new ProductModel(
                        editingId.ToString(), // Convertir editingId a string si es necesario
                        textBox2.Text,
                        buyPrice,
                        salePrice,
                        amount,
                        categoryId // Usar ID de categoría como Guid
                    );

                    if (controller.UpdateProduct(editingId.ToString(), updatedProduct)) // Asegúrate de que este método acepte un Guid
                    {
                        MessageBox.Show("Producto actualizado");
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el producto.");
                    }

                    cleanTextboxes();
                }
                else
                {
                    MessageBox.Show("Los campos 3 y 4 deben ser números decimales y el campo 5 debe ser un número entero.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, completa todos los campos.");
            }
        }

        private void cleanTextboxes()
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            comboBox1.SelectedIndex = -1; // Deseleccionar el ComboBox
        }

        // Evento para crear o editar producto
        private void button1_Click(object sender, EventArgs e)
        {
            handleSubmit();
            this.Close();
        }

        // Validación de entrada para precios y cantidad
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateDecimalInput(sender, e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateDecimalInput(sender, e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos para la cantidad
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ValidateDecimalInput(object sender, KeyPressEventArgs e)
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
    }
}