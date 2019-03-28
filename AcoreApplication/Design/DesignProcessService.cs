using AcoreApplication.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AcoreApplication.Model;

namespace AcoreApplication.Design
{
    class DesignProcessService : IProcessService
    {
        public bool Delete(Process process)
        {
            throw new NotImplementedException();
        }

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

        public bool Insert()
        {
            throw new NotImplementedException();
        }

        public bool Update(Process process)
        {
            throw new NotImplementedException();
        }
    }
}
