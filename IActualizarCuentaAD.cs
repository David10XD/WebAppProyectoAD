using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebAppProyectoAD
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDispatcherEngine" in both code and config file together.
    public class Constants
    {
        // Ensures consistency in the namespace declarations across services
        public const string Namespace = "http://172.16.0.102/";

    }

    [ServiceContract(Namespace = Constants.Namespace, Name = "ActualizarCuentaAD")]
    public interface IActualizarCuentaAD
    {
        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ExtraerDatosUsuario(string server, string dominio,string usuario, string password);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string CambiarContrasenia(string server, string dominio, string usuario, string password, string Nuevopassword, string Confirpassword);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string DesbloquearUsuario(string server, string dominio, string usuario, string password);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ValidarCuenta(string server, string dominio, string usuario, string password);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string FechaCaducidad(string server, string dominio, string usuario, string password);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string GenerarUsuarios(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string DetalleUsuario(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta);


        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ListaUsuarios();
    }
}
