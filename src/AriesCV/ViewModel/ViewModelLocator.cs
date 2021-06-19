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
            SimpleIoc.Default.Register(() => new MenuFileModel());
            SimpleIoc.Default.Register(() => new MenuLayoutModel());
            SimpleIoc.Default.Register(() => new CVToolKitModel());
            SimpleIoc.Default.Register(() => new CVWorkerContainerModel());
            SimpleIoc.Default.Register(() => new TestModel());
        }


        public AriesMainModel AriesMain => SimpleIoc.Default.GetInstance<AriesMainModel>();
        public MenuFileModel MenuFile => SimpleIoc.Default.GetInstance<MenuFileModel>();
        public MenuLayoutModel MenuLayout => SimpleIoc.Default.GetInstance<MenuLayoutModel>();
        public CVToolKitModel CVToolKit => SimpleIoc.Default.GetInstance<CVToolKitModel>();
        public CVWorkerContainerModel CvWorkerContainer => SimpleIoc.Default.GetInstance<CVWorkerContainerModel>();
        public TestModel Testmodel => SimpleIoc.Default.GetInstance<TestModel>();

    }
}