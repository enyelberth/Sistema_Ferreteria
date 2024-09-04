using Sistema_Ferreteria.Controllers;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sistema_Ferreteria.Views
{
    public partial class CategoryForm : Form
    {
        private CategoryController categoryController = new CategoryController();

        public CategoryForm()
        {
            InitializeComponent();
        }

        private void handleSubmit()
        {
            
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, ingresa un nombre para la categoría.");
                return; 
            }

            
            categoryController.AddCategory(textBox1.Text);
           
            this.Close(); 
        }

        // Evento del botón para agregar la categoría
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            handleSubmit();
        }

       
    }
}