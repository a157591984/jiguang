using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.teacher
{
    public partial class recordingComment : System.Web.UI.Page
    {
        private static SoundRecord recorder = null;    // 录音 
        protected string hwCTime = string.Empty;
        protected string stuHwId = string.Empty;
        protected string tqId = string.Empty;
        protected string recordingPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            recorder = new SoundRecord();
            hwCTime = pfunction.ToShortDate(Request["hwCTime"].Filter());
            stuHwId = Request["stuHwId"].Filter();
            tqId = Request["tqId"].Filter();
            string filePath = string.Format("/Upload/Resource/teacherRecording/{0}/{1}/{2}.wav"
                , pfunction.ToShortDate(hwCTime), stuHwId, tqId);
            if (File.Exists(Server.MapPath(filePath)))
            {
                recordingPath = filePath + "?" + DateTime.Now.ToString();
            }
        }

        //protected void btnPlay_Click(object sender, EventArgs e)
        //{
        //    //播放录音   也可以适用window系统带的TTS（Text To Speech）播放录音  
        //    System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Server.MapPath("1.wav"));
        //    sp.PlaySync();
        //}

        [WebMethod]
        public static string StartRecording(string hwCTime, string stuHwId, string tqId)
        {
            try
            {
                hwCTime = hwCTime.Filter();
                stuHwId = stuHwId.Filter();
                tqId = tqId.Filter();

                string wavfile = null;

                string basicFolder = HttpContext.Current.Server.MapPath("/Upload/Resource/teacherRecording/");
                string filePath = string.Format("{0}/{1}/{2}/", basicFolder, hwCTime, stuHwId);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath += string.Format("{0}.wav", tqId);
                wavfile = filePath;

                recorder.SetFileName(wavfile);

                recorder.RecStart();
                return "0";
            }
            catch (Exception)
            {
                return "1";
            }
        }

        [WebMethod]
        public static string StopRecording()
        {
            try
            {
                recorder.RecStop();
                recorder = null;
                recorder = new SoundRecord();

                return "0";
            }
            catch (Exception)
            {
                return "1";
            }
        }

        [WebMethod]
        public static string DelRecording(string hwCTime, string stuHwId, string tqId)
        {
            try
            {
                hwCTime = hwCTime.Filter();
                stuHwId = stuHwId.Filter();
                tqId = tqId.Filter();
                
                string basicFolder = HttpContext.Current.Server.MapPath("/Upload/Resource/teacherRecording/");
                string filePath = string.Format("{0}/{1}/{2}/", basicFolder, hwCTime, stuHwId);
                filePath += string.Format("{0}.wav", tqId);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}