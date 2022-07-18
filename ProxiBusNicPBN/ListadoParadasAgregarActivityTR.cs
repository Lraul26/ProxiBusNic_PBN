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
    [Activity(Label = "ListadoParadasAgregarActivityTR")]
    public class ListadoParadasAgregarActivityTR : Activity
    {
        int idRuta;
        ListView lvParadas;
        Button btnEliminar, btnAgregar;
        TextView lblListadoParadas;
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();
        List<proxibusnicweb.ParadasWS> listaParadas = new List<proxibusnicweb.ParadasWS>();
        List<proxibusnicweb.BusParadaWS> paradasBus;
        List<proxibusnicweb.BusParadaWS> bpb = new List<proxibusnicweb.BusParadaWS>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            idRuta = Intent.GetIntExtra("id", 0);
            if (idRuta>0)
            {
                SetContentView(Resource.Layout.ListadoParadasAgregarTR);
                lvParadas = FindViewById<ListView>(Resource.Id.lvParadas);



            }
            else
            {
                Toast.MakeText(Application.Context, "Debe seleccionar una ruta válida para asignarle paradas", ToastLength.Short).Show();
            }



        }
    }
}