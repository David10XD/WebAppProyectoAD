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

using System.IO;

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
        public string CambiarContrasenia(string server, string dominio, string usuario, string password, string Nuevopassword, string Confirpassword, int EstadoProceso)
        {

            string Resultado = "";

            try
            {

                //VerErrores("usuario: " + usuario, "PruebasSistemaWEB", "PruebasSistemaWEB");
                //VerErrores("dominio: " + dominio, "PruebasSistemaWEB", "PruebasSistemaWEB");
                //VerErrores("password: " + password, "PruebasSistemaWEB", "PruebasSistemaWEB");
                //VerErrores("Nuevopassword: " + Nuevopassword, "PruebasSistemaWEB", "PruebasSistemaWEB");
                //VerErrores("Confirpassword: " + Confirpassword, "PruebasSistemaWEB", "PruebasSistemaWEB");

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
               
                DirectoryEntry account = ad.GetUser(path, admUsu, admPass, usu, ref cErr, EstadoProceso);
                try
                {
                    DirectorySearcher search = new DirectorySearcher(account);

                    if (EstadoProceso == 1)//userPrincipalName
                    {
                        
                        search.Filter = "(&(objectClass=user)(userPrincipalName=" + usu + "))";
                    }
                    else if (EstadoProceso == 2)//sAMAccountName
                    {   
                        search.Filter = "(&(objectClass=user)(sAMAccountName=" + usu + "))";
                    }

                    account = search.FindOne().GetDirectoryEntry();
                    VerErrores("paso 1 C: " + newpass, "PruebasSistemaWEB", "PruebasSistemaWEB");
                    account.Invoke("SetPassword", newpass);
                    account.Properties["LockOutTime"].Value = 0;
                    account.CommitChanges();
                    account.RefreshCache();
                    VerErrores("paso 2 C: " + newpass, "PruebasSistemaWEB", "PruebasSistemaWEB");
                    //string quotePwd = String.Format(@"""{0}""", newpass);
                    //byte[] pwdBin = System.Text.Encoding.Unicode.GetBytes(quotePwd);

                    //string oc = System.Convert.ToBase64String(pwdBin);

                    //account.Properties["unicodePwd"].Clear();
                    //account.Properties["unicodePwd"].Add(oc);
                    //account.CommitChanges();

                    Resultado = "Contraseña actualizada.";
                }
                catch (Exception ex)
                {
                    VerErrores("ex.Message: " + ex.Message.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                    return "Error al intentar cambiar la contraseña" + Environment.NewLine + Environment.NewLine + ex.Message;
                }

                //AD ad = new AD();
                //DirectoryEntry de = ad.GetUser(path, admUsu, admPass, usu, ref cErr);
                //if (cErr != "")
                //    return cErr;

                //DirectoryEntry oDE;
                //oDE = new DirectoryEntry(de.Path, usu, oldpass, AuthenticationTypes.Secure);

                //try
                //{
                //    // Cambia la contraseña
                //    oDE.Invoke("ChangePassword", new object[] { oldpass, newpass });
                //    Resultado = "Contraseña actualizada.";
                //}
                //catch (Exception ex)
                //{
                //    return "Error al intentar cambiar la contraseña" + Environment.NewLine + Environment.NewLine + ex.Message;
                //}
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
        public string DesbloquearUsuario(string server, string dominio, string usuario, string password, string Carpeta,string datosUsuarios)
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
                //DirectoryEntry entry = new DirectoryEntry(path, username, pass);
                string pathFinal = Carpeta;
                var dsDirectoryEntry = new DirectoryEntry(pathFinal, username, pass);
                var dsSearch = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(SAMAccountName=" + datosUsuarios + "))" };

                var dsResults = dsSearch.FindOne();
                var myEntry = dsResults.GetDirectoryEntry();
                int val = (int)myEntry.Properties["userAccountControl"].Value;
                string displayname = (string)myEntry.Properties["displayname"].Value;

                try
                {
                    if (val == 512)
                    {
                        myEntry.Properties["userAccountControl"].Value = 514;
                        myEntry.CommitChanges();
                        Resultado = "Cuenta bloqueda Correctamente..";
                    }
                    else if (val == 514 || val == 546)
                    {
                        myEntry.Properties["userAccountControl"].Value = 512;
                        myEntry.CommitChanges();
                        Resultado = "Cuenta desbloqueda Correctamente..";
                    }
                }
                catch (Exception ex)
                {
                    Resultado = ex.Message.ToString();
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
        public string ValidarCuenta(string server, string dominio, string usuario, string password, string Carpeta, string datosUsuarios, int EstadoProceso)
        {

            string Resultado = "";
            string[] cadena = datosUsuarios.Split(';');
            string CambiarClaveAlInicio = "";
            try
            {

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                //Por ejemplo: 'LDAP://ejemplo.lan.es' o  'LDAP://NOMBRE_SERVIDOR'
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR

                string pathFinal = Carpeta;
                var dsDirectoryEntry = new DirectoryEntry(path, username, pass);
                DirectorySearcher search = new DirectorySearcher();              
                if (EstadoProceso == 1)//userPrincipalName
                {
                    search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(userPrincipalName=" + cadena[0] + "))" };
                }
                else if (EstadoProceso == 2)//sAMAccountName
                {
                    search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(SAMAccountName=" + cadena[0] + "))" };
                }

                var dsResults = search.FindOne();
                string useraccountcontrol = "";
                ResultPropertyCollection fields = dsResults.Properties;
                foreach (String ldapField in fields.PropertyNames)
                {
                    if (ldapField == "pwdlastset")
                    {
                        foreach (Object myCollection in fields[ldapField])
                        {
                            CambiarClaveAlInicio = myCollection.ToString();
                        }
                    }
                    else if (ldapField == "useraccountcontrol")
                    {
                        foreach (Object myCollection in fields[ldapField])
                        {
                            useraccountcontrol = myCollection.ToString();
                        }
                    }
                }

                try
                {
                    VerErrores("path: "+ path, "PruebasSistemaWEB", "PruebasSistemaWEB");
                    //VerErrores("cadena[0]"+ cadena[0], "PruebasSistemaWEB", "PruebasSistemaWEB");
                    //VerErrores("cadena[1]"+ cadena[1], "PruebasSistemaWEB", "PruebasSistemaWEB");
                    using (DirectoryEntry adsEntry = new DirectoryEntry(path, cadena[0], cadena[1]))
                    {
                        using (DirectorySearcher adsSearcher = new DirectorySearcher(adsEntry))
                        {
                            VerErrores("PASO1 : ", "PruebasSistemaWEB", "PruebasSistemaWEB");
                            if (EstadoProceso == 1)//userPrincipalName
                            {

                                adsSearcher.Filter = "(userPrincipalName=" + cadena[0] + ")";
                            }
                            else if (EstadoProceso == 2)//sAMAccountName
                            {
                                adsSearcher.Filter = "(sAMAccountName=" + cadena[0] + ")";
                            }

                            try
                            {
                                SearchResult adsSearchResult = adsSearcher.FindOne();
                                VerErrores("PASO2 : ", "PruebasSistemaWEB", "PruebasSistemaWEB");
                                if (adsSearchResult.Properties["cn"].Count > 0)
                                {
                                    if (dsResults != null)
                                    {
                                        VerErrores("PASO3 : ", "PruebasSistemaWEB", "PruebasSistemaWEB");

                                        var myEntry = dsResults.GetDirectoryEntry();
                                        int val = (int)myEntry.Properties["userAccountControl"].Value;
                                        VerErrores("val : "+ val.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                        int contador = 0;
                                        if (myEntry.Properties["badPwdCount"].Value == null)
                                        {
                                            contador = 0;
                                        }
                                        else
                                        {
                                            contador = (int)myEntry.Properties["badPwdCount"].Value;
                                        }

                                        VerErrores("contador : " + contador.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");

                                        string displayname = (string)myEntry.Properties["cn"].Value;
                                       
                                        VerErrores("displayname : " + displayname.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");

                                        Resultado += " Existe el usuario;";
                                        Resultado += val.ToString() + ";";
                                        if (displayname == null)
                                        {
                                            Resultado += ";";
                                        }
                                        else
                                        {
                                            Resultado += displayname.ToString() + ";";
                                        }

                                        VerErrores("Resultado : " + Resultado.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");

                                        VerErrores("path : " + Resultado.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                        //VerErrores("cadena[0] : " + cadena[0].ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                        //VerErrores("cadena[1] : " + cadena[1].ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");

                                        DirectoryEntry entry = new DirectoryEntry(path, cadena[0], cadena[1]);

                                        try
                                        {
                                            entry.RefreshCache(); // This will force credentials validation
                                            Resultado += "Existe la cuenta" + ";";
                                        }
                                        catch (Exception ex)
                                        {
                                            //Validation failed -handle how you want
                                            Resultado += ex.Message + ";";
                                        }

                                        #region Validar Usuario Administrador
                                        object[] displayname1 = null;
                                        string displaynames = "";
                                        string resultado = "";
                                        int Admin = 0;
                                        string grupoPertenece = "";
                                        string distinguishedName = (string)myEntry.Properties["distinguishedName"].Value;
                                        VerErrores("distinguishedName : " + distinguishedName.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                        string[] cadena2 = distinguishedName.Split(',');
                                        grupoPertenece = cadena2[1].ToString();
                                        VerErrores("grupoPertenece : " + grupoPertenece.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");

                                        VerErrores("myEntry.Properties[memberof].Count : " + myEntry.Properties["memberof"].Count.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");

                                        if (myEntry.Properties["memberof"].Count == 1)
                                        {
                                            displaynames = (string)myEntry.Properties["memberof"].Value;
                                            resultado = Convert.ToString(displaynames);
                                            string[] cadena3 = resultado.Split(',');

                                            VerErrores("cadena3[0].ToString(1) : " + cadena3[0].ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");

                                            if (cadena3[0].ToString() == "CN=Domain Admins" || cadena3[0].ToString() == "CN=Usuarios del dominio" || cadena3[0].ToString() == "CN=Administrators" || cadena3[0].ToString() == "CN=Administradores")
                                            {
                                                Admin = 1;
                                                Resultado += Admin.ToString() + ";" + grupoPertenece + "↨" + distinguishedName + ";" + contador.ToString();
                                            }
                                            else
                                            {
                                                Resultado += Admin.ToString() + ";" + grupoPertenece + "↨" + distinguishedName + ";" + contador.ToString();
                                            }
                                        }
                                        else
                                        {
                                            displayname1 = (object[])myEntry.Properties["memberof"].Value;
                                            if (displayname1 == null)
                                            {
                                                Resultado += Admin.ToString() + ";" + grupoPertenece + "↨" + distinguishedName + ";" + contador.ToString();
                                            }
                                            else
                                            {
                                                VerErrores("Admin : " + Admin.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                                try
                                                {
                                                    for (int i = 0; i < displayname1.ToString().Length; i++)
                                                    {
                                                        resultado = Convert.ToString(displayname1[i]);
                                                        VerErrores("resultado : " + resultado.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                                        string[] cadena3 = resultado.Split(',');
                                                        VerErrores("cadena3[0].ToString(2) : " + cadena3[0].ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                                        if (cadena3[0].ToString() == "CN=Domain Admins" || cadena3[0].ToString() == "CN=Usuarios del dominio" || cadena3[0].ToString() == "CN=Administrators" || cadena3[0].ToString() == "CN=Administradores")
                                                        {
                                                            Admin = 1;
                                                            break;
                                                        }
                                                    }
                                                }
                                                catch(Exception ex)
                                                {
                                                    VerErrores("ex.Message : " + ex.Message.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                                }

                                                VerErrores("Admin : " + Admin.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                                if (Admin == 1)
                                                {
                                                    Resultado += Admin.ToString() + ";" + grupoPertenece + "↨" + distinguishedName + ";" + contador.ToString();
                                                }
                                                else
                                                {
                                                    Resultado += Admin.ToString() + ";" + grupoPertenece + "↨" + distinguishedName + ";" + contador.ToString();
                                                }

                                                VerErrores("Resultado Final: " + Resultado.ToString(), "PruebasSistemaWEB", "PruebasSistemaWEB");
                                            }
                                        }

                                        #endregion

                                        //Resultado += ";" + contador.ToString();
                                    }
                                    else
                                    {
                                        Resultado += " No existe el usuario ";
                                        Resultado += "-" + ";";
                                        Resultado += "-" + ";";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Resultado += " El nombre de usuario o contraseña son incorrectos. ";
                                Resultado += ";" + useraccountcontrol + ";";
                                Resultado += "-" + ";";
                            }
                            finally
                            {
                                adsEntry.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Resultado += " No existe el usuario ";
                    Resultado += "-" + ";";
                    Resultado += "-" + ";";
                }

            }
            catch (Exception ex)
            {
                Resultado = Resultado + ";" + ex.Message;
            }
            finally
            {

            }

            return Resultado + ";" + CambiarClaveAlInicio;
        }
        #endregion

        #region VerUsuario
        public string VerUsuario(string server, string dominio, string usuario, string password, string Carpeta, string datosUsuarios, int EstadoProceso)
        {

            string Resultado = "";
            string[] cadena = datosUsuarios.Split(';');
            try
            {

                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                //Por ejemplo: 'LDAP://ejemplo.lan.es' o  'LDAP://NOMBRE_SERVIDOR'
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR

                string pathFinal = Carpeta;
                var dsDirectoryEntry = new DirectoryEntry(path, username, pass);

                DirectorySearcher search = new DirectorySearcher();

                if (EstadoProceso == 1)//userPrincipalName
                {
                    search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(userPrincipalName=" + cadena[0] + "))" };
                }
                else if (EstadoProceso == 2)//sAMAccountName
                {
                    search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(SAMAccountName=" + cadena[0] + "))" };
                }

                var dsResults = search.FindOne();
                try
                {
                    if (dsResults != null)
                    {
                        var myEntry = dsResults.GetDirectoryEntry();
                        
                        string displayname = (string)myEntry.Properties["cn"].Value;
                        if (displayname == null)
                        {
                            Resultado += ";";
                        }
                        else
                        {
                            Resultado += displayname.ToString() + ";";
                        }
                        //Resultado += ";" + contador.ToString();
                    }
                    else
                    {
                        Resultado += " No existe el usuario ";
                        Resultado += "-" + ";";
                        Resultado += "-" + ";";
                    }

                }
                catch (Exception ex)
                {
                    Resultado += " No existe el usuario ";
                    Resultado += "-" + ";";
                    Resultado += "-" + ";";
                }

            }
            catch (Exception ex)
            {
                Resultado = Resultado + ";" + ex.Message;
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
                        newUser.Properties["displayName"].Value = cadena[1].ToString() + " " + cadena[2].ToString();// "Campuzano Arechua";// lastname.Text;
                        //newUser.Properties["initials"].Value = initials.Text;
                        //newUser.Properties["siplayName"].Value = displayname.Text;
                        //newUser.Properties["physicalDeliveryOfficeName"].Value = officename.Text;
                        newUser.Properties["telephoneNumber"].Value = cadena[3].ToString();//  "0989835824"; //phone.Text;
                        newUser.Properties["mail"].Value = cadena[4].ToString();// "guillermoarma@hotmail.com";// Email.Text;

                        newUser.Properties["Company"].Value = cadena[6].ToString();// "guillermoarma@hotmail.com";// Email.Text;
                        newUser.Properties["department"].Value = cadena[7].ToString();// "guillermoarma@hotmail.com";// Email.Text;
                        newUser.Properties["title"].Value = cadena[8].ToString();// "guillermoarma@hotmail.com";// Email.Text;
                        newUser.Properties["userPrincipalName"].Value = cadena[0].ToString() +"@" + dominio + ".com";// "guillermoarma@hotmail.com";// Email.Text;
                        newUser.CommitChanges();
                        newUser.Invoke("unicodepwd", cadena[5].ToString());
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
        public string ActualizarUsuarios(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta, int EstadoProceso)
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

                DirectorySearcher search = new DirectorySearcher();
                if (EstadoProceso == 1)//userPrincipalName
                {
                    search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(userPrincipalName=" + cadena[0] + "))" };
                }
                else if (EstadoProceso == 2)//sAMAccountName
                {
                    search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(SAMAccountName=" + cadena[0] + "))" };
                }

                var dsResults = search.FindOne();
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
                    Respuesta = "Usuario Actualizado correctamente..";
                }

                if (cadena[6].ToString() == "1")
                {
                    AD ad = new AD();
                    string cErr = "";
                    DirectoryEntry account = ad.GetUser(path, username, pass, cadena[0].ToString(), ref cErr, EstadoProceso);
                    DirectorySearcher search2 = new DirectorySearcher(account);
                    search2.Filter = "(&(objectClass=user)(sAMAccountName=" + cadena[0].ToString() + "))";
                    account = search2.FindOne().GetDirectoryEntry();
                    account.Invoke("SetPassword", cadena[7].ToString());
                    account.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }

            return Respuesta;
        }
        #endregion

        #region ListarUsuarios
        public string ListarUsuarios(string server, string dominio, string usuario, string password,  string Carpeta, int EstadoProceso)
        {
            string Respuesta = "";
            List<User> rst = new List<User>();
            User item;
            try
            {
                string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                string username = usuario.Trim();           //USUARIO DEL DOMINIO
                string pass = password.Trim();               //PASSWORD DEL USUARIO
                string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                string pathFinal = Carpeta;//"LDAP://OU=CBS,DC=cbs,DC=local";

                DirectoryEntry adSearchRoot = new DirectoryEntry(pathFinal, username, pass);
                DirectorySearcher adSearcher = new DirectorySearcher(adSearchRoot);

                adSearcher.Filter = "(&(objectClass=user)(objectCategory=person))";
                if (EstadoProceso == 1)//userPrincipalName
                {
                    adSearcher.PropertiesToLoad.Add("userPrincipalName");
                }
                else
                {
                    adSearcher.PropertiesToLoad.Add("samaccountname");
                }                  
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
                        else if (result.Properties.Contains("userPrincipalName"))
                        {
                            item = new User();

                            item.UserName = (String)result.Properties["userPrincipalName"][0];

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

        #region ListaGrupos
        public string ListaGrupos()
        {
            string Respuesta = "";
            try
            {
                AD ad = new AD();
                PrincipalContext contexto = new PrincipalContext(ContextType.Machine);
                GroupPrincipal insUserPrincipal = new GroupPrincipal(contexto);
                insUserPrincipal.Name = "*";
                PrincipalSearchResult<Principal> results = ad.buscaGrp(insUserPrincipal);
                foreach (Principal p in results)
                {
                    //datos.Items.Add(p);
                    Respuesta += p.UserPrincipalName  + " " + p.Description +" " + p.DisplayName +" " + p.Name + " " + p.SamAccountName +" " + p.DistinguishedName ;
                }

            }
            catch (Exception ex)
            {
                Respuesta = ex.Message;
            }
            return Respuesta;
        }
        #endregion

        #region VerErrores
        public void VerErrores(string valor, string Carpeta, string rucEmpresa)
        {
            try
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
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Exception: " + ex.Message);
            }
        }
        #endregion

    }
}
