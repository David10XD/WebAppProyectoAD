using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CapaEntidad;
using System.Globalization;
using System.IO;

namespace CapaDatos
{
   public class ADUsuarioAD
    {
        #region ValidarRegistro
        public static List<EUsuariosAD> GetConsultarUsuarioAD(Int64 IdUsuarioAd, int Tipo)
        {
            List<EUsuariosAD> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("ConsultarUsuarioAD", cnx);
                cmd.Parameters.AddWithValue("@IdUsuarioAd", IdUsuarioAd);
                cmd.Parameters.AddWithValue("@Tipo", Tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EUsuariosAD>();
                while (dr.Read())
                {
                    EUsuariosAD detUsuarios = new EUsuariosAD();
                    detUsuarios.IdUsuarioAd = Convert.ToInt64(dr["IdUsuarioAd"].ToString());
                    detUsuarios.Servidor = Convert.ToString(dr["Servidor"].ToString());
                    detUsuarios.Dominio = Convert.ToString(dr["Dominio"].ToString());
                    detUsuarios.Usuario = Convert.ToString(dr["Usuario"].ToString());
                    detUsuarios.Clave = Convert.ToString(dr["Clave"].ToString());
                    detUsuarios.TipoLogueo = Convert.ToInt32(dr["TipoLogueo"].ToString());
                    listaUsuarios.Add(detUsuarios);
                }
            }
            catch (Exception ex)
            {
                listaUsuarios = null;
                VerErrores("ex.Message-base de Datos: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", 1);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return listaUsuarios;
        }
        #endregion

        #region VerErrores
        public static void VerErrores(string valor, string Carpeta, string rucEmpresa, int tipo)
        {
            try
            {
                if (tipo == 1)
                {
                    string fecha;
                    fecha = DateTime.Now.ToString("dd-MM-yyyy");//DateTime.Now.ToShortDateString().Replace("/", "-");
                    if (!Directory.Exists(@"C:\\" + rucEmpresa + "\\" + Carpeta + "\\" + fecha))
                    {
                        Directory.CreateDirectory(@"C:\\" + rucEmpresa + "\\" + Carpeta + "\\" + fecha);
                    }

                    string path = @"C:\\" + rucEmpresa + "\\" + Carpeta + "\\" + fecha + "\\log.txt";
                    TextWriter tw = new StreamWriter(path, true);
                    tw.WriteLine("A fecha de : " + DateTime.Now.ToString() + ": " + valor);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Exception: " + ex.Message);
            }
        }
        #endregion
    }
}
