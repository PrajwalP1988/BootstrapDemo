using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace imageFilter.Models
{
    public class RunCLI
    {
        private Dictionary<string, string> pathParam = new Dictionary<string, string>() {
            {"--multirow -o pano.pto pano.pto", "cpfind" },
            {"-i pano.pto -o pano.pto", "celeste_standalone" },
            {"-o pano.pto pano.pto", "cpclean" },
            //{"-v --output pano.pto pano.pto", "ptoclean" },
            {"-a -l -s -o pano.pto pano.pto", "autooptimiser" },
            {"-a -l -s -m -o pano.pto pano.pto", "autooptimiser" },
            {"--opt y,p,r -o pano.pto pano.pto", "pto_var" },
            {"-n -o pano.pto pano.pto", "autooptimiser" },
            //{"-m -o pano.pto pano.pto", "autooptimiser" },
            {"-o pano.pto --center --straighten --canvas=AUTO --crop=AUTO pano.pto", "pano_modify" },
            {"--assistant pano.pto", "hugin_executor" }
        };

        public string GUID { get; set; }
        public RunCLI(string guid)
        {
            GUID = guid;
        }

        public void RunProcess()
        {
            //Parallel.Invoke(() => CreateAndRunCLI());
            CreateAndRunCLI();
        }

        private void CreateAndRunCLI()
        {
            try
            {
                string cPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Hugin\\bin\\");
                string ptoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Temp\\" + GUID.ToString() + ".pto");
                string outputImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "images\\" + GUID.ToString() + ".JPG");
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles\\" + GUID.ToString() + "\\");
                string files = string.Join(" ", Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles\\" + GUID.ToString()));
                string cParams = "-p 2 -o " + ptoPath + " " + files;
                string filename = Path.Combine(cPath, "pto_gen.exe");

                RunCommand(filename, cParams);

                foreach (var item in pathParam)
                {
                    string param = item.Key.ToString();
                    param = param.Replace("pano.pto", ptoPath);
                    RunCommand(Path.Combine(cPath, item.Value), param);
                }

                RunCommand(Path.Combine(cPath, "hugin_stitch_project"), "--output=" + outputImage + " " + ptoPath);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void RunCommand(string path, string param)
        {
            using (var proc = System.Diagnostics.Process.Start(path, param))
            {
                proc.WaitForExit();
            }
        }

    }
}