
using mitUpdate.configuracion;
using PcPay.Forms.Formularios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mitUpdate.formulario
{
    /// <summary>
    /// Lógica de interacción para frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtUser.Text)){
                System.Windows.Forms.MessageBox.Show("Favor ingresar usuario","Usuario",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Warning);
                txtUser.Focus();
                return;
            }
            if(string.IsNullOrWhiteSpace(txtPwd.Password)){
                System.Windows.Forms.MessageBox.Show("Favor ingresar contraseña", "Contraseña", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                txtPwd.Focus();
                return;
            }

            if (ingresarSistema())
            {
                Mouse.OverrideCursor = null;
                frmUpdate actualizar = new frmUpdate();
                actualizar.Show();
                this.Hide();
                
            }
            else {
                System.Windows.Forms.MessageBox.Show("Error al ingresar a la actualización", "Mit update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                txtUser.Text = "";
                txtPwd.Password = "";
                txtUser.Focus();
            }
        }

        private bool ingresarSistema()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Globales.cpIntegreEMV.dbgSetUrl(Globales.ip);
            return Globales.cpIntegreEMV.dbgLoginUser(txtUser.Text,txtPwd.Password.ToUpper());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Globales.ip = ConfigurationManager.AppSettings["ip"];
            Globales.ipPub = ConfigurationManager.AppSettings["ipPub"];
            Globales.ipPrep = ConfigurationManager.AppSettings["ipPrep"];
            Globales.ipvip = ConfigurationManager.AppSettings["ipvip"];
            Globales.ipPoints2 = ConfigurationManager.AppSettings["ipPoints2"];

            Globales.ipfe = ConfigurationManager.AppSettings["ipfe"];
            Globales.ipLoginInstalador = ConfigurationManager.AppSettings["ipLoginInstalador"];
            Globales.ipFirmaPanel = ConfigurationManager.AppSettings["ipFirmaPanel"];
            Globales.ipKeyWeb = ConfigurationManager.AppSettings["ipKeyWeb"];
            Globales.VersionApp = ConfigurationManager.AppSettings["versionApp"];
            Globales.VersionPcPay = ConfigurationManager.AppSettings["versionPcPay"];


            Globales.ip = Globales.DecryptString(Globales.ip, Globales.KEY_RC4, true);
            Globales.ipPub = Globales.DecryptString(Globales.ipPub, Globales.KEY_RC4, true);
            Globales.ipPrep = Globales.DecryptString(Globales.ipPrep, Globales.KEY_RC4, true);
            Globales.ipvip = Globales.DecryptString(Globales.ipvip, Globales.KEY_RC4, true);
            Globales.ipPoints2 = Globales.DecryptString(Globales.ipPoints2, Globales.KEY_RC4, true);
            Globales.ipfe = Globales.DecryptString(Globales.ipfe, Globales.KEY_RC4, true);
            Globales.ipLoginInstalador = Globales.DecryptString(Globales.ipLoginInstalador, Globales.KEY_RC4, true);
            Globales.ipFirmaPanel = Globales.DecryptString(Globales.ipFirmaPanel, Globales.KEY_RC4, true);
            Globales.ipKeyWeb = Globales.DecryptString(Globales.ipKeyWeb, Globales.KEY_RC4, true);
            txtUser.Focus();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Button_Click_1(null,null);
            Mouse.OverrideCursor = null;
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try {
                Button_Click(null, null);
            }
            catch
            {
            }
            Mouse.OverrideCursor = null;
        }
    }
}
