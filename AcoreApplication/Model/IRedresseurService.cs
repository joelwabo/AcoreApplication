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
        bool InsertRedresseur();
        bool DeleteRedresseur(Redresseur redresseur);
        bool UpdateRedresseur(Redresseur redresseur);
    }
}
