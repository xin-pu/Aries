

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AriesCV.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class CVMenuSystemModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public CVMenuSystemModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<CVMenuSystemModel>();
        }

        public CVMenuSystemModel CVMenuSystemModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CVMenuSystemModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}