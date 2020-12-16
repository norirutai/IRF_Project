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
            listBox1.DisplayMember = "City";
        }
        XDocument xdok = XDocument.Load("custumers.xml");
        BindingList<Custumer> custumers = new BindingList<Custumer>();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                var keres =
                from x in custumers
                where x.City.Contains(textBox1.Text)
                select x;
            listBox1.DataSource = keres.ToList();
        }
        
        public void getcustumers()
        {
            //XmlNodeList name = xml.GetElementsByTagName("Names");
            //XmlNodeList tel = xml.GetElementsByTagName("Tel");
            //XmlNodeList city = xml.GetElementsByTagName("City");
            //for (int j = 1; j < 101; j++)
            //{
            //    var custumer = new Custumer();
            //    custumer.Name = (name[j].InnerText);
            //    custumer.Tel = (tel[j].InnerText);
            //    custumer.City = (city[j].InnerText);
            //    custumer.ID = i;
            //    i++;
            //    custumers.Add(custumer);
            //}
            StreamReader sr = new StreamReader("custumers.xml");
            var xmlString = sr.ReadToEnd();

            var xml = new XmlDocument();
            xml.LoadXml(xmlString);
            int i = 1;
            foreach (XmlElement xmlElement in xml.DocumentElement)
            {
                var custumer = new Custumer();
                custumers.Add(custumer);
                var nameElement = (XmlElement)xmlElement.ChildNodes[0];
                if (nameElement == null)
                    continue;
                custumer.Name = nameElement.InnerText;
                var telElement = (XmlElement)xmlElement.ChildNodes[1];
                if (telElement == null)
                    continue;
                custumer.Tel = telElement.InnerText;
                var cityElemet = (XmlElement)xmlElement.ChildNodes[2];
                if (cityElemet == null)
                    continue;
                custumer.City = cityElemet.InnerText;
                custumer.ID = i;
                i++;
                
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
                var keres =
               from x in custumers
               where x.ID == kiv
               select x;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Application.StartupPath;
                sfd.Filter = "Comma Seperated Values (*.csv)|*.csv";
                sfd.DefaultExt = "csv";
                sfd.AddExtension = true;
                if (sfd.ShowDialog() != DialogResult.OK) return;
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    foreach (var k in keres)
                    {
                        sw.Write(k.Name);
                        sw.Write(";");
                        sw.Write(k.Tel);
                        sw.Write(";");
                        sw.Write(k.City);
                    }
            }
            
        }
    }
    
}

