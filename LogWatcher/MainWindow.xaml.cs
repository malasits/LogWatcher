using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using LogWatcher.Models;

namespace LogWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Globális változók inicializálása
        static ILog Log = LogManager.GetLogger(typeof(MainWindow));
        private List<LogModel> LogModelList = new List<LogModel>();

        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            Log.Info(string.Format("Alkalmazás elindítva"));
        }

        /// <summary>
        /// Kilépés az alkalmazásból menügomb kattintásra
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(this.MainScreen != null)
            {
                var ret = MessageBox.Show("Biztosan kilép az alkalmazásból?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                //Ha valóban be szeretné zárni az alkalmazást a felhasználó
                if(ret == System.Windows.MessageBoxResult.Yes)
                {
                    this.MainScreen.Close();
                    Log.Info(string.Format("Alkalmazás bezárása"));
                }
            }
        }

        /// <summary>
        /// Hozzáad/hozzáadja a kiválasztott log fájlokat az alkalmazásoz
        /// </summary>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Log.InfoFormat("{0} - Start", System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                //File megnyitása
                OpenFileDialog file = new OpenFileDialog();
                file.Multiselect = true;
                file.Filter = "log fájlok |*.log";

                //Ha lett fájl kiválasztva
                if (file.ShowDialog() == true)
                {
                    foreach (var item in file.FileNames)
                    {
                        LogModel log = new LogModel();

                        log.FileName = item.Remove(0, item.LastIndexOf('\\') + 1);
                        log.Path = file.FileName;
                        log.FileSize = new System.IO.FileInfo(log.Path).Length;

                        //Kiválasztott fájlok feltöltése a globális listába [LogModelList]
                        //Ha a lista nem tartalmaz objektumot
                        //Vagy ha nem szerepel a listában az objektum

                        if(!(LogModelList.Any(x=>x.FileName == log.FileName)) || LogModelList.Count == 0)
                        {
                            LogModelList.Add(log);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.ErrorFormat("{0} - ERROR - Hiba történt a fájl megnyitása köben", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Log.Error(String.Format("{0} - ERROR -  Message: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, error.Message));
                Log.Error(String.Format("{0} - ERROR -  StackTrace: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, error.StackTrace));
            }

            Log.InfoFormat("{0} - Sikeresen lefutott", System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}
