using System.Collections.ObjectModel;

namespace AcoreApplication.Model
{
    public interface IProcessService
    {
        ObservableCollection<Process> GetAllData();
    }
}