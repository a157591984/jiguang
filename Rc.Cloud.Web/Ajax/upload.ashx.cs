using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.SessionState;
using System.IO;
using System.Text;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Cloud.Model;
using Rc.Common.StrUtility;
using System.Collections.Generic;
using Rc.Common.Config;
namespace jqUploadify.scripts
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class upload : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 得到请求内容
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {


                context.Response.ContentType = "text/plain";
                context.Response.Charset = "utf-8";

                string strResource_ID = string.Empty;//资源标识
                string strFile_Suffix = string.Empty;//资源扩展名
                string strFile_Name = string.Empty;//资源名称
                string strResource_MD5 = string.Empty;//资源MD5
                string strResource_DataStream = string.Empty;//资源数据流
                int strResource_ContentLength = 0;//资源内容长度
                string strResource_ContentHtml = string.Empty;//资源HTML内容
                string strResourceFolder_Id = string.Empty;//资源所在文件夹标识
                DateTime strCreateTime = DateTime.Now;//资源上传时间

                string strResourceToResourceFolder_Id = string.Empty;//资源文件关系标识


                string strResource_Type = string.Empty;//资源类型
                string strResource_Class = string.Empty;//资源类别
                string strFile_Owner = string.Empty;//资源所属
                string strCreateUser = string.Empty;//资源创建人

                string strResource_Version = string.Empty;//教材版本

                //教案类型
                string strLessonPlan_Type = string.Empty;
                //年级学期
                string strGradeTerm = string.Empty;
                //学科
                string strSubject = string.Empty;

                strResourceToResourceFolder_Id = Guid.NewGuid().ToString();
                //获取前端提交的文件流
                HttpPostedFile file = context.Request.Files["Filedata"];
                //获取文件的长度
                strResource_ContentLength = file.ContentLength;
                //获取文件名称
                strFile_Name = file.FileName;
                //获取的文件流转
                byte[] bytes = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    bytes = binaryReader.ReadBytes(file.ContentLength);
                }
                //在最后得到MD5 原因是 方法体内有清除stream的方法。

                strResource_DataStream = Convert.ToBase64String(bytes);
                strResource_MD5 = Rc.Common.StrUtility.clsUtility.GetMd5(strResource_DataStream);
                if (!String.IsNullOrEmpty(context.Request["fid"]))
                {

                    strResourceFolder_Id = context.Request["fid"].ToString().Filter();
                }
                if (!String.IsNullOrEmpty(context.Request["R_Version"]))
                {

                    strResource_Version = context.Request["R_Version"].ToString().Filter();
                }
                if (!String.IsNullOrEmpty(context.Request["LP_Type"]))
                {

                    strLessonPlan_Type = context.Request["LP_Type"].ToString().Filter();
                }
                if (!String.IsNullOrEmpty(context.Request["GT"]))
                {

                    strGradeTerm = context.Request["GT"].ToString().Filter();
                }
                if (!String.IsNullOrEmpty(context.Request["subj"]))
                {

                    strSubject = context.Request["subj"].ToString().Filter();
                }
                if (!String.IsNullOrEmpty(context.Request["R_Type"]))
                {

                    strResource_Type = context.Request["R_Type"].ToString().Filter();
                }
                if (!String.IsNullOrEmpty(context.Request["R_Class"]))
                {

                    strResource_Class = context.Request["R_Class"].ToString().Filter();
                }
                if (!String.IsNullOrEmpty(context.Request["uid"]))
                {

                    strFile_Owner = context.Request["uid"].ToString().Filter();
                    strCreateUser = context.Request["uid"].ToString().Filter();
                }
              
                if (file != null)
                {
                    if (file.FileName.LastIndexOf('.') != -1)
                    {
                        strFile_Suffix = file.FileName.Substring(file.FileName.LastIndexOf('.'), file.FileName.Length - file.FileName.LastIndexOf('.'));
                    }



                    string isExistResource = string.Empty;
                    isExistResource = new Rc.BLL.Resources.BLL_Resource().ExistsByMD5(strResource_MD5);
                    //服务器上没有这个文件则添加文件
                    if (isExistResource == "")
                    {
                        strResource_ID = Guid.NewGuid().ToString();
                        Model_Resource modelResource = new Model_Resource();
                        modelResource.Resource_Id = strResource_ID;
                        modelResource.Resource_MD5 = strResource_MD5;
                        modelResource.Resource_DataStrem = strResource_DataStream;
                        modelResource.Resource_ContentLength = strResource_ContentLength;
                        
                        modelResource.CreateTime = strCreateTime;

                        //添加资源
                        bool i = new Rc.BLL.Resources.BLL_Resource().Add(modelResource);
                    }
                    else
                    {
                        strResource_ID = isExistResource;
                    }
                    //得到教材版本
                    List<Model_ResourceFolder> listmodelRf = new Rc.BLL.Resources.BLL_ResourceFolder().GetModelList(" ResourceFolder_ID = '" + strResourceFolder_Id + "'");
                    if (listmodelRf != null)
                    {
                        strResource_Version = listmodelRf[0].Resource_Version;
                    }
                    else
                    {
                        //如果没得到使用通用版
                        strResource_Version = Resource_VersionConst.通用版;
                    }
                    Model_ResourceToResourceFolder modelRTOF = new Model_ResourceToResourceFolder();
                    modelRTOF.ResourceToResourceFolder_Id = strResourceToResourceFolder_Id;
                    modelRTOF.ResourceFolder_Id = strResourceFolder_Id;
                    modelRTOF.Resource_Id = strResource_ID;
                    modelRTOF.File_Name = strFile_Name;
                    modelRTOF.Resource_Name = strFile_Name;
                    modelRTOF.Resource_Type = strResource_Type;
                    modelRTOF.Resource_Class = strResource_Class;
                    modelRTOF.Resource_Version = strResource_Version;
                    modelRTOF.LessonPlan_Type = strLessonPlan_Type;
                    modelRTOF.GradeTerm = strGradeTerm;
                    modelRTOF.Subject = strSubject;
                   
                    modelRTOF.File_Suffix = strFile_Suffix;
                    modelRTOF.File_Owner = strFile_Owner;
                    modelRTOF.CreateFUser = strCreateUser;
                    modelRTOF.CreateTime = strCreateTime;

                    //添加资源与文件的关系
                    bool j = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().Add(modelRTOF);

                }

                context.Response.Write("1");
            }
            catch (Exception ex)
            {

                context.Response.Write(ex.Message.ToString());
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        //public Model_VSysUserRole GetLoginUserInfo()
        //{
        //    object loginUser = null;
        //    if (System.Web.HttpContext.Current.Session["LoginUser"] != null)
        //    {
        //        loginUser = System.Web.HttpContext.Current.Session["LoginUser"];
        //        System.Web.HttpContext.Current.Session["LoginUser"] = loginUser;
        //    }
        //    else
        //    {
        //        Rc.Common.StrUtility.clsUtility.ErrorDispose(1, true);
        //        System.Web.HttpContext.Current.Response.End();
        //    }
        //    if (loginUser!=null)
        //    {
        //        return (Model_VSysUserRole)loginUser;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
    }
}
