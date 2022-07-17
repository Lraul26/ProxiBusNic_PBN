using Android.App;
using Android.Content;
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
    internal class AdapterParadaEditar : BaseAdapter
    {
        Activity context;
        List<proxibusnicweb.ParadasWS> ListaParadas;

        public AdapterParadaEditar(Activity context, List<ParadasWS> listaParadas)
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
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }
    }
}