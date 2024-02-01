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

namespace CapaDatos
{
    public class ADValidar
    {
        #region ValidarRegistro
        public static List<EValidar> ValidarRegistro(EPreguntas objPregunta)
        {
            List<EValidar> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("ValidarRegistro", cnx);
                cmd.Parameters.AddWithValue("@IdDominio", objPregunta.IdDominio);
                cmd.Parameters.AddWithValue("@Server", objPregunta.Server);
                cmd.Parameters.AddWithValue("@Usuario", objPregunta.Usuario);
                cmd.Parameters.AddWithValue("@Contrasenia", objPregunta.Contrasenia);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EValidar>();
                while (dr.Read())
                {
                    EValidar detUsuarios = new EValidar();
                    detUsuarios.contador = Convert.ToInt32(dr["contador"].ToString());
                    detUsuarios.clave = Convert.ToString(dr["clave"].ToString());

                    listaUsuarios.Add(detUsuarios);
                }
            }
            catch (Exception ex)
            {
                listaUsuarios = null;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return listaUsuarios;
        }
        #endregion
    }
}
