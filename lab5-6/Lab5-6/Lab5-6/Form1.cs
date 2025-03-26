using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_6
{
    public partial class Form1: Form
    {

        public Form1()
        {
            InitializeComponent();
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            //TreeView for more than one student. Can be used for only one, too
            List<Student> students = new List<Student>
            {
                new Student("Post Malone", 41, new Dictionary<string, int>
                {
                    { "System Programming", 95 },
                    { "OSs", 97 },
                    { "Database Organization", 91 },
                    { "OOP", 87 },
                    { "Cybersecurity", 100 },
                    { "Math", 90 },
                    { "Physics", 89 }
                }),
                new Student("Roseanne Park", 41, new Dictionary<string, int>
                {
                    { "System Programming", 96 },
                    { "OSs", 94 },
                    { "Database Organization", 99 },
                    { "OOP", 100 },
                    { "Cybersecurity", 97 },
                    { "Math", 100 },
                    { "Physics", 95 }
                })
            };

            foreach (var student in students)
            {
                DisplayObjectDetails(student);
            }

        }

        private void DisplayObjectDetails(object obj)
        {
            Type type = obj.GetType();
            TreeNode rootNode = new TreeNode(type.FullName + " Details");

            //Properties
            TreeNode propertiesNode = new TreeNode("Properties");
            foreach (PropertyInfo prop in type.GetProperties())
            {
                object value = prop.GetValue(obj);
                if (value is Dictionary<string, int> dict)
                {
                    TreeNode collectionNode = new TreeNode(prop.Name + " (Grades)");
                    foreach (var kvp in dict)
                    {
                        collectionNode.Nodes.Add(new TreeNode($"{kvp.Key}: {kvp.Value}"));
                    }
                    propertiesNode.Nodes.Add(collectionNode);
                }
                else
                {
                    propertiesNode.Nodes.Add(new TreeNode($"{prop.Name} ({prop.PropertyType.Name}): {value}"));
                }
            }
            rootNode.Nodes.Add(propertiesNode);

            //Methods
            TreeNode methodsNode = new TreeNode("Methods");
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                TreeNode methodNode = new TreeNode(method.Name + "()");

                //Get parameters and their types
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    TreeNode parametersNode = new TreeNode("Parameters");
                    foreach (var param in parameters)
                    {
                        parametersNode.Nodes.Add(new TreeNode($"{param.Name}: {param.ParameterType.Name}"));
                    }
                    methodNode.Nodes.Add(parametersNode);
                }

                methodsNode.Nodes.Add(methodNode);
            }
            rootNode.Nodes.Add(methodsNode);

            treeView.Nodes.Add(rootNode);
            treeView.ExpandAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int GroupNumber { get; set; }
        public int GPA { get; private set; }
        public Dictionary<string, int> Subjects { get; set; }

        public Student() {
            Subjects = new Dictionary<string, int>();
            GPA = 0;
        }
        public Student(string Name, int GroupNumber, Dictionary<string, int> Subjects)
        {
            this.Name = Name;
            this.GroupNumber = GroupNumber;
            this.Subjects = Subjects ?? new Dictionary<string, int>();
            GPA = CalculateGPA();
        }

        public int CalculateGPA() {
            if (Subjects == null || !Subjects.Any())
            {
                return 0;
            }

            return (int)Math.Ceiling(Subjects.Values.Average());
        }
        public List<string> GetEnrolledSubjects()
        {
            return Subjects.Keys.ToList();
        }

        public Dictionary<string, int> GetTranscript()
        {
            return Subjects;
        }
    }
}
