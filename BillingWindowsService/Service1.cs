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

namespace BillingWindowsService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer1 = null;
        BillingWindowsService bws = new BillingWindowsService();
        public Service1()
        {
            this.ServiceName = "iBand Billing Windows Service";
            this.EventLog.Log = "Application";

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }


        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            this.timer1.Interval = 30 * 1000; //30 Secs
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            LogClass.writeLog("iBand Billing Windows Service Started");
        }
        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            LogClass.writeLog("Timer ticked and job done");
            bws.GetUserBillingForToday();
        }
        protected override void OnStop()
        {
            timer1.Enabled = false;
            LogClass.writeLog("iBand Billing Windows Service Stopped");
        }
    }
}
