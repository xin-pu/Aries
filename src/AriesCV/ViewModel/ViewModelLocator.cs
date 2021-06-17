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
            /// 否则 design time 模式下，会重复注册Instance.
            SimpleIoc.Default.Reset();
            SimpleIoc.Default.Register(() => new AriesMainModel());
            SimpleIoc.Default.Register(() => new CVMenuSystemModel());
            SimpleIoc.Default.Register(() => new CVToolKitModel());
            SimpleIoc.Default.Register(() => new CVWorkerContainerModel());
        }


        public AriesMainModel AriesMain => SimpleIoc.Default.GetInstance<AriesMainModel>();
        public CVMenuSystemModel CVMenuSystem => SimpleIoc.Default.GetInstance<CVMenuSystemModel>();
        public CVToolKitModel CVToolKit => SimpleIoc.Default.GetInstance<CVToolKitModel>();
        public CVWorkerContainerModel CvWorkerContainerModel => SimpleIoc.Default.GetInstance<CVWorkerContainerModel>();
    }
}