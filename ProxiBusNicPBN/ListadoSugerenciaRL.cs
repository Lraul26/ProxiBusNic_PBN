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
    [Activity(Label = "ListadoSugerenciaRL")]
    public class ListadoSugerenciaRL : Activity
    {
        proxibusnicweb.ProxiBusNicWS servicio = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.SugerenciaWS sugerencia = null;

        ListView lvsugerencia;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListaSugerenciaRL);

            lvsugerencia = (ListView)FindViewById(Resource.Id.lvSugerencia);
            lvsugerencia.ItemClick += Lvsugerencia_ItemClick;
            listaPorUsuario();
        }
        private void listaPorUsuario()
        {
            var correo = Clases.Global.Usuario.correo;
            lvsugerencia.Adapter = new Clases.AdapterSugerencia(this, servicio.ListarSugerencias().Where(x => x.UsuarioCreacion == correo).ToList());
        }
        private void Lvsugerencia_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = new Intent(this, typeof(EditarSugerenciaRL));

            sugerencia = servicio.ListarSugerencias().ToList()[e.Position];
            item.PutExtra("id", sugerencia.Id);
            StartActivity(item);
            Toast.MakeText(Application.Context, "Ha Seleccionado: " + sugerencia.DescripcionSugerencia, ToastLength.Short).Show();
        }

       
    }
}