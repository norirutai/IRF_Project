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
            dataGridView1.DataSource = custumers;
        }
        BindingList<Custumer> custumers = new BindingList<Custumer>();
        public void getcustumers()
        {
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
        public void iras()
        {
            List<Custumer> random = new List<Custumer>();
            decimal szam = numericUpDown1.Value;
            for (int i = 0; i < szam; i++)
            {
                Random rnd = new Random();
                int kiv = rnd.Next(1, 101);
                random =
               (from x in custumers
                where x.ID == kiv 
                select x).ToList();
            }
            BindingList<Custumer> ToCall = new BindingList<Custumer>(random);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                foreach (var c in ToCall)
                {
                    sw.Write(c.Name);
                    sw.Write(";");
                    sw.Write(c.Tel);
                    sw.Write(";");
                    sw.Write(c.City);
                }
        }

    }
    
}

