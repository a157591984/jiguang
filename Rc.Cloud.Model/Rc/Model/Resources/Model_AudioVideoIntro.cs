namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_AudioVideoIntro
    {
        private string _audiovideobookid;
        private string _audiovideointroid;
        private string _audiovideoname;
        private string _audiovideotypeenum;
        private string _audiovideourl;
        private DateTime? _createtime;
        private string _createuser;
        private string _filename;

        public string AudioVideoBookId
        {
            get
            {
                return this._audiovideobookid;
            }
            set
            {
                this._audiovideobookid = value;
            }
        }

        public string AudioVideoIntroId
        {
            get
            {
                return this._audiovideointroid;
            }
            set
            {
                this._audiovideointroid = value;
            }
        }

        public string AudioVideoName
        {
            get
            {
                return this._audiovideoname;
            }
            set
            {
                this._audiovideoname = value;
            }
        }

        public string AudioVideoTypeEnum
        {
            get
            {
                return this._audiovideotypeenum;
            }
            set
            {
                this._audiovideotypeenum = value;
            }
        }

        public string AudioVideoUrl
        {
            get
            {
                return this._audiovideourl;
            }
            set
            {
                this._audiovideourl = value;
            }
        }

        public DateTime? CreateTime
        {
            get
            {
                return this._createtime;
            }
            set
            {
                this._createtime = value;
            }
        }

        public string CreateUser
        {
            get
            {
                return this._createuser;
            }
            set
            {
                this._createuser = value;
            }
        }

        public string FileName
        {
            get
            {
                return this._filename;
            }
            set
            {
                this._filename = value;
            }
        }
    }
}

