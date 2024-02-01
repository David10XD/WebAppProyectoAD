using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace CapaConexion
{
    public class Conexion
    {
        #region conectar
        public SqlConnection conectar()
        {
            SqlConnection cn = new SqlConnection();

            // DESARROLLO
            //cn.ConnectionString = "Data Source=AZ-ADDS02\\SQLEXPRESS; Initial Catalog=AD; User Id=sa; Password=CAfKsUBnD0s";
            cn.ConnectionString = "Data Source=CQUILUMBAQUIN\\SQLEXPRESS; Initial Catalog=ADServicios; User Id=sa; Password=Sql$erver2014";
            // PRODUCCION--ASERTIA-MDM-001\\SQLEXPRESS
            //cn.ConnectionString = "Data Source=ASERTIA-MDM-001\\SQLEXPRESS; Initial Catalog=AD; User Id=sa; Password=CAfKsUBnD0s";

            // PRODUCCION--AZ-ADDS01\SQLEXPRESS
            //cn.ConnectionString = "Data Source=AZ-ADDS01\\SQLEXPRESS; Initial Catalog=AD; User Id=sa; Password=CAfKsUBnD0s";

            //cn.ConnectionString = "Data Source=SRV-CBS-ERP\\SQLEXPRESS; Initial Catalog=AD; User Id=sa; Password=CAfKsUBnD0s";

            return cn;
        }
        #endregion
    }
}
