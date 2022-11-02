using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectorRostros
{
    public partial class Form1 : Form
    {
        static readonly CascadeClassifier cascade = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        public Form1()
        {
            InitializeComponent();
        }
        FilterInfoCollection MisDispositivos;
        bool hay = false; 
        void HayDispositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MisDispositivos.Count > 0)
            {
                hay = true;

                //for (int i = 0; i < MisDispositivos.Count; i++)
                //{
                //    comboBox1.Items.Add(MisDispositivos[i].Name.ToString());
                //}
                //comboBox1.Text = "Carga de dispositivos";
            }
            else
            {
                hay = false;
            }
            
        }
        VideoCaptureDevice MiWebCam;
        //Capture capture = new Capture();
        private void Form1_Load(object sender, EventArgs e)
        {
            HayDispositivos();
            if (hay)
            {
                MiWebCam = new VideoCaptureDevice(MisDispositivos[0].MonikerString);
               
            }

            OpenFileDialog o = new OpenFileDialog();
            //Application.Idle += new EventHandler(ProcesarImgc);
            MiWebCam.NewFrame += new AForge.Video.NewFrameEventHandler(ProcesarImg);//MiWebCam.Start();
            MiWebCam.Start();
        }


        //private void ProcesarImgc(object sender, EventArgs e)
        //{
        //    pictureBox1.Image = (Bitmap)capture.QueryFrame().Bitmap.Clone();
        //    Bitmap ImgBit = new Bitmap(pictureBox1.Image);
        //    Image<Rgb, Byte> image = new Image<Rgb, Byte>((ImgBit));
        //}
        bool d = true; 
        private  void ProcesarImg(object sender, NewFrameEventArgs e)
        {
            
          
            
            try
            {


                //pictureBox1.Image = (Bitmap)e.Frame.Clone();

                Bitmap ImgBit = new Bitmap((Bitmap)e.Frame.Clone());
                {
                    var image = new Image<Rgb, Byte>(ImgBit);
                    

                    Rectangle[] rectangulos = cascade.DetectMultiScale(image, 1.1, 3);


                    foreach (Rectangle Rec in rectangulos)
                    {
                        using (Graphics graphics = Graphics.FromImage(ImgBit))
                        {
                            

                            using (Pen lapiz = new Pen (Color.Blue, 2))
                            {
                               
                                graphics.DrawRectangle(lapiz, Rec);
                            }
                        }
                        
                    }
                    pictureBox1.Image = ImgBit;
                    //e.Frame.Dispose();
                    //ImgBit.Dispose();
                }

            }
            catch (Exception)
            {

                
            }
            
        }

        
    }
}
