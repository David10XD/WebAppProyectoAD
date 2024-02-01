using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CapaEntidad;

namespace WebAppServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISerWebApp" en el código y en el archivo de configuración a la vez.

    public class Constants
    {
        // Ensures consistency in the namespace declarations across services
        public const string Namespace = "http://172.16.0.102/";

    }


    [ServiceContract]
    public interface ISerWebApp
    {
        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string CrearUsuario(string UsuarioIngreso, string ClaveIngreso, string Nombre1, string Nombre2, string Apellido1, string Apellido2, string Identificacion, string NombreUsuario, string Email, string Telefono, string Compania, string Departamento, string Titulo, string Contrasenia, string Portal, string Estado,string EstadoProceso);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ValidarCuenta(string UsuarioIngreso,string ClaveIngreso, string Usuario, string Clave);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string BloquerCuenta(string UsuarioIngreso, string ClaveIngreso, string Usuario, string Clave);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string DesbloquearCuenta(string UsuarioIngreso, string ClaveIngreso, string Usuario, string Clave);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ListaOU(string UsuarioIngreso, string ClaveIngreso, string Usuario, string Clave);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ListaUsuarios(string UsuarioIngreso, string ClaveIngreso, string Usuario, string Clave,string UO);

    }
}
