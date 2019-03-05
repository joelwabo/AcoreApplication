using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using AcoreApplication.Model;
using AcoreApplication.Design;

namespace AcoreApplication.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IAutomateService, DesignAutomateService>();
                SimpleIoc.Default.Register<IProcessService, DesignProcessService>();
                SimpleIoc.Default.Register<IHistoriqueService, DesignHistoriqueService>();
            }
            
            else
            {
                SimpleIoc.Default.Register<IAutomateService, AutomateService>();
                SimpleIoc.Default.Register<IProcessService, ProcessService>();
                SimpleIoc.Default.Register<IHistoriqueService, HistoriqueService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MachineViewModel>();
        }

        public MainViewModel MainView
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public LoginViewModel LoginView
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public MachineViewModel MachineView
        {
            get { return ServiceLocator.Current.GetInstance<MachineViewModel>(); }
        }

        public static void Cleanup()
        {
        }
    }
}