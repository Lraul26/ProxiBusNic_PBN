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
    
    public class ListadoParadasActivityTR : Activity
    {
        ListView lvParadas;
        TextView lblListadoParadas;
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();
        List<proxibusnicweb.ParadasWS> listaParadas=new List<proxibusnicweb.ParadasWS>();
        List<proxibusnicweb.BusParadaWS> paradasBus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListadoParadasTR);
            int idRuta=Intent.GetIntExtra("id",0); 

               proxibusnicweb.BusWS rutaSeleccionada =db.ListarBus().Where(b => b.Id == idRuta).FirstOrDefault();
            
            lblListadoParadas = FindViewById<TextView>(Resource.Id.tvtitulo);
            lblListadoParadas.Text ="Paradas de la ruta "+ rutaSeleccionada.NumeroRuta;
         
           // SetContentView(Resource.Layout.ListadoParadasTR);
            lvParadas= FindViewById<ListView>(Resource.Id.lvParadas);

            paradasBus = db.ListarBusParada().Where(x => x.BusId == idRuta).ToList();

           

            if (Clases.Global.Usuario.usuarioAnonimo)
            {
                for (int i =0;i<paradasBus.Count;i++)
                {
                    int id=paradasBus[i].ParadaId;
                    listaParadas.Add(db.ListarParadaActivas().Where(p=>p.Id==id).FirstOrDefault());
                }
                
            }
            else
            {
                for (int i = 0; i < paradasBus.Count; i++)
                {
                    int id = paradasBus[i].ParadaId;
                    listaParadas.Add(db.ListarParada().Where(p => p.Id == id).FirstOrDefault());
                }
               
            }
            


            lvParadas.Adapter = new Clases.ParadasBusAdapter(this, listaParadas);

            

            lvParadas.ItemClick += LvParadas_ItemClick;



        }

        private void LvParadas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent i = new Intent(this, typeof(DetalleParadaActivityTR));


            //    var idBus = paradasBus[lvParadas.SelectedItemPosition];
            int id = listaParadas[e.Position].Id;

            i.PutExtra("id", id);
            StartActivity(i);
        }
    }
}