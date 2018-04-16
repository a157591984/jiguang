using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.test
{
    public partial class testRecording : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            //开始录音  
            StartRecord();
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
            //停止录音  
            StopRecord(Server.MapPath("1.wav"));
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //播放录音   也可以适用window系统带的TTS（Text To Speech）播放录音  
            SoundPlayer sp = new SoundPlayer(Server.MapPath("1.wav"));
            sp.PlaySync();
        }

        //mciSendStrin.是用来播放多媒体文件的API指令，可以播放MPEG,AVI,WAV,MP3,等等，下面介绍一下它的使用方法：  
        //第一个参数：要发送的命令字符串。字符串结构是:[命令][设备别名][命令参数].  
        //第二个参数：返回信息的缓冲区,为一指定了大小的字符串变量.  
        //第三个参数：缓冲区的大小,就是字符变量的长度.  
        //第四个参数：回调方式，一般设为零  
        //返回值：函数执行成功返回零，否则返回错误代码  
        [System.Runtime.InteropServices.DllImport("WINMM.DLL", EntryPoint = "mciSendString", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            int uReturnLength,
            int hwndCallback);

        public void mciSendString(String cmd)
        {
            int a = mciSendString(cmd, "", 0, 0);
        }

        public void StartRecord()
        {
            mciSendString("set wave bitpersample 8");
            mciSendString("set wave samplespersec 11025");
            mciSendString("set wave channels 1");
            //mciSendString("set wave format tag pcm");

            mciSendString("stop movie");
            mciSendString("close movie");
            mciSendString("open new type WAVEAudio alias movie");

            mciSendString("record movie");
        }

        public void StopRecord(string filename)
        {
            mciSendString("stop movie");
            mciSendString("save movie " + filename);
            mciSendString("close movie");
        }

    }
}