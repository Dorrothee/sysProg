using System;
using System.IO;
using System.Windows.Forms;

namespace Task_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Set the default path to the root of C drive
            string initialPath = @"C:\";
            textBoxPath.Text = initialPath;
            LoadFilesAndDirectories(initialPath);
        }

        //Load files and directories in the current path
        private void LoadFilesAndDirectories(string path)
        {
            try
            {
                //Clear the ListView
                listViewItems.Items.Clear();

                //Load directories using GetDirectories()
                foreach (var dir in Directory.GetDirectories(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    //Add a new row for this directory
                    ListViewItem dirItem = new ListViewItem(dirInfo.Name);
                    dirItem.SubItems.Add("Directory");  //Second column: Type
                    dirItem.Tag = dirInfo.FullName;     //Store the full path in the Tag property in the first column
                    listViewItems.Items.Add(dirItem);
                }

                //Load files using GetFiles()
                foreach (var file in Directory.GetFiles(path))
                {
                    FileInfo fileInfo = new FileInfo(file);

                    //Add a new row for this file
                    ListViewItem fileItem = new ListViewItem(fileInfo.Name);
                    fileItem.SubItems.Add("File");
                    fileItem.Tag = fileInfo.FullName;
                    listViewItems.Items.Add(fileItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files or directories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Navigate to a specified path when Enter is pressed in the TextBox
        private void textBoxPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)    //Check if Enter key was pressed
            {
                NavigateToPath();   //Call the navigation method
            }
        }

        //Method to navigate to the desired path entered in the TextBox
        private void NavigateToPath()
        {
            string targetPath = textBoxPath.Text.Trim();

            if (string.IsNullOrEmpty(targetPath))   //Validate user input
            {
                MessageBox.Show("Please enter a valid path", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (Directory.Exists(targetPath))   //Check if the entered path exists
                {
                    LoadFilesAndDirectories(targetPath);    //Load the specified directory
                }
                else
                {
                    //Show an error message if the directory does not exist
                    MessageBox.Show("The specified directory does not exist", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to path: {ex.Message}", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Navigate to parent directory
        private void buttonBack_Click(object sender, EventArgs e)
        {
            try
            {
                string currentPath = textBoxPath.Text.Trim();
                DirectoryInfo directoryInfo = new DirectoryInfo(currentPath);

                if (directoryInfo.Parent != null)
                {
                    string parentPath = directoryInfo.Parent.FullName;
                    textBoxPath.Text = parentPath;
                    LoadFilesAndDirectories(parentPath);
                }
                else
                {
                    MessageBox.Show("No parent directory found", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating back: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Double-click logic for opening files or directories
        private void listViewItems_DoubleClick(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count > 0)
            {
                //Get the full path from the selected item's Tag
                string fullPath = listViewItems.SelectedItems[0].Tag.ToString();

                //Check if it's a directory
                if (Directory.Exists(fullPath))
                {
                    //Navigate into the directory
                    textBoxPath.Text = fullPath;
                    LoadFilesAndDirectories(fullPath);
                }
                else if (File.Exists(fullPath))
                {
                    try
                    {
                        //If it's a file, open its content
                        textBoxFileContent.Text = File.ReadAllText(fullPath);
                        textBoxFileContent.Tag = fullPath;  //Store this file path for saving later
                        buttonSaveContent.Enabled = true;   //Enable the "Save" button
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Item is neither a valid file nor a directory
                    MessageBox.Show("This item no longer exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Save edited content to file
        private void buttonSaveContent_Click(object sender, EventArgs e)
        {
            string fullPath = textBoxFileContent.Tag?.ToString();
            if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
            {
                try
                {
                    File.WriteAllText(fullPath, textBoxFileContent.Text);
                    MessageBox.Show("File content saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No file selected for saving", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Create a new file in the current directory
        private void buttonCreateFile_Click(object sender, EventArgs e)
        {
            string currentPath = textBoxPath.Text.Trim();
            string newFileName = textBoxNewFileName.Text.Trim();

            if (string.IsNullOrWhiteSpace(newFileName))
            {
                MessageBox.Show("Please enter a valid file name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullPath = Path.Combine(currentPath, newFileName);

            try
            {
                if (!File.Exists(fullPath))
                {
                    File.Create(fullPath).Close();  //Close to release the lock
                    LoadFilesAndDirectories(currentPath);   //Refresh the directory view
                    MessageBox.Show("File created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("A file with this name already exists", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Delete a file in the current directory
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count > 0)
            {
                var selectedItem = listViewItems.SelectedItems[0];
                string selectedName = selectedItem.Text;
                string selectedType = selectedItem.SubItems[1].Text;
                string path = Path.Combine(textBoxPath.Text, selectedName);

                try
                {
                    if (selectedType == "Folder")
                    {
                        Directory.Delete(path, true);
                    }
                    else
                    {
                        File.Delete(path);
                    }

                    MessageBox.Show($"{selectedName} has been deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Refresh list after deletion
                    LoadFilesAndDirectories(textBoxPath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}