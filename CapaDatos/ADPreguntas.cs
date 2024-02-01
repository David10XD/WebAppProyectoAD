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
    public class ADPreguntas
    {
        #region InsertarModificarEliminarRespuestaUsuarios
        public static ERespuesta InsertarModificarEliminarRespuestaUsuarios(EPreguntas objPregunta)
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
               
                cmd = new SqlCommand("InsertarModificarEliminarRespuestaUsuarios", cnx);

                cmd.Parameters.AddWithValue("@IdProceso", objPregunta.IdProceso);
                cmd.Parameters.AddWithValue("@IdPreguntas", objPregunta.IdPreguntas);
                cmd.Parameters.AddWithValue("@IdDominio", objPregunta.IdDominio);
                cmd.Parameters.AddWithValue("@Server", objPregunta.Server);
                cmd.Parameters.AddWithValue("@Usuario", objPregunta.Usuario);
                cmd.Parameters.AddWithValue("@Contrasenia", objPregunta.Contrasenia);
                cmd.Parameters.AddWithValue("@Contenedor", objPregunta.Contenedor);
                cmd.Parameters.AddWithValue("@Respuestas", objPregunta.Respuestas);
                cmd.Parameters.AddWithValue("@Titulo", objPregunta.Titulo);
                cmd.Parameters.AddWithValue("@Email", objPregunta.Email);
                cmd.Parameters.AddWithValue("@NombreCompleto", objPregunta.NombreCompleto);
                cmd.Parameters.AddWithValue("@Compania", objPregunta.Compania);
                cmd.Parameters.AddWithValue("@Departamento", objPregunta.Departamento);
                cmd.Parameters.AddWithValue("@Telefono", objPregunta.Telefono);
                cmd.Parameters.AddWithValue("@Tipo", objPregunta.Tipo);
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

        #region InsertarModificarEliminarUsuariosPortal
        public static ERespuesta InsertarModificarEliminarUsuariosPortal(EUsuario usuario)
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

                cmd = new SqlCommand("InsertarModificarElminarUsuariosPortal", cnx);

                cmd.Parameters.AddWithValue("@IdUsuarioPortal", usuario.IdUsuarioPortal);
                cmd.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                cmd.Parameters.AddWithValue("@Identificacion", usuario.Identificacion);
                cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@Compania", usuario.Compania);
                cmd.Parameters.AddWithValue("@Departamento", usuario.Departamento);
                cmd.Parameters.AddWithValue("@Titulo", usuario.Titulo);
                cmd.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                cmd.Parameters.AddWithValue("@Portal", usuario.Portal);
                cmd.Parameters.AddWithValue("@Unidad", usuario.Unidad);
                cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                cmd.Parameters.AddWithValue("@Tipo", usuario.Tipo);
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
                    Respuesta.mensaje = "El usuario ya se encuentra registrado";
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

        #region InsertarModificarEliminarRegistroUO
        public static ERespuesta InsertarModificarEliminarRegistroUO(string JsonUO,int Estado,int Tipo)
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

                cmd = new SqlCommand("InsertarModificarEliminarRegistroUO", cnx);

                cmd.Parameters.AddWithValue("@JsonUO", JsonUO);
                cmd.Parameters.AddWithValue("@Estado", Estado);
                cmd.Parameters.AddWithValue("@Tipo", Tipo);
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
                    Respuesta.mensaje = "El usuario ya se encuentra registrado";
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

        #region InsertarModificarEliminarUnidad
        public static ERespuesta InsertarModificarEliminarUnidad(Int64 IdUO, int Estado, int Tipo)
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

                cmd = new SqlCommand("InsertarModificarEliminarUnidad", cnx);

                cmd.Parameters.AddWithValue("@IdUO", IdUO);
                cmd.Parameters.AddWithValue("@Estado", Estado);
                cmd.Parameters.AddWithValue("@Tipo", Tipo);
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
                    Respuesta.mensaje = "El usuario ya se encuentra registrado";
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

        #region ActualizarUsuario
        public static ERespuesta ActualizarUsuario(EPreguntas objPregunta)
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

                cmd = new SqlCommand("ActualizarUsuario", cnx);
                cmd.Parameters.AddWithValue("@Usuario", objPregunta.Usuario);
                cmd.Parameters.AddWithValue("@Contrasenia", objPregunta.Contrasenia);
                cmd.Parameters.AddWithValue("@Tipo", objPregunta.Tipo);
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

        #region MostrarPreguntas
        public static List<EPreguntas> MostrarPreguntas()
        {
            List<EPreguntas> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("MostrarPreguntas", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EPreguntas>();
                while (dr.Read())
                {
                    EPreguntas detUsuarios = new EPreguntas();
                    detUsuarios.IdPreguntas = Convert.ToInt32(dr["IdPreguntas"].ToString());
                    detUsuarios.Descripcion = Convert.ToString(dr["Descripcion"].ToString());
                  
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

        #region MostrarPreguntasCombo
        public static List<ECombos> MostrarPreguntasCombo()
        {
            List<ECombos> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("MostrarPreguntas", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<ECombos>();
                while (dr.Read())
                {
                    ECombos detUsuarios = new ECombos();
                    detUsuarios.Id = dr["IdPreguntas"].ToString();
                    detUsuarios.Valor = dr["Descripcion"].ToString();

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

        #region MostrarUnidadCombo
        public static List<ECombos> MostrarUnidadCombo(Int64 IdUO, int Tipo)
        {
            List<ECombos> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();
                cmd = new SqlCommand("ConsultarUnidad", cnx);
                cmd.Parameters.AddWithValue("@IdUO", IdUO);
                cmd.Parameters.AddWithValue("@Tipo", Tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<ECombos>();
                while (dr.Read())
                {
                    ECombos detUsuarios = new ECombos();
                    detUsuarios.Id = dr["id"].ToString();
                    detUsuarios.Valor = dr["Nombre"].ToString();

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

        #region ConsultarPreguntas
        public static List<EPreguntas> ConsultarPreguntas(string Usuario, int IdPreguntas,string Pregunta, int Tipo)
        {
            List<EPreguntas> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("ConsultarPreguntas", cnx);
                cmd.Parameters.AddWithValue("@Usuario", Usuario);
                cmd.Parameters.AddWithValue("@IdPreguntas", IdPreguntas);
                cmd.Parameters.AddWithValue("@Pregunta", Pregunta);
                cmd.Parameters.AddWithValue("@Tipo", Tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EPreguntas>();
                while (dr.Read())
                {
                    EPreguntas detUsuarios = new EPreguntas();
                    detUsuarios.IdPreguntas = Convert.ToInt32(dr["IdPreguntas"].ToString());
                    detUsuarios.Descripcion = Convert.ToString(dr["Descripcion"].ToString());
                    detUsuarios.Respuestas = Convert.ToString(dr["Respuestas"].ToString());
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

        #region ConsultarUnidad
        public static List<EListaOU> ConsultarUnidad(Int64 IdUO, int Tipo)
        {
            List<EListaOU> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("ConsultarUnidad", cnx);
                cmd.Parameters.AddWithValue("@IdUO", IdUO);
                cmd.Parameters.AddWithValue("@Tipo", Tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EListaOU>();
                while (dr.Read())
                {
                    EListaOU detUsuarios = new EListaOU();
                    detUsuarios.id = Convert.ToInt64(dr["id"].ToString());
                    detUsuarios.Nombre = Convert.ToString(dr["Nombre"].ToString());
                    detUsuarios.Unidad = Convert.ToString(dr["Unidad"].ToString());
                    detUsuarios.Estado = Convert.ToString(dr["Estado"].ToString());
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


        #region ConsultarUnidadNombre
        public static List<EListaOU> ConsultarUnidadNombre(string Nombre, int Tipo)
        {
            List<EListaOU> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("ConsultarUnidadNombre", cnx);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@Tipo", Tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EListaOU>();
                while (dr.Read())
                {
                    EListaOU detUsuarios = new EListaOU();
                    detUsuarios.id = Convert.ToInt64(dr["id"].ToString());
                    detUsuarios.Nombre = Convert.ToString(dr["Nombre"].ToString());
                    detUsuarios.Unidad = Convert.ToString(dr["Unidad"].ToString());
                    detUsuarios.Estado = Convert.ToString(dr["Estado"].ToString());
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

        #region RespuestaUsuario
        public static List<EPreguntas> RespuestaUsuario(EPreguntas objPregunta)
        {
            List<EPreguntas> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("RespuestaUsuario", cnx);
                cmd.Parameters.AddWithValue("@IdDominio", objPregunta.IdDominio);
                cmd.Parameters.AddWithValue("@Server", objPregunta.Server);
                cmd.Parameters.AddWithValue("@Usuario", objPregunta.Usuario);
                cmd.Parameters.AddWithValue("@Contrasenia", objPregunta.Contrasenia);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EPreguntas>();
                while (dr.Read())
                {
                    EPreguntas detUsuarios = new EPreguntas();
                    detUsuarios.IdProceso = Convert.ToInt32(dr["IdProceso"].ToString());
                    detUsuarios.IdPreguntas = Convert.ToInt32(dr["IdPreguntas"].ToString());
                    detUsuarios.Descripcion = Convert.ToString(dr["Descripcion"].ToString());
                    detUsuarios.Respuestas = Convert.ToString(dr["Respuestas"].ToString());
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

        #region MostrarEnvioMail
        public static List<EParametrosCorreo> MostrarEnvioMail(EParametrosCorreo parametrosCorreo)
        {
            List<EParametrosCorreo> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("MostrarEnvioMail", cnx);
                cmd.Parameters.AddWithValue("@tipo", parametrosCorreo.tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<EParametrosCorreo>();
                while (dr.Read())
                {
                    EParametrosCorreo detUsuarios = new EParametrosCorreo();
                    detUsuarios.smtpAddress = Convert.ToString(dr["smtpAddress"].ToString());
                    detUsuarios.emailFrom = Convert.ToString(dr["emailFrom"].ToString());
                    detUsuarios.password = Convert.ToString(dr["password"].ToString());
                    detUsuarios.portNumber = Convert.ToInt32(dr["portNumber"].ToString());
                    detUsuarios.enableSSL = Convert.ToBoolean(dr["enableSSL"].ToString());
                    detUsuarios.Mensaje = Convert.ToString(dr["Mensaje"].ToString());
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

        #region MostrarPreguntas
        public static List<User> ConsultaListaUsuarios()
        {
            List<User> listaUsuarios = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                CapaConexion.Conexion cn = new CapaConexion.Conexion();
                SqlConnection cnx = cn.conectar();
                cnx.Open();

                cmd = new SqlCommand("ConsultaListaUsuarios", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                listaUsuarios = new List<User>();
                while (dr.Read())
                {
                    User detUsuarios = new User();
                    detUsuarios.UserName = dr["Usuario"].ToString();
                    detUsuarios.JobTitle = dr["Titulo"].ToString();
                    detUsuarios.Email = dr["Email"].ToString();
                    detUsuarios.DisplayName = dr["NombreCompleto"].ToString();
                    detUsuarios.Company = dr["Compania"].ToString();
                    detUsuarios.Deparment = dr["Departamento"].ToString();
                    detUsuarios.Phone = dr["Telefono"].ToString();
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
