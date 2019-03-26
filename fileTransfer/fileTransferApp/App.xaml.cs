using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Drawing;

namespace fileTransferApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
        public App()
        {
            nIcon.Icon = new Icon(Environment.CurrentDirectory + "\\Icon.ico");
            nIcon.Visible = true;
            nIcon.ShowBalloonTip(5000, "Sticker Printer", "Application is running !!!", System.Windows.Forms.ToolTipIcon.Info);
            nIcon.MouseDown += NIcon_MouseDown;

            //RegisterWCFService();
        }

        //private void RegisterWCFService()
        //{

        //    #region Register WCF Service

        //    var oServiceHost = new ServiceHost(typeof(Service));

        //    foreach (ServiceEndpoint EP in oServiceHost.Description.Endpoints)
        //        EP.Behaviors.Add(new BehaviorAttribute());

        //    oServiceHost.Open();

        //    #endregion
        //}

        private void NIcon_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                CreateContextMenu();
            }
        }

        private void CreateContextMenu()
        {
            nIcon.ContextMenuStrip =
              new System.Windows.Forms.ContextMenuStrip();
            nIcon.ContextMenuStrip.Items.Add("Open").Click += (s, e) => StartThread();
            nIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void StartThread()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void ExitApplication()
        {
            MainWindow.Close();
            nIcon.Dispose();
            nIcon = null;
        }

    }
}
