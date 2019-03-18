using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using imageFilter.Models;

namespace imageFilter.Controllers
{
    public class UploadFilesController : Controller
    {
        public UploadFilesController()
        {
            ViewBag.guid = Guid.NewGuid();
        }

        [HttpGet]
        public ActionResult GetFiles(long Id)
        {
            UploadsViewModel viewModel = Session["Uploads"] as UploadsViewModel;

            return PartialView("~/Views/Shared/_UploadsPartial.cshtml", (viewModel == null ? new UploadsViewModel().Uploads : viewModel.Uploads));
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(FormCollection form, HttpPostedFileBase[] files)
        {
            UploadsViewModel uploadsViewModel = null;

            try
            {
                //Ensure model state is valid
                if (ModelState.IsValid)
                {

                    Session["Uploads"] = null;

                    //iterating through multiple file collection   
                    foreach (HttpPostedFileBase file in files)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/" + ViewBag.guid));
                            var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/" + ViewBag.guid + "/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);

                            uploadsViewModel = Session["Uploads"] != null ? Session["Uploads"] as UploadsViewModel : new UploadsViewModel();
                            uploadsViewModel.ID = long.Parse(form["id"]);
                            myFile upload = new myFile();
                            upload.FileID = uploadsViewModel.Uploads.Count + 1;
                            upload.FileName = file.FileName;
                            upload.FilePath = ("~/UploadedFiles/" + ViewBag.guid + "/") + file.FileName;
                            upload.GuidID = ViewBag.guid;

                            uploadsViewModel.Uploads.Add(upload);
                            Session["Uploads"] = uploadsViewModel;

                            //Validate Image
                            //ValidateImageForFishEye(ServerSavePath);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.FilesCount = (int)files.Count();
                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }

                    }

                    return PartialView("~/Views/Shared/_UploadsPartial.cshtml", uploadsViewModel.Uploads);

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return View();
            
        }

        //[HttpPost]
        //public ActionResult UploadFile(FormCollection form, HttpPostedFileBase file)
        //{
        //    UploadsViewModel uploadsViewModel = Session["Uploads"] != null ? Session["Uploads"] as UploadsViewModel : new UploadsViewModel();
        //    uploadsViewModel.ID = long.Parse(form["id"]);
        //    myFile upload = new myFile();
        //    upload.FileID = uploadsViewModel.Uploads.Count + 1;
        //    upload.FileName = file.FileName;
        //    upload.FilePath = "~/Uploads/" + file.FileName;
        //    //if (file.ContentLength < 4048576) we can check file size before saving if we need to restrict file size or we can check it on client side as well  
        //    //{  
        //    if (file != null)
        //    {
        //        file.SaveAs(Server.MapPath(upload.FilePath));
        //        uploadsViewModel.Uploads.Add(upload);
        //        Session["Uploads"] = uploadsViewModel;
        //    }
        //    // Save FileName and Path to Database according to your business requirements  
        //    //}  
        //    return PartialView("~/Views/Shared/_UploadsPartial.cshtml", uploadsViewModel.Uploads);
        //}

        private void ValidateImageForFishEye(string imagePath)
        {
            var directories = ImageMetadataReader.ReadMetadata(imagePath);

            // print out all metadata
            foreach (var directory in directories)
                foreach (var tag in directory.Tags)
                    Console.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");

            // access the date time
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            var dateTime = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTime);
        }

    }
}