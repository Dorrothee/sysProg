namespace Task_3
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
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.listViewItems = new System.Windows.Forms.ListView();
            this.textBoxFileContent = new System.Windows.Forms.TextBox();
            this.buttonSaveContent = new System.Windows.Forms.Button();
            this.buttonCreateFile = new System.Windows.Forms.Button();
            this.textBoxNewFileName = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(12, 12);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(550, 22);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPath_KeyDown);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(580, 10);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(80, 25);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Go Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // listViewItems
            // 
            this.listViewItems.FullRowSelect = true;
            this.listViewItems.HideSelection = false;
            this.listViewItems.Location = new System.Drawing.Point(12, 40);
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(650, 300);
            this.listViewItems.Columns.Add("Name", 300);  //First column for file/folder name
            this.listViewItems.Columns.Add("Type", 100); //Second column for type ("File" or "Directory")
            this.listViewItems.TabIndex = 2;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            this.listViewItems.View = System.Windows.Forms.View.Details;
            this.listViewItems.DoubleClick += new System.EventHandler(this.listViewItems_DoubleClick);
            // 
            // textBoxFileContent
            // 
            this.textBoxFileContent.Location = new System.Drawing.Point(12, 350);
            this.textBoxFileContent.Multiline = true;
            this.textBoxFileContent.Name = "textBoxFileContent";
            this.textBoxFileContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFileContent.Size = new System.Drawing.Size(550, 100);
            this.textBoxFileContent.TabIndex = 3;
            this.textBoxFileContent.Text = "File Content";
            // 
            // buttonSaveContent
            // 
            this.buttonSaveContent.Location = new System.Drawing.Point(580, 370);
            this.buttonSaveContent.Name = "buttonSaveContent";
            this.buttonSaveContent.Size = new System.Drawing.Size(80, 25);
            this.buttonSaveContent.TabIndex = 4;
            this.buttonSaveContent.Text = "Save";
            this.buttonSaveContent.UseVisualStyleBackColor = true;
            this.buttonSaveContent.Click += new System.EventHandler(this.buttonSaveContent_Click);
            // 
            // buttonCreateFile
            // 
            this.buttonCreateFile.Location = new System.Drawing.Point(580, 457);
            this.buttonCreateFile.Name = "buttonCreateFile";
            this.buttonCreateFile.Size = new System.Drawing.Size(80, 25);
            this.buttonCreateFile.TabIndex = 5;
            this.buttonCreateFile.Text = "Create";
            this.buttonCreateFile.UseVisualStyleBackColor = true;
            this.buttonCreateFile.Click += new System.EventHandler(this.buttonCreateFile_Click);
            // 
            // textBoxNewFileName
            // 
            this.textBoxNewFileName.Location = new System.Drawing.Point(12, 460);
            this.textBoxNewFileName.Name = "textBoxNewFileName";
            this.textBoxNewFileName.Size = new System.Drawing.Size(550, 22);
            this.textBoxNewFileName.TabIndex = 6;
            this.textBoxNewFileName.Text = "Enter the file name to be created";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(580, 405);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(80, 25);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(680, 500);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.listViewItems);
            this.Controls.Add(this.textBoxFileContent);
            this.Controls.Add(this.buttonSaveContent);
            this.Controls.Add(this.buttonCreateFile);
            this.Controls.Add(this.textBoxNewFileName);
            this.Name = "Form1";
            this.Text = "File Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.TextBox textBoxFileContent;
        private System.Windows.Forms.Button buttonSaveContent;
        private System.Windows.Forms.Button buttonCreateFile;
        private System.Windows.Forms.TextBox textBoxNewFileName;
        private System.Windows.Forms.Button deleteButton;
    }
}

