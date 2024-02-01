using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocio
{
   public class NPreguntas
    {
        #region InsertarModificarEliminarRespuestaUsuarios
        public static ERespuesta InsertarModificarEliminarRespuestaUsuarios(EPreguntas objPregunta)
        {
            ERespuesta respuesta = new ERespuesta();

            respuesta = ADPreguntas.InsertarModificarEliminarRespuestaUsuarios(objPregunta);

            return respuesta;
        }
        #endregion

        #region InsertarModificarEliminarUsuariosPortal
        public static ERespuesta InsertarModificarEliminarUsuariosPortal(EUsuario usuario)
        {
            ERespuesta respuesta = new ERespuesta();

            respuesta = ADPreguntas.InsertarModificarEliminarUsuariosPortal(usuario);

            return respuesta;
        }
        #endregion

        #region InsertarModificarEliminarRegistroUO
        public static ERespuesta InsertarModificarEliminarRegistroUO(string JsonUO, int Estado, int Tipo)
        {
            ERespuesta respuesta = new ERespuesta();

            respuesta = ADPreguntas.InsertarModificarEliminarRegistroUO(JsonUO, Estado, Tipo);

            return respuesta;
        }
        #endregion

        #region InsertarModificarEliminarUnidad
        public static ERespuesta InsertarModificarEliminarUnidad(Int64 IdUO, int Estado, int Tipo)
        {
            ERespuesta respuesta = new ERespuesta();

            respuesta = ADPreguntas.InsertarModificarEliminarUnidad(IdUO, Estado, Tipo);

            return respuesta;
        }
        #endregion

        #region ActualizarUsuario
        public static ERespuesta ActualizarUsuario(EPreguntas objPregunta)
        {
            ERespuesta respuesta = new ERespuesta();

            respuesta = ADPreguntas.ActualizarUsuario(objPregunta);

            return respuesta;
        }
        #endregion

        #region MostrarPreguntas
        public static List<EPreguntas> MostrarPreguntas()
        {
            return ADPreguntas.MostrarPreguntas();
        }
        #endregion

        #region ConsultaListaUsuarios
        public static List<User> ConsultaListaUsuarios()
        {
            return ADPreguntas.ConsultaListaUsuarios();
        }
        #endregion

        #region ConsultarUnidad
        public static List<EListaOU> ConsultarUnidad(Int64 IdUO, int Tipo)
        {
            return ADPreguntas.ConsultarUnidad(IdUO, Tipo);
        }
        #endregion

        #region ConsultarUnidadNombre
        public static List<EListaOU> ConsultarUnidadNombre(string Nombre, int Tipo)
        {
            return ADPreguntas.ConsultarUnidadNombre(Nombre, Tipo);
        }
        #endregion

        #region MostrarPreguntasCombo
        public static List<ECombos> MostrarPreguntasCombo()
        {
            return ADPreguntas.MostrarPreguntasCombo();
        }
        #endregion

        #region MostrarUnidadCombo
        public static List<ECombos> MostrarUnidadCombo(Int64 IdUO, int Tipo)
        {
            return ADPreguntas.MostrarUnidadCombo(IdUO, Tipo);
        }
        #endregion

        #region MostrarPreguntas
        public static List<EPreguntas> ConsultarPreguntas(string Usuario, int IdPreguntas,string Pregunta, int Tipo)
        {
            return ADPreguntas.ConsultarPreguntas(Usuario, IdPreguntas, Pregunta, Tipo);
        }
        #endregion

        #region RespuestaUsuario
        public static List<EPreguntas> RespuestaUsuario(EPreguntas objPregunta)
        {
            return ADPreguntas.RespuestaUsuario(objPregunta);
        }
        #endregion

        #region MostrarEnvioMail
        public static List<EParametrosCorreo> MostrarEnvioMail(EParametrosCorreo parametrosCorreo)
        {
            return ADPreguntas.MostrarEnvioMail(parametrosCorreo);
        }
        #endregion
    }
}
