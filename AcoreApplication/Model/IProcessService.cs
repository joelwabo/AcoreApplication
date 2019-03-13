using System.Collections.ObjectModel;

namespace AcoreApplication.Model
{
    public interface IProcessService
    {
        ObservableCollection<Process> GetAllData();
        bool InsertProcess();
        bool DeleteProcess(Process process);
        bool UpdateProcess(Process process);
    }
}