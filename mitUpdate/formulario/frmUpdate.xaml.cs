
using mitUpdate.configuracion;
using mitUpdate.formulario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PcPay.Forms.Formularios
{
    /// <summary>
    /// Lógica de interacción para frmUpdate.xaml
    /// </summary>
    public partial class frmUpdate : Window
    {
        private string strIp, strCarpeta, strUsers, strPass, strFile, strVersion, strVersioActual;
        private bool isComplete;
        private WebClient cliente;
        private cargandoFormulario ventana;
        public frmUpdate()
        {
            InitializeComponent();

        }
    
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente = new WebClient();
                cliente.DownloadProgressChanged += new DownloadProgressChangedEventHandler(cargando);
                cliente.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(cargado);
                strVersion = Globales.VersionPcPay;

                strVersioActual = Globales.VersionApp;
                //NOMBRE DEL ARCHIVO DEL SERVIDOR.
                Globales.ArchivoServidor = "PcPay.msi.server";
                Globales.ArchivoInstalador = "PcPay.msi";
                Globales.ArchivoActualizar = "PcPay.exe";
                switch(Globales.TipoUpdate(strVersioActual,strVersion)){
                        //EN TODOS LOS CASOS ENTRA A MSI, YA QUE AHORA SE COMPILA LA DLL Y PCPAY CON VERSIONES IGUALES, POR LOS QUE AMBOS CAMBIAN
                    case "msi":
                        Globales.ArchivoServidor = "PcPay.msi.server";
                        Globales.ArchivoInstalador = "PcPay.msi";
                        Globales.ArchivoActualizar = "PcPay.exe";
                        Globales.TA = 2;
                        break;
                    case "exe":
                         Globales.ArchivoServidor = "PcPay.msi.server";
                        Globales.ArchivoInstalador = "PcPay.msi";
                        Globales.ArchivoActualizar = "PcPay.exe";
                        Globales.TA = 2;
                        break;
                    default:
                        Close();
                        break;
                }
                
                lblStatus.Content = "Haga clic en el botón Update para actualizar PcPay";
            }
            catch {
            }
        }

        private void cargado(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            System.Windows.Forms.DialogResult r = System.Windows.Forms.MessageBox.Show("¿Desea ejecutar el archivo descargado?", "Actualización", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
            if (System.Windows.Forms.DialogResult.Yes == r)
            {
                cerrando = false;
                this.Hide();
                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += new DoWorkEventHandler(ejecutarInstalacion);
                bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(instalacionCompletada);

                bg.RunWorkerAsync();

                ventana = new cargandoFormulario();
                ventana.ShowDialog();
            }
            else {
                cerrando = true;
            }
            lblStatus.Content = "Archivo descargado correctamente.";
            isComplete = true;
            Mouse.OverrideCursor = null;
        }

        private void instalacionCompletada(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Windows.Forms.DialogResult pregunta;
            pregunta = System.Windows.Forms.MessageBox.Show("La aplicación se ha instalado correctamente ¿Desea ejecutar programa?","Instalación completada",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Information);
            if (pregunta == System.Windows.Forms.DialogResult.Yes)
            {
                //Código para ejecutar la nueva versión de pcpay.. 
            }
            else {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void ejecutarInstalacion(object sender, DoWorkEventArgs e)
        {
            desintalador.actualizando();
        }

        private void cargando(object sender, DownloadProgressChangedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            PrgBar1.Value = e.ProgressPercentage;
            porcentaje.Content = e.ProgressPercentage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string C1, C2, Instalador, abierto;
                Instalador = Globales.ArchivoServidor.Substring(0,Globales.ArchivoServidor.IndexOf(".server"));
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                PrgBar1.Value = 0;
              //  Update();
                cargado(null,null);
            }
            catch { 
            
            }

        }

        private void Update()
        {
            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                string pathLocal = string.Empty;

                downloadActualizador();

                Mouse.OverrideCursor = null;
            }
            catch {
                System.Windows.Forms.MessageBox.Show("Error al actualizar.");
            }
        }

        private void downloadActualizador()
        {
            try
            {
                ruta = Globales.ip + "/pgs/jsp/cpagos/cargas/actualizador/" + Globales.ArchivoServidor;
                rutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.Recent) + "\\" + Globales.ArchivoInstalador;
                File.Delete(rutaArchivo);
                cliente.DownloadFileAsync(new Uri(ruta), rutaArchivo);  
            }
            catch {
                PrgBar1.Value = 0;
                lblStatus.Content = "Error de la descarga";
            }

        }

        public string ruta { get; set; }

        public string rutaArchivo { get; set; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(cerrando){
                Application.Current.Shutdown();
            }
        }

        public bool cerrando = true;
    }
}
