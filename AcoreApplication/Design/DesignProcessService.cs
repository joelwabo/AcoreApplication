using AcoreApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Design
{
    class DesignProcessService : IProcessService
    {
        public ObservableCollection<Process> GetAllData()
        {
            ObservableCollection<Process> result = new ObservableCollection<Process>();
            Process process = new Process();
            process.Id = 1;
            process.Nom = "Cathonisation";
            process.IMax = 5000;
            process.UMax = 20;
            process.Inverseur = false;
            process.AH = true;
            process.Pulse = true;
            result.Add(process);
            return result;
        }
    }
}
