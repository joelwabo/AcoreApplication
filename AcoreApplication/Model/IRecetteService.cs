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
        bool InsertRecette();
        bool DeleteRecette(Recette recette);
        bool UpdateRecette(Recette recette);
    }
}
