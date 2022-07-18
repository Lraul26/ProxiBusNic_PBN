using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Graphics;

using Android.Widget;
using ProxiBusNicPBN.proxibusnicweb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxiBusNicPBN.Clases
{
    internal class AdapaterDetalleParadaRL : BaseAdapter
    {
        Activity context;
        List<proxibusnicweb.ParadasWS> Listap;

        public AdapaterDetalleParadaRL(Activity context, List<ParadasWS> listap)
        {
            this.context = context;
            Listap = listap;
        }

        public override int Count => Listap.Count();

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //elemento devuelto
            var item = Listap[position];
            //Definimos el formato fila
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListaDetalleParadaRL, null);
            proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS(); proxibusnicweb.ParadasWS parada;

            var idruta = serve.ListarBusParada().Where(x => x.ParadaId == item.Id).FirstOrDefault().BusId;
            var numeroruta = serve.ListarBus().ToList().Where(x => x.Id == idruta).FirstOrDefault().NumeroRuta;
            var fotobus = serve.ListarBus().ToList().Where(x => x.Id == idruta).FirstOrDefault().FotoBus;

            TextView txt1 = view.FindViewById<TextView>(Resource.Id.nombreparada);
            txt1.Text = numeroruta;
           
            return view;
        }
      
    }
}