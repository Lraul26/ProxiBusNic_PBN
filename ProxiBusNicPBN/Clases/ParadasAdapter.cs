using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxiBusNicPBN.Clases
{
    internal class ParadasAdapter : BaseAdapter
    {

        Activity context;
        List<proxibusnicweb.ParadasWS> lista;

        public ParadasAdapter(Activity context, List<proxibusnicweb.ParadasWS> lista)
        {
            this.context = context;
            this.lista = lista;
        }

        public override int Count => lista.Count;

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
            var item = lista[position];
            //Definimos el formato fila
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
    
            TextView txtruta = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            txtruta.Text = "Parada: " + item.Descripcion.ToString();
            txtruta.SetTextColor(Color.LightGray);



            TextView txtAlias = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            if (String.IsNullOrEmpty(item.Alias))
            {
                txtAlias.Text = "Alias: N/A";
            }
            else
            {
                txtAlias.Text = "Alias: " + item.Alias.ToString();
            }

            txtAlias.SetTextColor(Color.LightGray);
            return view;

        }
    }
}