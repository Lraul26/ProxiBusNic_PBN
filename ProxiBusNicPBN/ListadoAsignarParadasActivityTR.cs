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
    [Activity(Label = "ListadoParadasActivity")]
    
    public class ListadoAsignarParadasActivityTR : Activity
    {
        int idRuta;
        ListView lvParadas;
        ImageButton btnEliminar, btnAgregar;
        Button btnEliminar, btnAgregar;
        TextView lblListadoParadas;
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();
        List<proxibusnicweb.ParadasWS> listaParadas=new List<proxibusnicweb.ParadasWS>();
        List<proxibusnicweb.BusParadaWS> paradasBus;
        List<proxibusnicweb.BusParadaWS >bpb= new List<proxibusnicweb.BusParadaWS>();
        private proxibusnicweb.ParadasWS buscarParada(int id)
        {
            return db.ListarParada().Where(p => p.Id==id).FirstOrDefault();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListadoAsignarParadasTR);
             idRuta = Intent.GetIntExtra("id", 0);

            proxibusnicweb.BusWS rutaSeleccionada = db.ListarBus().Where(b => b.Id == idRuta).FirstOrDefault();
            btnEliminar = FindViewById<ImageButton>(Resource.Id.btnEliminar);
            btnEliminar = FindViewById<Button>(Resource.Id.btnEliminar);
            btnEliminar.Click += BtnEliminar_Click;

            btnAgregar = FindViewById<ImageButton>(Resource.Id.btnAgregar);
            btnAgregar = FindViewById<Button>(Resource.Id.btnAgregar);
            btnAgregar.Click += BtnAgregar_Click;

            lblListadoParadas = FindViewById<TextView>(Resource.Id.tvtitulo);
            lblListadoParadas.Text = "Paradas de la ruta " + rutaSeleccionada.NumeroRuta;



            // SetContentView(Resource.Layout.ListadoParadasTR);
            lvParadas = FindViewById<ListView>(Resource.Id.lvParadas);

            //   paradasBus = db.ListarBusParada().Where(x => x.BusId == idRuta).ToList();
            cargarDatos();



            lvParadas.ItemClick += LvParadas_ItemClick;

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this,typeof(ListadoParadasAgregarActivityTR));
            i.PutExtra("id",idRuta);
            StartActivity(i);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (bpb.Count>0)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Eliminar");
                alert.SetMessage("¿Desea eliminar los " + bpb.Count + " registros?").SetPositiveButton("Sí", (senderAlert, args) =>
                {
                    foreach(proxibusnicweb.BusParadaWS b in bpb)
                 db.EliminarBusesParadas(b.Id);
                    Toast.MakeText(Application.Context, "Se han eliminado los registros", ToastLength.Short).Show();
                    cargarDatos();
                }).SetNegativeButton("No", (senderAlert, args) => { }).Show();

            }
            else
            {
                Toast.MakeText(Application.Context, "Debe seleccionar registros para eliminar", ToastLength.Short).Show();
            }

        }

        private void  cargarDatos()
        {
            paradasBus = db.ListarBusParada().Where(dp => dp.BusId == idRuta).ToList();
            List<string> listaParadasNombres = new List<string>();
            foreach (proxibusnicweb.BusParadaWS bp in paradasBus)
            {
                var p = buscarParada(bp.ParadaId);
                listaParadas.Add(p);
                listaParadasNombres.Add(p.Descripcion);

            }

            //lvParadas.Adapter = new ArrayAdapter(this,Android.Resource.Layout.SimpleListItemChecked, listaParadas.Select(x=>x.Descripcion).ToList());
            lvParadas.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemMultipleChoice, listaParadasNombres);
            lvParadas.ChoiceMode = ChoiceMode.Multiple;
   
        }

        private void LvParadas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           if (lvParadas.IsItemChecked(e.Position))
            {
                bpb.Add(paradasBus[e.Position]);
            }
            else
            {
                bpb.Remove(paradasBus[e.Position]);
            }
        
        }
    }
}