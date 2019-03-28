using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    public interface IRedresseurService
    {
        ObservableCollection<Redresseur> GetAllData();
        bool Insert();
        bool Delete(int id);
        bool Delete(Redresseur redresseur);
        bool Update(Redresseur redresseur);
    }
}
