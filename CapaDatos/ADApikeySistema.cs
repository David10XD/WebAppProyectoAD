﻿using System;
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
    public class ADApikeySistema
    {
        #region InsertarModificarEliminarApikeySistema
        public static ERespuesta InsertarModificarEliminarApikeySistema(EApikeySistema apikeySistema)
        {
            DataTable dtResultados = new DataTable();
            ERespuesta Respuesta = new ERespuesta();
            String respuestaSP = "";
            SqlCommand cmd = null;
            SqlDataReader dr = null;

            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();

                cnx.Open();

                cmd = new SqlCommand("InsertarModificarEliminarApikeySistema", cnx);

                cmd.Parameters.AddWithValue("@Id", apikeySistema.Id);
                cmd.Parameters.AddWithValue("@Apikey", apikeySistema.Apikey);
                cmd.Parameters.AddWithValue("@Usuario", apikeySistema.Usuario);
                cmd.Parameters.AddWithValue("@Tipo", apikeySistema.Tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                dtResultados.Load(dr);



                if (Convert.ToInt32(dtResultados.Rows[0][1].ToString()) == 1)
                {
                    Respuesta.estado = "1";
                    Respuesta.mensaje = "Datos Guardados con Exito.";
                    Respuesta.tipoMensaje = "success";
                    Respuesta.resultado = respuestaSP.ToString();
                }
                else if (Convert.ToInt32(dtResultados.Rows[0][1].ToString()) == 2)
                {
                    Respuesta.estado = "1";
                    Respuesta.mensaje = "Datos Actualizado con Exito.";
                    Respuesta.tipoMensaje = "success";
                    Respuesta.resultado = respuestaSP.ToString();
                }
                else if (Convert.ToInt32(dtResultados.Rows[0][1].ToString()) == 4)
                {
                    Respuesta.estado = "4";
                    Respuesta.mensaje = "El email o nombre usuario ya existe.;4";
                    Respuesta.tipoMensaje = "success";
                    Respuesta.resultado = respuestaSP.ToString();
                }
                else if (Convert.ToInt32(dtResultados.Rows[0][1].ToString()) == -1)
                {
                    Respuesta.estado = "-1";
                    Respuesta.mensaje = "No se registro el usuario por favor validar";
                    Respuesta.tipoMensaje = "danger";
                    Respuesta.resultado = respuestaSP.ToString();
                }
                else
                {
                    Respuesta.estado = respuestaSP.ToString();
                    Respuesta.mensaje = "Ocurrio un error al guardar los datos.";
                    Respuesta.tipoMensaje = "danger";
                }

            }
            catch (Exception ex)
            {
                Respuesta.estado = "0";
                Respuesta.mensaje = ex.Message.ToString();
                Respuesta.tipoMensaje = "danger";
            }
            finally
            {
                cmd.Connection.Close();
            }


            return Respuesta;

        }
        #endregion
    }
}
