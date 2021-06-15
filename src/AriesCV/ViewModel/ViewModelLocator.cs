using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows;

namespace AriesCV.ViewModel
{
    public class ViewModelLocator
    {

        public static ViewModelLocator Instance => new Lazy<ViewModelLocator>(() =>
            Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;


        public ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new AriesMainModel(), "AriesMain");
            SimpleIoc.Default.Register(() => new CVMenuSystemModel(),  "CVMenuSystem");
            SimpleIoc.Default.Register(() => new CVToolKitModel(), "CVToolKit");
        }


        public AriesMainModel AriesMain => SimpleIoc.Default.GetInstance<AriesMainModel>("AriesMain");
        public CVMenuSystemModel CVMenuSystem => SimpleIoc.Default.GetInstance<CVMenuSystemModel>("CVMenuSystem");
        public CVToolKitModel CVToolKit => SimpleIoc.Default.GetInstance<CVToolKitModel>("CVToolKit");




    }
}