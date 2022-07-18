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
    [Activity(Label = "AccionesAvanzadasRL")]
    public class AccionesAvanzadasRL : Activity
    {
        ImageButton AgregarBus, EditarBus, AgregarParada, EditarParada;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AccionesAvanzadasRL);
            // Create your application here
            AgregarBus = FindViewById<ImageButton>(Resource.Id.btnAgrgarBus);
            EditarBus = FindViewById<ImageButton>(Resource.Id.btnEditarBus);
            AgregarParada = FindViewById<ImageButton>(Resource.Id.btnAgregarParada);
            EditarParada = FindViewById<ImageButton>(Resource.Id.btnEditarParada);

            AgregarParada.Click += AgregarParada_Click;
            AgregarBus.Click += AgregarBus_Click;
            EditarBus.Click += EditarBus_Click;
            EditarParada.Click += EditarParada_Click;
        }
        private void EditarParada_Click(object sender, EventArgs e)
        {
            var item = new Intent(this, typeof(ListaParadasEditarRL));
            StartActivity(item);
        }

        private void EditarBus_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(ListadoEditarRutasActivityTR));           
            StartActivity(i);
        }

        private void AgregarBus_Click(object sender, EventArgs e)
        {
            var item = new Intent(this, typeof(AgregarRutasActivityTR));
            StartActivity(item);
        }

        private void AgregarParada_Click(object sender, EventArgs e)
        {
           var item = new Intent(this, typeof(AgregarParadaRL));
            StartActivity(item);
        }
    }
}