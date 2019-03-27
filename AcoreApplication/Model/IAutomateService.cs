using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcoreApplication.DataService;

namespace AcoreApplication.Model
{
    public interface IAutomateService
    {
        ObservableCollection<Automate> GetAllData();
    }
}
