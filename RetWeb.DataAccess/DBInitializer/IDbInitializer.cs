using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Cartopia.DataAccess.DBInitializer
{
    public interface IDbInitializer
    {
        void Initialize(); //This method will be responsible for creating an admin user and roles of the website
    }
}
