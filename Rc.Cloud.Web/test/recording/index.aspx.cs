using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.test.recording
{
    public partial class index : System.Web.UI.Page
    {
        private static SoundRecord recorder = null;    // 录音 
        protected void Page_Load(object sender, EventArgs e)
        {
            recorder = new SoundRecord();
        }

        //protected void btnAStart_Click(object sender, EventArgs e)
        //{


        //    // 

        //    // 录音设置 

        //    // 
        //    string wavfile = null;

        //    wavfile = Server.MapPath(Guid.NewGuid().ToString() + ".wav");

        //    recorder.SetFileName(wavfile);

        //    recorder.RecStart();
        //}

        //protected void btnAStop_Click(object sender, EventArgs e)
        //{
        //    recorder.RecStop();

        //    recorder = null;
        //}


        [WebMethod]
        public static string StartRecording()
        {
            try
            {
                string wavfile = null;

                wavfile = HttpContext.Current.Server.MapPath("/Upload/Resource/teacherRecording/1.wav");

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

        protected void btnS_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation =Server.MapPath("1.wav");  

        }

    }
}