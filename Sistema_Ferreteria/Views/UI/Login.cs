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

namespace Sistema_Ferreteria.Views
{
    public partial class Login : Form
    {

        authController authController =new authController();

        public Login()
        {
            InitializeComponent();
        }

            //funciones

        private void handleSubmit()
        {
            if(textBox2.Text == "admin" &&
               textBox1.Text == "admin")
            {
                MessageBox.Show("Autenticado Correctamente");
                authController.login();
                this.Close();
            }
        }

            //eventos

        private void button1_Click(object sender, EventArgs e)
        {
            handleSubmit();
        }
    }
}
