using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using ProxiBusNicPBN.proxibusnicweb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxiBusNicPBN.Clases
{
    internal class AdapterParadaRL : BaseAdapter
    {
        Activity context;
        List<proxibusnicweb.ParadasWS> ListaParada;

        public AdapterParadaRL(Activity context, List<ParadasWS> listaParada)
        {
            this.context = context;
            ListaParada = listaParada;
        }

        public override int Count => ListaParada.Count();

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
            var item = ListaParada[position];
            //Definimos el formato fila
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS();

            TextView TxtSugerencia = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            TxtSugerencia.Text = "Descripcion: " + item.Descripcion.ToString();
            TxtSugerencia.SetTextColor(Color.LightGray);

            TextView TxtUsuario = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            TxtUsuario.Text = "Total Ruta: " + serve.ListarBusParada().Where(x => x.ParadaId == item.Id).Count(x=> x.ParadaId == item.Id).ToString();
            TxtUsuario.SetTextColor(Color.LightGray);
            return view;
        }
    }
}