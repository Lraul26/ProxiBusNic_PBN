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
    [Activity(Label = "ListadoParadaRL")]
    public class ListadoParadaRL : Activity
    {
        proxibusnicweb.ProxiBusNicWS servicio = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.ParadasWS parada;
        List<proxibusnicweb.ParadasWS> listapa;

        ListView listaparada;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListaParadaRL);

            listaparada = (ListView)FindViewById(Resource.Id.ListaParada);
            listaparada.ItemClick += Listaparada_ItemClick;
         

            if (Clases.Global.Usuario.usuarioAnonimo)
            {
                listapa = servicio.ListarParadaActivas().ToList();
                listaparada.Adapter = new Clases.AdapterParadaRL(this, servicio.ListarParadaActivas().ToList());
            }
            else
            {
                listapa = servicio.ListarParada().ToList();
                listaparada.Adapter = new Clases.AdapterParadaRL(this, servicio.ListarParada().ToList());
            }
        }

        private void Listaparada_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = new Intent(this, typeof(DetalleListaParadaRl));
            parada = listapa[e.Position];
            item.PutExtra("id", parada.Id);
            StartActivity(item);
            Toast.MakeText(Application.Context, "Usted ha seleccionado: " + parada.Descripcion, ToastLength.Short).Show();
        }
    }
}