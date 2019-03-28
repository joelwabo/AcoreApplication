using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    public interface IRecetteService
    {
        bool Insert();
        bool Insert(Recette recette);
        bool Delete(Recette recette);
        bool Update(Recette recette);
    }
}
