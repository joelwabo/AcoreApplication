using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AcoreApplication.Model
{
    public interface IHistoriqueService
    {
        ObservableCollection<Historique> GetAllData();
    }
}
