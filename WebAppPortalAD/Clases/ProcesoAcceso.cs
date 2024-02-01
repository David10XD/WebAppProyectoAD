using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;

namespace WebAppPortalAD.Clases
{
    public class ProcesoAcceso
    {

        #region object ->ConsultarDatosUsuarios
        public string ConsultarDatosUsuarios(string pathDocumento)
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                <soapenv:Header/>
                    <soapenv:Body>
                        <tem:ExtraerDatosUsuario>
                            <!--Optional:-->
                            <tem:server>srvdc01</tem:server>
                            <!--Optional:-->
                            <tem:dominio>serviconta.int</tem:dominio>"+
                            "<!--Optional:-->" +
                            "<tem:usuario>pruebas</tem:usuario>" + 
                            "<!--Optional:-->" +
                            "<tem:password>password.1</tem:password>" +
                        "</tem:ExtraerDatosUsuario>" +
                    "</soapenv:Body>" +
                  "</soapenv:Envelope>");
            string soapResult = "";
            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                soapResult = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return soapResult;
        }                 
        public HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://172.16.0.102/ConsultarAD/ActualizarCuentaAD.svc?wsdl");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion

    }
}