using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Reflection;
using System.Globalization;
using System.Resources;
using System.Collections;
using System.Diagnostics;

namespace AcoreApplication.Resources.Lang
{
    public class Wrapper
    {


        private static ObjectDataProvider m_provider;

        
        public Wrapper()
        {
        }

        
        //devuelve una instancia nueva de nuestros recursos.
        public languages GetResourceInstance()
        {

            return new languages();
        }

        

        //Esta propiedad devuelve el ObjectDataProvider en uso.
        public static ObjectDataProvider ResourceProvider
        {
            get
            {
                if (m_provider == null)
                    m_provider = (ObjectDataProvider)App.Current.FindResource("LanguagesRes");
                return m_provider;
            }
        }

        //Este método cambia la cultura aplicada a los recursos y refresca la propiedad ResourceProvider.
        public static void ChangeCulture(CultureInfo culture)
        {
            Properties.Resources.Culture = culture;
            ResourceProvider.Refresh();
        }


        
    }
}
