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
    [Activity(Label = "AgregarComentarioActivity")]
    public class AgregarSugerenciaRL : Activity
    {
        /// <summary>
        /// Servcios web
        /// </summary>
        proxibusnicweb.ProxiBusNicWS servicio = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.ParadasWS paradaSeleccionada = null;
        proxibusnicweb.ProxiBusNicWS servi = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.SugerenciaWS sugerencia = new proxibusnicweb.SugerenciaWS();
        //---------------------------------------------------------------------------------//
        /// <summary>
        /// Atributos
        /// </summary>
        /// <param name="savedInstanceState"></param>
        ListView lvparada;
        EditText comentarios;
        ImageButton Aceptar, Cancelar, Editar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AgregarSugerenciaRL);
            // Create your application here

            lvparada = (ListView)FindViewById(Resource.Id.lvoaradaCom);
            comentarios = (EditText)FindViewById(Resource.Id.txtRegistrarComentario);
            Aceptar = (ImageButton)FindViewById(Resource.Id.bntAceptarCom);
            Cancelar = (ImageButton)FindViewById(Resource.Id.btnCancelarCom);
            Editar = (ImageButton)FindViewById(Resource.Id.btnEditarCom);

            lvparada.Adapter = new Clases.AdapterCometarioParada(this, servicio.ListarParadaActivas().ToList());
            lvparada.ItemClick += Lvparada_ItemClick;

            Aceptar.Click += Aceptar_Click;
            Cancelar.Click += Cancelar_Click;
            Editar.Click += Editar_Click;
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            var item = new Intent(this, typeof(ListadoSugerenciaRL));
            StartActivity(item);
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                sugerencia.DescripcionSugerencia = comentarios.Text;
                sugerencia.UsuarioCreacion = Clases.Global.Usuario.correo;
                sugerencia.ParadaId = paradaSeleccionada.Id;
                servi.AgregarSugerencia(sugerencia);
                Toast.MakeText(Application.Context, "Registro de Sugerencia Exitoso", ToastLength.Short).Show();
                limpiar();
            }
        }

        private void Lvparada_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            paradaSeleccionada = servicio.ListarParadaActivas().ToList()[e.Position];
            Toast.MakeText(Application.Context, "Usted ha seleccionado: " + paradaSeleccionada.Descripcion, ToastLength.Short).Show();
        }
        private bool Validar()
        {
            if (String.IsNullOrEmpty(comentarios.Text.Trim()))
            {
                Toast.MakeText(Application.Context, "Ingrese un Comentario", ToastLength.Short).Show();
                return false;
            }
            if (paradaSeleccionada == null)
            {
                Toast.MakeText(Application.Context, "Seleccione una Parada", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            comentarios.Text = String.Empty;
        }
    }
}