using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using AcoreApplication.Model;
using AcoreApplication.Design;
using Microsoft.Practices.ServiceLocation;

namespace AcoreApplication.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IProcessService, DesignProcessService>();
            }            
            else
            {
                SimpleIoc.Default.Register<IHistoriqueService, HistoriqueService>();
                SimpleIoc.Default.Register<IProcessService, ProcessService>();
                SimpleIoc.Default.Register<IRecetteService, RecetteService>();
                SimpleIoc.Default.Register<IRedresseurService, RedresseurService>();
                SimpleIoc.Default.Register<ISegmentService, SegmentService>();

                SimpleIoc.Default.Register<IRegistreService, RegistreService>();
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