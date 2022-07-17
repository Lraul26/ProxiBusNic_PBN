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
    [Activity(Label = "AgregarParadaRL")]
    public class AgregarParadaRL : Activity
    {

        proxibusnicweb.ParadasWS serve = new proxibusnicweb.ParadasWS();
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        EditText Descripcion, Alias, Longitud, Latitud;
        ImageView FotoParada;
        ImageButton BtnCamara, BtnGaleria, BtnGuardar;
        private byte[] bitmapData;
        bool Activo = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AgregarParadaRL);

            BtnGaleria = FindViewById<ImageButton>(Resource.Id.btnGaleria);
            BtnCamara = FindViewById<ImageButton>(Resource.Id.btnCamara);
            BtnGuardar = FindViewById<ImageButton>(Resource.Id.btnAceptar);

            Descripcion = FindViewById<EditText>(Resource.Id.editDescripcion);
            Alias = FindViewById<EditText>(Resource.Id.editAlias);
            Longitud = FindViewById<EditText>(Resource.Id.editlatitud);
            Latitud = FindViewById<EditText>(Resource.Id.editlongitud);

            FotoParada = FindViewById<ImageView>(Resource.Id.imageParada);

            BtnGaleria.Click += BtnGaleria_Click;
            BtnCamara.Click += BtnCamara_Click;
            BtnGuardar.Click += BtnGuardar_Click;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (validarVacios())
            {
                //si Alisas, longitud y latitud viene vacio//
                if (Alias.Text == String.Empty && Longitud.Text == String.Empty && Latitud.Text == String.Empty)
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = null;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = null;
                    serve.Latitud = null;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si Alisas y longitud vienre vacios//
                else if (Alias.Text == String.Empty && Longitud.Text == String.Empty)
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = null;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = null;
                    serve.Latitud = Latitud.Text;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si Alisas y latitud vienre vacios//
                else if (Alias.Text == String.Empty && Latitud.Text == String.Empty)
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = null;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = Longitud.Text;
                    serve.Latitud = null;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si longitud y latitud vienre vacios//
                else if (Longitud.Text == String.Empty && Latitud.Text == String.Empty)
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = Alias.Text;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = null;
                    serve.Latitud = null;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si alias vacios//
                else if (Alias.Text == String.Empty)
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = null;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = Longitud.Text;
                    serve.Latitud = Latitud.Text;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si longitud vacios//
                else if (Alias.Text == String.Empty)
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = Alias.Text;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = null;
                    serve.Latitud = Latitud.Text;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                //si latitud vacios//
                else if (Alias.Text == String.Empty)
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = Alias.Text;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = Longitud.Text;
                    serve.Latitud = null;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                else
                {
                    serve.Descripcion = Descripcion.Text;
                    serve.Alias = Alias.Text;
                    serve.FotoParada = bitmapData;
                    serve.Estado = Activo;
                    serve.Longitud = Longitud.Text;
                    serve.Latitud = Latitud.Text;
                    serve.UsuarioCreacion = Clases.Global.Usuario.correo;
                    serve.UsuarioModificacion = Clases.Global.Usuario.correo;
                }
                db.AgregarParadaCompleted += Db_AgregarParadaCompleted;
                db.AgregarParadaAsync(serve);
            }

        }

        private void Db_AgregarParadaCompleted(object sender, proxibusnicweb.AgregarParadaCompletedEventArgs e)
        {
            if (e.Result == -1)
            {
                Toast.MakeText(Application.Context, "No se Pudo registrar la parada", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Application.Context, "Parada registrada con exito", ToastLength.Short).Show();
                 Limpiar();
            }
        }

        private void BtnCamara_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            FotoParada.SetImageBitmap(bitmap);
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
        }
        private void BtnGaleria_Click(object sender, EventArgs e)
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
                CompressionQuality = 320 * 320
            });

            bitmapData = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
            FotoParada.SetImageBitmap(bitmap);
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
            Longitud.Text = String.Empty;
            Latitud.Text = String.Empty;
        }
    }
}