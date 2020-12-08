using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace beadandó
{
    
    public partial class Form1 : Form
    {
        XDocument xdok = XDocument.Load("custumers.xml");
        BindingList<Custumer> custumers = new BindingList<Custumer>();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            

        }
        private void getcustumers()
        {
            var xml = new XmlDocument();
            xml.Load("custumers.xml");
            foreach (XmlElement xmlElement in xml.DocumentElement)
            {
                var custumer = new Custumer();
                custumers.Add(custumer);
                custumer.cName = xmlElement.GetAttribute("Names");
                custumer.cTel = xmlElement.GetAttribute("Tel");
                custumer.cCity = xmlElement.GetAttribute("City");
            }
        }
    }
    
}
