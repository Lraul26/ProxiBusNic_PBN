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
    [Activity(Label = "ActivityLogin")]
    public class ActivityLogin : Activity
    {
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();

        Button btnregistrar;
        ImageButton btnentrar;
        EditText edtemail, edtpass;
        TextView tvemail, tvpass, tvresultado;
        CheckBox cbxRecordar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            // Create your application here

            tvemail = FindViewById<TextView>(Resource.Id.tvemail);
            tvpass = FindViewById<TextView>(Resource.Id.tvpass);
            tvresultado = FindViewById<TextView>(Resource.Id.tvresultado);
            edtemail = FindViewById<EditText>(Resource.Id.edtemail);
            edtpass = FindViewById<EditText>(Resource.Id.edtpass);
            btnregistrar = FindViewById<Button>(Resource.Id.btnregistrar);
            btnentrar = FindViewById<ImageButton>(Resource.Id.btnentrar);
            btnregistrar.Click += Btnregistrar_Click;
            btnentrar.Click += Btnentrar_Click;

            cbxRecordar = FindViewById<CheckBox>(Resource.Id.cbxRecordar);
        }
        private void Btnentrar_Click(object sender, EventArgs e)
        {


            string correo = edtemail.Text.Trim();
            string clave = edtpass.Text;
            if (correo.Length == 0 && clave.Length == 0)
            {
                Toast.MakeText(Application.Context, "Debe de llenar ambos campos\npara iniciar seisón", ToastLength.Short).Show();
            }

            else
            {
                try
                {
                    if (db.Login(correo, clave))
                    {
                        Clases.Global.Usuario.usuarioAnonimo = db.RolAnonimo(correo);
                        if (cbxRecordar.Checked)
                        {

                            ISharedPreferences preferencia = Application.Context.GetSharedPreferences("informacion", FileCreationMode.Private);
                            ISharedPreferencesEditor editor = preferencia.Edit();
                            editor.PutString("correo", correo);
                            editor.PutString("clave", clave);
                            editor.PutBoolean("recordar", cbxRecordar.Checked);
                            editor.PutBoolean("usuarioAnonimo", Clases.Global.Usuario.usuarioAnonimo);
                            editor.Apply();


                        }

                        Clases.Global.Usuario.correo = correo;
                        Clases.Global.Usuario.clave = clave;


                        var res = new Intent(this, typeof(MainActivity));
                        StartActivity(res);
                        this.Finish();
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "No se ha podido verificar su cuenta\ningrese sus datos nuevamente", ToastLength.Short).Show();
                    }

                }
                catch
                {
                    Toast.MakeText(Application.Context, "Verifique su conexión a internet", ToastLength.Short).Show();
                }

            }
        }

        private void Btnregistrar_Click(object sender, EventArgs e)
        {
            var res = new Intent(this, typeof(ActivityRegistrar));
            StartActivity(res);
        }
    }
}