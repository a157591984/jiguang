namespace Rc.BLL.Resources
{
    using Rc.DAL.Resources;
    using System;
    using System.Data;

    public class CommonHandel
    {
        private Rc.DAL.Resources.CommonHandel dal = new Rc.DAL.Resources.CommonHandel();

        public string GetClassNameByID(string ClassID)
        {
            return this.dal.GetClassNameByID(ClassID);
        }

        public DataSet GetGradeAllClass(string GradeID)
        {
            return this.dal.GetGradeAllClass(GradeID);
        }

        public DataSet GetHomeWorkInfo(string StrWhere)
        {
            return this.dal.GetHomeWorkInfo(StrWhere);
        }

        public DataSet GetListByPageForCommonHandel(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageForCommonHandel(strWhere, orderby, startIndex, endIndex);
        }

        public int GetRecordCountForCommonHandel(string strWhere)
        {
            return this.dal.GetRecordCountForCommonHandel(strWhere);
        }

        public string GetStudentNameByID(string StudentId)
        {
            return this.dal.GetStudentNameByID(StudentId);
        }

        public DataSet GetTeacherAllClass(string TeacherId, string StrWhere)
        {
            return this.dal.GetTeacherAllClass(TeacherId, StrWhere);
        }

        public DataSet GetTeacherAllClassByRTRFolder_Id(string TeacherId, string ResourceToResourceFolder_Id)
        {
            return this.dal.GetTeacherAllClassByRTRFolder_Id(TeacherId, ResourceToResourceFolder_Id);
        }

        public DataSet GetTeacherAllClassBySubject(string TeacherId, string Subject)
        {
            return this.dal.GetTeacherAllClassBySubject(TeacherId, Subject);
        }

        public DataSet GetTeacherAllClassForCommon(string TeacherId, string StrWhere)
        {
            return this.dal.GetTeacherAllClassForCommon(TeacherId, StrWhere);
        }
    }
}

