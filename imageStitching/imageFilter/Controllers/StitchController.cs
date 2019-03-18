using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using imageFilter.Models;

namespace imageFilter.Controllers
{
    public class StitchController : Controller
    {
        // GET: Stitch
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewResult()
        {
            if(Request?.QueryString["guid_"] != null)
            {
                ViewBag.guid_ = Request.QueryString["guid_"].ToString();
                Session["imagePath"] = null;
                ViewBag.ImagePath = null;
            }

            if(Session["imagePath"] != null)
            {
                ViewBag.ImagePath = Path.Combine(Server.MapPath("~/images"), Session["imagePath"] + "0.jpg");
                //ViewBag.ImagePath = Session["imagePath"] + ".jpg";

                string outputImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "images\\" + Session["imagePath"] + ".tif");
                ConvertTiffToJpeg(outputImage);
            }

            if(Request?.QueryString["guid_"] == null && Session["imagePath"] == null)
            {
                Response.Redirect("~/UploadFiles/UploadFile");
            }

            return View();
        }

        [HttpPost]
        public ActionResult ViewResult(FormCollection fc)
        {
            RunProcess(fc["guid_"]);
            ViewBag.Status = "Success";
            Session["imagePath"] = fc["guid_"];
            return View();
        }

        public static string[] ConvertTiffToJpeg(string fileName)
        {
            using (Image imageFile = Image.FromFile(fileName))
            {
                FrameDimension frameDimensions = new FrameDimension(
                    imageFile.FrameDimensionsList[0]);

                // Gets the number of pages from the tiff image (if multipage) 
                int frameNum = imageFile.GetFrameCount(frameDimensions);
                string[] jpegPaths = new string[frameNum];

                for (int frame = 0; frame < frameNum; frame++)
                {
                    // Selects one frame at a time and save as jpeg. 
                    imageFile.SelectActiveFrame(frameDimensions, frame);
                    using (Bitmap bmp = new Bitmap(imageFile))
                    {
                        jpegPaths[frame] = String.Format("{0}\\{1}{2}.jpg",
                            Path.GetDirectoryName(fileName),
                            Path.GetFileNameWithoutExtension(fileName),
                            frame);
                        bmp.Save(jpegPaths[frame], ImageFormat.Jpeg);
                    }
                }

                return jpegPaths;
            }
        }

        private void RunProcess(string guid)
        {
            RunCLI run = new RunCLI(guid);
            run.RunProcess();
        }

    }
}