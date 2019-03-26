using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FileProcess
{
    public static class FileProcess
    {
        private static List<string> filesUploaded;

        public static void StartProcess(string path)
        {
            ReadDirectory(path);
        }

        private static void ReadDirectory(string path)
        {
            IEnumerable<string> files = System.IO.Directory.EnumerateFiles(path);

            foreach(string file in files)
            {
                Parallel.Invoke(() => PostImage(file));
            }

        }

        public static void PostImage(string path_)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            byte[] imagebytearraystring = ImageFileToByteArray(path_);
            form.Add(new ByteArrayContent(imagebytearraystring, 0, imagebytearraystring.Count()), "file", Path.GetFileName(path_));
            HttpResponseMessage response = httpClient.PostAsync("http://fileTransfer.com:8010/api/Upload", form).Result;

            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
        }

        private static byte[] ImageFileToByteArray(string fullFilePath)
        {
            FileStream fs = File.OpenRead(fullFilePath);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return bytes;
        }

    }
}
