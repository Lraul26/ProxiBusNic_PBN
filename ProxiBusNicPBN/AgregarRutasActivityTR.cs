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
    public class AgregarRutasActivityTR : Activity
    {
        ImageButton btnAsignarParadas, btnGuardar,btnTomarFoto, btnAbrirGaleria,btnBorrarFoto;
        CheckBox chkEstado;
        EditText txtNumeroRuta;
        List<proxibusnicweb.ParadasWS> paradas;
        ImageView imgFotoBus;
        byte[] imagenAByte;
        // string[] arr;
        //  bool[] paradasMarcadas;
        int idRuta=0;
        
        proxibusnicweb.ProxiBusNicWS db = new proxibusnicweb.ProxiBusNicWS();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegistrarRutaTR);
            paradas = db.ListarParada().ToList();
            //arr = paradas.Select(p => p.Descripcion).ToArray();
            chkEstado = FindViewById<CheckBox>(Resource.Id.chkEstado);
            txtNumeroRuta = FindViewById<EditText>(Resource.Id.txtNumeroRuta);

            btnAsignarParadas = FindViewById<ImageButton>(Resource.Id.btnAsignarParadas);
            btnAsignarParadas.Click += BtnAsignarParadas_Click;

             imgFotoBus = FindViewById<ImageView>(Resource.Id.imgFotoBus);
            imgFotoBus.SetImageResource(Resource.Drawable.Ruta);

            btnGuardar = FindViewById<ImageButton>(Resource.Id.btnGuardar);
            btnGuardar.Click += BtnGuardar_Click;

            btnTomarFoto = FindViewById<ImageButton>(Resource.Id.btnAbrirCamara);
            btnTomarFoto.Click += BtnTomarFoto_Click;
            btnAbrirGaleria = FindViewById<ImageButton>(Resource.Id.btnAbrirGaleria);
            btnAbrirGaleria.Click += BtnAbrirGaleria_Click;

            btnBorrarFoto = FindViewById<ImageButton>(Resource.Id.btnBorrarFoto);
            btnBorrarFoto.Click += BtnBorrarFoto_Click;
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
            if (idRuta == 0)
            {
                Toast.MakeText(Application.Context, "Primero registre la ruta para asignarle sus paradas", ToastLength.Short).Show();
            }
            else
            {

                Intent i = new Intent(this, typeof(ListadoParadasAgregarActivityTR));
                i.PutExtra("id", idRuta);
                StartActivity(i);
            }

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
                proxibusnicweb.BusWS ruta = new proxibusnicweb.BusWS();
                ruta.NumeroRuta = txtNumeroRuta.Text.Trim();
                ruta.Estado = chkEstado.Checked;
                ruta.FotoBus = imagenAByte;
                ruta.UsuarioCreacion = Clases.Global.Usuario.correo;
                ruta.UsuarioModificacion = Clases.Global.Usuario.correo;
                  idRuta = db.AgregarBus(ruta);
                if (idRuta>0)
                {
                    Toast.MakeText(Application.Context, "Se ha registrado la ruta, puede proseguir a asignarle paradas", ToastLength.Short).Show();
                    limpiar();
                }
            }
        }


        //protected override Dialog OnCreateDialog(int id)
        //{

        //    paradasMarcadas = new bool[paradas.Count];
        //    switch (id)
        //    {
        //        case 0:
        //            {
                
        //                return new AlertDialog.Builder(this)
        //                    .SetTitle("Paradas")
        //                    .SetPositiveButton("Ok",this)
        //                    .SetNegativeButton("Cancelar",this)
        //                    .SetMultiChoiceItems(arr, paradasMarcadas, this)
        //                .Create();
        //                //break;
        //            }
        //        default:
        //            break;
        //    }
        //    return null;
        //}

        //public void OnClick(IDialogInterface dialog, int which)
        //{
        //    if (which < 0)
        //    {
        //        Toast.MakeText(Application.Context, "OK", ToastLength.Short).Show();

        //    }
        //    else
        //    {
        //        Toast.MakeText(Application.Context, "Cancelar", ToastLength.Short).Show();
        //    }
            
        //}

        //public void OnClick(IDialogInterface dialog, int which, bool isChecked)
        //{
        //    Toast.MakeText(Application.Context, paradas[which]+(isChecked?"marcado":"desmarcado"), ToastLength.Short).Show();
        //}
    }
}