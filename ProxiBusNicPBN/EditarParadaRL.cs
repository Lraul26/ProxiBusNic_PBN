using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProxiBusNicPBN
{
    [Activity(Label = "EditarParadaRL")]
    public class EditarParadaRL : Activity
    {
        proxibusnicweb.ProxiBusNicWS serve = new proxibusnicweb.ProxiBusNicWS();
        proxibusnicweb.ParadasWS parada = null;
        proxibusnicweb.ParadasWS paradas = new proxibusnicweb.ParadasWS();

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        ImageButton aceptar, eliminar, camara, galeria, atras;
        EditText Descripcion, Alias, longitud, latitud;
        ImageView Foto;
        private byte[] bitmapData;
        bool Activo = true;
        int id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditarParadaRL);
            // Create your application here

            id = Intent.GetIntExtra("id", 0);
            parada = serve.ListarParada().ToList().Where(x => x.Id == id).FirstOrDefault();

            aceptar = (ImageButton)FindViewById(Resource.Id.btnEditarAceptar);
            eliminar = (ImageButton)FindViewById(Resource.Id.btnEditarCancelar);
            camara = (ImageButton)FindViewById(Resource.Id.btnEditarCamara);
            galeria = (ImageButton)FindViewById(Resource.Id.btnEditarfoto);
            atras = (ImageButton)FindViewById(Resource.Id.btnregresar);

            Descripcion = (EditText)FindViewById(Resource.Id.etEditarParada);
            Alias = (EditText)FindViewById(Resource.Id.etEditarAlias);
            longitud = (EditText)FindViewById(Resource.Id.etEditarlatitud);
            latitud = (EditText)FindViewById(Resource.Id.etEditarlongitud);

            Foto = (ImageView)FindViewById(Resource.Id.ivEditarParada);

            camara.Click += Camara_Click;
            eliminar.Click += Eliminar_Click;
            galeria.Click += Galeria_Click;
            atras.Click += Atras_Click;
            aceptar.Click += Aceptar_Click;

            Descripcion.Text = parada.Descripcion;
            Alias.Text = parada.Alias;

        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (validarVacios())
            {
                ////si Alisas, longitud y latitud viene vacio//
                //if (Alias.Text == String.Empty && longitud.Text == String.Empty && latitud.Text == String.Empty)
                //{
                //    paradas.Id = id;
                //    paradas.Descripcion = Descripcion.Text;
                //    paradas.Alias = null;
                //    paradas.FotoParada = bitmapData;
                //    paradas.Estado = Activo;
                //    paradas.Longitud = null;
                //    paradas.Latitud = null;
                //    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;

                //}
                ////si Alisas y longitud vienre vacios//
                //else if (Alias.Text == String.Empty && longitud.Text == String.Empty)
                //{
                //    paradas.Id = id;
                //    paradas.Descripcion = Descripcion.Text;
                //    paradas.Alias = null;
                //    paradas.FotoParada = bitmapData;
                //    paradas.Estado = Activo;
                //    paradas.Longitud = null;
                //    paradas.Latitud = latitud.Text;
                //    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                //}
                ////si Alisas y latitud vienre vacios//
                //else if (Alias.Text == String.Empty && latitud.Text == String.Empty)
                //{
                //    paradas.Id = id;
                //    paradas.Descripcion = Descripcion.Text;
                //    paradas.Alias = null;
                //    paradas.FotoParada = bitmapData;
                //    paradas.Estado = Activo;
                //    paradas.Longitud = longitud.Text;
                //    paradas.Latitud = null;
                //    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                //}
                ////si longitud y latitud vienre vacios//
                //else if (longitud.Text == String.Empty && latitud.Text == String.Empty)
                //{
                //    paradas.Id = id;
                //    paradas.Descripcion = Descripcion.Text;
                //    paradas.Alias = Alias.Text;
                //    paradas.FotoParada = bitmapData;
                //    paradas.Estado = Activo;
                //    paradas.Longitud = null;
                //    paradas.Latitud = null;
                //    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                //}
                ////si alias vacios//
                //else if (Alias.Text == String.Empty)
                //{
                //    paradas.Id = id;
                //    paradas.Descripcion = Descripcion.Text;
                //    paradas.Alias = null;
                //    paradas.FotoParada = bitmapData;
                //    paradas.Estado = Activo;
                //    paradas.Longitud = longitud.Text;
                //    paradas.Latitud = latitud.Text;
                //    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                //}
                ////si longitud vacios//
                //else if (Alias.Text == String.Empty)
                //{
                //    paradas.Id = id;
                //    paradas.Descripcion = Descripcion.Text;
                //    paradas.Alias = Alias.Text;
                //    paradas.FotoParada = bitmapData;
                //    paradas.Estado = Activo;
                //    paradas.Longitud = null;
                //    paradas.Latitud = latitud.Text;
                //    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                //}
                ////si latitud vacios//
                //else if (Alias.Text == String.Empty)
                //{
                //    paradas.Id = id;
                //    paradas.Descripcion = Descripcion.Text;
                //    paradas.Alias = Alias.Text;
                //    paradas.FotoParada = bitmapData;
                //    paradas.Estado = Activo;
                //    paradas.Longitud = longitud.Text;
                //    paradas.Latitud = null;
                //    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                   
                //}
                //else
                //{
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = Alias.Text;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = longitud.Text;
                    paradas.Latitud = latitud.Text;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                 
                //}
                serve.EditarParadasCompleted += Serve_EditarParadasCompleted;
                serve.EditarParadasAsync(paradas);
            }
        }

        private void Serve_EditarParadasCompleted(object sender, proxibusnicweb.EditarParadasCompletedEventArgs e)
        {
            if (e.Result == -1)
            {
                Toast.MakeText(Application.Context, "No se Pudo editar la parada", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Application.Context, "Parada editada con exito", ToastLength.Short).Show();
                Limpiar();
            }
        }

        private void Atras_Click(object sender, EventArgs e)
        {
            var res = new Intent(this, typeof(ListaParadasEditarRL));
            StartActivity(res);
        }

        private void Galeria_Click(object sender, EventArgs e)
        {
            SubirFoto();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Eliminar Parada");
            alert.SetMessage("¿Desea Eliminar esta Parada?").SetPositiveButton("Sí", (senderAlert, args) =>
            {
                ISharedPreferences preferencia = Application.GetSharedPreferences("informacion", FileCreationMode.Private);
                ISharedPreferencesEditor editor = preferencia.Edit();
                editor.Clear();
                editor.Apply();
                this.Finish();

                serve.EliminarParadaCompleted += Serve_EliminarParadaCompleted;
                serve.EliminarParadaAsync(id);

                var res = new Intent(this, typeof(ListaParadasEditarRL));
                StartActivity(res);

            }).SetNegativeButton("No", (senderAlert, args) => { }).Show();
        }

        private void Serve_EliminarParadaCompleted(object sender, proxibusnicweb.EliminarParadaCompletedEventArgs e)
        {
            if (!e.Result.respuesta)
            {
                Toast.MakeText(Application.Context, "No se Pudo Eliminar la parada", ToastLength.Short).Show();
            }
            Toast.MakeText(Application.Context, "La parada se elimino con exito", ToastLength.Short).Show();
        }

        private void Camara_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            Foto.SetImageBitmap(bitmap);
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        async void SubirFoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "El Dispositivo no es compatible", ToastLength.Short).Show();
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 320 * 320
            });

            bitmapData = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
            Foto.SetImageBitmap(bitmap);
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
        }
        private bool validarVacios()
        {
            if (String.IsNullOrEmpty(Descripcion.Text.Trim()))
            {
                Toast.MakeText(Application.Context, "Ingrese una descripcion", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        private void Limpiar()
        {
            Descripcion.Text = String.Empty;
            Alias.Text = String.Empty;
            longitud.Text = String.Empty;
            latitud.Text = String.Empty;
        }
    }
}