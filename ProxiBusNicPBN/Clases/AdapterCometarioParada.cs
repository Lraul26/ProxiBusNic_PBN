using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProxiBusNicPBN.proxibusnicweb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxiBusNicPBN.Clases
{
    internal class AdapterCometarioParada : BaseAdapter
    {
        Activity context;
        List<proxibusnicweb.ParadasWS> ListaParadas;

        public AdapterCometarioParada(Activity context, List<ParadasWS> listaParadas)
        {
            this.context = context;
            ListaParadas = listaParadas;
        }

        public override int Count => ListaParadas.Count();

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
            //Elemento devuelto
            var item = ListaParadas[position];
            //Formato
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            TextView txtDescripcion = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            txtDescripcion.Text = item.Descripcion;
            txtDescripcion.SetTextColor(Color.LightGray);

            TextView txtDescripcion2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            txtDescripcion2.Text = "Lat: " + item.Latitud + " Long: " + item.Longitud;
            txtDescripcion2.SetTextColor(Color.LightGray);
            return view;
        }
    }
}