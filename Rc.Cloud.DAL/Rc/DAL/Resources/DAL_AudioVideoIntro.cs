namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_AudioVideoIntro
    {
        public bool Add(Model_AudioVideoIntro model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into AudioVideoIntro(");
            builder.Append("AudioVideoIntroId,AudioVideoBookId,FileName,AudioVideoTypeEnum,AudioVideoName,AudioVideoUrl,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@AudioVideoIntroId,@AudioVideoBookId,@FileName,@AudioVideoTypeEnum,@AudioVideoName,@AudioVideoUrl,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoIntroId", SqlDbType.Char, 0x24), new SqlParameter("@AudioVideoBookId", SqlDbType.Char, 0x24), new SqlParameter("@FileName", SqlDbType.NVarChar, 200), new SqlParameter("@AudioVideoTypeEnum", SqlDbType.VarChar, 50), new SqlParameter("@AudioVideoName", SqlDbType.NVarChar, 200), new SqlParameter("@AudioVideoUrl", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.AudioVideoIntroId;
            cmdParms[1].Value = model.AudioVideoBookId;
            cmdParms[2].Value = model.FileName;
            cmdParms[3].Value = model.AudioVideoTypeEnum;
            cmdParms[4].Value = model.AudioVideoName;
            cmdParms[5].Value = model.AudioVideoUrl;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_AudioVideoIntro DataRowToModel(DataRow row)
        {
            Model_AudioVideoIntro intro = new Model_AudioVideoIntro();
            if (row != null)
            {
                if (row["AudioVideoIntroId"] != null)
                {
                    intro.AudioVideoIntroId = row["AudioVideoIntroId"].ToString();
                }
                if (row["AudioVideoBookId"] != null)
                {
                    intro.AudioVideoBookId = row["AudioVideoBookId"].ToString();
                }
                if (row["FileName"] != null)
                {
                    intro.FileName = row["FileName"].ToString();
                }
                if (row["AudioVideoTypeEnum"] != null)
                {
                    intro.AudioVideoTypeEnum = row["AudioVideoTypeEnum"].ToString();
                }
                if (row["AudioVideoName"] != null)
                {
                    intro.AudioVideoName = row["AudioVideoName"].ToString();
                }
                if (row["AudioVideoUrl"] != null)
                {
                    intro.AudioVideoUrl = row["AudioVideoUrl"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    intro.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    intro.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return intro;
        }

        public bool Delete(string AudioVideoIntroId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from AudioVideoIntro ");
            builder.Append(" where AudioVideoIntroId=@AudioVideoIntroId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoIntroId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = AudioVideoIntroId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AudioVideoIntroIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from AudioVideoIntro ");
            builder.Append(" where AudioVideoIntroId in (" + AudioVideoIntroIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string AudioVideoIntroId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from AudioVideoIntro");
            builder.Append(" where AudioVideoIntroId=@AudioVideoIntroId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoIntroId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = AudioVideoIntroId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AudioVideoIntroId,AudioVideoBookId,FileName,AudioVideoTypeEnum,AudioVideoName,AudioVideoUrl,CreateUser,CreateTime ");
            builder.Append(" FROM AudioVideoIntro ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" AudioVideoIntroId,AudioVideoBookId,FileName,AudioVideoTypeEnum,AudioVideoName,AudioVideoUrl,CreateUser,CreateTime ");
            builder.Append(" FROM AudioVideoIntro ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.AudioVideoIntroId desc");
            }
            builder.Append(")AS Row, T.*  from AudioVideoIntro T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_AudioVideoIntro GetModel(string AudioVideoIntroId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AudioVideoIntroId,AudioVideoBookId,FileName,AudioVideoTypeEnum,AudioVideoName,AudioVideoUrl,CreateUser,CreateTime from AudioVideoIntro ");
            builder.Append(" where AudioVideoIntroId=@AudioVideoIntroId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoIntroId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = AudioVideoIntroId;
            new Model_AudioVideoIntro();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM AudioVideoIntro ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_AudioVideoIntro model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update AudioVideoIntro set ");
            builder.Append("AudioVideoBookId=@AudioVideoBookId,");
            builder.Append("FileName=@FileName,");
            builder.Append("AudioVideoTypeEnum=@AudioVideoTypeEnum,");
            builder.Append("AudioVideoName=@AudioVideoName,");
            builder.Append("AudioVideoUrl=@AudioVideoUrl,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where AudioVideoIntroId=@AudioVideoIntroId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AudioVideoBookId", SqlDbType.Char, 0x24), new SqlParameter("@FileName", SqlDbType.NVarChar, 200), new SqlParameter("@AudioVideoTypeEnum", SqlDbType.VarChar, 50), new SqlParameter("@AudioVideoName", SqlDbType.NVarChar, 200), new SqlParameter("@AudioVideoUrl", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AudioVideoIntroId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.AudioVideoBookId;
            cmdParms[1].Value = model.FileName;
            cmdParms[2].Value = model.AudioVideoTypeEnum;
            cmdParms[3].Value = model.AudioVideoName;
            cmdParms[4].Value = model.AudioVideoUrl;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.AudioVideoIntroId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

