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
    [Activity(Label = "EditarSugerenciaRL")]
    public class EditarSugerenciaRL : Activity
    {
        proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.SugerenciaWS sugerencia = null;

        proxibusnicweb.SugerenciaWS sugerencias = new proxibusnicweb.SugerenciaWS();

        int id;
        TextView TxtParada;
        EditText txtSugerencia;
        ImageButton Aceptar, Eliminar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditarSugerenciaRL);

            id = Intent.GetIntExtra("id", 0);
            sugerencia = serve.ListarSugerencias().ToList().Where(x => x.Id == id).FirstOrDefault();

            TxtParada = (TextView)FindViewById(Resource.Id.txtParada);
            txtSugerencia = (EditText)FindViewById(Resource.Id.editarsugerencia);
            Eliminar = (ImageButton)FindViewById(Resource.Id.btnEliminarComent);
            Aceptar = (ImageButton)FindViewById(Resource.Id.btnAceptarComent);

            TxtParada.Text = sugerencia.DescripcionSugerencia;

            Aceptar.Click += Aceptar_Click;
            Eliminar.Click += Eliminar_Click;
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Eliminar Comentario");
            alert.SetMessage("¿Desea Eliminar este comentario?").SetPositiveButton("Sí", (senderAlert, args) =>
            {
                ISharedPreferences preferencia = Application.GetSharedPreferences("informacion", FileCreationMode.Private);
                ISharedPreferencesEditor editor = preferencia.Edit();
                editor.Clear();
                editor.Apply();
                this.Finish();

                serve.EliminaraSugerenciasCompleted += Serve_EliminaraSugerenciasCompleted;
                serve.EliminaraSugerenciasAsync(id);

                var item = new Intent(this, typeof(AgregarSugerenciaRL));
                StartActivity(item);

            }).SetNegativeButton("No", (senderAlert, args) => { }).Show();
        }
        private void Serve_EliminaraSugerenciasCompleted(object sender, proxibusnicweb.EliminaraSugerenciasCompletedEventArgs e)
        {
            if (!e.Result.respuesta)
            {
                Toast.MakeText(Application.Context, "No se Pudo Eliminar el comentario", ToastLength.Short).Show();
            }
            Toast.MakeText(Application.Context, "El comentario se elimino con exito", ToastLength.Short).Show();
        }

        private bool validar()
        {
            if (String.IsNullOrEmpty(txtSugerencia.Text.Trim()))
            {
                Toast.MakeText(Application.Context, "El campo Sugerencia no Puede ir Vacio", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var idparada = sugerencia.ParadaId;
                sugerencias.DescripcionSugerencia = txtSugerencia.Text;
                sugerencias.UsuarioCreacion = Clases.Global.Usuario.correo;
                sugerencias.ParadaId = idparada;
                serve.EditarSugerenciaCompleted += Serve_EditarSugerenciaCompleted;
                serve.EditarSugerenciaAsync(sugerencias);
               
            }
        }

        private void Serve_EditarSugerenciaCompleted(object sender, proxibusnicweb.EditarSugerenciaCompletedEventArgs e)
        {
            if (e.Result == -1 )
            {
                Toast.MakeText(Application.Context, "No se Pudo editar el comentario", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Application.Context, "comentario editado con exito", ToastLength.Short).Show();
            }
        }
    }
}