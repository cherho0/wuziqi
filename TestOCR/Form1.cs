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
using Tesseract;

namespace TestOCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            pictureBox1.ImageLocation = file;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fi = File.GetCreationTime("lng/chi_sim.traineddata");
            TesseractEngine engine = new TesseractEngine(@"./tessdata", "chi_sim", EngineMode.Default);
            var p = engine.Process(Pix.LoadFromFile(openFileDialog1.FileName));
            textBox1.Text= p.GetText();
            engine.Dispose();
        }
    }
}
