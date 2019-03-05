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

            return result;
        }
    }
}
