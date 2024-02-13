using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CezarApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(encryptedTextBox.Text))
            {
                MessageBox.Show("The ciphertext and key fields cannot be empty.", "Error");
                return;
            }

            if (!int.TryParse(keyTextBox.Text, out int key))
            {
                MessageBox.Show("The key must be an integer.", "Error");
                return;
            }

            string encryptedText = encryptedTextBox.Text;
            string decryptedText = MainFunctions.Decrypt(encryptedText, key);
            plaintextTextBox.Text = decryptedText;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(plaintextTextBox.Text) || string.IsNullOrWhiteSpace(keyTextBox.Text))
            {
                MessageBox.Show("The plaintext and key fields cannot be empty.", "Error");
                return;
            }
            if (!int.TryParse(keyTextBox.Text, out int key))
            {
                MessageBox.Show("The key must be an integer.", "Error");
                return;
            }
            string plaintext = plaintextTextBox.Text;
            string encryptedText = MainFunctions.Encrypt(plaintext, key);
            encryptedTextBox.Text = encryptedText;
        }

        private void DetectKeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(plaintextTextBox.Text) || string.IsNullOrWhiteSpace(encryptedTextBox.Text))
            {
                MessageBox.Show("The plaintext and ciphertext fields cannot be empty.", "Error");
                return;
            }
            string plaintext = plaintextTextBox.Text;
            string encryptedText = encryptedTextBox.Text;
            int detectedKey = MainFunctions.DetectKey(plaintext, encryptedText);
            MessageBox.Show($"Determined key: {detectedKey}");
        }

        private async void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(encryptedTextBox.Text))
            {
                MessageBox.Show("The ciphertext field cannot be empty.", "Error");
                return;
            }
            //var progressDialog = await DialogManager.ShowProgressAsync("Атака", "Идет атака...");
            //progressDialog.Show();
            string encryptedText = encryptedTextBox.Text;
            TextVariables[] solutions = await MainFunctions.Attack(encryptedText);

            StringBuilder sb = new StringBuilder();
            foreach (var solution in solutions)
            {
                sb.AppendLine($"Key: {solution.keyVar}, Decrypted Text: {solution.textVar}");
            }
            //progressDialog.Close();
            MessageBox.Show(sb.ToString(), "Potential Solutions");
        }
    }
}
