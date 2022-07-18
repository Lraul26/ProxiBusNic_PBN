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
        proxibusnicweb.ParadasWS parada;
        proxibusnicweb.ParadasWS paradas = new proxibusnicweb.ParadasWS();

        ImageButton aceptar, eliminar, camara, galeria, atras,borrar;
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

            if (parada != null)
            {
                //Botones 
                aceptar = (ImageButton)FindViewById(Resource.Id.btnEditarAceptar);
                eliminar = (ImageButton)FindViewById(Resource.Id.btnEditarCancelar);
                camara = (ImageButton)FindViewById(Resource.Id.btnEditarCamara);
                galeria = (ImageButton)FindViewById(Resource.Id.btnEditarfoto);
                borrar = (ImageButton)FindViewById(Resource.Id.btnlimpiarfoto);

                //textview
                atras = (ImageButton)FindViewById(Resource.Id.btnregresar);
                Descripcion = (EditText)FindViewById(Resource.Id.etEditarParada);
                Alias = (EditText)FindViewById(Resource.Id.etEditarAlias);
                longitud = (EditText)FindViewById(Resource.Id.etEditarlatitud);
                latitud = (EditText)FindViewById(Resource.Id.etEditarlongitud);
                Foto = (ImageView)FindViewById(Resource.Id.ivEditarParada);

                //eventos click
                camara.Click += Camara_Click;
                eliminar.Click += Eliminar_Click;
                galeria.Click += Galeria_Click;
                atras.Click += Atras_Click;
                aceptar.Click += Aceptar_Click;
                borrar.Click += Borrar_Click;

                //Mostramos Dato del registro
                Descripcion.Text = parada.Descripcion;
                Alias.Text = parada.Alias;
                if (parada.FotoParada != null)
                {
                    Foto.SetImageBitmap(byteArrayToImage(parada.FotoParada));
                }
                else
                {
                    Foto.SetImageResource(Resource.Drawable.ParadaPorDefecto);
                }
                longitud.Text = parada.Longitud;
                latitud.Text = parada.Latitud;
            }
            else
            {
                Toast.MakeText(Application.Context, "El registro no se encontro", ToastLength.Short).Show();
            }

        }

        private void Borrar_Click(object sender, EventArgs e)
        {
            Foto.SetImageBitmap(null);
            bitmapData = null;

            Foto.SetImageResource(Resource.Drawable.ParadaPorDefecto);
        }

        public Android.Graphics.Bitmap byteArrayToImage(byte[] source)
        {
            Android.Graphics.Bitmap mp = BitmapFactory.DecodeByteArray(source, 0, source.Length);
            return mp;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (validarVacios())
            {
                //si Alisas, longitud y latitud viene vacio//
                if (Alias.Text == String.Empty && longitud.Text == String.Empty && latitud.Text == String.Empty)
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = null;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = null;
                    paradas.Latitud = null;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;

                }
                //si Alisas y longitud vienre vacios//
                else if (Alias.Text == String.Empty && longitud.Text == String.Empty)
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = null;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = null;
                    paradas.Latitud = latitud.Text;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si Alisas y latitud vienre vacios//
                else if (Alias.Text == String.Empty && latitud.Text == String.Empty)
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = null;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = longitud.Text;
                    paradas.Latitud = null;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si longitud y latitud vienre vacios//
                else if (longitud.Text == String.Empty && latitud.Text == String.Empty)
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = Alias.Text;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = null;
                    paradas.Latitud = null;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si alias vacios//
                else if (Alias.Text == String.Empty)
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = null;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = longitud.Text;
                    paradas.Latitud = latitud.Text;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si longitud vacios//
                else if (Alias.Text == String.Empty)
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = Alias.Text;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = null;
                    paradas.Latitud = latitud.Text;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si latitud vacios//
                else if (Alias.Text == String.Empty)
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = Alias.Text;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = longitud.Text;
                    paradas.Latitud = null;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;

                }
                else
                {
                    paradas.Id = id;
                    paradas.Descripcion = Descripcion.Text;
                    paradas.Alias = Alias.Text;
                    paradas.FotoParada = bitmapData;
                    paradas.Estado = Activo;
                    paradas.Longitud = longitud.Text;
                    paradas.Latitud = latitud.Text;
                    paradas.UsuarioModificacion = Clases.Global.Usuario.correo;

                }
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
                this.Finish();
            }
        }

        private void Atras_Click(object sender, EventArgs e)
        {
            var res = new Intent(this, typeof(ListaParadasEditarRL));
            StartActivity(res);
        }
        private void Eliminar_Click(object sender, EventArgs e)
        {
           
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Eliminar");
                alert.SetMessage("¿Desea eliminar el registro de parada?").SetPositiveButton("Sí", (senderAlert, args) =>
                {
                    try
                    {
                        proxibusnicweb.ResultadoSW r = serve.EliminarParada(id);

                        if (r.respuesta)
                        {
                            Toast.MakeText(Application.Context, r.mensaje, ToastLength.Short).Show();
                            this.Finish();
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, r.mensaje, ToastLength.Short).Show();
                        }

                    }
            catch
            {
                Toast.MakeText(Application.Context, "No se puede eliminar la parada, posee Sugerencias...", ToastLength.Short).Show();
            }

           
                }).SetNegativeButton("No", (senderAlert, args) => { }).Show();
          
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

        private void Galeria_Click(object sender, EventArgs e)
        {
            SubirFoto();
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
                CompressionQuality = 40
            });

            bitmapData = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
            Foto.SetImageBitmap(bitmap);
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
            Foto.SetImageBitmap(null);
            bitmapData = null;
            Foto.SetImageResource(Resource.Drawable.Ruta);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}