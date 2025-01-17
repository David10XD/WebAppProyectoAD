﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WebAppTramaAD.ADProceso {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_ActualizarCuentaAD", Namespace="http://172.16.0.102/")]
    public partial class ActualizarCuentaAD : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ExtraerDatosUsuarioOperationCompleted;
        
        private System.Threading.SendOrPostCallback CambiarContraseniaOperationCompleted;
        
        private System.Threading.SendOrPostCallback DesbloquearUsuarioOperationCompleted;
        
        private System.Threading.SendOrPostCallback ValidarCuentaOperationCompleted;
        
        private System.Threading.SendOrPostCallback FechaCaducidadOperationCompleted;
        
        private System.Threading.SendOrPostCallback GenerarUsuariosOperationCompleted;
        
        private System.Threading.SendOrPostCallback ActualizarUsuariosOperationCompleted;
        
        private System.Threading.SendOrPostCallback ListarUsuariosOperationCompleted;
        
        private System.Threading.SendOrPostCallback ListaUsuariosOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ActualizarCuentaAD() {
            this.Url = global::WebAppTramaAD.Properties.Settings.Default.WebAppTramaAD_ADProceso_ActualizarCuentaAD;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ExtraerDatosUsuarioCompletedEventHandler ExtraerDatosUsuarioCompleted;
        
        /// <remarks/>
        public event CambiarContraseniaCompletedEventHandler CambiarContraseniaCompleted;
        
        /// <remarks/>
        public event DesbloquearUsuarioCompletedEventHandler DesbloquearUsuarioCompleted;
        
        /// <remarks/>
        public event ValidarCuentaCompletedEventHandler ValidarCuentaCompleted;
        
        /// <remarks/>
        public event FechaCaducidadCompletedEventHandler FechaCaducidadCompleted;
        
        /// <remarks/>
        public event GenerarUsuariosCompletedEventHandler GenerarUsuariosCompleted;
        
        /// <remarks/>
        public event ActualizarUsuariosCompletedEventHandler ActualizarUsuariosCompleted;
        
        /// <remarks/>
        public event ListarUsuariosCompletedEventHandler ListarUsuariosCompleted;
        
        /// <remarks/>
        public event ListaUsuariosCompletedEventHandler ListaUsuariosCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/ExtraerDatosUsuario", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ExtraerDatosUsuario(string server, string dominio, string usuario, string password) {
            object[] results = this.Invoke("ExtraerDatosUsuario", new object[] {
                        server,
                        dominio,
                        usuario,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ExtraerDatosUsuarioAsync(string server, string dominio, string usuario, string password) {
            this.ExtraerDatosUsuarioAsync(server, dominio, usuario, password, null);
        }
        
        /// <remarks/>
        public void ExtraerDatosUsuarioAsync(string server, string dominio, string usuario, string password, object userState) {
            if ((this.ExtraerDatosUsuarioOperationCompleted == null)) {
                this.ExtraerDatosUsuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExtraerDatosUsuarioOperationCompleted);
            }
            this.InvokeAsync("ExtraerDatosUsuario", new object[] {
                        server,
                        dominio,
                        usuario,
                        password}, this.ExtraerDatosUsuarioOperationCompleted, userState);
        }
        
        private void OnExtraerDatosUsuarioOperationCompleted(object arg) {
            if ((this.ExtraerDatosUsuarioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExtraerDatosUsuarioCompleted(this, new ExtraerDatosUsuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/CambiarContrasenia", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CambiarContrasenia(string server, string dominio, string usuario, string password, string Nuevopassword, string Confirpassword) {
            object[] results = this.Invoke("CambiarContrasenia", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        Nuevopassword,
                        Confirpassword});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CambiarContraseniaAsync(string server, string dominio, string usuario, string password, string Nuevopassword, string Confirpassword) {
            this.CambiarContraseniaAsync(server, dominio, usuario, password, Nuevopassword, Confirpassword, null);
        }
        
        /// <remarks/>
        public void CambiarContraseniaAsync(string server, string dominio, string usuario, string password, string Nuevopassword, string Confirpassword, object userState) {
            if ((this.CambiarContraseniaOperationCompleted == null)) {
                this.CambiarContraseniaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCambiarContraseniaOperationCompleted);
            }
            this.InvokeAsync("CambiarContrasenia", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        Nuevopassword,
                        Confirpassword}, this.CambiarContraseniaOperationCompleted, userState);
        }
        
        private void OnCambiarContraseniaOperationCompleted(object arg) {
            if ((this.CambiarContraseniaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CambiarContraseniaCompleted(this, new CambiarContraseniaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/DesbloquearUsuario", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DesbloquearUsuario(string server, string dominio, string usuario, string password) {
            object[] results = this.Invoke("DesbloquearUsuario", new object[] {
                        server,
                        dominio,
                        usuario,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DesbloquearUsuarioAsync(string server, string dominio, string usuario, string password) {
            this.DesbloquearUsuarioAsync(server, dominio, usuario, password, null);
        }
        
        /// <remarks/>
        public void DesbloquearUsuarioAsync(string server, string dominio, string usuario, string password, object userState) {
            if ((this.DesbloquearUsuarioOperationCompleted == null)) {
                this.DesbloquearUsuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDesbloquearUsuarioOperationCompleted);
            }
            this.InvokeAsync("DesbloquearUsuario", new object[] {
                        server,
                        dominio,
                        usuario,
                        password}, this.DesbloquearUsuarioOperationCompleted, userState);
        }
        
        private void OnDesbloquearUsuarioOperationCompleted(object arg) {
            if ((this.DesbloquearUsuarioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DesbloquearUsuarioCompleted(this, new DesbloquearUsuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/ValidarCuenta", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ValidarCuenta(string server, string dominio, string usuario, string password) {
            object[] results = this.Invoke("ValidarCuenta", new object[] {
                        server,
                        dominio,
                        usuario,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ValidarCuentaAsync(string server, string dominio, string usuario, string password) {
            this.ValidarCuentaAsync(server, dominio, usuario, password, null);
        }
        
        /// <remarks/>
        public void ValidarCuentaAsync(string server, string dominio, string usuario, string password, object userState) {
            if ((this.ValidarCuentaOperationCompleted == null)) {
                this.ValidarCuentaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnValidarCuentaOperationCompleted);
            }
            this.InvokeAsync("ValidarCuenta", new object[] {
                        server,
                        dominio,
                        usuario,
                        password}, this.ValidarCuentaOperationCompleted, userState);
        }
        
        private void OnValidarCuentaOperationCompleted(object arg) {
            if ((this.ValidarCuentaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ValidarCuentaCompleted(this, new ValidarCuentaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/FechaCaducidad", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string FechaCaducidad(string server, string dominio, string usuario, string password) {
            object[] results = this.Invoke("FechaCaducidad", new object[] {
                        server,
                        dominio,
                        usuario,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FechaCaducidadAsync(string server, string dominio, string usuario, string password) {
            this.FechaCaducidadAsync(server, dominio, usuario, password, null);
        }
        
        /// <remarks/>
        public void FechaCaducidadAsync(string server, string dominio, string usuario, string password, object userState) {
            if ((this.FechaCaducidadOperationCompleted == null)) {
                this.FechaCaducidadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFechaCaducidadOperationCompleted);
            }
            this.InvokeAsync("FechaCaducidad", new object[] {
                        server,
                        dominio,
                        usuario,
                        password}, this.FechaCaducidadOperationCompleted, userState);
        }
        
        private void OnFechaCaducidadOperationCompleted(object arg) {
            if ((this.FechaCaducidadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FechaCaducidadCompleted(this, new FechaCaducidadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/GenerarUsuarios", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GenerarUsuarios(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta) {
            object[] results = this.Invoke("GenerarUsuarios", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        datosUsuarios,
                        Carpeta});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GenerarUsuariosAsync(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta) {
            this.GenerarUsuariosAsync(server, dominio, usuario, password, datosUsuarios, Carpeta, null);
        }
        
        /// <remarks/>
        public void GenerarUsuariosAsync(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta, object userState) {
            if ((this.GenerarUsuariosOperationCompleted == null)) {
                this.GenerarUsuariosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerarUsuariosOperationCompleted);
            }
            this.InvokeAsync("GenerarUsuarios", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        datosUsuarios,
                        Carpeta}, this.GenerarUsuariosOperationCompleted, userState);
        }
        
        private void OnGenerarUsuariosOperationCompleted(object arg) {
            if ((this.GenerarUsuariosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerarUsuariosCompleted(this, new GenerarUsuariosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/ActualizarUsuarios", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ActualizarUsuarios(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta) {
            object[] results = this.Invoke("ActualizarUsuarios", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        datosUsuarios,
                        Carpeta});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ActualizarUsuariosAsync(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta) {
            this.ActualizarUsuariosAsync(server, dominio, usuario, password, datosUsuarios, Carpeta, null);
        }
        
        /// <remarks/>
        public void ActualizarUsuariosAsync(string server, string dominio, string usuario, string password, string datosUsuarios, string Carpeta, object userState) {
            if ((this.ActualizarUsuariosOperationCompleted == null)) {
                this.ActualizarUsuariosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnActualizarUsuariosOperationCompleted);
            }
            this.InvokeAsync("ActualizarUsuarios", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        datosUsuarios,
                        Carpeta}, this.ActualizarUsuariosOperationCompleted, userState);
        }
        
        private void OnActualizarUsuariosOperationCompleted(object arg) {
            if ((this.ActualizarUsuariosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ActualizarUsuariosCompleted(this, new ActualizarUsuariosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/ListarUsuarios", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListarUsuarios(string server, string dominio, string usuario, string password, string Carpeta) {
            object[] results = this.Invoke("ListarUsuarios", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        Carpeta});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListarUsuariosAsync(string server, string dominio, string usuario, string password, string Carpeta) {
            this.ListarUsuariosAsync(server, dominio, usuario, password, Carpeta, null);
        }
        
        /// <remarks/>
        public void ListarUsuariosAsync(string server, string dominio, string usuario, string password, string Carpeta, object userState) {
            if ((this.ListarUsuariosOperationCompleted == null)) {
                this.ListarUsuariosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarUsuariosOperationCompleted);
            }
            this.InvokeAsync("ListarUsuarios", new object[] {
                        server,
                        dominio,
                        usuario,
                        password,
                        Carpeta}, this.ListarUsuariosOperationCompleted, userState);
        }
        
        private void OnListarUsuariosOperationCompleted(object arg) {
            if ((this.ListarUsuariosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarUsuariosCompleted(this, new ListarUsuariosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://172.16.0.102/ActualizarCuentaAD/ListaUsuarios", RequestNamespace="http://172.16.0.102/", ResponseNamespace="http://172.16.0.102/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListaUsuarios() {
            object[] results = this.Invoke("ListaUsuarios", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListaUsuariosAsync() {
            this.ListaUsuariosAsync(null);
        }
        
        /// <remarks/>
        public void ListaUsuariosAsync(object userState) {
            if ((this.ListaUsuariosOperationCompleted == null)) {
                this.ListaUsuariosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListaUsuariosOperationCompleted);
            }
            this.InvokeAsync("ListaUsuarios", new object[0], this.ListaUsuariosOperationCompleted, userState);
        }
        
        private void OnListaUsuariosOperationCompleted(object arg) {
            if ((this.ListaUsuariosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListaUsuariosCompleted(this, new ListaUsuariosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ExtraerDatosUsuarioCompletedEventHandler(object sender, ExtraerDatosUsuarioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtraerDatosUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExtraerDatosUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void CambiarContraseniaCompletedEventHandler(object sender, CambiarContraseniaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CambiarContraseniaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CambiarContraseniaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void DesbloquearUsuarioCompletedEventHandler(object sender, DesbloquearUsuarioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DesbloquearUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DesbloquearUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ValidarCuentaCompletedEventHandler(object sender, ValidarCuentaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ValidarCuentaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ValidarCuentaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void FechaCaducidadCompletedEventHandler(object sender, FechaCaducidadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FechaCaducidadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FechaCaducidadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void GenerarUsuariosCompletedEventHandler(object sender, GenerarUsuariosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerarUsuariosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GenerarUsuariosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ActualizarUsuariosCompletedEventHandler(object sender, ActualizarUsuariosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ActualizarUsuariosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ActualizarUsuariosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ListarUsuariosCompletedEventHandler(object sender, ListarUsuariosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarUsuariosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarUsuariosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ListaUsuariosCompletedEventHandler(object sender, ListaUsuariosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListaUsuariosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListaUsuariosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591