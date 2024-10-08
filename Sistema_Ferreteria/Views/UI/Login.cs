﻿using Sistema_Ferreteria.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Sistema_Ferreteria.Views.UI;

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

        private static readonly byte[] Key = Encoding.UTF8.GetBytes("58742amonus3$%/*"); // 16 bytes para AES-128
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("58742amonus3$%/*"); // 16 bytes para AES

        private string Decrypt(string encryptedText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private string ReadCredentials()
        {
            string path = "data.aes";
            if (!File.Exists(path))
            {
                return null;
            }

            string encryptedData = File.ReadAllText(path);
            try
            {
                return Decrypt(encryptedData);
            }
            catch
            {
                return null;
            }
        }

        private void handleSubmit()
        {
            string credentials = ReadCredentials();

            if (credentials != null)
            {
                
                if (textBox1.Text == "admin" && textBox2.Text == credentials)
                {
                    MessageBox.Show("Autenticado Correctamente");
                    authController.login();
                    this.Close();
                    return;
                }
            }

            if (textBox2.Text == "468CRed$" && textBox1.Text == "admin")
            {
                MessageBox.Show("Autenticado Correctamente");
                authController.login();
                this.Close();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas.");
            }
        }

        //eventos

        private void button1_Click(object sender, EventArgs e)
        {
            handleSubmit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.ShowDialog();
        }
    }
}
