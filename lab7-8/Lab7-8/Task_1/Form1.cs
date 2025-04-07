using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Task_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string rootPath = @"C:\university\4year\2semester\sysProg\lab7-8"; //We can set here our target directory
            treeView1.Nodes.Clear();
            DirectoryInfo rootDir = new DirectoryInfo(rootPath);

            if (rootDir.Exists)
            {
                TreeNode rootNode = new TreeNode(rootDir.Name) { Tag = rootDir.FullName };
                treeView1.Nodes.Add(rootNode);
                PopulateTree(rootDir, rootNode);
            }
        }

        private void PopulateTree(DirectoryInfo dirInfo, TreeNode parentNode)
        {
            try
            {
                //Add directories
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    TreeNode dirNode = new TreeNode(dir.Name) { Tag = dir.FullName };
                    parentNode.Nodes.Add(dirNode);
                    PopulateTree(dir, dirNode); //Recursively add subdirectories
                }

                //Add files
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    TreeNode fileNode = new TreeNode(file.Name) { Tag = file.FullName };
                    parentNode.Nodes.Add(fileNode);
                }

                treeView1.ExpandAll();
            }
            catch (UnauthorizedAccessException)
            {
                parentNode.Nodes.Add(new TreeNode("Access Denied"));
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
