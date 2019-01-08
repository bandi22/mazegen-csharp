using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labyrinth
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        // Exit button
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dRes = MessageBox.Show("Are you sure you want to exit?", "Exit program", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dRes == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Int32.Parse(textBox1.Text);

                Grid grid = new Grid();
                grid.Generate(n);
                grid.CreateMaze(grid);
                int dim = (10 * n) + 1;

                Bitmap bmp = new Bitmap(dim, dim);
                Graphics g = Graphics.FromImage(bmp);
               
                g.Clear(Color.White);
                Pen pen = new Pen(Color.DarkBlue);
                
                for (int i = 0; i < grid.cells.Length; i++)
                  {
                        if (grid.cells[i].wallW == true)
                        {
                        g.DrawLine(pen, (grid.cells[i].x * 10), (grid.cells[i].y * 10), (grid.cells[i].x * 10) + 10, (grid.cells[i].y * 10));
                        }

                        if (grid.cells[i].wallS == true)
                        {
                            g.DrawLine(pen, (grid.cells[i].x * 10) + 10, (grid.cells[i].y * 10), (grid.cells[i].x * 10) + 10, (grid.cells[i].y * 10) + 10);
                        }

                        if (grid.cells[i].wallE == true)
                        {
                            g.DrawLine(pen, (grid.cells[i].x * 10) + 10, (grid.cells[i].y * 10) + 10, (grid.cells[i].x * 10), (grid.cells[i].y * 10) + 10);
                        }

                        if (grid.cells[i].wallN == true)
                        {
                            g.DrawLine(pen, (grid.cells[i].x * 10), (grid.cells[i].y * 10) + 10, (grid.cells[i].x * 10), (grid.cells[i].y * 10));
                        }
                    } //for

                pictureBox1.Image = bmp;

                if (!button2.Enabled)
                {
                    button2.Enabled = true;
                }

            } //try
            

            catch (Exception ex)
            {

            }


        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save Generated Image";
            saveFileDialog1.Filter = "JPEG Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp|PNG Image (*.png)|*.png";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }

                fs.Close();
            } //if
        }
    }
} //namespace
