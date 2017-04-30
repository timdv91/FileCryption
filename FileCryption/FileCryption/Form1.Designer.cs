namespace FileCryption
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnKeyFile = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtKeyfilePath = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtEncryptFilePath = new System.Windows.Forms.TextBox();
            this.btnRunEncryption = new System.Windows.Forms.Button();
            this.btnFileToEncrypt = new System.Windows.Forms.Button();
            this.txtDecryptFilePath = new System.Windows.Forms.TextBox();
            this.btnRunDecrypt = new System.Windows.Forms.Button();
            this.btnFileToDecrypt = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKeyFile
            // 
            this.btnKeyFile.Location = new System.Drawing.Point(12, 12);
            this.btnKeyFile.Name = "btnKeyFile";
            this.btnKeyFile.Size = new System.Drawing.Size(93, 23);
            this.btnKeyFile.TabIndex = 0;
            this.btnKeyFile.Text = "choose keyFile";
            this.btnKeyFile.UseVisualStyleBackColor = true;
            this.btnKeyFile.Click += new System.EventHandler(this.btnKeyFile_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtKeyfilePath);
            this.splitContainer1.Panel1.Controls.Add(this.btnKeyFile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(714, 177);
            this.splitContainer1.SplitterDistance = 43;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer2.Size = new System.Drawing.Size(714, 130);
            this.splitContainer2.SplitterDistance = 101;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtKeyfilePath
            // 
            this.txtKeyfilePath.Location = new System.Drawing.Point(112, 13);
            this.txtKeyfilePath.Name = "txtKeyfilePath";
            this.txtKeyfilePath.Size = new System.Drawing.Size(590, 20);
            this.txtKeyfilePath.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(714, 101);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtEncryptFilePath);
            this.tabPage1.Controls.Add(this.btnRunEncryption);
            this.tabPage1.Controls.Add(this.btnFileToEncrypt);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(706, 75);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "FileCrypt";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtDecryptFilePath);
            this.tabPage2.Controls.Add(this.btnRunDecrypt);
            this.tabPage2.Controls.Add(this.btnFileToDecrypt);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(706, 75);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Decrypt";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtEncryptFilePath
            // 
            this.txtEncryptFilePath.Location = new System.Drawing.Point(107, 9);
            this.txtEncryptFilePath.Name = "txtEncryptFilePath";
            this.txtEncryptFilePath.Size = new System.Drawing.Size(590, 20);
            this.txtEncryptFilePath.TabIndex = 5;
            // 
            // btnRunEncryption
            // 
            this.btnRunEncryption.Location = new System.Drawing.Point(8, 35);
            this.btnRunEncryption.Name = "btnRunEncryption";
            this.btnRunEncryption.Size = new System.Drawing.Size(93, 23);
            this.btnRunEncryption.TabIndex = 4;
            this.btnRunEncryption.Text = "Run ";
            this.btnRunEncryption.UseVisualStyleBackColor = true;
            this.btnRunEncryption.Click += new System.EventHandler(this.btnRunEncryption_Click);
            // 
            // btnFileToEncrypt
            // 
            this.btnFileToEncrypt.Location = new System.Drawing.Point(8, 6);
            this.btnFileToEncrypt.Name = "btnFileToEncrypt";
            this.btnFileToEncrypt.Size = new System.Drawing.Size(93, 23);
            this.btnFileToEncrypt.TabIndex = 3;
            this.btnFileToEncrypt.Text = "File to encrypt";
            this.btnFileToEncrypt.UseVisualStyleBackColor = true;
            this.btnFileToEncrypt.Click += new System.EventHandler(this.btnFileToEncrypt_Click);
            // 
            // txtDecryptFilePath
            // 
            this.txtDecryptFilePath.Location = new System.Drawing.Point(107, 9);
            this.txtDecryptFilePath.Name = "txtDecryptFilePath";
            this.txtDecryptFilePath.Size = new System.Drawing.Size(590, 20);
            this.txtDecryptFilePath.TabIndex = 5;
            // 
            // btnRunDecrypt
            // 
            this.btnRunDecrypt.Location = new System.Drawing.Point(8, 35);
            this.btnRunDecrypt.Name = "btnRunDecrypt";
            this.btnRunDecrypt.Size = new System.Drawing.Size(93, 23);
            this.btnRunDecrypt.TabIndex = 4;
            this.btnRunDecrypt.Text = "Run";
            this.btnRunDecrypt.UseVisualStyleBackColor = true;
            this.btnRunDecrypt.Click += new System.EventHandler(this.btnRunDecrypt_Click);
            // 
            // btnFileToDecrypt
            // 
            this.btnFileToDecrypt.Location = new System.Drawing.Point(8, 6);
            this.btnFileToDecrypt.Name = "btnFileToDecrypt";
            this.btnFileToDecrypt.Size = new System.Drawing.Size(93, 23);
            this.btnFileToDecrypt.TabIndex = 3;
            this.btnFileToDecrypt.Text = "File to decrypt";
            this.btnFileToDecrypt.UseVisualStyleBackColor = true;
            this.btnFileToDecrypt.Click += new System.EventHandler(this.btnFileToDecrypt_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(714, 25);
            this.progressBar1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 177);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "FileCryption";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKeyFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtKeyfilePath;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtEncryptFilePath;
        private System.Windows.Forms.Button btnRunEncryption;
        private System.Windows.Forms.Button btnFileToEncrypt;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtDecryptFilePath;
        private System.Windows.Forms.Button btnRunDecrypt;
        private System.Windows.Forms.Button btnFileToDecrypt;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

