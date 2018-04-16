using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using System.Text;
namespace Rc.Cloud.Web.Principal
{
    public partial class StatsGradeClassHW_Hierarchy : Rc.Cloud.Web.Common.FInitData
    {

        public string GradeId = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
        }

        [WebMethod]
        public static string GetStatsGradeClassHW_Hierarchy(string GradeId, string ResourceToResourceFolder_Id)
        {
            try
            {
                StringBuilder StrBody = new StringBuilder();
                StringBuilder StrBodyGrade = new StringBuilder();
                GradeId = GradeId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string TempClassName = string.Empty;
                string ClassHWScoreLevelCount = string.Empty;
                string TempDate = string.Empty;
                string Onedate = string.Empty;
                string Twodate = string.Empty;
                string Threedate = string.Empty;
                string Fourdate = string.Empty;
                ///年级
                //BLL_StatsGradeClassHW_Hierarchy bllStatsGradeClassHW_Hierarchy = new BLL_StatsGradeClassHW_Hierarchy();
                //List<Model_StatsGradeClassHW_Hierarchy> StatsGradeClassHW_HierarchyList = new List<Model_StatsGradeClassHW_Hierarchy>();
                //StatsGradeClassHW_HierarchyList = bllStatsGradeClassHW_Hierarchy.GetModelList("ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by Hierarchy");
                //List<Model_StatsGradeClassHW_Hierarchy> listGradeDistict = StatsGradeClassHW_HierarchyList.Where((x, i) => StatsGradeClassHW_HierarchyList.FindIndex(z => z.Gradeid == x.Gradeid) == i).ToList();//去重后数据

                //foreach (var item in listGradeDistict)
                //{
                //    StrBodyGrade.Append("<tr><td>年级(" + Convert.ToInt32(item.ClassAllCount) + "人)</td>");
                //    List<Model_StatsGradeClassHW_Hierarchy> listSub = StatsGradeClassHW_HierarchyList.Where(x => x.Gradeid == item.Gradeid).ToList();//子级数据
                //    if (listSub.Count == 4)
                //    {
                //        foreach (var itemSub in listSub)
                //        {
                //            StrBodyGrade.AppendFormat("<td>{1}%({0}人)</td>", Convert.ToInt32(itemSub.HierarchyCount.ToString().clearLastZero()), item.HierarchyCountRate.ToString().clearLastZero());

                //        }
                //    }
                //    else
                //    {
                //        string td1 = "<td>0%(0人)</td>", td2 = "<td>0%(0人)</td>", td3 = "<td>0%(0人)</td>", td4 = "<td>0%(0人)</td>";
                //        foreach (var itemSub in listSub)
                //        {
                //            switch (Convert.ToInt16(itemSub.Hierarchy))
                //            {
                //                case 1:
                //                    td1 = "<td>" + itemSub.HierarchyCountRate + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                //                    break;
                //                case 2:
                //                    td2 = "<td>" + itemSub.HierarchyCountRate + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                //                    break;
                //                case 3:
                //                    td3 = "<td>" + itemSub.HierarchyCountRate + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                //                    break;
                //                case 4:
                //                    td4 = "<td>" + itemSub.HierarchyCountRate + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                //                    break;
                //            }
                //        }
                //        StrBodyGrade.Append(td1 + td2 + td3 + td4);
                //    }
                //}
                //1,2,3
                ///班级
                BLL_StatsGradeClassHW_Hierarchy bllStatsGradeClassHW_Hierarchy = new BLL_StatsGradeClassHW_Hierarchy();
                List<Model_StatsGradeClassHW_Hierarchy> StatsGradeClassHW_HierarchyList = new List<Model_StatsGradeClassHW_Hierarchy>();
                List<Model_StatsGradeClassHW_Hierarchy> listAll = new List<Model_StatsGradeClassHW_Hierarchy>();//所有数据
                //Hierarchy=0为未提交学生数据
                listAll = bllStatsGradeClassHW_Hierarchy.GetModelList(" Hierarchy>0 and ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and Gradeid='" + GradeId + "' order by Hierarchy ");
                List<Model_StatsGradeClassHW_Hierarchy> listDistict = listAll.Where((x, i) => listAll.FindIndex(z => z.ClassID == x.ClassID) == i).ToList();//去重后数据
                foreach (var item in listDistict)
                {
                    StrBody.AppendFormat("<tr><td>{0}({1}人)</td>", item.ClassName, item.ClassAllCount);
                    List<Model_StatsGradeClassHW_Hierarchy> listSub = listAll.Where(x => x.ClassID == item.ClassID).ToList();//子级数据
                    if (listSub.Count == 4)
                    {
                        foreach (var itemSub in listSub)
                        {
                            StrBody.AppendFormat("<td>{1}%({0}人)</td>", Convert.ToInt32(itemSub.HierarchyCount), itemSub.HierarchyCountRate.ToString().clearLastZero());
                            if (itemSub.Hierarchy == 1)
                            {
                                Onedate += itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                            }
                            if (itemSub.Hierarchy == 2)
                            {
                                Twodate += itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                            }
                            if (itemSub.Hierarchy == 3)
                            {
                                Threedate += itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                            }
                            if (itemSub.Hierarchy == 4)
                            {
                                Fourdate += itemSub.HierarchyCount.ToString().clearLastZero() + ",";
                            }

                        }
                    }
                    else
                    {
                        string td1 = "<td>0%(0人)</td>", td2 = "<td>0%(0人)</td>", td3 = "<td>0%(0人)</td>", td4 = "<td>0%(0人)</td>";
                        string data1 = "0,", data2 = "0,", data3 = "0,", data4 = "0,";
                        foreach (var itemSub in listSub)
                        {
                            switch (Convert.ToInt16(itemSub.Hierarchy))
                            {
                                case 1:
                                    td1 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                                    data1 = itemSub.HierarchyCount + ",";
                                    break;
                                case 2:
                                    td2 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                                    data2 = itemSub.HierarchyCount + ",";
                                    break;
                                case 3:
                                    td3 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                                    data3 = itemSub.HierarchyCount + ",";
                                    break;
                                case 4:
                                    td4 = "<td>" + itemSub.HierarchyCountRate.ToString().clearLastZero() + "%(" + Convert.ToInt32(itemSub.HierarchyCount) + "人)</td>";
                                    data4 = itemSub.HierarchyCount + ",";
                                    break;
                            }
                        }
                        Onedate += data1;
                        Twodate += data2;
                        Threedate += data3;
                        Fourdate += data4;
                        StrBody.Append(td1 + td2 + td3 + td4);
                    }


                    TempClassName += item.ClassName.TrimEnd() + "|";
                }

                StrBody.AppendFormat("</tr>");
                TempDate = "[{\"name\":\"第一层次\",\"data\":[" + Onedate.TrimEnd(',') + "]},"
                    + "{\"name\":\"第二层次\",\"data\":[" + Twodate.TrimEnd(',') + "]},"
                    + "{\"name\":\"第三层次\",\"data\":[" + Threedate.TrimEnd(',') + "]},"
                    + "{\"name\":\"第四层次\",\"data\":[" + Fourdate.TrimEnd(',') + "]}]";
                if (string.IsNullOrEmpty(StrBody.ToString()))
                {
                    return "";
                }
                //    [
                //    {
                //        name: '优秀',
                //        data: [49, 71, 76, 99, 100, 76, 35, 48, 62, 94, 95, 54]
                //    },
                //]
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        TempClassName = TempClassName.TrimEnd('|'),
                        TempDate = TempDate,
                        tb = StrBody.ToString(),
                        tbGrade = StrBodyGrade.ToString(),
                    });
                }

            }
            catch (Exception)
            {

                return "";
            }
        }

    }
}