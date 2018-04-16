namespace Rc.Common.AppUtility
{
    using System;
    using System.Data;

    public class XMLVerifyExcel
    {
        private int X = -1;
        private int Y = -1;

        public bool VerificationExcel(DataTable dtExcel, string XmlPath, string tableKey)
        {
            DataTable table = new XMLClass().ReadExcel_SQL(XmlPath, tableKey);
            if (dtExcel.Rows.Count == table.Rows.Count)
            {
                return false;
            }
            for (int i = 0; i < dtExcel.Columns.Count; i++)
            {
                if (dtExcel.Rows[0][i].ToString() != table.Rows[i]["ColumnName"].ToString())
                {
                    return false;
                }
            }
            try
            {
                this.X = 0;
                while (this.X < dtExcel.Columns.Count)
                {
                    SqlDbType type = (SqlDbType) Enum.Parse(typeof(SqlDbType), table.Rows[this.X]["DataType"].ToString());
                    string str = table.Rows[this.X]["IsNotNull"].ToString().ToLower();
                    this.Y = 1;
                    while (this.Y < dtExcel.Rows.Count)
                    {
                        if (((dtExcel.Rows[this.Y][this.X] != DBNull.Value) && !string.IsNullOrEmpty(dtExcel.Rows[this.Y][this.X].ToString())) || (str != "false"))
                        {
                            switch (type)
                            {
                                case SqlDbType.DateTime:
                                    Convert.ToDateTime(dtExcel.Rows[this.Y][this.X]);
                                    break;

                                case SqlDbType.Decimal:
                                    Convert.ToDouble(dtExcel.Rows[this.Y][this.X]);
                                    break;

                                case SqlDbType.Float:
                                    Convert.ToDouble(dtExcel.Rows[this.Y][this.X]);
                                    break;

                                case SqlDbType.Int:
                                    Convert.ToInt32(dtExcel.Rows[this.Y][this.X]);
                                    break;
                            }
                        }
                        this.Y++;
                    }
                    this.X++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetColumnNumber
        {
            get
            {
                return this.X;
            }
        }

        public int GetRowNumber
        {
            get
            {
                return this.Y;
            }
        }
    }
}

