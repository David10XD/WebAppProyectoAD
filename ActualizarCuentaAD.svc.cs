using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Windows.Forms;

using System.DirectoryServices;  //Hay que añadirlo en References
using System.DirectoryServices.AccountManagement;
using Newtonsoft.Json;

namespace WebAppProyectoAD
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ActualizarCuentaAD" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ActualizarCuentaAD.svc o ActualizarCuentaAD.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ActualizarCuentaAD : IActualizarCuentaAD
    {
        #region ExtraerDatosUsuario
        public string ExtraerDatosUsuario(string server, string dominio, string usuario, string password)
        {

            string Resultado = "";
            try
            {
                //Cursor = Cursors.WaitCursor;

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                //Por ejemplo: 'LDAP://ejemplo.lan.es' o  'LDAP://NOMBRE_SERVIDOR'
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                //string propiedad = t5.Text.Trim();

                // Crea el objeto de conexión LDAP
                DirectoryEntry myLdapConnection = createDirectoryEntry(path, domUsu, pass);

                // Crea el objeto search que operará con la conexión LDAP y dónde expecificamos
                // la búsqueda que queremos hacer añadiendo un filtro

                DirectorySearcher search = new DirectorySearcher(myLdapConnection);
                search.Filter = "(samaccountname=" + username + ")";

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    // Si existe el usuario, mostramos las propiedades que ha recuperado
                    ResultPropertyCollection fields = result.Properties;

                    foreach (String ldapField in fields.PropertyNames)
                    {
                        foreach (Object myCollection in fields[ldapField])
                            //Resultado += Environment.NewLine + (String.Format("{0,-20} : {1}", ldapField, myCollection.ToString()));
                            Resultado += Environment.NewLine + (String.Format("{0,-20} : {1}", "<tr><td>" + ldapField + "</td>", "<td>" + myCollection.ToString() + "</td></tr>"));
                    }
                }

                else
                {
                    // Si no encuentra el usuario
                    return "Usuario no encontrado!";
                }
            }
            catch (Exception ex)
            {
                return "Exception caught:\n\n" + ex.ToString();
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }

            return Resultado;
        }
        #endregion

        #region CambiarContrasenia
        public string CambiarContrasenia(string server, string dominio, string usuario, string password, string Nuevopassword, string Confirpassword)
        {

            string Resultado = "";

            try
            {

                if (Nuevopassword.Trim() != Confirpassword.Trim())
                {
                    return "Las contraseñas no coinciden";
                }
                //else
                //{
                //    Resultado = ChangeMyPassword(server, dominio, usuario, password, Nuevopassword);
                //}

                #region proceso
                string path = @"LDAP://" + server.Trim();            //CAMBIAR POR VUESTRO PATH (URL DEL SERVICIO DE DIRECTORIO LDAP)
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string admUsu = usuario.Trim();           //USUARIO DEL DOMINIO
                string admPass = password.Trim();               //PASSWORD DEL USUARIO
                string domUsu = dominio + @"\" + admUsu;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                //string propiedad = t5.Text.Trim();
                string usu = usuario.Trim();
                string oldpass = password.Trim();
                string newpass = Nuevopassword.Trim();
                string cErr = "";

                AD ad = new AD();
                DirectoryEntry de = ad.GetUser(path, admUsu, admPass, usu, ref cErr);
                if (cErr != "")
                    return cErr;

                DirectoryEntry oDE;
                oDE = new DirectoryEntry(de.Path, usu, oldpass, AuthenticationTypes.Secure);

                try
                {
                    // Cambia la contraseña
                    oDE.Invoke("ChangePassword", new object[] { oldpass, newpass });
                    Resultado = "Contraseña actualizada.";
                }
                catch (Exception ex)
                {
                    return "Error al intentar cambiar la contraseña" + Environment.NewLine + Environment.NewLine + ex.Message;
                }
                #endregion

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {

            }

            return Resultado;
        }
        #endregion

        #region DesbloquearUsuario
        public string DesbloquearUsuario(string server, string dominio, string usuario, string password)
        {

            string Resultado = "";

            try
            {

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                //Por ejemplo: 'LDAP://ejemplo.lan.es' o  'LDAP://NOMBRE_SERVIDOR'
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR


                DirectoryEntry entry = new DirectoryEntry(path, username, pass);

                DirectorySearcher search = new DirectorySearcher(entry) { Filter = "(sAMAccountName=" + username + ")" };
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("pwdLastSet");
                try
                {
                    SearchResult result = search.FindOne();
                    if (result == null)
                    {
                        Resultado = "No hay respuesta: ";
                    }
                    else
                    {
                        Int64 pwdLastSetValue = (Int64)result.Properties["pwdLastSet"][0];

                        Resultado = "Cuenta desbloqueda Correctamente..";
                    }

                }
                catch (Exception ex)
                {
                    Resultado = ex.Message;
                }

            }

            catch (Exception ex)
            {
                Resultado = ex.Message;
            }
            finally
            {

            }

            return Resultado;
        }
        #endregion

        #region ValidarCuenta
        public string ValidarCuenta(string server, string dominio, string usuario, string password)
        {

            string Resultado = "";

            try
            {

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                //Por ejemplo: 'LDAP://ejemplo.lan.es' o  'LDAP://NOMBRE_SERVIDOR'
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR


                DirectoryEntry entry = new DirectoryEntry(path, username, pass);

                try
                {
                    entry.RefreshCache(); // This will force credentials validation
                    Resultado = "Existe la cuenta";
                }
                catch (Exception ex)
                {
                    // Validation failed - handle how you want
                    Resultado = ex.Message;
                }

            }

            catch (Exception ex)
            {
                Resultado = ex.Message;
            }
            finally
            {

            }

            return Resultado;
        }
        #endregion

        #region FechaCaducidad
        public string FechaCaducidad(string server, string dominio, string usuario, string password)
        {

            string Resultado = "";

            try
            {

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                //Por ejemplo: 'LDAP://ejemplo.lan.es' o  'LDAP://NOMBRE_SERVIDOR'
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR

                try
                {
                    using (var userEntry = new DirectoryEntry("WinNT://" + dominio + '/' + username + ",user"))
                    {
                        DateTime fecha = (DateTime)userEntry.InvokeGet("PasswordExpirationDate");
                        Resultado = fecha.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Resultado = ex.Message;
                }

            }
            catch (Exception ex)
            {
                Resultado = ex.Message;
            }
            finally
            {

            }

            return Resultado;
        }
        #endregion

        #region createDirectoryEntry
        static DirectoryEntry createDirectoryEntry(String path, String user, String pass)
        {
            DirectoryEntry ldapConnection = new DirectoryEntry(path, user, pass, AuthenticationTypes.Secure);

            return ldapConnection;
        }
        #endregion

        #region ChangeMyPassword
        public string ChangeMyPassword(string server, string domainName, string userName, string currentPassword, string newPassword)
        {
            string Resultado = "";
            try
            {
                string ldapPath = server;
                DirectoryEntry directionEntry = new DirectoryEntry(ldapPath, domainName + "\\" + userName, currentPassword);
                if (directionEntry != null)

                {
                    DirectorySearcher search = new DirectorySearcher(directionEntry);
                    search.Filter = "(SAMAccountName=" + userName + ")";
                    SearchResult result = search.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry userEntry = result.GetDirectoryEntry();
                        if (userEntry != null)
                        {
                            userEntry.Invoke("ChangePassword", new object[] { currentPassword, newPassword });
                            userEntry.CommitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return "Error al intentar cambiar la contraseña" + Environment.NewLine + Environment.NewLine + ex.Message;
            }

            return Resultado;
        }
        #endregion

        #region GenerarUsuarios
        public string GenerarUsuarios(string server, string dominio, string usuario, string password, string datosUsuarios,string Carpeta)
        {
            string Respuesta = "";

            try
            {
                string[] cadena = datosUsuarios.Split(';');

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                string pathFinal = Carpeta;//"LDAP://OU=CBS,DC=cbs,DC=local";

                DirectoryEntry adUserFolder = new DirectoryEntry(pathFinal, username, pass);
                DirectoryEntry newUser = adUserFolder.Children.Add("CN=" + cadena[1].ToString() + " " + cadena[2].ToString(), "User");

                if (DirectoryEntry.Exists(newUser.Path))
                {
                    Respuesta = "El usuario ya existe";
                }
                else
                {
                    if(cadena[5].ToString().Length >=9)
                    {
                        newUser.Properties["samAccountName"].Value = cadena[0].ToString();// "gacampuzano";// accountname.Text;
                        newUser.Properties["givenName"].Value = cadena[1].ToString();// "Guillermo Armando";// firstname.Text;
                        newUser.Properties["sn"].Value = cadena[2].ToString();// "Campuzano Arechua";// lastname.Text;
                        newUser.Properties["cn"].Value = cadena[1].ToString() + " " + cadena[2].ToString();// "Campuzano Arechua";// lastname.Text;
                        //newUser.Properties["initials"].Value = initials.Text;
                        //newUser.Properties["siplayName"].Value = displayname.Text;
                        //newUser.Properties["physicalDeliveryOfficeName"].Value = officename.Text;
                        newUser.Properties["telephoneNumber"].Value = cadena[3].ToString();//  "0989835824"; //phone.Text;
                        newUser.Properties["mail"].Value = cadena[4].ToString();// "guillermoarma@hotmail.com";// Email.Text;

                        newUser.Properties["Company"].Value = cadena[6].ToString();// Email.Text;
                        newUser.Properties["department"].Value = cadena[7].ToString();// Email.Text;
                        newUser.Properties["title"].Value = cadena[8].ToString();// Email.Text;
                        newUser.Properties["userPrincipalName"].Value = cadena[0].ToString();
                        newUser.CommitChanges();
                        newUser.Invoke("setpassword", cadena[5].ToString());
                        newUser.Properties["userAccountControl"].Value = 512;
                        newUser.CommitChanges();
                        Respuesta = "Usuario registrado correctamente..";
                    }
                    else
                    {
                        Respuesta = "La clave debe tener al menos 9 caracteres..";
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }

            return Respuesta;
        }
        #endregion

        #region ActualizarUsuarios
        public string ActualizarUsuarios(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta)
        {
            string Respuesta = "";

            try
            {
                string[] cadena = datosUsuarios.Split(';');

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                string pathFinal = Carpeta;//"LDAP://OU=CBS,DC=cbs,DC=local";

                var dsDirectoryEntry = new DirectoryEntry(pathFinal, username, pass);
                var dsSearch = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(SAMAccountName=" + cadena[0].ToString() + "))" };

                var dsResults = dsSearch.FindOne();
                if (dsResults == null)
                {
                    Respuesta = "El usuario no existe";
                }
                else
                {
                    var myEntry = dsResults.GetDirectoryEntry();
                    myEntry.Properties["Company"].Value = cadena[1].ToString();// Email.Text;
                    myEntry.Properties["department"].Value = cadena[2].ToString();// Email.Text;
                    myEntry.Properties["title"].Value = cadena[3].ToString();// Email.Text;
                    myEntry.Properties["mail"].Value = cadena[4].ToString();// Email.Text;
                    myEntry.Properties["telephoneNumber"].Value = cadena[5].ToString();

                    myEntry.CommitChanges();
                    Respuesta = "Usuario actualizado correctamente..";
                }
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }

            return Respuesta;
        }
        #endregion

        #region DetalleUsuario
        public string DetalleUsuario(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta)
        {
            string Respuesta = "";
            List<User> rst = new List<User>();
            try
            {
                string[] cadena = datosUsuarios.Split(';');

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                string pathFinal = Carpeta;//"LDAP://OU=CBS,DC=cbs,DC=local";

                DirectoryEntry adSearchRoot = new DirectoryEntry(path, username, pass);
                DirectorySearcher adSearcher = new DirectorySearcher(adSearchRoot);

                adSearcher.Filter = "(&(objectClass=user)(objectCategory=person))";
                adSearcher.PropertiesToLoad.Add("samaccountname");
                adSearcher.PropertiesToLoad.Add("title");
                adSearcher.PropertiesToLoad.Add("mail");
                adSearcher.PropertiesToLoad.Add("usergroup");
                adSearcher.PropertiesToLoad.Add("company");
                adSearcher.PropertiesToLoad.Add("department");
                adSearcher.PropertiesToLoad.Add("telephoneNumber");
                adSearcher.PropertiesToLoad.Add("mobile");
                adSearcher.PropertiesToLoad.Add("displayname");
                SearchResult result;
                SearchResultCollection iResult = adSearcher.FindAll();

                User item;
                if (iResult != null)
                {
                    for (int counter = 0; counter < iResult.Count; counter++)
                    {
                        result = iResult[counter];
                        if (result.Properties.Contains("samaccountname"))
                        {
                            item = new User();

                            item.UserName = (String)result.Properties["samaccountname"][0];

                            if (result.Properties.Contains("displayname"))
                            {
                                item.DisplayName = (String)result.Properties["displayname"][0];
                            }

                            if (result.Properties.Contains("mail"))
                            {
                                item.Email = (String)result.Properties["mail"][0];
                            }

                            if (result.Properties.Contains("company"))
                            {
                                item.Company = (String)result.Properties["company"][0];
                            }

                            if (result.Properties.Contains("title"))
                            {
                                item.JobTitle = (String)result.Properties["title"][0];
                            }

                            if (result.Properties.Contains("department"))
                            {
                                item.Deparment = (String)result.Properties["department"][0];
                            }

                            if (result.Properties.Contains("telephoneNumber"))
                            {
                                item.Phone = (String)result.Properties["telephoneNumber"][0];
                            }

                            if (result.Properties.Contains("mobile"))
                            {
                                item.Mobile = (String)result.Properties["mobile"][0];
                            }
                            rst.Add(item);
                        }
                    }

                    Respuesta = JsonConvert.SerializeObject(rst, Formatting.Indented);

                }

                adSearcher.Dispose();
                adSearchRoot.Dispose();

            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }

            return Respuesta;
        }
        #endregion


        #region ListaUsuarios
        public string ListaUsuarios()
        {
            string Respuesta = "";
            try
            {
                AD ad = new AD();
                PrincipalContext contexto = new PrincipalContext(ContextType.Machine);
                UserPrincipal insUserPrincipal = new UserPrincipal(contexto);
                insUserPrincipal.Name = "*";
                PrincipalSearchResult<Principal> results = ad.buscaUsu(insUserPrincipal);
                foreach (Principal p in results)
                {
                    //datos.Items.Add(p);
                    Respuesta += p + ";";
                }

            }
            catch(Exception ex)
            {
                Respuesta = ex.Message;
            }
            return Respuesta;
        }
        #endregion

    }
}
