namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_ListStructure
    {
        private ListStructure dal = new ListStructure();

        public bool ExecAddDescriptionTable(string database, string tablename, string tableinfo)
        {
            return this.dal.ExecAddDescriptionTable(database, tablename, tableinfo);
        }

        public bool ExecAddDescriptionTableColumn(string database, string tablename, string tableColumn, string tablecolumninfo)
        {
            return this.dal.ExecAddDescriptionTableColumn(database, tablename, tableColumn, tablecolumninfo);
        }

        public bool ExecUpdataDescriptionTable(string database, string tablename, string tableinfo)
        {
            return this.dal.ExecUpdataDescriptionTable(database, tablename, tableinfo);
        }

        public bool ExecUpdataDescriptionTableColumn(string database, string tablename, string tableColumn, string tablecolumninfo)
        {
            return this.dal.ExecUpdataDescriptionTableColumn(database, tablename, tableColumn, tablecolumninfo);
        }

        public DataSet GetListStructure(string database, string Content, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.dal.GetListStructure(database, Content, PageIndex, PageSize, out rCount, out pCount);
        }
    }
}

