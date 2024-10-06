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

namespace Sistema_Ferreteria.Views.Products
{
    public partial class SearchProductTable : Form
    {

        ProductoController controller = new ProductoController();
        authController authController = new authController();

        private string search;

        public SearchProductTable()
        {
            InitializeComponent();
           
        }

        private string editingID;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


            //funciones

        public void setSearch(string search)
        {
            this.search = search;
            
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

        private void loadProducts()
        {
            var products = controller.GetProducts();

            var filteredProducts = products.Where(product =>
             product.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 
            );


            dataGridView1.Rows.Clear();


            foreach (var product in filteredProducts)
            {
                dataGridView1.Rows.Add(
                    product.Id,
                    product.Name,
                    product.BuyPrice,
                    product.SalePrice,
                    product.Amount,
                    product.Category,
                    product.CreationDate,
                    product.UpdateDate
                );
            }
        }


            //eventos

        private void SearchProductTable_Load(object sender, EventArgs e)
        {
            loadProducts();
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
                textBox2.Text = "1";
                controller.DecreaseQuantity(editingID, currentValue);
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
                textBox2.Text = "1";
                controller.IncreaseQuantity(editingID, currentValue);
                loadProducts();
                SetFocusOnDataGridViewRow(editingID);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto.");
            }
        }

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
                        textBox2.Text = "1";
                        textBox2.ReadOnly = false;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!authController.checkAuth())
            {
                MessageBox.Show("Para realizar esta acción necesita autenticación");
                return;
            }

            if (editingID != "")
            {
                var result = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este producto?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    textBox2.Text = "1"; 
                    controller.DeleteProduct(editingID);
                    editingID = "";
                    loadProducts();
                }
            }
            else
            {
                MessageBox.Show("Para eliminar, seleccione el ID de un producto");
            }
        }
    }
}
