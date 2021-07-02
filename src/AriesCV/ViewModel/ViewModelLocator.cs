using AriesCV.ViewModel.ToolKit;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows;
using AriesCV.ViewModel.Menu;

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
            SimpleIoc.Default.Register(() => new MenuRunner());
            SimpleIoc.Default.Register(() => new ToolKitMatModel());
            SimpleIoc.Default.Register(() => new ToolKitMatsModel());
            SimpleIoc.Default.Register(() => new ToolKitContourModel());
            SimpleIoc.Default.Register(() => new WorkerContainerModel());
        }


        public AriesMainModel AriesMain => SimpleIoc.Default.GetInstance<AriesMainModel>();
        public MenuFileModel MenuFile => SimpleIoc.Default.GetInstance<MenuFileModel>();
        public MenuLayoutModel MenuLayout => SimpleIoc.Default.GetInstance<MenuLayoutModel>();
        public MenuRunner MenuRunner => SimpleIoc.Default.GetInstance<MenuRunner>();
        public ToolKitMatModel ToolKitMat => SimpleIoc.Default.GetInstance<ToolKitMatModel>();
        public ToolKitMatsModel ToolKitMats => SimpleIoc.Default.GetInstance<ToolKitMatsModel>();
        public ToolKitContourModel ToolKitContour => SimpleIoc.Default.GetInstance<ToolKitContourModel>();
        public WorkerContainerModel CvWorkerContainer => SimpleIoc.Default.GetInstance<WorkerContainerModel>();
      

    }
}