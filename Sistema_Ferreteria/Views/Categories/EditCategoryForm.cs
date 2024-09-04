using Sistema_Ferreteria.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sistema_Ferreteria.Views
{
    public partial class EditCategoryForm : Form
    {

        private string editingId;

        CategoryController categoryController = new CategoryController();

        public EditCategoryForm()
        {
            InitializeComponent();
        }


        private void handleSubmit()
        {
            categoryController.UpdateCategory(editingId, textBox2.Text);
        }

        public void setEdit(string id)
        {
            editingId = id;

            var category = categoryController.GetCategoryById(id);

            if (category != null)
            {

                textBox2.Text = category.Name;
               

            }
            else
            {
                MessageBox.Show("categoria no encontrada.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            handleSubmit();
            this.Close();
        }
    }
}
