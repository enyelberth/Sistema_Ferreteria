using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Ferreteria.Views.UI
{
    public partial class LoadArchive : Form
    {
        public LoadArchive()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Configurar el cuadro de diálogo
                openFileDialog.Title = "Selecciona un archivo de inventario";
                openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ProductTable productTable = new ProductTable();

                    
                    productTable.Show();

                    this.Hide();
                    
                }
            }
        }
    }
}
