using System;
using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.DirectoryServices;  //Hay que añadirlo en References
using System.DirectoryServices.AccountManagement;
using Newtonsoft.Json;
using System.Configuration;

using System.IO;
using System.Security.Cryptography;// encriptacion

namespace WebAppServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SerWebApp" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SerWebApp.svc o SerWebApp.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SerWebApp : ISerWebApp
    {
        #region CrearUsuario
        public string CrearUsuario(string Usuario, string Clave, string Nombre1, string Nombre2, string Apellido1, string Apellido2, string Identificacion, string NombreUsuario, string Email, string Telefono, string Compania,string Departamento, string Titulo,string Contrasenia, string Portal,string Estado,string EstadoProceso)
        {
            string Respuesta = "";
            string oGUID = string.Empty;
            string server = ConfigurationManager.AppSettings["server"].ToString();
            string dominio = ConfigurationManager.AppSettings["dominio"].ToString();
            string usuarios = ConfigurationManager.AppSettings["usuario"].ToString();
            string password = ConfigurationManager.AppSettings["clave"].ToString();
            string Carpeta = ConfigurationManager.AppSettings["carpeta"].ToString();
            int generarLog = Convert.ToInt32(ConfigurationManager.AppSettings["generarlog"].ToString());

            VerErrores("server: " + server, "CreacionUsuarios", "Generarlog", generarLog);
            VerErrores("dominio: " + dominio, "CreacionUsuarios", "Generarlog", generarLog);
            VerErrores("usuarios: " + usuarios, "CreacionUsuarios", "Generarlog", generarLog);
            VerErrores("Carpeta: " + Carpeta, "CreacionUsuarios", "Generarlog", generarLog);

            VerErrores("Portal: " + Portal, "CreacionUsuarios", "Generarlog", generarLog);
            VerErrores("Estado: " + Estado, "CreacionUsuarios", "Generarlog", generarLog);

            if (Usuario == "UsuarioAd" && Clave == "U$uar102023")
            {
                try
                {
                    Respuesta = "El servicio no esta disponible hasta que se genere la integración";
                    string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                    VerErrores("server: " + server, "CreacionUsuarios", "Generarlog", generarLog);
                    string username = usuarios.Trim();           //USUARIO DEL DOMINIO
                    VerErrores("usuarios: " + usuarios, "CreacionUsuarios", "Generarlog", generarLog);
                    string pass = password.Trim();               //PASSWORD DEL USUARIO
                    VerErrores("password: " + password, "CreacionUsuarios", "Generarlog", generarLog);
                    string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                    VerErrores("dominio: " + dominio, "CreacionUsuarios", "Generarlog", generarLog);
                    string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                    VerErrores("domUsu: " + domUsu, "CreacionUsuarios", "Generarlog", generarLog);
                    string pathFinal = "";
                    if (Departamento == "USUARIOS")
                    {
                        Carpeta = Carpeta.Replace("LDAP://", "");
                        pathFinal = "LDAP://" + "OU=" + Departamento + "," + Carpeta;
                        VerErrores("pathFinal: " + pathFinal, "CreacionUsuarios", "Generarlog", generarLog);
                    }
                    else if (Departamento == "DOCENTES")
                    {
                        Carpeta = Carpeta.Replace("LDAP://", "");
                        pathFinal = "LDAP://" + "OU=" + Departamento + "," + Carpeta;
                        VerErrores("pathFinal: " + pathFinal, "CreacionUsuarios", "Generarlog", generarLog);
                    }
                    else if (Departamento == "")
                    {
                        pathFinal = Carpeta;
                    }

                    //pathFinal = Carpeta;//"LDAP://OU=CBS,DC=cbs,DC=local";
                    //OU=USUARIOS,DC=indoamerica,DC=edu,DC=ec
                    //            DC=indoamerica,DC=edu,DC=ec
                    //LDAP://DC=indoamerica,DC=edu,DC=ec
                    VerErrores("Carpeta: " + Carpeta, "CreacionUsuarios", "Generarlog", generarLog);
                    VerErrores("pathFinal: " + pathFinal, "CreacionUsuarios", "Generarlog", generarLog);
                    DirectoryEntry adUserFolder = new DirectoryEntry(pathFinal, username, pass);

                    if (Estado == "1")
                    {
                        #region Registro nuevo Usuario
                        VerErrores("Nombres: " + Nombre1 + " " + Nombre2, "CreacionUsuarios", "Generarlog", generarLog);
                        VerErrores("Apellidos: " + Apellido1 + " " + Apellido2, "CreacionUsuarios", "Generarlog", generarLog);
                        DirectoryEntry newUser = adUserFolder.Children.Add("CN=" + Nombre1 + " " + Nombre2 + " " + Apellido1 + " " + Apellido2, "User");

                        if (DirectoryEntry.Exists(newUser.Path))
                        {
                            Respuesta = "El usuario ya existe";
                        }
                        else
                        {
                            if (Contrasenia.Length >= 9)
                            {
                                VerErrores("samAccountName: " + NombreUsuario, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["samAccountName"].Value = NombreUsuario;
                                VerErrores("givenName: " + Nombre1 + " " + Nombre2, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["givenName"].Value = Nombre1 + " " + Nombre2;
                                VerErrores("sn: " + Apellido1 + " " + Apellido2, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["sn"].Value = Apellido1 + " " + Apellido2;
                                newUser.Properties["cn"].Value = Nombre1 + " " + Nombre2 + " " + Apellido1 + " " + Apellido2;
                                newUser.Properties["displayName"].Value = Nombre1 + " " + Nombre2 + " " + Apellido1 + " " + Apellido2;
                                VerErrores("telephoneNumber: " + Telefono, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["telephoneNumber"].Value = Telefono;
                                VerErrores("mail: " + Email, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["mail"].Value = Email;
                                VerErrores("Company: " + Compania, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["Company"].Value = Compania;
                                VerErrores("department: " + Departamento, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["department"].Value = Departamento;
                                VerErrores("title: " + Titulo, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["title"].Value = Titulo;
                                VerErrores("userPrincipalName: " + NombreUsuario + "@" + dominio, "CreacionUsuarios", "Generarlog", generarLog);
                                newUser.Properties["userPrincipalName"].Value = NombreUsuario + "@" + dominio; //+ ".com";
                                newUser.CommitChanges();
                                oGUID = newUser.Guid.ToString();
                                newUser.Invoke("SetPassword", new object[] { DecryptString(Contrasenia) });
                                //newUser.Invoke("unicodepwd", new object[] { Contrasenia });
                                int val = (int)newUser.Properties["userAccountControl"].Value;
                                newUser.Properties["userAccountControl"].Value = val & ~0x2; //Habilitar
                                //newUser.Properties["userAccountControl"].Value = val | 0x2; //Desabilitar
                                newUser.CommitChanges();
                                //newUser.Properties["userAccountControl"].Value = 512;//cuenta activa
                                newUser.Properties["userAccountControl"].Value = 0;//Resetear contraseña
                                newUser.CommitChanges();
                                adUserFolder.Close();
                                newUser.Close();
                                #region GuardarUsuarioEnBaseDeDatos
                                EUsuario usuario = new EUsuario();
                                usuario.IdUsuarioPortal = 0;
                                usuario.Nombres = Nombre1 + " " + Nombre2;
                                usuario.Apellidos = Apellido1 + " " + Apellido2;
                                usuario.Identificacion = Identificacion;
                                usuario.NombreUsuario = NombreUsuario;
                                usuario.Email = Email;
                                usuario.Telefono = Telefono;
                                usuario.Compania = Compania;
                                usuario.Departamento = Departamento;
                                usuario.Titulo = Titulo;
                                usuario.Contrasenia = Contrasenia;
                                usuario.Portal = Portal;
                                usuario.Unidad = "";
                                usuario.Estado = Estado;
                                usuario.Tipo = Convert.ToInt32(Estado);
                                ERespuesta respuesta = new ERespuesta();
                                respuesta = NPreguntas.InsertarModificarEliminarUsuariosPortal(usuario);
                                VerErrores("respuesta Base de Datos: " + respuesta.mensaje, "CreacionUsuarios", "Generarlog", generarLog);
                                #endregion

                                Respuesta = "Usuario registrado correctamente..";
                            }
                            else
                            {
                                Respuesta = "La clave debe tener al menos 9 caracteres..";
                            }
                        }
                        #endregion
                    }
                    else if (Estado == "2")
                    {
                        #region Actualizar nuevo Usuario

                        var dsDirectoryEntry = new DirectoryEntry(pathFinal, username, pass);

                        DirectorySearcher search = new DirectorySearcher();
                        //if (EstadoProceso == 1)//userPrincipalName
                        //{
                        search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(userPrincipalName=" + Email + "))" };
                        //}
                        //else if (EstadoProceso == 2)//sAMAccountName
                        //{
                        //    search = new DirectorySearcher(dsDirectoryEntry) { Filter = "(&(objectClass=user)(SAMAccountName=" + cadena[0] + "))" };
                        //}

                        //VerErrores("cadena[0]: " + cadena[0], "PruebasSistemaWEB", "PruebasSistemaWEB", Bandera);

                        var dsResults = search.FindOne();
                        if (dsResults == null)
                        {
                            Respuesta = "El usuario no existe";
                            VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
                        }
                        else
                        {
                            var myEntry = dsResults.GetDirectoryEntry();
                            myEntry.Properties["Company"].Value = Compania;// Email.Text;
                            myEntry.Properties["department"].Value = Departamento;// Email.Text;
                            myEntry.Properties["title"].Value = Titulo;// Email.Text;
                            myEntry.Properties["mail"].Value = Email;// Email.Text;
                            myEntry.Properties["telephoneNumber"].Value = Telefono;

                            myEntry.CommitChanges();


                            //if (cadena[6].ToString() == "1")
                            //{
                            AD ad = new AD();
                            string cErr = "";
                            DirectoryEntry account = ad.GetUser(path, username, pass, Email, ref cErr, 1);
                            DirectorySearcher search2 = new DirectorySearcher(account);
                            //if (EstadoProceso == 1)//userPrincipalName
                            //{

                            search2.Filter = "(&(objectClass=user)(userPrincipalName=" + Email + "))";
                            //}
                            //else if (EstadoProceso == 2)//sAMAccountName
                            //{
                            //   search2.Filter = "(&(objectClass=user)(sAMAccountName=" + cadena[0].ToString() + "))";
                            //}
                            //search2.Filter = "(&(objectClass=user)(sAMAccountName=" + cadena[0].ToString() + "))";
                            account = search2.FindOne().GetDirectoryEntry();
                            //account.Invoke("SetPassword", Contrasenia);
                            account.Invoke("SetPassword", new object[] { DecryptString(Contrasenia) });
                            account.CommitChanges();
                            account.Close();
                            dsDirectoryEntry.Close();
                            myEntry.Close();
                            //Respuesta = "Contraseña actualizada.";
                            //}

                            Respuesta = "Usuario Actualizado correctamente..";
                            VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
                        }
                        #endregion
                    }

                }
                catch (Exception ex)
                {
                    Respuesta = ex.Message;
                    VerErrores("ex.Message: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                }
            }
            else
            {
                Respuesta = "Las Credenciales ingresadas no son correctas";
                VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
            }

            return Respuesta;
        }
        #endregion

        #region ValidarCuenta
        public string ValidarCuenta(string UsuarioIngreso, string ClaveIngreso, string UsuarioLogin, string Clavelogin)
        {
            string Respuesta = "";
            string Carpeta = ConfigurationManager.AppSettings["carpeta"].ToString();
            int generarLog = Convert.ToInt32(ConfigurationManager.AppSettings["generarlog"].ToString());
            string distinguishedName = "";
            string memberof = "";
            string[] cadena2 = null;
            string Admin = "";
            string Estado = "";
            string ContadorIntentos = "";
            bool EstadoBool = false;
            string CambiarClaveAlInicio = "";
            string NombreUsuario = "";
            string UnidadOU = "";
            int TipoLogueo = 0;
            if (UsuarioIngreso == "UsuarioAd" && ClaveIngreso == "U$uar102023")
            {
                VerErrores("UsuarioIngreso: " + UsuarioIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                VerErrores("ClaveIngreso: " + ClaveIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                List<EUsuariosAD> EUsuario = NUsuarioAD.GetConsultarUsuarioAD(0, 0);
                VerErrores("EUsuario.Count: " + EUsuario.Count.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                if (EUsuario.Count > 0)
                {
                    string oGUID = string.Empty;
                    string server = "";
                    string dominio = "";
                    string usuarios = "";
                    string password = "";
                    foreach (EUsuariosAD ee in EUsuario)
                    {
                        server = ee.Servidor;
                        dominio = ee.Dominio;
                        usuarios = ee.Usuario;
                        password = ee.Clave;
                        TipoLogueo = ee.TipoLogueo;
                    }

                    try
                    {
                        Respuesta = "El servicio no esta disponible hasta que se genere la integración";
                        string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                        VerErrores("server: " + server, "CreacionUsuarios", "Generarlog", generarLog);
                        string username = usuarios.Trim();           //USUARIO DEL DOMINIO
                        VerErrores("usuarios: " + usuarios, "CreacionUsuarios", "Generarlog", generarLog);
                        string pass = DecryptString(password.Trim());               //PASSWORD DEL USUARIO
                        VerErrores("password: " + password, "CreacionUsuarios", "Generarlog", generarLog);
                        string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                        VerErrores("dominio: " + dominio, "CreacionUsuarios", "Generarlog", generarLog);
                        string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                        VerErrores("domUsu: " + domUsu, "CreacionUsuarios", "Generarlog", generarLog);

                        DirectoryEntry entry = new DirectoryEntry(path, username, pass);

                        try
                        {
                            DirectorySearcher search = new DirectorySearcher(entry);
                            if (TipoLogueo == 0)
                            {
                                search = new DirectorySearcher(entry) { Filter = "(&(objectClass=user)(samaccountname=" + UsuarioLogin + "))" };
                            }
                            else if (TipoLogueo == 1)
                            {
                                search = new DirectorySearcher(entry) { Filter = "(&(objectClass=user)(userprincipalname=" + UsuarioLogin + "))" };
                            }
                            //search.Filter = "(&(objectClass=user)(objectClass=person)(userPrincipalName=*" + UsuarioLogin.Trim() + "*)(samAccountName=*" + UsuarioLogin.Trim() + "*))";
                            SearchResult result = search.FindOne();
                            if (result != null)
                            {
                                // Si existe el usuario, mostramos las propiedades que ha recuperado
                                ResultPropertyCollection fields = result.Properties;
                                foreach (String ldapField in fields.PropertyNames)
                                {
                                    if (ldapField == "useraccountcontrol")
                                    {
                                        foreach (Object myCollection in fields[ldapField])
                                        {
                                            //t4.Text += Environment.NewLine + (String.Format("{0,-20} : {1}", ldapField, myCollection.ToString()));
                                            Estado = myCollection.ToString();
                                            bool rvalor = ValidarEstadoActivoUsuario(myCollection.ToString());
                                            EstadoBool = rvalor;
                                        }
                                    }
                                    else if (ldapField == "distinguishedname")
                                    {
                                        foreach (Object myCollection in fields[ldapField])
                                        {
                                            distinguishedName = myCollection.ToString();
                                            cadena2 = distinguishedName.Split(',');
                                            if (cadena2[0].ToString() == "CN=Domain Admins" || cadena2[0].ToString() == "CN=Administrators" || cadena2[0].ToString() == "CN=Administradores" || cadena2[0].ToString() == "CN=Administrator")
                                            {
                                                Admin = "Administrador";
                                            }
                                            else
                                            {
                                                Admin = "Usuario";
                                            }
                                        }
                                    }
                                    else if (ldapField == "badpwdcount")
                                    {
                                        foreach (Object myCollection in fields[ldapField])
                                        {
                                            ContadorIntentos = myCollection.ToString();
                                        }
                                    }
                                    else if (ldapField == "pwdlastset")
                                    {
                                        foreach (Object myCollection in fields[ldapField])
                                        {
                                            CambiarClaveAlInicio = myCollection.ToString();
                                        }
                                    }
                                    else if (ldapField == "cn")
                                    {
                                        foreach (Object myCollection in fields[ldapField])
                                        {
                                            NombreUsuario = myCollection.ToString();
                                        }
                                    }
                                    else if (ldapField == "adspath")
                                    {
                                        foreach (Object myCollection in fields[ldapField])
                                        {
                                            UnidadOU = myCollection.ToString();
                                        }
                                    }

                                    if (result.Properties.Contains("memberof"))
                                    {
                                        foreach (Object myCollection in fields[ldapField])
                                        {
                                            memberof = myCollection.ToString();
                                        }
                                    }
                                    else
                                    {
                                        Admin = "Usuario";
                                    }
                                }

                                DirectoryEntry Entry = new DirectoryEntry(path, UsuarioLogin, Clavelogin);
                                DirectorySearcher Searcher = new DirectorySearcher(Entry);
                                Searcher.SearchScope = SearchScope.OneLevel;

                                try
                                {
                                    System.DirectoryServices.SearchResult Results = Searcher.FindOne();
                                    if ((Results != null))
                                    {
                                        //Los datos son correctos
                                        EstadoBool = true;
                                        List<EValidarUsuario> rst = new List<EValidarUsuario>();
                                        EValidarUsuario items = new EValidarUsuario();
                                        items.Estado = Estado;
                                        items.Perfil = Admin;
                                        items.Memberof = memberof;
                                        items.ContadorIntentos = ContadorIntentos;
                                        items.EstadoBool = EstadoBool;
                                        items.CambiarClaveAlInicio = CambiarClaveAlInicio;
                                        items.NombreUsuario = NombreUsuario;
                                        items.UnidadOU = UnidadOU;
                                        items.Mensaje = "Usuario encontrado!";
                                        rst.Add(items);
                                        Respuesta = JsonConvert.SerializeObject(rst, Formatting.Indented);
                                    }
                                    else
                                    {
                                        EstadoBool = false;
                                    }
                                }

                                catch (Exception ex)
                                {
                                    // Si no encuentra el usuario
                                    List<EValidarUsuario> rst1 = new List<EValidarUsuario>();
                                    EValidarUsuario items1 = new EValidarUsuario();
                                    items1.Estado = Estado;
                                    items1.Perfil = "";
                                    items1.Memberof = "";
                                    items1.ContadorIntentos = "";
                                    items1.EstadoBool = false;
                                    items1.CambiarClaveAlInicio = "";
                                    items1.NombreUsuario = "";
                                    items1.UnidadOU = UnidadOU;
                                    items1.Mensaje = "El nombre de usuario o contraseña son incorrectos";
                                    rst1.Add(items1);
                                    Respuesta = JsonConvert.SerializeObject(rst1, Formatting.Indented);
                                }
                            }

                            else
                            {
                                // Si no encuentra el usuario
                                List<EValidarUsuario> rst = new List<EValidarUsuario>();
                                EValidarUsuario items = new EValidarUsuario();
                                items.Estado = "";
                                items.Perfil = "";
                                items.Memberof = "";
                                items.ContadorIntentos = "";
                                items.EstadoBool = false;
                                items.CambiarClaveAlInicio = "";
                                items.NombreUsuario = "";
                                items.UnidadOU = "";
                                items.Mensaje = "Usuario no encontrado!";
                                rst.Add(items);
                                Respuesta = JsonConvert.SerializeObject(rst, Formatting.Indented);
                            }

                        }
                        catch (Exception ex)
                        {
                            // Validation failed - handle how you want
                            VerErrores("ex.Message-2: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                        }

                    }
                    catch (Exception ex)
                    {
                        Respuesta = ex.Message;
                        VerErrores("ex.Message-1: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                    }
                }
                else
                {
                    Respuesta = "No hay comunicación con la base de datos";
                    VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
                }
            }
            else
            {
                Respuesta = "Las Credenciales ingresadas no son correctas";
                VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
            }

            return Respuesta;
        }
        #endregion

        #region BloquearCuenta
        public string BloquerCuenta(string UsuarioIngreso, string ClaveIngreso, string UsuarioLogin, string Clavelogin)
        {
            string Respuesta = "";
            string Carpeta = ConfigurationManager.AppSettings["carpeta"].ToString();
            int generarLog = Convert.ToInt32(ConfigurationManager.AppSettings["generarlog"].ToString());
            if (UsuarioIngreso == "UsuarioAd" && ClaveIngreso == "U$uar102023")
            {
                VerErrores("UsuarioIngreso: " + UsuarioIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                VerErrores("ClaveIngreso: " + ClaveIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                List<EUsuariosAD> EUsuario = NUsuarioAD.GetConsultarUsuarioAD(0, 0);
                VerErrores("EUsuario.Count: " + EUsuario.Count.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                if (EUsuario.Count > 0)
                {
                    string oGUID = string.Empty;
                    string server = "";
                    string dominio = "";
                    string usuarios = "";
                    string password = "";
                    foreach (EUsuariosAD ee in EUsuario)
                    {
                        server = ee.Servidor;
                        dominio = ee.Dominio;
                        usuarios = ee.Usuario;
                        password = ee.Clave;
                    }

                    try
                    {
                        Respuesta = "El servicio no esta disponible hasta que se genere la integración";
                        string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                        VerErrores("server: " + server, "CreacionUsuarios", "Generarlog", generarLog);
                        string username = usuarios.Trim();           //USUARIO DEL DOMINIO
                        VerErrores("usuarios: " + usuarios, "CreacionUsuarios", "Generarlog", generarLog);
                        string pass = DecryptString(password.Trim());               //PASSWORD DEL USUARIO
                        VerErrores("password: " + password, "CreacionUsuarios", "Generarlog", generarLog);
                        string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                        VerErrores("dominio: " + dominio, "CreacionUsuarios", "Generarlog", generarLog);
                        string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                        VerErrores("domUsu: " + domUsu, "CreacionUsuarios", "Generarlog", generarLog);

                        DirectoryEntry entry = new DirectoryEntry(path, username, pass);

                        try
                        {
                            DirectorySearcher search = new DirectorySearcher(entry);
                            search = new DirectorySearcher(entry) { Filter = "(&(objectClass=user)(SAMAccountName=" + UsuarioLogin + "))" };
                            SearchResult result = search.FindOne();
                            if (result != null)
                            {
                                DirectoryEntry userEntry = result.GetDirectoryEntry();
                                int userFlags = (int)userEntry.Properties["userAccountControl"].Value;
                                int newFlags = userFlags | 0x0002; // Activar el bit de bloqueo de la cuenta
                                userEntry.Properties["userAccountControl"].Value = newFlags;
                                userEntry.CommitChanges();
                                // Console.WriteLine("Usuario bloqueado con éxito.");
                                Respuesta = "Usuario bloqueado con éxito.";
                                VerErrores("Respuesta: " + Respuesta.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                            }
                            else
                            {
                                //Console.WriteLine("Usuario no encontrado.");
                                Respuesta = "Usuario no encontrado.";
                                VerErrores("Respuesta: " + Respuesta.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                            }

                            // Liberar los recursos
                            search.Dispose();
                            entry.Dispose();

                        }
                        catch (Exception ex)
                        {
                            Respuesta = ex.Message;
                            VerErrores("ex.Message-1: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                        }

                    }
                    catch (Exception ex)
                    {
                        Respuesta = ex.Message;
                        VerErrores("ex.Message-2: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                    }

                }
            }
            else
            {
                Respuesta = "Las Credenciales ingresadas no son correctas";
                VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
            }
            return Respuesta;
        }
        #endregion

        #region DesbloquearCuenta
        public string DesbloquearCuenta(string UsuarioIngreso, string ClaveIngreso, string UsuarioLogin, string Clavelogin)
        {
            string Respuesta = "";
            string Carpeta = ConfigurationManager.AppSettings["carpeta"].ToString();
            int generarLog = Convert.ToInt32(ConfigurationManager.AppSettings["generarlog"].ToString());
            if (UsuarioIngreso == "UsuarioAd" && ClaveIngreso == "U$uar102023")
            {
                VerErrores("UsuarioIngreso: " + UsuarioIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                VerErrores("ClaveIngreso: " + ClaveIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                List<EUsuariosAD> EUsuario = NUsuarioAD.GetConsultarUsuarioAD(0, 0);
                VerErrores("EUsuario.Count: " + EUsuario.Count.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                if (EUsuario.Count > 0)
                {
                    string oGUID = string.Empty;
                    string server = "";
                    string dominio = "";
                    string usuarios = "";
                    string password = "";
                    foreach (EUsuariosAD ee in EUsuario)
                    {
                        server = ee.Servidor;
                        dominio = ee.Dominio;
                        usuarios = ee.Usuario;
                        password = ee.Clave;
                    }

                    try
                    {
                        Respuesta = "El servicio no esta disponible hasta que se genere la integración";
                        string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                        VerErrores("server: " + server, "CreacionUsuarios", "Generarlog", generarLog);
                        string username = usuarios.Trim();           //USUARIO DEL DOMINIO
                        VerErrores("usuarios: " + usuarios, "CreacionUsuarios", "Generarlog", generarLog);
                        string pass = DecryptString(password.Trim());               //PASSWORD DEL USUARIO
                        VerErrores("password: " + password, "CreacionUsuarios", "Generarlog", generarLog);
                        string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                        VerErrores("dominio: " + dominio, "CreacionUsuarios", "Generarlog", generarLog);
                        string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                        VerErrores("domUsu: " + domUsu, "CreacionUsuarios", "Generarlog", generarLog);

                        DirectoryEntry entry = new DirectoryEntry(path, username, pass);

                        try
                        {
                            DirectorySearcher search = new DirectorySearcher(entry);
                            search = new DirectorySearcher(entry) { Filter = "(&(objectClass=user)(SAMAccountName=" + UsuarioLogin + "))" };
                            SearchResult result = search.FindOne();
                            if (result != null)
                            {
                                DirectoryEntry userEntry = result.GetDirectoryEntry();
                                int userFlags = (int)userEntry.Properties["userAccountControl"].Value;
                                int newFlags = userFlags & ~0x0002; // Desactivar el bit de bloqueo de la cuenta
                                userEntry.Properties["userAccountControl"].Value = newFlags;
                                userEntry.CommitChanges();
                                // Console.WriteLine("Usuario bloqueado con éxito.");
                                Respuesta = "Usuario desbloqueado con éxito.";
                                VerErrores("Respuesta: " + Respuesta.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                            }
                            else
                            {
                                //Console.WriteLine("Usuario no encontrado.");
                                Respuesta = "Usuario no encontrado.";
                                VerErrores("Respuesta: " + Respuesta.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                            }

                            // Liberar los recursos
                            search.Dispose();
                            entry.Dispose();

                        }
                        catch (Exception ex)
                        {
                            Respuesta = ex.Message;
                            VerErrores("ex.Message-1: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                        }

                    }
                    catch (Exception ex)
                    {
                        Respuesta = ex.Message;
                        VerErrores("ex.Message-2: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                    }

                }
            }
            else
            {
                Respuesta = "Las Credenciales ingresadas no son correctas";
                VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
            }
            return Respuesta;
        }
        #endregion

        #region ListaOU
        public string ListaOU(string UsuarioIngreso, string ClaveIngreso, string UsuarioLogin, string Clavelogin)
        {
            string Respuesta = "";
            string Carpeta = ConfigurationManager.AppSettings["carpeta"].ToString();
            int generarLog = Convert.ToInt32(ConfigurationManager.AppSettings["generarlog"].ToString());
            if (UsuarioIngreso == "UsuarioAd" && ClaveIngreso == "U$uar102023")
            {
                VerErrores("UsuarioIngreso: " + UsuarioIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                VerErrores("ClaveIngreso: " + ClaveIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                List<EUsuariosAD> EUsuario = NUsuarioAD.GetConsultarUsuarioAD(0, 0);
                VerErrores("EUsuario.Count: " + EUsuario.Count.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                if (EUsuario.Count > 0)
                {
                    string oGUID = string.Empty;
                    string server = "";
                    string dominio = "";
                    string usuarios = "";
                    string password = "";
                    foreach (EUsuariosAD ee in EUsuario)
                    {
                        server = ee.Servidor;
                        dominio = ee.Dominio;
                        usuarios = ee.Usuario;
                        password = ee.Clave;
                    }

                    try
                    {
                        Respuesta = "El servicio no esta disponible hasta que se genere la integración";
                        string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                        VerErrores("server: " + server, "CreacionUsuarios", "Generarlog", generarLog);
                        string username = usuarios.Trim();           //USUARIO DEL DOMINIO
                        VerErrores("usuarios: " + usuarios, "CreacionUsuarios", "Generarlog", generarLog);
                        string pass = DecryptString(password.Trim());               //PASSWORD DEL USUARIO
                        VerErrores("password: " + password, "CreacionUsuarios", "Generarlog", generarLog);
                        string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                        VerErrores("dominio: " + dominio, "CreacionUsuarios", "Generarlog", generarLog);
                        string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                        VerErrores("domUsu: " + domUsu, "CreacionUsuarios", "Generarlog", generarLog);

                        DirectoryEntry entry = new DirectoryEntry(path, username, pass);

                        try
                        {
                            DirectorySearcher searcher = new DirectorySearcher(entry);
                            // Obtiene una colección de los OU principales
                            var ouSearcher = new DirectorySearcher(entry, "(objectCategory=organizationalUnit)");
                            ouSearcher.SearchScope = SearchScope.OneLevel;
                            var ouCollection = ouSearcher.FindAll();
                            // Itera sobre los resultados y muestra los nombres de los OU principales

                            List<EListaOU> rst = new List<EListaOU>();
                            foreach (SearchResult ou in ouCollection)
                            {
                                //var ouName = ou.Properties["name"][0].ToString();
                                //t4.Text += Environment.NewLine + (String.Format("{0,-20} : {1}", ouName, ou.Path.ToString().Trim()));
                                EListaOU items = new EListaOU();
                                items.Nombre = ou.Properties["name"][0].ToString().Trim();
                                items.Unidad = ou.Path.ToString().Trim();
                                items.Estado = "Por Registrar";
                                rst.Add(items);
                            }
                            Respuesta = JsonConvert.SerializeObject(rst, Formatting.Indented);
                            // Liberar los recursos
                            searcher.Dispose();
                            entry.Dispose();

                        }
                        catch (Exception ex)
                        {
                            Respuesta = ex.Message;
                            VerErrores("ex.Message-1: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                        }

                    }
                    catch (Exception ex)
                    {
                        Respuesta = ex.Message;
                        VerErrores("ex.Message-2: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                    }

                }
            }
            else
            {
                Respuesta = "Las Credenciales ingresadas no son correctas";
                VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
            }
            return Respuesta;
        }
        #endregion

        #region ListaUsuarios

        #region ValidarEstadoActivoUsuarioString
        public string ValidarEstadoActivoUsuarioString(string estado)
        {
            string activo = "Inactivo";

            if (estado == "512" || estado == "66048" || estado == "262656" || estado == "8388608")
            {
                activo = "Activo";
            }
            else if (estado == "514" || estado == "66050" || estado == "66082")
            {
                activo = "Inactivo";
            }

            return activo;
        }
        #endregion

        public string ListaUsuarios(string UsuarioIngreso, string ClaveIngreso, string UsuarioLogin, string Clavelogin, string UO)
        {
            string Respuesta = "";
            int TipoLogueo = 0;
            Int32 IdEstado = 0;
            string Carpeta = ConfigurationManager.AppSettings["carpeta"].ToString();
            int generarLog = Convert.ToInt32(ConfigurationManager.AppSettings["generarlog"].ToString());
            if (UsuarioIngreso == "UsuarioAd" && ClaveIngreso == "U$uar102023")
            {
                VerErrores("UsuarioIngreso: " + UsuarioIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                VerErrores("ClaveIngreso: " + ClaveIngreso.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                List<EUsuariosAD> EUsuario = NUsuarioAD.GetConsultarUsuarioAD(0, 0);
                VerErrores("EUsuario.Count: " + EUsuario.Count.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                if (EUsuario.Count > 0)
                {
                    string oGUID = string.Empty;
                    string server = "";
                    string dominio = "";
                    string usuarios = "";
                    string password = "";
                    foreach (EUsuariosAD ee in EUsuario)
                    {
                        server = ee.Servidor;
                        dominio = ee.Dominio;
                        usuarios = ee.Usuario;
                        password = ee.Clave;
                        TipoLogueo = ee.TipoLogueo;
                    }


                    try
                    {
                        Respuesta = "El servicio no esta disponible hasta que se genere la integración";
                        string path = @"LDAP://" + server.Trim();  //CAMBIAR POR VUESTRO PATH (URL DEL SERVIDOR DE DIRECTORIO LDAP)
                        VerErrores("server: " + server, "CreacionUsuarios", "Generarlog", generarLog);
                        string username = usuarios.Trim();           //USUARIO DEL DOMINIO
                        VerErrores("usuarios: " + usuarios, "CreacionUsuarios", "Generarlog", generarLog);
                        string pass = DecryptString(password.Trim());               //PASSWORD DEL USUARIO
                        VerErrores("password: " + password, "CreacionUsuarios", "Generarlog", generarLog);
                        string dominios = dominio.Trim();            //CAMBIAR POR VUESTRO DOMINIO
                        VerErrores("dominio: " + dominio, "CreacionUsuarios", "Generarlog", generarLog);
                        string domUsu = dominios + @"\" + username;  //CADENA DE DOMINIO + USUARIO A COMPROBAR
                        VerErrores("domUsu: " + domUsu, "CreacionUsuarios", "Generarlog", generarLog);
                        List<User> rst = new List<User>();
                        User item;
                        if (UO != "")
                        {
                            path = UO;
                        }
                        DirectoryEntry entry = new DirectoryEntry(path, username, pass);
                        DirectorySearcher search = new DirectorySearcher(entry);
                        if (UsuarioLogin == "")
                        {
                            search.Filter = "(&(objectClass=user)(objectCategory=person))";
                        }
                        else
                        {
                            search.Filter = "(&(objectClass=user)(objectClass=person)(userPrincipalName=*" + UsuarioLogin.Trim() + "*)(samAccountName=*" + UsuarioLogin.Trim() + "*))";
                            //if (TipoLogueo == 0)
                            //{
                            //    search = new DirectorySearcher(entry) { Filter = "(&(objectClass=user)(samaccountname=" + UsuarioLogin + "))" };
                            //}
                            //else if (TipoLogueo == 1)
                            //{
                            //    search = new DirectorySearcher(entry) { Filter = "(&(objectClass=user)(userprincipalname=" + UsuarioLogin + "))" };
                            //}
                        }

                        search.PropertiesToLoad.Add("userPrincipalName");
                        search.PropertiesToLoad.Add("samaccountname");
                        search.PropertiesToLoad.Add("title");
                        search.PropertiesToLoad.Add("mail");
                        search.PropertiesToLoad.Add("usergroup");
                        search.PropertiesToLoad.Add("company");
                        search.PropertiesToLoad.Add("department");
                        search.PropertiesToLoad.Add("telephoneNumber");
                        search.PropertiesToLoad.Add("mobile");
                        search.PropertiesToLoad.Add("displayname");
                        search.PropertiesToLoad.Add("objectcategory");
                        search.PropertiesToLoad.Add("distinguishedname");
                        search.PropertiesToLoad.Add("accountexpirationdate");
                        search.PropertiesToLoad.Add("useraccountcontrol");
                        SearchResult result;
                        SearchResultCollection iResult = search.FindAll();
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
                                    if (result.Properties.Contains("objectcategory"))
                                    {
                                        item.Objectcategory = (String)result.Properties["objectcategory"][0];
                                    }
                                    if (result.Properties.Contains("distinguishedname"))
                                    {
                                        item.Distinguishedname = (String)result.Properties["distinguishedname"][0];
                                    }
                                    if (result.Properties.Contains("accountexpires"))
                                    {
                                        item.AccountExpirationDate = (DateTime)result.Properties["accountexpires"][0];
                                    }
                                    if (result.Properties.Contains("useraccountcontrol"))
                                    {
                                        IdEstado = (Int32)result.Properties["useraccountcontrol"][0];
                                        item.Estado = ValidarEstadoActivoUsuarioString(IdEstado.ToString());
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
                                    if (result.Properties.Contains("objectcategory"))
                                    {
                                        item.Objectcategory = (String)result.Properties["objectcategory"][0];
                                    }
                                    if (result.Properties.Contains("distinguishedname"))
                                    {
                                        item.Distinguishedname = (String)result.Properties["distinguishedname"][0];
                                    }
                                    if (result.Properties.Contains("accountexpires"))
                                    {
                                        item.AccountExpirationDate = (DateTime)result.Properties["accountexpires"][0];
                                    }
                                    if (result.Properties.Contains("useraccountcontrol"))
                                    {
                                        IdEstado = (Int32)result.Properties["useraccountcontrol"][0];
                                        item.Estado = ValidarEstadoActivoUsuarioString(IdEstado.ToString());
                                    }
                                    rst.Add(item);
                                }
                            }
                            Respuesta = JsonConvert.SerializeObject(rst, Formatting.Indented);
                        }
                        else
                        {
                            // Si no encuentra el usuario
                            Respuesta = "Usuario no encontrado!";
                        }

                    }
                    catch (Exception ex)
                    {
                        Respuesta = ex.Message;
                        VerErrores("ex.Message-2: " + ex.Message.ToString(), "CreacionUsuarios", "Generarlog", generarLog);
                    }

                }
            }
            else
            {
                Respuesta = "Las Credenciales ingresadas no son correctas";
                VerErrores("Respuesta: " + Respuesta, "CreacionUsuarios", "Generarlog", generarLog);
            }
            return Respuesta;
        }
        #endregion

        #region OtrosProcesos

        #region metodo encriptacion
        public string EncryptString(string txtClaro)
        {
            byte[] clearTextBytes = Encoding.UTF8.GetBytes(txtClaro);
            System.Security.Cryptography.SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();
            MemoryStream ms = new MemoryStream();
            byte[] rgbIV = Encoding.ASCII.GetBytes("ryojvlzmdalyglrj");
            byte[] key = Encoding.ASCII.GetBytes("hcxilkqbbhczfeultgbskdmaunivmfuo");
            CryptoStream cs = new CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
            cs.Write(clearTextBytes, 0, clearTextBytes.Length);
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        #endregion

        #region metodo desencriptacion
        public string DecryptString(string txtEncriptado)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(txtEncriptado);
            MemoryStream ms = new MemoryStream();
            System.Security.Cryptography.SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();
            byte[] rgbIV = Encoding.ASCII.GetBytes("ryojvlzmdalyglrj");
            byte[] key = Encoding.ASCII.GetBytes("hcxilkqbbhczfeultgbskdmaunivmfuo");
            CryptoStream cs = new CryptoStream(ms, rijn.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write);
            cs.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        #endregion

        #region ValidarEstado
        public bool ValidarEstadoActivoUsuario(string estado)
        {
            bool activo = false;

            if (estado == "512" || estado == "66048" || estado == "262656" || estado == "8388608")
            {
                activo = true;
            }
            else if (estado == "514" || estado == "66050" || estado == "66082")
            {
                activo = false;
            }

            return activo;
        }
        #endregion

        #region VerErrores
        public void VerErrores(string valor, string Carpeta, string rucEmpresa, int tipo)
        {
            try
            {
                if (tipo == 1)
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Exception: " + ex.Message);
            }
        }
        #endregion

        #endregion

    }
}
