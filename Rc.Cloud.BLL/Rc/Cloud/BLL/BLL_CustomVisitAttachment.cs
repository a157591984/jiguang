namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_CustomVisitAttachment
    {
        private readonly DAL_CustomVisitAttachment DAL = new DAL_CustomVisitAttachment();

        public int Add(Model_CustomVisitAttachment model)
        {
            return this.DAL.Add(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string customvisitattachment_id)
        {
            return this.DAL.DeleteByPK(customvisitattachment_id);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByLogic(string customvisitattachment_id)
        {
            return this.DAL.ExistsByLogic(customvisitattachment_id);
        }

        public bool ExistsByPK(string customvisitattachment_id)
        {
            return this.DAL.ExistsByPK(customvisitattachment_id);
        }

        public int GetCustomVisitAttachmentCount(string strCondition, params object[] param)
        {
            return this.DAL.GetCustomVisitAttachmentCount(strCondition, param);
        }

        public Model_CustomVisitAttachment GetCustomVisitAttachmentModelByLogic(string customvisitattachment_id)
        {
            return this.DAL.GetCustomVisitAttachmentModelByLogic(customvisitattachment_id);
        }

        public Model_CustomVisitAttachment GetCustomVisitAttachmentModelByPK(string customvisitattachment_id)
        {
            return this.DAL.GetCustomVisitAttachmentModelByPK(customvisitattachment_id);
        }

        public List<Model_CustomVisitAttachment> GetCustomVisitAttachmentModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetCustomVisitAttachmentModelList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_CustomVisitAttachment> GetCustomVisitAttachmentModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetCustomVisitAttachmentModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_CustomVisitAttachment> GetCustomVisitAttachmentModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetCustomVisitAttachmentCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetCustomVisitAttachmentModelListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetCustomVisitAttachmentCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public int Update(Model_CustomVisitAttachment model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

