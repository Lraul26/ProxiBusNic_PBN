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
    [Activity(Label = "ListadoParadasAgregarActivityTR")]
    public class ListadoParadasAgregarActivityTR : Activity
    {
        int idRuta;
        ListView lvParadas;
        ImageButton btnAgregarParadas;
        Button btnAgregarParadas;
         
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();
        List<proxibusnicweb.ParadasWS> listaParadas = new List<proxibusnicweb.ParadasWS>();
        //List<proxibusnicweb.BusParadaWS> paradasBus;
        List<proxibusnicweb.BusParadaWS> bpb = new List<proxibusnicweb.BusParadaWS>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            idRuta = Intent.GetIntExtra("id", 0);
            if (idRuta>0)
            {
                SetContentView(Resource.Layout.ListadoParadasAgregarTR);
                lvParadas = FindViewById<ListView>(Resource.Id.lvParadas);

                cargarDatos();

                lvParadas.ItemClick += LvParadas_ItemClick;

                btnAgregarParadas = FindViewById<ImageButton>(Resource.Id.btnAgregarParadas);
                btnAgregarParadas = FindViewById<Button>(Resource.Id.btnAgregarParadas);
                btnAgregarParadas.Click += BtnAgregarParadas_Click;
            }
            else
            {
                Toast.MakeText(Application.Context, "Debe seleccionar una ruta válida para asignarle paradas", ToastLength.Short).Show();
            }



        }

        private void BtnAgregarParadas_Click(object sender, EventArgs e)
        {
            if (bpb.Count==0)
            {
                Toast.MakeText(Application.Context, "Debe seleccionar al menos una parada para guardar", ToastLength.Short).Show();
            }
            else
            {
                foreach (proxibusnicweb.BusParadaWS bp in bpb)
                {
                    db.AgregarBusParadas(bp);
                
                }
                Toast.MakeText(Application.Context, "Se han registrado las paradas", ToastLength.Short).Show();
            }
            this.Finish();
        }

        private void LvParadas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            proxibusnicweb.BusParadaWS i=new proxibusnicweb.BusParadaWS();
            i.BusId = idRuta;
            i.ParadaId = listaParadas[e.Position].Id;
            if (lvParadas.IsItemChecked(e.Position))
            {
                 bpb.Add(i);
            }
            else
            {   bpb.Remove(i);
            }
        }

        //private proxibusnicweb.ParadasWS buscarParada(int id)
        //{
        //    return db.ListarParada().Where(p => p.Id == id).FirstOrDefault();
        //}

        private void cargarDatos()
        {
          //  paradasBus = db.ListarBusParada().Where(dp => dp.BusId == idRuta).ToList();
          //  List<string> listaParadasNombres = new List<string>();
            listaParadas = db.ListarParada().ToList();
          //  foreach (proxibusnicweb.BusParadaWS bp in paradasBus)
        //    {
               // var p = buscarParada(bp.ParadaId);
              //  listaParadas.Add(p);
             //   listaParadasNombres.Add(p.Descripcion);

        //    }

            //lvParadas.Adapter = new ArrayAdapter(this,Android.Resource.Layout.SimpleListItemChecked, listaParadas.Select(x=>x.Descripcion).ToList());
            lvParadas.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemMultipleChoice, listaParadas.Select(p=>p.Descripcion).ToList());
            lvParadas.ChoiceMode = ChoiceMode.Multiple;

        }
    }
}