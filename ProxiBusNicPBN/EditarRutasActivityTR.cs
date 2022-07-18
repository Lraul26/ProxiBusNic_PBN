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
    [Activity(Label = "AgregarRutasActivity")]
    //public class AgregarRutasActivity : Activity, IDialogInterfaceOnClickListener, IDialogInterfaceOnMultiChoiceClickListener
    public class EditarRutasActivityTR : Activity
    {
        ImageButton btnAsignarParadas, btnGuardar, btnTomarFoto, btnAbrirGaleria, btnBorrarFoto,btnEliminar;
        CheckBox chkEstado;
        EditText txtNumeroRuta;
        List<proxibusnicweb.ParadasWS> paradas;
        ImageView imgFotoBus;
        byte[] imagenAByte;
        int IdRuta = 0;
        proxibusnicweb.BusWS ruta;
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditarRutaTR);
            int id = Intent.GetIntExtra("id", 0);
            ruta = db.ListarBus().Where(x => x.Id == id).FirstOrDefault();


            if (ruta != null)
            {
                paradas = db.ListarParada().ToList();
                //arr = paradas.Select(p => p.Descripcion).ToArray();
                chkEstado = FindViewById<CheckBox>(Resource.Id.chkEstado);
                txtNumeroRuta = FindViewById<EditText>(Resource.Id.txtNumeroRuta);

                btnAsignarParadas = FindViewById<ImageButton>(Resource.Id.btnReasignarParadas);
                btnAsignarParadas.Click += BtnAsignarParadas_Click;

                imgFotoBus = FindViewById<ImageView>(Resource.Id.imgFotoBus);
                imgFotoBus.SetImageResource(Resource.Drawable.Ruta);
                llenado();
                btnGuardar = FindViewById<ImageButton>(Resource.Id.btnGuardar);
                btnGuardar.Click += BtnGuardar_Click;

                btnTomarFoto = FindViewById<ImageButton>(Resource.Id.btnAbrirCamara);
                btnTomarFoto.Click += BtnTomarFoto_Click;

                btnAbrirGaleria = FindViewById<ImageButton>(Resource.Id.btnAbrirGaleria);
                btnAbrirGaleria.Click += BtnAbrirGaleria_Click;

                btnBorrarFoto = FindViewById<ImageButton>(Resource.Id.btnBorrarFoto);
                btnBorrarFoto.Click += BtnBorrarFoto_Click;

                btnEliminar = FindViewById<ImageButton>(Resource.Id.btnEliminar);
                btnEliminar.Click += BtnEliminar_Click;
            }
            else
            {
                Toast.MakeText(Application.Context, "No se ha encontrado el registro", ToastLength.Short).Show();
            }
            
        }
 

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
          
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Eliminar");
                alert.SetMessage("¿Desea eliminar el registro?").SetPositiveButton("Sí", (senderAlert, args) =>
                {
                    try
                    {

                        proxibusnicweb.ResultadoSW r = db.EliminarBus(ruta.Id);

                    if (r.respuesta)
                    {
                        Toast.MakeText(Application.Context, r.mensaje, ToastLength.Short).Show();
                        //limpiar();
                        this.Finish();
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, r.mensaje, ToastLength.Short).Show();
                    }
                }
            catch
            {
                Toast.MakeText(Application.Context, "No se puede eliminar la ruta porque posee paradas", ToastLength.Short).Show();
            }
        }).SetNegativeButton("No", (senderAlert, args) => { }).Show();


             
         
            
        }

        private void llenado(){
          
            txtNumeroRuta.Text = ruta.NumeroRuta;
            chkEstado.Checked = ruta.Estado;

            if (ruta.FotoBus != null)
            {
                imgFotoBus.SetImageBitmap(byteArrayToImage(ruta.FotoBus));
            }
            else
            {
                imgFotoBus.SetImageResource(Resource.Drawable.Ruta);
            }

        }

        public Android.Graphics.Bitmap byteArrayToImage(byte[] source)
        {
            // byte[] temp_img = this.convertCvToImage(img);
            Android.Graphics.Bitmap mp = BitmapFactory.DecodeByteArray(source, 0, source.Length);
            return mp;
        }

        private void BtnBorrarFoto_Click(object sender, EventArgs e)
        {

            imgFotoBus.SetImageBitmap(null);
            imagenAByte = null;

            imgFotoBus.SetImageResource(Resource.Drawable.Ruta);
        }

        async void subirFoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(Application.Context, "La subida no es soportada por el dispositivo", ToastLength.Short).Show();
            }
            var archivo= await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
            PhotoSize=Plugin.Media.Abstractions.PhotoSize.Full,
            CompressionQuality=40
            }
            );
            imagenAByte = System.IO.File.ReadAllBytes(archivo.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imagenAByte, 0, imagenAByte.Length);
            imgFotoBus.SetImageBitmap(bitmap);
        }
        private void BtnAbrirGaleria_Click(object sender, EventArgs e)
        {
            subirFoto();
        }

        private void BtnTomarFoto_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum]
Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imgFotoBus.SetImageBitmap(bitmap);
            //Bitmap to Byte

            //convertimos a Byte para luego almacenarlo en el servidor
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                imagenAByte = stream.ToArray();
            }
        }

        private void BtnAsignarParadas_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this,typeof(ListadoAsignarParadasActivityTR));
            i.PutExtra("id", ruta.Id);
            StartActivity(i);

        }
        private bool validar()
        {
            if (String.IsNullOrEmpty(txtNumeroRuta.Text))
            {
                Toast.MakeText(Application.Context, "Debe de haber un número de ruta", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            txtNumeroRuta.Text = "";
            chkEstado.Checked = false;


            imgFotoBus.SetImageBitmap(null);
            imagenAByte = null;

            imgFotoBus.SetImageResource(Resource.Drawable.Ruta);
            
        }
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
           
            if (validar())
            {
                //proxibusnicweb.BusWS ruta = new proxibusnicweb.BusWS();
                //ruta.Id=
                ruta.NumeroRuta = txtNumeroRuta.Text.Trim();
                ruta.Estado = chkEstado.Checked;
                ruta.FotoBus = imagenAByte;
               // ruta.UsuarioCreacion = Clases.Global.Usuario.correo;
                ruta.UsuarioModificacion = Clases.Global.Usuario.correo;
                  IdRuta = db.EditarBus(ruta);
                if (IdRuta>0)
                {
                    Toast.MakeText(Application.Context, "Se ha modificado la ruta", ToastLength.Short).Show();
                    limpiar();
                    this.Finish();
                }
            }
        }

    }
}