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
        public Form1()
        {
            InitializeComponent();
            getcustumers();
            listBox1.DataSource = custumers;
            listBox1.DisplayMember = "cCity";
        }
        //XDocument xdok = XDocument.Load("custumers.xml");
        BindingList<Custumer> custumers = new BindingList<Custumer>();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<Custumer> keres =
                from x in custumers
                where x.cCity.Contains(textBox1.Text)
                select x;
        }
        public void getcustumers()
        {
            var xml = new XmlDocument();
            xml.Load("custumers.xml");
            foreach (XmlElement xmlElement in xml.DocumentElement)
            {
                for (int i = 1; i < 101; i++)
                {
                    var custumer = new Custumer();

                    custumer.cName = xmlElement.GetAttribute("Names");
                    custumer.cTel = xmlElement.GetAttribute("Tel");
                    custumer.cCity = xmlElement.GetAttribute("City");
                    custumer.cID = i;
                    custumers.Add(custumer);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iras();
            
        }
        private void iras()
        {
            
            for (int i = 0; i < 5; i++)
            {
                Random rnd = new Random();
                int kiv = rnd.Next(1, 101);
                IEnumerable<Custumer> keres =
               from x in custumers
               where x.cID == kiv
               select x;
               Console.WriteLine(keres);
            }
            
        }
    }
    
}

