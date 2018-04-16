namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsStuHW_Analysis_KP_Info
    {
        public bool Add(Model_StatsStuHW_Analysis_KP_Info model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsStuHW_Analysis_KP_Info(");
            builder.Append("StatsStuHW_Analysis_KP_Info_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,StartDate,EndDate,HWCount,TQCount,TQCount_Right,TQCount_Wrong,TQMastery,PCT70,PCT90,GKCount,GKScore,KPCount_Wrong,KP_Wrong_Text,Info,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StatsStuHW_Analysis_KP_Info_Id,@Student_Id,@Subject,@Resource_Version,@Book_Type,@Parent_Id,@StartDate,@EndDate,@HWCount,@TQCount,@TQCount_Right,@TQCount_Wrong,@TQMastery,@PCT70,@PCT90,@GKCount,@GKScore,@KPCount_Wrong,@KP_Wrong_Text,@Info,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@StatsStuHW_Analysis_KP_Info_Id", SqlDbType.Char, 0x24), new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@HWCount", SqlDbType.Int, 4), new SqlParameter("@TQCount", SqlDbType.Int, 4), new SqlParameter("@TQCount_Right", SqlDbType.Int, 4), new SqlParameter("@TQCount_Wrong", SqlDbType.Int, 4), new SqlParameter("@TQMastery", SqlDbType.Decimal, 9), new SqlParameter("@PCT70", SqlDbType.Int, 4), new SqlParameter("@PCT90", SqlDbType.Int, 4), new SqlParameter("@GKCount", SqlDbType.Int, 4), 
                new SqlParameter("@GKScore", SqlDbType.Decimal, 9), new SqlParameter("@KPCount_Wrong", SqlDbType.Int, 4), new SqlParameter("@KP_Wrong_Text", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Info", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CreateTime", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.StatsStuHW_Analysis_KP_Info_Id;
            cmdParms[1].Value = model.Student_Id;
            cmdParms[2].Value = model.Subject;
            cmdParms[3].Value = model.Resource_Version;
            cmdParms[4].Value = model.Book_Type;
            cmdParms[5].Value = model.Parent_Id;
            cmdParms[6].Value = model.StartDate;
            cmdParms[7].Value = model.EndDate;
            cmdParms[8].Value = model.HWCount;
            cmdParms[9].Value = model.TQCount;
            cmdParms[10].Value = model.TQCount_Right;
            cmdParms[11].Value = model.TQCount_Wrong;
            cmdParms[12].Value = model.TQMastery;
            cmdParms[13].Value = model.PCT70;
            cmdParms[14].Value = model.PCT90;
            cmdParms[15].Value = model.GKCount;
            cmdParms[0x10].Value = model.GKScore;
            cmdParms[0x11].Value = model.KPCount_Wrong;
            cmdParms[0x12].Value = model.KP_Wrong_Text;
            cmdParms[0x13].Value = model.Info;
            cmdParms[20].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsStuHW_Analysis_KP_Info DataRowToModel(DataRow row)
        {
            Model_StatsStuHW_Analysis_KP_Info info = new Model_StatsStuHW_Analysis_KP_Info();
            if (row != null)
            {
                if (row["StatsStuHW_Analysis_KP_Info_Id"] != null)
                {
                    info.StatsStuHW_Analysis_KP_Info_Id = row["StatsStuHW_Analysis_KP_Info_Id"].ToString();
                }
                if (row["Student_Id"] != null)
                {
                    info.Student_Id = row["Student_Id"].ToString();
                }
                if (row["Subject"] != null)
                {
                    info.Subject = row["Subject"].ToString();
                }
                if (row["Resource_Version"] != null)
                {
                    info.Resource_Version = row["Resource_Version"].ToString();
                }
                if (row["Book_Type"] != null)
                {
                    info.Book_Type = row["Book_Type"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    info.Parent_Id = row["Parent_Id"].ToString();
                }
                if ((row["StartDate"] != null) && (row["StartDate"].ToString() != ""))
                {
                    info.StartDate = new DateTime?(DateTime.Parse(row["StartDate"].ToString()));
                }
                if ((row["EndDate"] != null) && (row["EndDate"].ToString() != ""))
                {
                    info.EndDate = new DateTime?(DateTime.Parse(row["EndDate"].ToString()));
                }
                if ((row["HWCount"] != null) && (row["HWCount"].ToString() != ""))
                {
                    info.HWCount = new int?(int.Parse(row["HWCount"].ToString()));
                }
                if ((row["TQCount"] != null) && (row["TQCount"].ToString() != ""))
                {
                    info.TQCount = new int?(int.Parse(row["TQCount"].ToString()));
                }
                if ((row["TQCount_Right"] != null) && (row["TQCount_Right"].ToString() != ""))
                {
                    info.TQCount_Right = new int?(int.Parse(row["TQCount_Right"].ToString()));
                }
                if ((row["TQCount_Wrong"] != null) && (row["TQCount_Wrong"].ToString() != ""))
                {
                    info.TQCount_Wrong = new int?(int.Parse(row["TQCount_Wrong"].ToString()));
                }
                if ((row["TQMastery"] != null) && (row["TQMastery"].ToString() != ""))
                {
                    info.TQMastery = new decimal?(decimal.Parse(row["TQMastery"].ToString()));
                }
                if ((row["PCT70"] != null) && (row["PCT70"].ToString() != ""))
                {
                    info.PCT70 = new int?(int.Parse(row["PCT70"].ToString()));
                }
                if ((row["PCT90"] != null) && (row["PCT90"].ToString() != ""))
                {
                    info.PCT90 = new int?(int.Parse(row["PCT90"].ToString()));
                }
                if ((row["GKCount"] != null) && (row["GKCount"].ToString() != ""))
                {
                    info.GKCount = new int?(int.Parse(row["GKCount"].ToString()));
                }
                if ((row["GKScore"] != null) && (row["GKScore"].ToString() != ""))
                {
                    info.GKScore = new decimal?(decimal.Parse(row["GKScore"].ToString()));
                }
                if ((row["KPCount_Wrong"] != null) && (row["KPCount_Wrong"].ToString() != ""))
                {
                    info.KPCount_Wrong = new int?(int.Parse(row["KPCount_Wrong"].ToString()));
                }
                if (row["KP_Wrong_Text"] != null)
                {
                    info.KP_Wrong_Text = row["KP_Wrong_Text"].ToString();
                }
                if (row["Info"] != null)
                {
                    info.Info = row["Info"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    info.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return info;
        }

        public bool Delete(string StatsStuHW_Analysis_KP_Info_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Analysis_KP_Info ");
            builder.Append(" where StatsStuHW_Analysis_KP_Info_Id=@StatsStuHW_Analysis_KP_Info_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Analysis_KP_Info_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Analysis_KP_Info_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsStuHW_Analysis_KP_Info_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsStuHW_Analysis_KP_Info ");
            builder.Append(" where StatsStuHW_Analysis_KP_Info_Id in (" + StatsStuHW_Analysis_KP_Info_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsStuHW_Analysis_KP_Info_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsStuHW_Analysis_KP_Info");
            builder.Append(" where StatsStuHW_Analysis_KP_Info_Id=@StatsStuHW_Analysis_KP_Info_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Analysis_KP_Info_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Analysis_KP_Info_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsStuHW_Analysis_KP_Info_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,StartDate,EndDate,HWCount,TQCount,TQCount_Right,TQCount_Wrong,TQMastery,PCT70,PCT90,GKCount,GKScore,KPCount_Wrong,KP_Wrong_Text,Info,CreateTime ");
            builder.Append(" FROM StatsStuHW_Analysis_KP_Info ");
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
            builder.Append(" StatsStuHW_Analysis_KP_Info_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,StartDate,EndDate,HWCount,TQCount,TQCount_Right,TQCount_Wrong,TQMastery,PCT70,PCT90,GKCount,GKScore,KPCount_Wrong,KP_Wrong_Text,Info,CreateTime ");
            builder.Append(" FROM StatsStuHW_Analysis_KP_Info ");
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
                builder.Append("order by T.StatsStuHW_Analysis_KP_Info_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsStuHW_Analysis_KP_Info T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsStuHW_Analysis_KP_Info GetModel(string StatsStuHW_Analysis_KP_Info_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsStuHW_Analysis_KP_Info_Id,Student_Id,Subject,Resource_Version,Book_Type,Parent_Id,StartDate,EndDate,HWCount,TQCount,TQCount_Right,TQCount_Wrong,TQMastery,PCT70,PCT90,GKCount,GKScore,KPCount_Wrong,KP_Wrong_Text,Info,CreateTime from StatsStuHW_Analysis_KP_Info ");
            builder.Append(" where StatsStuHW_Analysis_KP_Info_Id=@StatsStuHW_Analysis_KP_Info_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsStuHW_Analysis_KP_Info_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsStuHW_Analysis_KP_Info_Id;
            new Model_StatsStuHW_Analysis_KP_Info();
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
            builder.Append("select count(1) FROM StatsStuHW_Analysis_KP_Info ");
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

        public bool Update(Model_StatsStuHW_Analysis_KP_Info model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsStuHW_Analysis_KP_Info set ");
            builder.Append("Student_Id=@Student_Id,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("Book_Type=@Book_Type,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("HWCount=@HWCount,");
            builder.Append("TQCount=@TQCount,");
            builder.Append("TQCount_Right=@TQCount_Right,");
            builder.Append("TQCount_Wrong=@TQCount_Wrong,");
            builder.Append("TQMastery=@TQMastery,");
            builder.Append("PCT70=@PCT70,");
            builder.Append("PCT90=@PCT90,");
            builder.Append("GKCount=@GKCount,");
            builder.Append("GKScore=@GKScore,");
            builder.Append("KPCount_Wrong=@KPCount_Wrong,");
            builder.Append("KP_Wrong_Text=@KP_Wrong_Text,");
            builder.Append("Info=@Info,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StatsStuHW_Analysis_KP_Info_Id=@StatsStuHW_Analysis_KP_Info_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Student_Id", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@Book_Type", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@HWCount", SqlDbType.Int, 4), new SqlParameter("@TQCount", SqlDbType.Int, 4), new SqlParameter("@TQCount_Right", SqlDbType.Int, 4), new SqlParameter("@TQCount_Wrong", SqlDbType.Int, 4), new SqlParameter("@TQMastery", SqlDbType.Decimal, 9), new SqlParameter("@PCT70", SqlDbType.Int, 4), new SqlParameter("@PCT90", SqlDbType.Int, 4), new SqlParameter("@GKCount", SqlDbType.Int, 4), new SqlParameter("@GKScore", SqlDbType.Decimal, 9), 
                new SqlParameter("@KPCount_Wrong", SqlDbType.Int, 4), new SqlParameter("@KP_Wrong_Text", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Info", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StatsStuHW_Analysis_KP_Info_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.Student_Id;
            cmdParms[1].Value = model.Subject;
            cmdParms[2].Value = model.Resource_Version;
            cmdParms[3].Value = model.Book_Type;
            cmdParms[4].Value = model.Parent_Id;
            cmdParms[5].Value = model.StartDate;
            cmdParms[6].Value = model.EndDate;
            cmdParms[7].Value = model.HWCount;
            cmdParms[8].Value = model.TQCount;
            cmdParms[9].Value = model.TQCount_Right;
            cmdParms[10].Value = model.TQCount_Wrong;
            cmdParms[11].Value = model.TQMastery;
            cmdParms[12].Value = model.PCT70;
            cmdParms[13].Value = model.PCT90;
            cmdParms[14].Value = model.GKCount;
            cmdParms[15].Value = model.GKScore;
            cmdParms[0x10].Value = model.KPCount_Wrong;
            cmdParms[0x11].Value = model.KP_Wrong_Text;
            cmdParms[0x12].Value = model.Info;
            cmdParms[0x13].Value = model.CreateTime;
            cmdParms[20].Value = model.StatsStuHW_Analysis_KP_Info_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

