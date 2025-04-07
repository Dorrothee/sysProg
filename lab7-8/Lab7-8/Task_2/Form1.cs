using System;
using System.Configuration; //For accessing the App.config
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2
{
    public partial class Form1 : Form
    {
        //Tracks all found files with their details globally
        private readonly List<FileInfo> foundFiles = new List<FileInfo>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            listResults.Items.Clear(); //Clear previous search results
            foundFiles.Clear(); //Clear previously tracked files

            string fileName = textBoxFile.Text.Trim();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show("Please enter a filename or pattern (e.g., *.txt).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listResults.Items.Add("--------------------------------------");
            listResults.Items.Add("Searching... Please wait!");
            listResults.Items.Add("--------------------------------------");

            try
            {
                //Run the search asynchronously
                await Task.Run(() => SearchAllDrives(fileName));
            }
            catch (Exception ex)
            {
                //Catch any unexpected exceptions
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listResults.Items.Add("--------------------------------------");
                listResults.Items.Add("Search Completed");
                listResults.Items.Add("--------------------------------------");

                //Show the final paths with their details
                if (foundFiles.Count > 0)
                {
                    listResults.Items.Add("Final Paths Found:");

                    foreach (var fileInfo in foundFiles)
                    {
                        //Generate and display file details
                        string fileDetails =
                            $"Found: {fileInfo.FullName} | Size: {fileInfo.Length / 1024.0:F2} KB | Created: {fileInfo.CreationTime}";
                        listResults.Items.Add(fileDetails);
                    }
                }
                else
                {
                    listResults.Items.Add("No files were found matching the search criteria.");
                }
            }
        }

        private void SearchAllDrives(string fileName)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady) //Skip inaccessible drives
                {
                    SearchInDirectory(drive.RootDirectory.FullName, fileName);
                }
                else
                {
                    UpdateListBox($"Skipped drive: {drive.Name} (Not Ready)");
                }
            }
        }

        private void SearchInDirectory(string rootDirectory, string fileName)
        {
            //Load restricted paths from App.config dynamically
            List<string> restrictedPaths = GetRestrictedPaths();

            try
            {
                //Skip known restricted folders using the restrictedPaths list
                foreach (string restrictedPath in restrictedPaths)
                {
                    if (rootDirectory.StartsWith(restrictedPath, StringComparison.OrdinalIgnoreCase))
                    {
                        UpdateListBox($"Skipped restricted folder: {rootDirectory}");
                        return;
                    }
                }

                //Find files in the current directory based on the search pattern
                foreach (string file in Directory.EnumerateFiles(rootDirectory, fileName, SearchOption.TopDirectoryOnly))
                {
                    FileInfo fileInfo = new FileInfo(file);

                    //Add the FileInfo object to the list of found files
                    foundFiles.Add(fileInfo);
                }

                //Recursively search subdirectories
                foreach (string directory in Directory.EnumerateDirectories(rootDirectory))
                {
                    try
                    {
                        SearchInDirectory(directory, fileName);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        UpdateListBox($"Access Denied: {directory}");
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                UpdateListBox($"Access Denied: {rootDirectory}");
            }
            catch (Exception ex)
            {
                UpdateListBox($"Error: {ex.Message}");
            }
        }


        private void UpdateListBox(string text)
        {
            if (listResults.InvokeRequired)
            {
                listResults.Invoke(new Action(() => listResults.Items.Add(text)));
            }
            else
            {
                listResults.Items.Add(text);
            }
        }

        private List<string> GetRestrictedPaths()
        {
            //Fetch paths from App.config
            string paths = ConfigurationManager.AppSettings["RestrictedPaths"];

            if (string.IsNullOrEmpty(paths))
            {
                //If no paths are configured, log a message and return an empty list
                UpdateListBox("No restricted paths found in configuration");
                return new List<string>();
            }

            //Split the comma-separated paths into a list
            List<string> restrictedPaths = new List<string>(paths.Split(','));

            //Trim whitespace from each path and return the list
            for (int i = 0; i < restrictedPaths.Count; i++)
            {
                restrictedPaths[i] = restrictedPaths[i].Trim(); //Remove leading/trailing spaces
            }

            return restrictedPaths;
        }
    }
}