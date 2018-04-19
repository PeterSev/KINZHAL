using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
//using System.Runtime.InteropServices;


namespace KINZHAL
{
    public delegate void Event_FPS_Update(double fps);
    public delegate void EventBitmap(Bitmap bmp);
    public class SDI_Capture
    {
        // FilterInfoCollection класс содержащий коллекцию фильтров (по этому классу узнаешь какие подключены платы видео-захвата)
        // Ссылка на описание, глянь и все поймешь, так же реализовано в этой программе
        // http://aforgenet.com/aforge/framework/docs/html/8d13e608-64f7-8c03-84b1-1bce74dabecd.htm
        private FilterInfoCollection videoDevices;


        // Видео источник локального устройста видеозахвата
        // Этот класс видео источник захватывает видео данные с локального устройства,
        // таких как USB-веб-камеры или платы видеозахвата, которые поддерживают DirectShow интерфейс. 
        // Размер видео и размер снимка можно настроить.
        //   http://aforgenet.com/aforge/framework/docs/html/f4d3c2ba-605c-f066-f969-68260ce5e141.htm
        private VideoCaptureDevice videoDevice;



        // Получает возможности видео устройста, размер видео и т.д
        // http://aforgenet.com/aforge/framework/docs/html/7645f228-9d76-a77e-6a0f-a5b9cce471db.htm
        private VideoCapabilities[] videoCapabilities;


        VideoDeviceInfo videoDeviceInfo;
        //int width = 1280, height = 1024;
        //Bitmap bitmap;
        Stopwatch stopwatch = new Stopwatch();
        Stopwatch sw;
        long time = 0; float fps = 0;

        //PictureBox pVideo; //ссылка на пикчербокс, передается в конструкторе
        frmMain frmMain;
        Multimedia.Timer tmr;


        public event Event_FPS_Update eventFPS, eventFPS_FrameReceived;
        public event EventBitmap updateBitmap;
        public SDI_Capture(frmMain frm)
        {
            videoDeviceInfo = new VideoDeviceInfo();
            frmMain = frm;


            tmr = new Multimedia.Timer();
            tmr.Period = 1000;
            tmr.Mode = Multimedia.TimerMode.Periodic;
            tmr.Tick += tmr_Tick;

            //frmMain.videoSourcePlayer.Parent = frmMain.pict1;
            //frmMain.videoSourcePlayer.Parent = frmMain.panelPict;

            /*frmMain.videoSourcePlayer.Parent = frmMain.pict1;
            frmMain.videoSourcePlayer.Location = new System.Drawing.Point(0, 0);
            frmMain.videoSourcePlayer.Size = new Size(0,0);*/
 
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            int framesReceived = 0;
            if (videoDevice != null)
                framesReceived = videoDevice.FramesReceived;

            if (sw == null)
            {
                sw = new Stopwatch();
                sw.Start();
            }
            else
            {
                sw.Stop();
                float fps = 1000.0f * framesReceived / sw.ElapsedMilliseconds;

                eventFPS_FrameReceived(fps);

                sw.Reset();
                sw.Start();
            }
        }

        public bool DisconnectCapture()
        {
            return Disconnect();
        }

        bool Disconnect()
        {
            try
            {
                if (videoDevice != null && videoDevice.IsRunning)
                {
                    videoDevice.SignalToStop();
                    //videoDevice.Stop();
                    //videoDevice.NewFrame -= videoDevice_NewFrame;
                    frmMain.videoSourcePlayer.NewFrame -= videoSourcePlayer_NewFrame;
                    tmr.Stop();
                    videoDevice = null;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool ConnectCapture(int indexVideoDevice, int indexVideoInput, int indexSizeVideo)
        {
            try
            {
                videoDevice = new VideoCaptureDevice(videoDevices[indexVideoDevice].MonikerString);

                

                if (videoCapabilities != null)
                {
                    if (indexVideoInput != -1)
                        videoDevice.CrossbarVideoInput = videoDevice.AvailableCrossbarVideoInputs[indexVideoInput];

                    if (indexSizeVideo != -1)
                        videoDevice.VideoResolution = videoCapabilities[indexSizeVideo];
                }


                frmMain.videoSourcePlayer.VideoSource = videoDevice;
                //frmMain.videoSourcePlayer.Location = new System.Drawing.Point(0, 0);
                //frmMain.videoSourcePlayer.Size = new Size(videoDevice.VideoResolution.FrameSize.Width, videoDevice.VideoResolution.FrameSize.Height);

                frmMain.videoSourcePlayer.NewFrame += videoSourcePlayer_NewFrame;


                videoDevice.Start();

                tmr.Start();

            }
            catch { return false; }
                

            return true;
        }

        void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            if (videoDevice != null && videoDevice.IsRunning)
            {
                stopwatch.Stop();
                time = stopwatch.ElapsedMilliseconds;
                if (time != 0)
                    fps = (1000.0f / time);
                stopwatch.Restart();

                if (eventFPS != null) eventFPS(fps);
            }
        }

        void videoDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (videoDevice!=null && videoDevice.IsRunning)
            {
                stopwatch.Stop();
                time = stopwatch.ElapsedMilliseconds;
                if (time != 0)
                    fps = (1000.0f / time);
                stopwatch.Restart();

                //updateBitmap((Bitmap)eventArgs.Frame);

                updateBitmap((Bitmap)eventArgs.Frame.Clone());

                if (eventFPS != null) eventFPS(fps);
            }
        }


        public VideoDeviceInfo selectIndexChanged(int indexVideoDevice)
        {
            videoDeviceInfo.lstPhysicalOuts.Clear();
            videoDeviceInfo.lstSizeVideo.Clear();

            videoDevice = new VideoCaptureDevice(videoDevices[indexVideoDevice].MonikerString);
            if (videoDevice == null) return null;

            
            for (int i = 0; i < videoDevice.AvailableCrossbarVideoInputs.Length; i++)
                videoDeviceInfo.lstPhysicalOuts.Add(videoDevice.AvailableCrossbarVideoInputs[i].Type.ToString());  //дает исключения, видно лишь в интеллитрейс


            videoCapabilities = videoDevice.VideoCapabilities;
            foreach (VideoCapabilities capability in videoCapabilities)
                videoDeviceInfo.lstSizeVideo.Add(string.Format("{0}x{1}", capability.FrameSize.Width, capability.FrameSize.Height));

            return videoDeviceInfo;
        }

        public List<string> FindDevices()
        {
            videoDeviceInfo.lstDevices.Clear();

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                foreach (FilterInfo dev in videoDevices)
                    videoDeviceInfo.lstDevices.Add(dev.Name);
            }
            else
                videoDeviceInfo.lstDevices.Add("No device found");

            return videoDeviceInfo.lstDevices;
        }
    }



    public class VideoDeviceInfo
    {
        public List<string> lstDevices;
        public List<string> lstPhysicalOuts;
        public List<string> lstSizeVideo;

        public VideoDeviceInfo()
        {
            lstDevices = new List<string>();
            lstPhysicalOuts = new List<string>();
            lstSizeVideo = new List<string>();
        }
    }
}
