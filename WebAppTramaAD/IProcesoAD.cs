using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebAppTramaAD
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDispatcherEngine" in both code and config file together.
    public class Constants
    {
        // Ensures consistency in the namespace declarations across services
        public const string Namespace = "http://172.16.0.102/";

    }

    [ServiceContract(Namespace = Constants.Namespace, Name = "ProcesoAD")]
    public interface IProcesoAD
    {
        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string CrearUsuarios(string datosUsuarios);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ActualizarUsuarios(string datosUsuarios);

        [XmlSerializerFormat(Style = OperationFormatStyle.Document), OperationContract]
        string ListaUsuarios();
    }
}
