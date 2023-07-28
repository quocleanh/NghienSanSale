using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace NSS.CMS.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public JsonResult UploadFile(HttpPostedFileBase[] uploadedFiles)
        { 
            try
            {
                ClaimsPrincipal identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var username = identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                                   .Select(c => c.Value).FirstOrDefault();
                
                if (uploadedFiles != null)
                {
                    string returnImagePath = string.Empty;
                    string fileName;
                    string Extension;
                    string imageName;
                    string imageSavePath;
                    string SavePath;
                    string ReturnPath;
                    if (uploadedFiles.Length > 0)
                    {
                        foreach (var file in uploadedFiles)
                        {
                            fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            Extension = Path.GetExtension(file.FileName);
                            imageName = fileName;
                            // Get full path by parentID 
                            SavePath = "~/Content/uploads";
                            ReturnPath = "https://cms.nghiensansale.vn/" + SavePath + imageName + Extension;
                            SavePath = Server.MapPath(SavePath);
                            bool folderExists = Directory.Exists(SavePath);
                            if (!folderExists)
                                System.IO.Directory.CreateDirectory(SavePath);
                            imageSavePath = SavePath + imageName + Extension;
                            file.SaveAs(imageSavePath);
                            returnImagePath = imageSavePath;
                             try
                            {
                                
                                return Json(Convert.ToString(returnImagePath), JsonRequestBehavior.AllowGet);
                            }
                            catch (Exception ex)
                            {
                                return Json(Convert.ToString("-1"), JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    return Json(Convert.ToString("1"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exx)
            {
                return Json(Convert.ToString("-1"), JsonRequestBehavior.AllowGet);
            }
            return Json(Convert.ToString("-1"), JsonRequestBehavior.AllowGet);
        }
    }
}