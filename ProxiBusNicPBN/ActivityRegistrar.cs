using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxiBusNicPBN
{
    [Activity(Label = "ActivityRegistrar")]
    public class ActivityRegistrar : Activity
    {
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();
        Button BtnSesio;
        ImageButton btnCrearCuenta;
        EditText txtCorreo, txtClaveUno, txtClaveDos;
        CheckBox cbxRecordar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registrar);
            // Create your application here
            BtnSesio = FindViewById<Button>(Resource.Id.btnIniciarSesion);
            btnCrearCuenta = FindViewById<ImageButton>(Resource.Id.btnCrearCuenta);
            btnCrearCuenta.Click += btnCrearCuenta_Click;

            txtCorreo = FindViewById<EditText>(Resource.Id.txtCorreo);
            txtClaveUno = FindViewById<EditText>(Resource.Id.txtPrimerClave);
            txtClaveDos = FindViewById<EditText>(Resource.Id.txtSegundaClave);

            cbxRecordar = FindViewById<CheckBox>(Resource.Id.cbxMantenerSesion);

        }
        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {

            try
            {
                if (validarVacios())
                {
                    var resultado = db.CrearUsuario(txtCorreo.Text.Trim(), txtClaveUno.Text);
                    if (resultado.respuesta)
                    {
                        Clases.Global.Usuario.usuarioAnonimo = db.RolAnonimo(txtCorreo.Text.Trim());
                        if (cbxRecordar.Checked)
                        {

                            ISharedPreferences preferencia = Application.Context.GetSharedPreferences("informacion", FileCreationMode.Private);
                            ISharedPreferencesEditor editor = preferencia.Edit();
                            editor.PutString("correo", txtCorreo.Text.Trim());
                            editor.PutString("clave", txtClaveUno.Text);
                            editor.PutBoolean("recordar", cbxRecordar.Checked);
                            editor.PutBoolean("usuarioAnonimo", Clases.Global.Usuario.usuarioAnonimo);
                            editor.Apply();

                        }
                        else
                        {
                            
                            Clases.Global.Usuario.correo = txtCorreo.Text.Trim();
                            Clases.Global.Usuario.clave = txtClaveUno.Text;
                        }

                        Toast.MakeText(Application.Context, resultado.mensaje, ToastLength.Short).Show();
                        this.Finish();
                        Intent res = new Intent(this, typeof(MainActivity));
                        StartActivity(res);
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, resultado.mensaje, ToastLength.Short).Show();
                    }
                }

            }
            catch
            {
                Toast.MakeText(Application.Context, "Verifique su conexión a internet", ToastLength.Short).Show();
            }
        }
        private bool validarVacios()
        {
            if (String.IsNullOrEmpty(txtCorreo.Text.Trim()))
            {
                Toast.MakeText(Application.Context, "Ingrese un correo", ToastLength.Short).Show();
                return false;
            }
            if (String.IsNullOrEmpty(txtClaveUno.Text) || String.IsNullOrEmpty(txtClaveDos.Text))
            {
                Toast.MakeText(Application.Context, "Debe de ingresar una contraseña", ToastLength.Short).Show();
                return false;
            }
            if (!txtClaveUno.Text.Equals(txtClaveDos.Text))
            {
                Toast.MakeText(Application.Context, "Las contraseñas deben coincidir", ToastLength.Short).Show();
                return false;
            }


            return true;
        }
    }
}