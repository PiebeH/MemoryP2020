using MemoryGame.ViewModels;
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

namespace MemoryGame
{
    public partial class HighscoresForm : Form
    {
        public HighscoresForm()
        {
            InitializeComponent();
            HighScore();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HighScore();
        }

        private void HighScore()
        {
           
            string FilePath = @"C:\Users\moege\Source\Repos\MemoryP2020\MemoryGame\SaveGame.sav";
            StreamReader Sr = new StreamReader(FilePath);
            label1.Text = Sr.ReadToEnd();
            Sr.Close();
            int HighScore = int.Parse(label1.Text);
            int Score = int.Parse(textBox1.Text);
            //if (File.Exists(FilePath))
            //{
            //    StreamReader Sr = new StreamReader(FilePath);
            //    label1.Text = Sr.ReadToEnd();
            //    Sr.Close();
            //    int HighScore = int.Parse(label1.Text);
            //    int Score = int.Parse(textBox1.Text);
            //    if (Score > HighScore)
            //    {
            //        label1.Text = Score.ToString();
            //        StreamWriter Sw = new StreamWriter(FilePath);
            //        Sw.Write(label1.Text);
            //        Sw.Close();
            //    }
            //    else
            //    {

            //    }
            //}
            //else
            //{
            //    using (StreamWriter Sw = File.AppendText(FilePath))
            //    {
            //        Sw.Write("0");
            //        Sw.Close();
            //    }
            //}
        }
    }
}
