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

        private void loadProducts()
        {
            dataGridView1.Rows.Add("prueba", "prueba", "prueba", "prueba", "5");
        }


            //eventos

        private void SearchProductTable_Load(object sender, EventArgs e)
        {
            loadProducts();
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
    }
}
