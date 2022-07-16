using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxiBusNicPBN
{
    [Activity(MainLauncher = true, Theme = "@style/SplashTema", Icon = "@drawable/OriginalLogo", NoHistory = true)]
    public class Splash : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ISharedPreferences preferencia = Application.GetSharedPreferences("informacion", FileCreationMode.Private);
            bool recordar = preferencia.GetBoolean("recordar", false);
            Clases.Global.Usuario.usuarioAnonimo = preferencia.GetBoolean("usuarioAnonimo", false);
            Clases.Global.Usuario.correo = preferencia.GetString("correo", "");
            Clases.Global.Usuario.clave =  preferencia.GetString("clave", "");
            Toast.MakeText(Application.Context, ""+Clases.Global.Usuario.usuarioAnonimo, ToastLength.Short).Show();
            if (recordar)
            {             
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            else
            {
                Clases.Global.Usuario.usuarioAnonimo = false;
                Clases.Global.Usuario.correo = null;
                Clases.Global.Usuario.clave =null;

                ISharedPreferencesEditor editor = preferencia.Edit();
                editor.Clear();
                editor.Apply();

                Intent intent = new Intent(this, typeof(ActivityLogin));
                StartActivity(intent);
            }
            this.Finish();
        }
    }
}