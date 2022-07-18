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

namespace ProxiBusNicPBN
{
    [Activity(Label = "DetalleParadaActivity")]
    public class DetalleParadaActivityTR : Activity
    {
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.ParadasWS parada;
        TextView NomParada, txtAlias, txtUbicacion;
        ImageButton btnIr;
        Button btnIr;
        ImageView imgView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetalleParadaTR);


            int id = Intent.GetIntExtra("id", 0);
            parada = db.ListarParada().Where(x => x.Id == id).FirstOrDefault();

            NomParada = FindViewById<TextView>(Resource.Id.txtparada);
            txtAlias = FindViewById<TextView>(Resource.Id.txtAlias);
            txtUbicacion = FindViewById<TextView>(Resource.Id.txtUbicacion);
            NomParada.Text = parada.Descripcion;
            imgView = FindViewById<ImageView>(Resource.Id.imgParada);
            btnIr = FindViewById<ImageButton>(Resource.Id.btnIr);
            btnIr = FindViewById<Button>(Resource.Id.btnIr);
            btnIr.Click += BtnIr_Click;

            if (parada.FotoParada != null)
            {
                imgView.SetImageBitmap(byteArrayToImage(parada.FotoParada));
            }
            else
            {
                imgView.SetImageResource(Resource.Drawable.ParadaPorDefecto);
            }
            if (String.IsNullOrEmpty(parada.Alias))
            {
                txtAlias.Text = "Alias: N/A";
            }
            else
            {
                txtAlias.Text = parada.Alias;
            }

            if (String.IsNullOrEmpty(parada.Longitud) && String.IsNullOrEmpty(parada.Latitud))
            {
                txtUbicacion.Text = "Ubicación\nN/A";
                txtUbicacion.Text = "Ubicación: N/A";
            }
            else
            {
                txtAlias.Text = "Ubicacion\n\nLatitud: " + parada.Latitud + " Longitud" + parada.Longitud;
                txtAlias.Text = "Ubicacion: Latitud: " + parada.Latitud + " Longitud" + parada.Longitud;
            }

        }

        private void BtnIr_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(parada.Latitud))
            {
                Toast.MakeText(Application.Context, "Debe de haber una latitud y una longitud para ir a la ubicación", ToastLength.Short).Show();
            }
            else
            {
                Intent i = new Intent(Intent.ActionView);
                string location = "geo: " + parada.Latitud + ", " + parada.Longitud;
                i.SetData(Android.Net.Uri.Parse(location));
                StartActivity(i);
            }
        }

        public Android.Graphics.Bitmap byteArrayToImage(byte[] source)
        {
            
            Android.Graphics.Bitmap mp = BitmapFactory.DecodeByteArray(source, 0, source.Length);
            return mp;
        }

    }
}