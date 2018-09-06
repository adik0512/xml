using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace xml
{
    class ElenemtToTree
    {
        public static void addElementToTree(XElement elementxml, TreeNode treeNode, int level)
        {
            level++;
            IEnumerable<XElement> elements = elementxml.Elements();
            foreach (XElement element in elements)
            {
                string description = element.Name.LocalName;
                if (!element.HasAttributes) description += " (" + element.Value + ")";
                TreeNode newNode = new TreeNode(description);
                treeNode.Nodes.Add(newNode);
                addElementToTree(element, newNode, level);
            }
        }
    }
}
