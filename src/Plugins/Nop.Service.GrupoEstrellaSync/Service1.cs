using System;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Nop.Service.GrupoEstrellaSync.Clients;
using Nop.Service.GrupoEstrellaSync.Helper;
using Timer = System.Timers.Timer;



namespace Nop.Service.GrupoEstrellaSync
{
    /// <summary>
    /// Windows Service
    /// </summary>
    public partial class Service1 : ServiceBase
    {
        private Timer _timer = new Timer();
        public  IConfiguration _configuration;
        private int _timerInterval;
        private string _logFolderName;
        private string _logFileName;
        private bool _syncProducts;
        private bool _syncClients;
        private bool _syncOrders;

        /// <summary>
        /// Constructor of Service1
        /// </summary>
        /// <param name="configuration"> <see cref:"IConfiguration"/> </param>
        public Service1(IConfiguration configuration)
        {
            _configuration = configuration;
            var settings = _configuration.GetSection("settings");

            _timerInterval = Convert.ToInt32(settings.GetSection("ServiceTimerInterval").Value);
            _logFolderName = settings.GetSection("LogFolderName").Value;
            _logFileName = settings.GetSection("LogFileName").Value;
            _syncProducts = Convert.ToBoolean(settings.GetSection("SyncProducts").Value);
            _syncClients = Convert.ToBoolean(settings.GetSection("SyncClients").Value);
            _syncOrders = Convert.ToBoolean(settings.GetSection("SyncOrders").Value);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WriteToFile("Service is started at " + DateTime.Now , LogHelper.Logtype.Info);
                _timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                _timer.Interval = _timerInterval * 60000; //in minutes
                _timer.Enabled = true;

                Sync();
            }
            catch (Exception e)
            {

                WriteToFile(e.Message + " at " + DateTime.Now, LogHelper.Logtype.Error);
            }

        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now , LogHelper.Logtype.Error);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now , LogHelper.Logtype.Info);
            Sync();
        }

        /// <summary>
        /// Use for whrite log file ane event viewer error
        /// </summary>
        /// <param name="message"> use for message error</param>
        /// <param name="type"> <see cref="LogHelper.Logtype"/> </param>
        public void WriteToFile(string message , LogHelper.Logtype type)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName + "\\" + _logFileName + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            using StreamWriter sw = File.AppendText(filepath);
            sw.WriteLine(message);

            LogHelper.LogEventViewer(message, type);
        }

        private async void Sync()
        {
            if (_syncClients)
            {
                SyncClients();
            }

            if (_syncProducts)
            {
                await SyncCategories();
                SyncProduct();
            }

            if (_syncOrders)
            {
                SyncOrders();
            }
        }

        private async Task SyncProduct()
        {
            try
            {
                WriteToFile("Products syncing is started at " + DateTime.Now , LogHelper.Logtype.Info);

                await new ProductClient(this).SyncProducts();

                WriteToFile("Products syncing ended at" + DateTime.Now , LogHelper.Logtype.Info);
            }
            catch (Exception e)
            {
                //Pendent LOG4NET
                WriteToFile("ERROR Product syncing: " + e.Message + " StackTrace:" +  e.StackTrace + " InnerException StackTrace:" + e.InnerException?.StackTrace + " InnerException.Message:" + e.InnerException?.Message + " at" + DateTime.Now , LogHelper.Logtype.Error);
            }

        }
        
        private async Task SyncCategories()
        {
            try
            {
                WriteToFile("Categories syncing is started at " + DateTime.Now , LogHelper.Logtype.Info);
                
                await new CategoryClient(this).SyncCategories();

                WriteToFile("Categories syncing ended at" + DateTime.Now , LogHelper.Logtype.Info);
            }
            catch (Exception e)
            {
                //Pendent LOG4NET
                WriteToFile("ERROR Categories syncing: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now , LogHelper.Logtype.Error);
            }

        }

        private async Task SyncClients()
        {
            try
            {

                WriteToFile("Clients syncing is started at " + DateTime.Now, LogHelper.Logtype.Info);
                ClientsClient customerClient = new ClientsClient(this,this);
                
                await customerClient.SyncClients();

                WriteToFile("Clients syncing ended at" + DateTime.Now, LogHelper.Logtype.Info);
            }
            catch (Exception e)
            {
                //Pendent LOG4NET
                WriteToFile("ERROR Clients syncing: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, LogHelper.Logtype.Error);
            }

        }

        private async Task SyncOrders()
        {
            try
            {
                WriteToFile("Orders syncing is started at " + DateTime.Now, LogHelper.Logtype.Info);
                OrderClient orderClient = new OrderClient(this,new ClientsClient(this,this));
                
                await orderClient.SyncOrders();

                WriteToFile("Orders syncing ended at" + DateTime.Now, LogHelper.Logtype.Info);
            }
            catch (Exception e)
            {
                WriteToFile("ERROR Orders syncing: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, LogHelper.Logtype.Error);
            }

        }

        /// <summary>
        /// Use for run windows service as ConsoleApp
        /// </summary>
        /// <param name="args"></param>
        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
            OnStop();
        }
    }
}
