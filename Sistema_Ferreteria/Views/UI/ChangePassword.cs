using Sistema_Ferreteria.Controllers;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Sistema_Ferreteria.Views.UI
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

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

        private string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        private void handleSubmit()
        {
            string currentPassword = ReadCredentials();

           
            if (currentPassword == null)
            {
                currentPassword = "468CRed$";
            }

            if (textBox1.Text == currentPassword)
            {
               
                string newPassword = textBox2.Text;
                string encryptedPassword = Encrypt(newPassword);

                File.WriteAllText("data.aes", encryptedPassword);

                MessageBox.Show("Contraseña guardada correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            handleSubmit();
        }
    }
}