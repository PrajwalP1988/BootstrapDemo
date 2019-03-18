using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace imageFilter.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            List<string> images = new List<string>();
            try
            {
                if (file.ContentLength > 0)
                {
                    string extenstion = System.IO.Path.GetExtension(file.FileName);
                    string _FileName = Guid.NewGuid().ToString() + "." + extenstion;
                    string _path = Path.Combine(Server.MapPath("~/filter"), _FileName);
                    file.SaveAs(_path);
                    images.Add(_path);
                    images.Add(GrayScale(_path, _FileName));
                    images.Add(CornerDetection(_path, _FileName));
                    images.Add(Invert(_path, _FileName));
                }
                ViewBag.Message = "File Uploaded Successfully !!";
                return View(images);
            }
            catch
            {
                ViewBag.Message = "File Upload failed!!";
                return View();
            }
        }

        private string GrayScale(string path, string name)
        {
            try
            {
                Bitmap newImage = (Bitmap)System.Drawing.Image.FromFile(path);
                AForge.Imaging.Filters.GrayscaleBT709 gray = new GrayscaleBT709();
                Bitmap _image = (Bitmap)gray.Apply(newImage);
                string _Path = Path.Combine(Server.MapPath("~/filter"), name + "Gray.png");
                _image.Save(_Path);
                _image.Dispose();
                return _Path;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string CornerDetection(string path, string name)
        {
            try
            {
                Bitmap newImage = (Bitmap)System.Drawing.Image.FromFile(path);
                // create corner detector's instance
                SusanCornersDetector scd = new SusanCornersDetector();
                // create corner maker filter
                CornersMarker filter = new CornersMarker(scd, Color.Red);
                // apply the filter
                filter.ApplyInPlace(newImage);
                string _Path = Path.Combine(Server.MapPath("~/filter"), name + "Corner.png");
                newImage.Save(_Path);
                newImage.Dispose();
                return _Path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Invert(string path, string name)
        {
            try
            {
                Bitmap newImage = (Bitmap)System.Drawing.Image.FromFile(path);
                // create filter
                Invert filter = new Invert();
                // apply the filter
                filter.ApplyInPlace(newImage);
                string _Path = Path.Combine(Server.MapPath("~/filter"), name + "Invert.png");
                newImage.Save(_Path);
                newImage.Dispose();
                return _Path;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}