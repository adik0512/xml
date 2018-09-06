using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace xml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //XDocument xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), 
            //    new XComment("Application Parameters"),
            //    new XElement("options", 
            //        new XElement("window", 
            //            new XAttribute("name", this.Text),
            //             new XElement("position", 
            //               new XElement("X", this.Left), 
            //               new XElement("Y", this.Top)), 
            //             new XElement("size", 
            //               new XElement("Width", this.Width), 
            //               new XElement("Height", this.Height)))));
            XDocument xml = new XDocument();
            XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
            XComment coment = new XComment("Application Parameters");
            XElement options = new XElement("options");
            XElement window = new XElement("window");
            XAttribute name = new XAttribute("name", this.Text);
            XElement position = new XElement("position");
            XElement X = new XElement("X",this.Left);
            XElement Y = new XElement("Y",this.Top);
            XElement size = new XElement("size");
            XElement Width = new XElement("Width",this.Width);
            XElement Height = new XElement("Height",this.Height);

            options.Add(window);
            window.Add(name);
            window.Add(position);
            position.Add(X);
            position.Add(Y);
            window.Add(size);
            size.Add(Width);
            size.Add(Height); 

            xml.Declaration = declaration;
            xml.Add(coment);
            xml.Add(options);
              
            xml.Save("Settings.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                XDocument xml = XDocument.Load("Settings.xml");

                this.Text = xml.Root.Element("window").Attribute("name").Value;

                XElement position = xml.Root.Element("window").Element("position");
                this.Left = int.Parse(position.Element("X").Value);
                this.Top = int.Parse(position.Element("Y").Value);

                XElement size = xml.Root.Element("window").Element("size");
                this.Width = int.Parse(size.Element("Width").Value);
                this.Height = int.Parse(size.Element("Height").Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error read xml:\n" + exc.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument xml = XDocument.Load("Settings.xml");

                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();

                TreeNode nodeMain = new TreeNode(xml.Root.Name.LocalName);
                ElenemtToTree.addElementToTree(xml.Root, nodeMain, 0);
                treeView1.Nodes.Add(nodeMain);
                nodeMain.ExpandAll();
                treeView1.EndUpdate();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error read xml:\n" + exc.Message);
            }
        }
    }
}
