using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using ProxiBusNicPBN.proxibusnicweb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxiBusNicPBN.Clases
{
    internal class AdapterSugerenciaRL : BaseAdapter
    {
        Activity context;
        List<proxibusnicweb.SugerenciaWS> sugerencia;

        public AdapterSugerenciaRL(Activity context, List<SugerenciaWS> sugerencia)
        {
            this.context = context;
            this.sugerencia = sugerencia;
        }

        public override int Count => sugerencia.Count();

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
            var item = sugerencia[position];
            //Definimos el formato fila
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS();

            TextView TxtSugerencia = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            TxtSugerencia.Text = "Comentario: " + item.DescripcionSugerencia.ToString();
            TxtSugerencia.SetTextColor(Color.LightGray);

            TextView TxtUsuario = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            TxtUsuario.Text = "Parada: " + serve.ListarParada().Where(x => x.Id == item.ParadaId).FirstOrDefault().Descripcion;
            TxtUsuario.SetTextColor(Color.LightGray);
            return view;
        }
    }
}