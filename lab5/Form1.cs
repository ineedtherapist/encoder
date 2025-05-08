using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace lab5
{
    public partial class Form1 : Form
    {
        private class EncryptedContainer
        {
            public string Version { get; set; } = "";
            public DateTime Timestamp { get; set; }
            public string KeyHash { get; set; } = "";
            public string Key { get; set; } = "";
            public string Salt { get; set; } = "";
            public string EncryptedData { get; set; } = "";
        }

        private const int KEY_LENGTH = 32; // 256 bits
        private const int SALT_SIZE = 16; // 128 bits
        private const int BLOCK_SIZE = 16; // 128 bits
        private const string FILE_EXTENSION = ".enc";

        public Form1()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            buttonProcess.Click += ButtonProcess_Click;
            buttonGenerateKey.Click += ButtonGenerateKey_Click;
            buttonSave.Click += ButtonSave_Click;
        }

        private void ButtonProcess_Click(object? sender, EventArgs e)
        {
            if (radioEncrypt.Checked)
            {
                ProcessEncryption();
            }
            else
            {
                ProcessDecryption();
            }
        }

        private void ButtonGenerateKey_Click(object? sender, EventArgs e)
        {
            // Generate a random key
            byte[] randomBytes = new byte[16]; // 128 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            
            // Convert to a readable string (hex format)
            string randomKey = BitConverter.ToString(randomBytes).Replace("-", "");
            textBoxKey.Text = randomKey;
        }
        
        private void ButtonSave_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxOutput.Text))
                {
                    MessageBox.Show("Немає даних для збереження. Спочатку виконайте шифрування.", 
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (string.IsNullOrEmpty(textBoxKey.Text))
                {
                    MessageBox.Show("Відсутній ключ шифрування.", 
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
        
                // Save to file with metadata
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = $"Зашифровані файли (*{FILE_EXTENSION})|*{FILE_EXTENSION}",
                    DefaultExt = FILE_EXTENSION,
                    Title = "Зберегти зашифрований файл"
                };
        
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Generate a secure key from the user input
                    byte[] key = GenerateKey(textBoxKey.Text);
                    
                    SaveEncryptedFile(saveDialog.FileName, textBoxOutput.Text, key);
                    MessageBox.Show("Файл успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessEncryption()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxInput.Text))
                {
                    MessageBox.Show("Будь ласка, введіть текст для шифрування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
        
                if (string.IsNullOrEmpty(textBoxKey.Text))
                {
                    MessageBox.Show("Будь ласка, введіть ключ шифрування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
        
                // Generate a secure key from the user input
                byte[] key = GenerateKey(textBoxKey.Text);
                
                // Encrypt the text using gamma encryption
                string encryptedText = EncryptText(textBoxInput.Text, key);
        
                // Show encrypted text in output box
                textBoxOutput.Text = encryptedText;
                
                MessageBox.Show("Шифрування завершено успішно! Для збереження файлу натисніть кнопку 'Зберегти'.", 
                    "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка шифрування: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessDecryption()
        {
            try
            {
                string encryptedText;
                byte[] key;

                // Check if we should decrypt from file or from input textbox
                if (string.IsNullOrEmpty(textBoxInput.Text))
                {
                    // If input is empty, prompt for file
                    OpenFileDialog openDialog = new OpenFileDialog
                    {
                        Filter = $"Зашифровані файли (*{FILE_EXTENSION})|*{FILE_EXTENSION}",
                        Title = "Відкрити зашифрований файл"
                    };
        
                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        (encryptedText, key) = LoadEncryptedFile(openDialog.FileName);
                        // Key is already set in the textbox by LoadEncryptedFile
                    }
                    else
                    {
                        return; // User cancelled file selection
                    }
                }
                else
                {
                    // Use text from input box
                    if (string.IsNullOrEmpty(textBoxKey.Text) || textBoxKey.Text == "Ключ завантажено з файлу")
                    {
                        MessageBox.Show("Будь ласка, введіть ключ для дешифрування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
        
                    encryptedText = textBoxInput.Text;
                    key = GenerateKey(textBoxKey.Text);
                }
        
                // Perform decryption
                string decryptedText = DecryptText(encryptedText, key);
                textBoxOutput.Text = decryptedText;
                
                MessageBox.Show("Дешифрування завершено успішно!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка дешифрування: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] GenerateKey(string userKey, byte[]? salt = null)
        {
            // Generate a random salt if none provided
            salt ??= new byte[SALT_SIZE];
            if (salt.Length == 0)
            {
                salt = new byte[SALT_SIZE];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            using (var deriveBytes = new Rfc2898DeriveBytes(userKey, salt, 100000, HashAlgorithmName.SHA256))
            {
                return deriveBytes.GetBytes(KEY_LENGTH);
            }
        }

        private string EncryptText(string plainText, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Write IV at the beginning
                    msEncrypt.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private string DecryptText(string cipherText, byte[] key)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                // Extract IV from the beginning of the ciphertext
                byte[] iv = new byte[BLOCK_SIZE];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;

                // Extract the actual ciphertext
                byte[] cipher = new byte[fullCipher.Length - iv.Length];
                Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                using (MemoryStream msDecrypt = new MemoryStream(cipher))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private void SaveEncryptedFile(string filename, string encryptedText, byte[] key)
        {
            // Generate and store salt
            byte[] salt = new byte[SALT_SIZE];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Create a secure container for the encrypted data
            var container = new EncryptedContainer
            {
                Version = "1.0",
                Timestamp = DateTime.UtcNow,
                KeyHash = Convert.ToBase64String(SHA256.HashData(key)),
                Key = Convert.ToBase64String(key),
                Salt = Convert.ToBase64String(salt),
                EncryptedData = encryptedText
            };

            string json = System.Text.Json.JsonSerializer.Serialize(container);
            File.WriteAllText(filename, json);
        }

        private (string encryptedText, byte[] key) LoadEncryptedFile(string filename)
        {
            string json = File.ReadAllText(filename);
            var container = System.Text.Json.JsonSerializer.Deserialize<EncryptedContainer>(json);
            
            if (container == null)
            {
                throw new CryptographicException("Недійсний формат файлу.");
            }
        
            // Get the salt and key from the file
            if (string.IsNullOrEmpty(container.Key) || string.IsNullOrEmpty(container.Salt))
            {
                throw new CryptographicException("Ключ або сіль не знайдені у файлі.");
            }
        
            byte[] salt = Convert.FromBase64String(container.Salt);
            byte[] key = Convert.FromBase64String(container.Key);
            
            // Verify key hash matches
            string keyHash = Convert.ToBase64String(SHA256.HashData(key));
            if (string.IsNullOrEmpty(container.KeyHash) || keyHash != container.KeyHash)
            {
                throw new CryptographicException("Перевірка цілісності файлу не пройдена.");
            }
        
            // Set the key in the textbox
            textBoxKey.Text = "Ключ завантажено з файлу";
        
            if (string.IsNullOrEmpty(container.EncryptedData))
            {
                throw new CryptographicException("Зашифровані дані не знайдені у файлі.");
            }

            return (container.EncryptedData, key);
        }

        // Method specifically for the Open menu item and button
        private void OpenEncryptedFile()
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog
                {
                    Filter = $"Зашифровані файли (*{FILE_EXTENSION})|*{FILE_EXTENSION}",
                    Title = "Відкрити зашифрований файл"
                };
        
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    (string encryptedText, byte[] key) = LoadEncryptedFile(openDialog.FileName);
                    string decryptedText = DecryptText(encryptedText, key);
                    textBoxOutput.Text = decryptedText;
                    MessageBox.Show("Файл успішно відкрито та дешифровано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка дешифрування: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}