using System;
using System.Windows;

namespace AriesCV
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
      

        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            var mutex = new System.Threading.Mutex(true, "ElectronicNeedleTherapySystem", out bool ret);

            if (ret) return;
            //MessageBox.Show("已有一个程序实例运行");
            Environment.Exit(0);

        }
    }
}
