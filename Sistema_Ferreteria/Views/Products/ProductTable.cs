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

namespace Sistema_Ferreteria.Views
{
    public partial class ProductTable : Form
    {
        public ProductTable()
        {
            InitializeComponent();
        }

        //variables

        private string editingID;


            //funciones 

        private void loadProducts()
        {
            dataGridView1.Rows.Add("prueba", "prueba", "prueba", "prueba", "5");
        }











            //eventos
      
        //categorias
        private void button6_Click(object sender, EventArgs e)
        {
            CategoryTable categoryTable = new CategoryTable();
            categoryTable.ShowDialog();
        }

        //iniciar sesion
        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();  
            login.ShowDialog();
        }

        //crear producto
        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.ShowDialog();
        }

        //buscar producto
        private void button3_Click(object sender, EventArgs e)
        {
            SearchProductTable searchProductTable = new SearchProductTable();
            searchProductTable.ShowDialog();
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
            if (e.RowIndex >= 0)
            {
                EditProductForm editProductForm = new EditProductForm();
                editProductForm.ShowDialog();
            }

            
        }

        //reducir cantidad
        private void button4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int currentValue))
            {
                currentValue -= 1;
                textBox2.Text = currentValue.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido.");
            }
        }


        //aumentar cantidad
        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int currentValue))
            {
                currentValue += 1;
                textBox2.Text = currentValue.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido.");
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
