using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Nop.Service.AppIPOSSync.Clients;
using Nop.Service.AppIPOSSync.Helpers;

namespace Nop.Service.AppIPOSSync
{
    /// <summary>
    /// Service that runs on a set interval (configured in appsettings.json).
    /// </summary>
    public class Service : ServiceBase
    {
        private readonly CategoryClient _categoryClient;
        private readonly ManufacturerClient _manufacturerClient;
        private readonly ProductClient _productClient;
        private readonly OrderClient _orderClient;
        private readonly CustomerClient _customerClient;
        private readonly Timer _timer = new Timer();
        private readonly int _timerInterval;
        private readonly string _logFolderName;
        private readonly string _logFileName;
        private readonly LogHelper _logHelper;

        /// <summary>
        /// Constructor of Service1
        /// </summary>
        /// <param name="categoryClient">Instance of <see cref="CategoryClient"/></param>
        /// <param name="manufacturerClient">Instance of <see cref="ManufacturerClient"/></param>
        /// <param name="productClient">Instance of <see cref="ProductClient"/></param>
        /// <param name="customerClient">Instance of <see cref="CustomerClient"/></param>
        /// <param name="orderClient">Instance of <see cref="OrderClient"/></param>
        public Service(CategoryClient categoryClient,
            ManufacturerClient manufacturerClient,
            ProductClient productClient,
            CustomerClient customerClient,
            OrderClient orderClient)
        {
            _categoryClient = categoryClient;
            _manufacturerClient = manufacturerClient;
            _productClient = productClient;
            _orderClient = orderClient;
            _customerClient = customerClient;
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();

            var settings = configuration.GetSection("Settings");

            int.TryParse(settings["ServiceTimerInterval"], out int timerInterval);

            _timerInterval = timerInterval;
            _logFolderName = settings["LogFolderName"];
            _logFileName = settings["LogFileName"];

            _logHelper = new LogHelper(settings["EventViewerName"]);
        }

        protected override async void OnStart(string[] args)
        {
            try
            {
                WriteToFile("Service started at " + DateTime.Now, EventLogEntryType.Information);
                _timer.Elapsed += OnElapsedTime;
                _timer.Interval = _timerInterval * 60000; // time in minutes
                _timer.Enabled = true;

                await Sync();
            }
            catch (Exception e)
            {
                WriteToFile(e.Message + " at " + DateTime.Now, EventLogEntryType.Error);
            }

        }

        protected override void OnStop()
        {
            WriteToFile("Service stopped at " + DateTime.Now, EventLogEntryType.Error);
        }

        private async void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service recall at " + DateTime.Now, EventLogEntryType.Information);
            await Sync();
        }

        private void WriteToFile(string message, EventLogEntryType type)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName + "\\" + _logFileName + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            
            if (!File.Exists(filepath))
            { 
                using StreamWriter sw = File.CreateText(filepath);
                sw.WriteLine(message);
            }
            else
            {
                using StreamWriter sw = File.AppendText(filepath);
                sw.WriteLine(message);
            }

            _logHelper.LogEventViewer(message, type);
        }

        private async Task Sync()
        {
            IConfigurationSection settings = ConfigurationHelper.GetConfiguration().GetSection("Settings");

            if (bool.Parse(settings["SyncProducts"]))
            {
                await SyncManufacturers();
                await SyncCategories();
                await SyncProduct();
            }

            if (bool.Parse(settings["SyncCustomers"]))
            {
                await SyncCustomers();
            }

            if (bool.Parse(settings["SyncOrders"]))
            {                
                await SyncOrders();
            }
        }

        private async Task SyncCategories()
        {
            try
            {

                WriteToFile("Categories synching started at " + DateTime.Now, EventLogEntryType.Information);
                await _categoryClient.Sync();

                WriteToFile("Categories synching ended at" + DateTime.Now, EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                WriteToFile("Error synching categories: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, EventLogEntryType.Error);
            }

        }

        private async Task SyncManufacturers()
        {
            try
            {

                WriteToFile("Manufacturers synching started at " + DateTime.Now, EventLogEntryType.Information);
                await _manufacturerClient.Sync();

                WriteToFile("Manufacturers synching ended at" + DateTime.Now, EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                WriteToFile("Error synching manufacturers: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, EventLogEntryType.Error);
            }

        }

        private async Task SyncProduct()
        {
            try
            {
                WriteToFile("Products synching started at " + DateTime.Now, EventLogEntryType.Information);
                await _productClient.Sync();

                WriteToFile("Products synching ended at" + DateTime.Now, EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                WriteToFile("Error synching products: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, EventLogEntryType.Error);
            }

        }

        private async Task SyncCustomers()
        {
            try
            {
                WriteToFile("Customers synching started at " + DateTime.Now, EventLogEntryType.Information);
                await _customerClient.Sync();

                WriteToFile("Customers synching ended at" + DateTime.Now, EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                WriteToFile("Error synching Customers: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, EventLogEntryType.Error);
            }

        }

        private async Task SyncOrders()
        {
            try
            {
                WriteToFile("Orders synching started at " + DateTime.Now, EventLogEntryType.Information);
                await _orderClient.Sync();

                WriteToFile("Orders synching ended at" + DateTime.Now, EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                WriteToFile("Error synching orders: " + e.Message + " StackTrace:" + e.StackTrace + " at" + DateTime.Now, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Use to run windows service as ConsoleApp
        /// </summary>
        /// <param name="args">Args from Main method.</param>
        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
            OnStop();
        }
    }
}