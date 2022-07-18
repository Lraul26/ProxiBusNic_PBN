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
    [Activity(Label = "ListarRutasActivity")]
    public class ListadoRutasActivityTR : Activity
    {
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();
        ListView lvRutas;
        List<proxibusnicweb.BusWS> listaBus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListadoRutasTR);

            lvRutas = FindViewById<ListView>(Resource.Id.lvBuses);

          
            if (Clases.Global.Usuario.usuarioAnonimo)
            {

                listaBus = db.ListarBusActivos().ToList();
                lvRutas.Adapter = new Clases.RutasAdapter(this, listaBus);
            }
            else
            {
                listaBus = db.ListarBus().ToList();
                lvRutas.Adapter = new Clases.RutasAdapter(this, listaBus);
                     
            }
        
            lvRutas.ItemClick += LvRutas_ItemClick;
        }

        private void LvRutas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
         Intent i = new Intent(this, typeof(ListadoParadasActivityTR));
            proxibusnicweb.BusWS bus;
            bus = listaBus[e.Position];
            i.PutExtra("id", bus.Id);
            StartActivity(i);
        }
    }
}