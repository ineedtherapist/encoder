namespace lab5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Initialize UI components with modern design
            this.Text = "Безпечне шифрування тексту";
            this.Icon = new Icon(SystemIcons.Shield, 40, 40);
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            
            // Create menu strip with modern style
            this.menuStrip = new System.Windows.Forms.MenuStrip
            {
                BackColor = Color.FromArgb(45, 66, 99),
                ForeColor = Color.White,
                Padding = new Padding(6, 2, 0, 2),
                RenderMode = ToolStripRenderMode.System
            };
            
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem("Файл");
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                new System.Windows.Forms.ToolStripMenuItem("Відкрити", null, (s, e) => OpenEncryptedFile()),
                new System.Windows.Forms.ToolStripMenuItem("Зберегти", null, (s, e) => ButtonSave_Click(s, e)),
                new System.Windows.Forms.ToolStripSeparator(),
                new System.Windows.Forms.ToolStripMenuItem("Вихід", null, (s, e) => Application.Exit())
            });
            
            this.menuStrip.Items.Add(this.fileMenu);
            this.MainMenuStrip = this.menuStrip;
            this.Controls.Add(this.menuStrip);
        
            // Створюємо основну панель для кращого розміщення контролів
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                RowCount = 2,
                ColumnCount = 2,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 50F),
                    new ColumnStyle(SizeType.Percent, 50F)
                },
                RowStyles = {
                    new RowStyle(SizeType.Percent, 65F),
                    new RowStyle(SizeType.Percent, 35F)
                }
            };
            
            // Ліва панель для введення
            Panel leftPanel = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 240),
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Margin = new Padding(5)
            };
            
            // Права панель для результатів
            Panel rightPanel = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 240),
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Margin = new Padding(5)
            };
            
            // Нижня панель для контролів
            Panel controlPanel = new Panel
            {
                BackColor = Color.FromArgb(45, 66, 99),
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Margin = new Padding(5)
            };
            controlPanel.Dock = DockStyle.Fill;
            
            // Налаштування елементів введення тексту
            this.labelInput = new System.Windows.Forms.Label
            {
                Dock = DockStyle.Top,
                Height = 25,
                Text = "Текст для шифрування/дешифрування:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(45, 66, 99),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            this.textBoxInput = new System.Windows.Forms.TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = System.Windows.Forms.ScrollBars.Vertical,
                PlaceholderText = "Введіть текст для шифрування/дешифрування...",
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point)
            };
            
            // Налаштування елементів виведення результату
            this.labelOutput = new System.Windows.Forms.Label
            {
                Dock = DockStyle.Top,
                Height = 25,
                Text = "Результат:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(45, 66, 99),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            this.textBoxOutput = new System.Windows.Forms.TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = System.Windows.Forms.ScrollBars.Vertical,
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                BackColor = Color.FromArgb(250, 250, 255)
            };
            
            // Додаємо елементи введення/виведення на панелі
            leftPanel.Controls.Add(this.textBoxInput);
            leftPanel.Controls.Add(this.labelInput);
            
            rightPanel.Controls.Add(this.textBoxOutput);
            rightPanel.Controls.Add(this.labelOutput);
            
            // Налаштування елементів контролю на нижній панелі
            TableLayoutPanel controlsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                RowStyles = {
                    new RowStyle(SizeType.Percent, 33F),
                    new RowStyle(SizeType.Percent, 33F),
                    new RowStyle(SizeType.Percent, 34F)
                }
            };
            
            // Панель для ключа
            FlowLayoutPanel keyPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(5)
            };
            
            this.labelKey = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Text = "Ключ:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                Margin = new Padding(0, 3, 5, 0)
            };
            
            this.textBoxKey = new System.Windows.Forms.TextBox
            {
                Width = 300,
                PlaceholderText = "Введіть ключ шифрування...",
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 0, 5, 0)
            };
            
            this.buttonGenerateKey = new System.Windows.Forms.Button
            {
                Width = 70,
                Height = 28,
                Text = "Ген",
                BackColor = Color.FromArgb(70, 100, 140),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point),
                Cursor = Cursors.Hand,
                Margin = new Padding(0)
            };
            this.buttonGenerateKey.FlatAppearance.BorderSize = 0;
            
            keyPanel.Controls.AddRange(new Control[] { this.labelKey, this.textBoxKey, this.buttonGenerateKey });
            
            // Радіо-кнопки для вибору режиму
            FlowLayoutPanel radioPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(5)
            };
            
            this.radioEncrypt = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                Text = "Шифрувати",
                Checked = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.White,
                Margin = new Padding(20, 0, 30, 0)
            };
            
            this.radioDecrypt = new System.Windows.Forms.RadioButton
            {
                AutoSize = true,
                Text = "Дешифрувати",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.White,
                Margin = new Padding(0)
            };
            
            radioPanel.Controls.AddRange(new Control[] { this.radioEncrypt, this.radioDecrypt });
            
            // Кнопки дій
            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(5)
            };
            
            this.buttonProcess = new System.Windows.Forms.Button
            {
                Width = 180,
                Height = 35,
                Text = "Виконати",
                BackColor = Color.FromArgb(70, 100, 140),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Cursor = Cursors.Hand,
                Margin = new Padding(20, 0, 30, 0)
            };
            this.buttonProcess.FlatAppearance.BorderSize = 0;
            
            this.buttonSave = new System.Windows.Forms.Button
            {
                Width = 180,
                Height = 35,
                Text = "Зберегти",
                BackColor = Color.FromArgb(60, 135, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Cursor = Cursors.Hand,
                Margin = new Padding(0)
            };
            this.buttonSave.FlatAppearance.BorderSize = 0;
            
            buttonsPanel.Controls.AddRange(new Control[] { this.buttonProcess, this.buttonSave });
            
            // Додаємо елементи на панель контролів
            controlsLayout.Controls.Add(keyPanel, 0, 0);
            controlsLayout.Controls.Add(radioPanel, 0, 1);
            controlsLayout.Controls.Add(buttonsPanel, 0, 2);
            
            controlPanel.Controls.Add(controlsLayout);
            
            // Збираємо всі панелі в головний контейнер
            mainLayout.Controls.Add(leftPanel, 0, 0);
            mainLayout.Controls.Add(rightPanel, 1, 0);
            mainLayout.SetColumnSpan(controlPanel, 2);
            mainLayout.Controls.Add(controlPanel, 0, 1);
            
            this.Controls.Add(mainLayout);
            
            // Set form properties with modern look
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        #endregion

        // UI Controls
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Button buttonGenerateKey;
        private System.Windows.Forms.RadioButton radioEncrypt;
        private System.Windows.Forms.RadioButton radioDecrypt;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.TextBox textBoxOutput;
    }
}