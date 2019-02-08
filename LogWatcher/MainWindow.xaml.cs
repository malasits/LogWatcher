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
using LogWatcher.DAO;
using System.Collections.Specialized;
//using LogWatcher.Models;

namespace LogWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Globális változók inicializálása
        static ILog Log = LogManager.GetLogger(typeof(MainWindow));
        //private List<LogModel> LogModelList = new List<LogModel>();

        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            Log.Info(string.Format("Alkalmazás elindítva"));

            var data = new FileReader(@"D:\Projects\BekisziteIgenyles.log");
            var dataList = data.getLogFileData();

            lvTeszt.ItemsSource = dataList.Skip(Math.Max(0, dataList.Count() - 1000));
            scrollViewer.ScrollToBottom();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
