using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DIP_hw1
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick_picbox1);
            pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick_picbox2);
            Console.WriteLine(Math.Acos(0.5));
        }

        Bitmap openImg, processImg;
        Stack<Bitmap> ImgRec = new Stack<Bitmap>();
        Bitmap img;
        Random r = new Random();
        long[,] resultx = new long[4, 5];
        long[,] resulty = new long[4, 5];
        int pointx = 0, pointy = 0;



        // Load Image
        private void button16_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImgRec.Clear();
                openImg = new Bitmap(openFileDialog1.FileName);
                processImg =new Bitmap(openImg);
                pictureBox1.Image = openImg;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        // Save Image
        private void button15_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                processImg.Save(saveFileDialog1.FileName);
            }
        }
        // Undo
        private void button14_Click(object sender, EventArgs e)
        {
            if(ImgRec.Count != 0)
                processImg = ImgRec.Pop();
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // R
        private void button1_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    Color RGB = processImg.GetPixel(x, y);
                    processImg.SetPixel(x, y, Color.FromArgb(RGB.R, RGB.R, RGB.R));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        // G
        private void button2_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    Color RGB = processImg.GetPixel(x, y);
                    processImg.SetPixel(x, y, Color.FromArgb(RGB.G, RGB.G, RGB.G));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        // B
        private void button3_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    Color RGB = processImg.GetPixel(x, y);
                    processImg.SetPixel(x, y, Color.FromArgb(RGB.B, RGB.B, RGB.B));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // gray scale
        private void button4_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    Color RGB = processImg.GetPixel(x, y);
                    int grayScale = (int)(RGB.R * 0.299 + RGB.G * 0.587 + RGB.B * 0.114);
                    processImg.SetPixel(x, y, Color.FromArgb(grayScale, grayScale, grayScale));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Median Filter
        private void button6_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 1; y < processImg.Height - 1; ++y)
            {
                for (int x = 1; x < processImg.Width - 1; ++x)
                {
                    int[] num = new int[9];
                    int count = 0;
                    for (int i = y - 1; i <= y + 1; ++i)
                    {
                        for (int j = x - 1; j <= x + 1; ++j)
                        {
                            Color RGB = processImg.GetPixel(j, i);
                            num[count++] = RGB.R;
                        }
                    }
                    Array.Sort(num);
                    processImg.SetPixel(x, y, Color.FromArgb(num[4], num[4], num[4]));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Mean Filter
        private void button5_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 1; y < processImg.Height-1; ++y)
            {
                for (int x = 1; x < processImg.Width-1; ++x)
                {
                    int sum = 0;
                    for (int i = y - 1; i <= y + 1; ++i)
                    {
                        for (int j = x - 1; j <= x + 1; ++j)
                        {
                            Color RGB = processImg.GetPixel(j, i);
                            sum += RGB.R;
                        }
                    }
                    sum /= 9;
                    processImg.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // thresholding
        private void button8_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    Color RGB = processImg.GetPixel(x, y);
                    if(RGB.R >= trackBar1.Value)
                        processImg.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    else
                        processImg.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        // vertical sobel detect
        private void button9_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            int[,] Gv = new int[,] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
            ImgRec.Push(new Bitmap(processImg));
            Bitmap img = new Bitmap(processImg);
            for (int y = 1; y < img.Height - 1; y++)
            {
                for (int x = 1; x < img.Width - 1; x++)
                {
                    int val = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Color RGB = img.GetPixel(x-1+j, y-1+i);
                            //textBox1.Text = (x - 1 + j).ToString() + ' ' + (y - 1 + i).ToString() + ' ' +i.ToString() + ' ' + j.ToString();
                            //return;
                            val += ((int)RGB.R) * Gv[i,j];
                        }
                    }
                    val = Math.Abs(val);
                    if (val > 255)
                        val = 255;
                    processImg.SetPixel(x, y, Color.FromArgb(val, val, val));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        // horizontal sobel detection
        private void button10_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            int[,] Gh = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            ImgRec.Push(new Bitmap(processImg));
            img = new Bitmap(processImg);
            for (int y = 1; y < img.Height - 1; ++y)
            {
                for (int x = 1; x < img.Width - 1; ++x)
                {
                    int val = 0;
                    for (int i = 0; i < 3; ++i)
                    {
                        for (int j = 0; j < 3; ++j)
                        {
                            Color RGB = img.GetPixel(x - 1 + j, y - 1 + i);
                            val += (int)RGB.R * Gh[i, j];
                        }
                    }
                    val = Math.Abs(val);
                    if (val > 255)
                        val = 255;
                    processImg.SetPixel(x, y, Color.FromArgb(val, val, val));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        // sobel edge detection
        private void button11_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            int[,] Gh = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            int[,] Gv = new int[,] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
            ImgRec.Push(new Bitmap(processImg));
            img = new Bitmap(processImg);
            for (int y = 1; y < img.Height - 1; ++y)
            {
                for (int x = 1; x < img.Width - 1; ++x)
                {
                    int val = 0, val2 = 0;
                    for (int i = 0; i < 3; ++i)
                    {
                        for (int j = 0; j < 3; ++j)
                        {
                            Color RGB = img.GetPixel(x - 1 + j, y - 1 + i);
                            val += (int)RGB.R * Gh[i, j];
                            val2 += (int)RGB.R * Gv[i, j];
                        }
                    }
                    val = (int)Math.Round(Math.Sqrt((double)(val * val + val2 * val2)));
                    if (val > 255)
                        val = 255;
                    processImg.SetPixel(x, y, Color.FromArgb(val, val, val));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        // edge overlapping
        private void button12_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    Color RGB = processImg.GetPixel(x, y);
                    if (RGB.R >= trackBar2.Value)
                        processImg.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                    else
                        processImg.SetPixel(x, y, openImg.GetPixel(x, y));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public void dfs(int x, int y, bool[,] v, Color c)
        {
            v[y, x] = true;
            processImg.SetPixel(x, y, c);
            if (x != 0)
            {
                if (y != 0 && !v[y - 1, x - 1])
                    dfs(x - 1, y - 1, v, c);
                if (y != processImg.Height - 1 && !v[y + 1, x - 1])
                    dfs(x - 1, y + 1, v, c);
                if(!v[y, x-1])
                    dfs(x - 1, y, v, c);
            }

            if(x != processImg.Width-1) {
                if (y != 0 && !v[y -1, x + 1])
                    dfs(x + 1, y - 1, v, c);
                if (y != processImg.Height - 1 && !v[y + 1, x + 1])
                    dfs(x + 1, y + 1, v, c);
                if(!v[y, x+1])
                    dfs(x + 1, y, v, c);
            }
            if(y != 0 && !v[y-1, x])
            {
                dfs(x, y - 1, v, c);
            }
            if(y != processImg.Height-1 && !v[y+1, x])
            {
                dfs(x, y + 1, v, c);
            }
        }

        // Connected Component
        private void button17_Click(object sender, EventArgs e)
        {
            if (openImg == null)
                return;
            int num = 0;
            ImgRec.Push(new Bitmap(processImg));
            bool[,] visit = new bool[processImg.Height, processImg.Width];
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    if(processImg.GetPixel(x, y).R == 0)
                        visit[y, x] = false;
                    else
                        visit[y, x] = true;
                }
            }
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    Color RGB = processImg.GetPixel(x, y);
                    if (visit[y, x])
                        continue;
                    else if (RGB.R == 0)
                    {
                        dfs(x, y, visit, Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));
                        ++num;
                    }
                    else
                        visit[y, x] = true;
                }
            }
            textBox1.Text = num.ToString();
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }


        // histogram equalization
        private void button7_Click(object sender, EventArgs e)
        {
            if (processImg == null)
                return;
            ImgRec.Push(new Bitmap(processImg));
            // draw original histogram of gray level
            int[] histogram_r = new int[256];
            float max = 0;

            for (int i = 0; i < 256; ++i)
                histogram_r[i] = 0;

            for (int i = 0; i < processImg.Width; i++)
            {
                for (int j = 0; j < processImg.Height; j++)
                {
                    Color RGB = processImg.GetPixel(i, j);
                    int gray = RGB.R;
                    histogram_r[gray]++;
                    if (max < histogram_r[gray])
                        max = histogram_r[gray];
                }
            }
            
            int histHeight = 128;
            Bitmap img = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = 0; i < histogram_r.Length; i++)
                {
                    float pct = histogram_r[i] / max;
                    g.DrawLine(Pens.Black,
                        new Point(i, img.Height - 5),
                        new Point(i, img.Height - 5 - (int)(pct * histHeight)) 
                        );
                }
            }
            pictureBox3.Image = img;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            // histogram equalization
            double p = 0;
            for(int i = 0; i < 256; ++i)
            {
                p += ((float)histogram_r[i] / (float)(processImg.Width * processImg.Height));
                histogram_r[i] = (int)Math.Round(p * 255);
            }


            for (int i = 0; i < processImg.Width; i++)
            {
                for (int j = 0; j < processImg.Height; j++)
                {
                    Color RGB = processImg.GetPixel(i, j);
                    int gray = RGB.R;
                    processImg.SetPixel(i, j, Color.FromArgb(histogram_r[gray], histogram_r[gray], histogram_r[gray]));
                }
            }
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            // draw histogram equalization histogram of gray level
            int[] histogram_r2 = new int[256];
            max = 0;

            for (int i = 0; i < processImg.Width; i++)
            {
                for (int j = 0; j < processImg.Height; j++)
                {
                    Color RGB = processImg.GetPixel(i, j);
                    int gray = RGB.R;
                    histogram_r2[gray]++;
                    if (max < histogram_r2[gray])
                        max = histogram_r2[gray];
                }
            }

            Bitmap img2 = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img2))
            {
                for (int i = 0; i < histogram_r2.Length; i++)
                {
                    float pct = histogram_r2[i] / max;
                    g.DrawLine(Pens.Black,
                        new Point(i, img2.Height - 5),
                        new Point(i, img2.Height - 5 - (int)(pct * histHeight))
                        );
                }
            }
            pictureBox4.Image = img2;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void MouseClick_picbox1(object sender, MouseEventArgs e)
        {

            if ((e.X < pictureBox1.Width) && (e.Y < pictureBox1.Height))
            {
                Console.WriteLine(e.X.ToString() + ' ' + e.Y.ToString());
                resultx[pointx++, 4] = e.X;
                resulty[pointy++, 4] = e.Y;
                if (pointx >= 4)
                {
                    pointx = 0;
                    pointy = 0;
                    Console.WriteLine();
                }
            }
        }
        private void MouseClick_picbox2(object sender, MouseEventArgs e)
        {

            if ((e.X < pictureBox2.Width) && (e.Y < pictureBox2.Height))
            {
                Console.WriteLine(e.X.ToString() + ' ' + e.Y.ToString());
                resultx[pointx, 0] = e.X;
                resulty[pointx, 0] = e.X;
                resultx[pointx, 1] = e.Y;
                resulty[pointx, 1] = e.Y;
                resultx[pointx, 2] = e.X * e.Y;
                resulty[pointx, 2] = e.X * e.Y;
                resultx[pointx, 3] = 1;
                resulty[pointx, 3] = 1;
                ++pointx;
                ++pointy;
                if (pointx >= 4)
                    pointx = 0;
                if (pointy >= 4)
                    pointy = 0;
            }
        }

        long gcd(long a, long b)
        {
            if (b == 0)
                return a;
            return gcd(b, a % b);
        }

        // Load target image
        private void button18_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                processImg = new Bitmap(openFileDialog1.FileName);
                pictureBox2.Image = processImg;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }


        // image registration
        private void button13_Click(object sender, EventArgs e)
        {
            // scale
            double imgA_xl = Math.Abs(resultx[0, 4] - resultx[1, 4]),
                imgB_xl = Math.Sqrt((resultx[0, 0] - resultx[1, 0]) * (resultx[0, 0] - resultx[1, 0]) + (resultx[0, 1] - resultx[1, 1]) * (resultx[0, 1] - resultx[1, 1]));
            double sx = imgA_xl / imgB_xl;
            textBox2.Text = sx.ToString();
            double imgA_yl = Math.Abs(resulty[0, 4] - resulty[2, 4]),
                imgB_yl = Math.Sqrt((resultx[0, 0] - resultx[2, 0]) * (resultx[0, 0] - resultx[2, 0]) + (resultx[0, 1] - resultx[2, 1]) * (resultx[0, 1] - resultx[2, 1]));
            double sy = imgA_yl / imgB_yl;
            textBox3.Text = sy.ToString();


            // rotation
            Console.WriteLine();
            double x1 = resultx[1, 4] - resultx[0, 4], y1 = 0;
            double x2 = (resultx[1, 0] - resultx[0, 0]) * sx , y2 = (resultx[1, 1] - resultx[0, 1]) * sy;
            double dot = x1 * x2 + y1 * y2;
            double det = x1 * y2 - y1 * x2;
            Console.WriteLine(dot.ToString() + ' ' + det.ToString());
            Console.WriteLine(x1.ToString() + ' ' + y1.ToString() + ' ' + x2.ToString() + ' ' + y2.ToString());
            Console.WriteLine(dot.ToString() + ' ' + det.ToString());
            double angle = Math.Atan2(det, dot) * 57.29577951;
            textBox4.Text = angle.ToString();
            Console.WriteLine("angle:" + angle);

            // 算 c1,c2,c3,c4,c5,c6,c7,c8
            // ref text book p.102
            double[] ansx = GaussFun(resultx);
            double[] ansy = GaussFun(resulty);
            Console.WriteLine(ansx[0].ToString() + ' ' + ansx[1].ToString() + ' ' + ansx[2].ToString() + ' ' + ansx[3].ToString() + ' ');
            Console.WriteLine(ansy[0].ToString() + ' ' + ansy[1].ToString() + ' ' + ansy[2].ToString() + ' ' + ansy[3].ToString() + ' ');

            Bitmap registered = new Bitmap(processImg.Width,processImg.Height);
            for (int y = 0; y < processImg.Height; ++y)
                for (int x = 0; x < processImg.Width; ++x)
                      registered.SetPixel(x, y, Color.FromArgb(0,0,0));

            // 找 v,w 對應的 x,y
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    int cor_x = (int)Math.Round(ansx[0] * x + ansx[1] * y + ansx[2] * x * y + ansx[3]);   
                    int cor_y = (int)Math.Round(ansy[0] * x + ansy[1] * y + ansy[2] * x * y + ansy[3]);
                    if(cor_x > 0 && cor_x < registered.Width && cor_y > 0 && cor_y < registered.Height)
                    {
                        registered.SetPixel(cor_x, cor_y, processImg.GetPixel(x, y));
                    }
                }
            }

            // 補空洞
            for (int y = 0; y < processImg.Height; ++y)
            {
                Color tmpcolor = Color.FromArgb(0, 0, 0);
                for (int x = 0; x < processImg.Width; ++x)
                {
                    
                    if(registered.GetPixel(x,y).R == 0 && tmpcolor.R != 0)
                    {
                        registered.SetPixel(x, y, tmpcolor);
                    }
                    else
                    {
                        tmpcolor = registered.GetPixel(x, y);
                    }
                }
            }


            // diffrence
            float D = 0;
            for (int y = 0; y < processImg.Height; ++y)
            {
                for (int x = 0; x < processImg.Width; ++x)
                {
                    D += Math.Abs(processImg.GetPixel(x, y).R - registered.GetPixel(x, y).R);
                }
            }
            D = D / (processImg.Height * processImg.Width);
            textBox5.Text = D.ToString();

            processImg = registered;
            pictureBox2.Image = processImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        }

        // 高斯法解線性方程式變數
        public double[] GaussFun(long[,] result)
        {
            double[] ans = new double[4];
            long Gcd, mul1, mul2, lcm;
            for (int i = 1; i < 4; ++i)
            {
                Gcd = gcd(Math.Abs(result[0, 3]), Math.Abs(result[i, 3]));
                lcm = result[0, 3] * result[i, 3] / Gcd;
                mul1 = lcm / Math.Abs(result[0, 3]);
                mul2 = lcm / Math.Abs(result[i, 3]);
                Console.WriteLine(lcm.ToString() + ' ' + mul1.ToString() + ' ' + mul2.ToString());
                if((result[0, 3] > 0 && result[i, 3] > 0)  || (result[0, 3] < 0 && result[i, 3] < 0))
                    for (int j = 0; j < 5; ++j)
                        result[i, j] = mul2 * result[i, j] - mul1 * result[0, j];
                else
                    for (int j = 0; j < 5; ++j)
                        result[i, j] = mul2 * result[i, j] + mul1 * result[0, j];
            }
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    Console.Write(result[i, j].ToString() + ' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            for (int i = 2; i < 4; ++i)
            {
                Gcd = gcd(Math.Abs(result[1, 2]), Math.Abs(result[i, 2]));
                lcm = result[1, 2] * result[i, 2] / Gcd;
                mul1 = lcm / Math.Abs(result[1, 2]);
                mul2 = lcm / Math.Abs(result[i, 2]);
                Console.WriteLine(lcm.ToString() + ' ' + mul1.ToString() + ' ' + mul2.ToString());
                if ((result[1, 2] > 0 && result[i, 2] > 0) || (result[1, 2] < 0 && result[i, 2] < 0))
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        result[i, j] = mul2 * result[i, j] - mul1 * result[1, j];
                    }
                    result[i, 4] = mul2 * result[i, 4] - mul1 * result[1, 4];
                }
                else
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        result[i, j] = mul2 * result[i, j] + mul1 * result[1, j];
                    }
                    result[i, 4] = mul2 * result[i, 4] + mul1 * result[1, 4];
                }
            }
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    Console.Write(result[i, j].ToString() + ' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Gcd = gcd(Math.Abs(result[2, 1]), Math.Abs(result[3, 1]));
            lcm = result[2, 1] * result[3, 1] / Gcd;
            mul1 = lcm / Math.Abs(result[2, 1]);
            mul2 = lcm / Math.Abs(result[3, 1]);
            Console.WriteLine(lcm.ToString() + ' ' + mul1.ToString() + ' ' + mul2.ToString());
            if ((result[2, 1] > 0 && result[3, 1] > 0) || (result[2, 1] < 0 && result[3, 1] < 0))
            {
                for (int j = 0; j < 2; ++j)
                {
                    result[3, j] = mul2 * result[3, j] - mul1 * result[2, j];
                }
                result[3, 4] = mul2 * result[3, 4] - mul1 * result[2, 4];
            }
            else
            {
                for (int j = 0; j < 2; ++j)
                {
                    result[3, j] = mul2 * result[3, j] + mul1 * result[2, j];
                }
                result[3, 4] = mul2 * result[3, 4] + mul1 * result[2, 4];
            }
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    Console.Write(result[i, j].ToString() + ' ');
                }
                Console.WriteLine();
            }
            ans[0] = (double)result[3, 4] / (double)result[3, 0];
            ans[1] = ((double)result[2, 4] - (double)result[2, 0] * (double)ans[0]) / (double)result[2, 1];
            ans[2] = ((double)result[1, 4] - (double)result[1, 0] * (double)ans[0] - (double)result[1, 1] * (double)ans[1]) / (double)result[1, 2];
            ans[3] = ((double)result[0, 4] - (double)result[0, 0] * (double)ans[0] - (double)result[0, 1] * (double)ans[1] - (double)result[0, 2] * (double)ans[2]) / (double)result[0, 3];
            return ans;
        }

        public double[] GaussFun2(long[,] result)
        {
            double[] ans = new double[2];
            long Gcd, mul1, mul2, lcm;
            Gcd = gcd(Math.Abs(result[0, 1]), Math.Abs(result[1, 1]));
            lcm = result[0, 1] * result[1, 1] / Gcd;
            mul1 = lcm / Math.Abs(result[0, 1]);
            mul2 = lcm / Math.Abs(result[1, 1]);
            Console.WriteLine(lcm.ToString() + ' ' + mul1.ToString() + ' ' + mul2.ToString());
            if ((result[2, 1] > 0 && result[3, 1] > 0) || (result[2, 1] < 0 && result[3, 1] < 0))
            {
                for (int j = 0; j < 2; ++j)
                {
                    result[3, j] = mul2 * result[3, j] - mul1 * result[2, j];
                }
                result[3, 4] = mul2 * result[3, 4] - mul1 * result[2, 4];
            }
            else
            {
                for (int j = 0; j < 2; ++j)
                {
                    result[3, j] = mul2 * result[3, j] + mul1 * result[2, j];
                }
                result[3, 4] = mul2 * result[3, 4] + mul1 * result[2, 4];
            }
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    Console.Write(result[i, j].ToString() + ' ');
                }
                Console.WriteLine();
            }
            ans[0] = (double)result[3, 4] / (double)result[3, 0];
            ans[1] = ((double)result[2, 4] - (double)result[2, 0] * (double)ans[0]) / (double)result[2, 1];
            return ans;
        }



    }
}
