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
    internal class AdapterBusRL : BaseAdapter
    {

        Activity context;
        List<proxibusnicweb.BusWS> listabus;

        public AdapterBusRL(Activity context, List<BusWS> listabus)
        {
            this.context = context;
            this.listabus = listabus;
        }

        public override int Count => listabus.Count();

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
            var item = listabus[position];
            //Definimos el formato fila
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListaBusRowRL, null);
            proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS();

            

            ImageView foto = view.FindViewById<ImageView>(Resource.Id.imageView1);
            if (item.FotoBus != null)
            {
                foto.SetImageBitmap(byteArrayToImage(item.FotoBus));
            }
            else
            {
                foto.SetImageResource(Resource.Drawable.Ruta);
            }
            TextView txt1 = view.FindViewById<TextView>(Resource.Id.textView1);
            txt1.Text = item.NumeroRuta;

            return view;
        }
        public Android.Graphics.Bitmap byteArrayToImage(byte[] source)
        {
            Android.Graphics.Bitmap mp = BitmapFactory.DecodeByteArray(source, 0, source.Length);
            return mp;
        }
    }
}