using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    public interface IOptionsService
    {
        ObservableCollection<DataService.Options> GetAllOptionsFromTableId(int id, string table);
    }
}
