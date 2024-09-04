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
    public partial class CategoryTable : Form
    {

        private int editingId;

        CategoryController controller = new CategoryController();
        public CategoryTable()
        {
            InitializeComponent();
            LoadCategories();
        }

        

        //funciones

        private void LoadCategories()
        {
            
            var categories = controller.GetCategories();

            
            dataGridView1.Rows.Clear();

            
            foreach (var category in categories)
            {
                dataGridView1.Rows.Add(
                    category.Id,                   
                    category.Name,                 
                    category.CreationDate.ToShortDateString(), 
                    category.UpdateDate.ToShortDateString()    
                );
            }
        }



        //eventos

        //crear categoria
        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();

            categoryForm.ShowDialog();
            LoadCategories();
        }

        //volver
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                EditCategoryForm editCategoryForm = new EditCategoryForm();
                editCategoryForm.setEdit(editingId);
                editCategoryForm.ShowDialog();
                LoadCategories();
            }
            
        }

        private void CategoryTable_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                editingId = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                
            }
        }
    }
}
