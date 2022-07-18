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
    [Activity(Label = "ListaParadasEditarRL")]
    public class ListaParadasEditarRL : Activity
    {
        proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.ParadasWS paradas = null;
        ListView listaparada;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListadoParadaEditarRL);
            // Create your application here

            listaparada = (ListView)FindViewById(Resource.Id.listaparada);
            listaparada.Adapter = new Clases.AdapterCometarioParadaRL(this, serve.ListarParada().ToList());
            listaparada.ItemClick += Listaparada_ItemClick;
        }

        private void Listaparada_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = new Intent(this, typeof(EditarParadaRL));
            paradas = serve.ListarParada().ToList()[e.Position];
            item.PutExtra("id", paradas.Id);
            StartActivity(item);
            Toast.MakeText(Application.Context, "usted ha Seleccionado: " + paradas.Descripcion, ToastLength.Short).Show();
        }
    }
}