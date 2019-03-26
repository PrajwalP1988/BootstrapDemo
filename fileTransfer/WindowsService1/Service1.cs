using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;
        public static string ServiceTimeInterval = ConfigurationManager.AppSettings["TimeIntervalInMinutes"].ToString();
        public static string path = ConfigurationManager.AppSettings["FolderPath"].ToString();
        public enum Action
        {
            Start,
            Stop
        }

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            InitTimer();
        }

        protected override void OnStop()
        {
            _timer?.Stop();
        }

        private void TimerState(Action action_)
        {
            if (_timer != null)
            {
                if (action_ == Action.Stop)
                {
                    this._timer.Stop();
                    this._timer.Enabled = false;
                }
                else
                {
                    _timer.Start();
                    this._timer.Enabled = true;
                }

            }
        }

        private void TimerStop1()
        {
            if (_timer != null)
            {
                this._timer.Stop();
                this._timer.Enabled = false;
            }
        }

        private void TimerStart1()
        {
            if (_timer != null)
            {
            }
        }

        private void InitTimer()
        {
            double interval = GetInterval();
            _timer = new Timer(interval);  // 30000 milliseconds = 30 seconds
            _timer.Enabled = true;
            _timer.AutoReset = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
        }

        private double GetInterval()
        {
            try
            {
                var time = ServiceTimeInterval;
                var timeInterval = int.Parse(time);
                TimeSpan ts = new TimeSpan(0, timeInterval, 0);
                return ts.TotalMilliseconds;
            }
            catch (Exception)
            {
                return 1000;
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StartProcess();
        }

        private void StartProcess()
        {
            TimerState(Action.Stop);
            try
            {
                FileProcess.FileProcess.StartProcess(path);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                TimerState(Action.Start);
            }
        }
    }
}
