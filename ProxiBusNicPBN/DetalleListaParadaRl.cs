using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
namespace ProxiBusNicPBN
{
    [Activity(Label = "Activity1")]
    public class DetalleListaParadaRl : Activity
    {
        proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS();
        List<proxibusnicweb.BusWS> listabus = new List<proxibusnicweb.BusWS>();
        List<proxibusnicweb.BusParadaWS> paradasBus;

      
        int id;
        ListView lista;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.listaRL);

            id = Intent.GetIntExtra("id", 0);
            proxibusnicweb.ParadasWS parada = serve.ListarParada().Where(x => x.Id == id).FirstOrDefault();

            lista = (ListView)FindViewById(Resource.Id.listabus);

            paradasBus = serve.ListarBusParada().Where(x => x.ParadaId == id).ToList();

            if (Clases.Global.Usuario.usuarioAnonimo)
            {
                for (int i = 0; i < paradasBus.Count; i++)
                {
                    int id = paradasBus[i].BusId;
                    listabus.Add(serve.ListarBusActivos().Where(p => p.Id == id).FirstOrDefault());
                }

            }
            else
            {
                for (int i = 0; i < paradasBus.Count; i++)
                {
                    int id = paradasBus[i].BusId;
                    listabus.Add(serve.ListarBus().Where(p => p.Id == id).FirstOrDefault());
                }

            }
            lista.Adapter = new Clases.AdapterBusRL(this, listabus);
        }

    }
}