using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;

namespace ProxiBusNicPBN
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        //private void FabOnClick(object sender, EventArgs eventArgs)
        //{
        //    View view = (View)sender;
        //    Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
        //        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        //}

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_rutas)
            {
                Intent i = new Intent(this,typeof(ListadoRutasActivityTR));
                StartActivity(i);
            }
            else if (id == Resource.Id.nav_parada)
            {

            }
            else if (id == Resource.Id.nav_opciones)
            {
                var res = new Intent(this, typeof(AccionesAvanzadasRL));
                StartActivity(res);
            }
            else if (id == Resource.Id.nav_tema)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Tema");
                alert.SetMessage("¿Desea cambiar el tema de la App?");
                alert.SetPositiveButton("Tema Oscuro", (senderAlert, args) =>
                {
                    ISharedPreferences tema = Application.GetSharedPreferences("Tema", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = tema.Edit();
                    editor.Clear();
                    editor.Apply();

                    Delegate.SetLocalNightMode(AppCompatDelegate.ModeNightYes);

                }).Show();
                alert.SetNegativeButton("Tema Claro", (senderAlert, args) => {
                    ISharedPreferences tema2 = Application.GetSharedPreferences("Tema", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = tema2.Edit();
                    editor.Clear();
                    editor.Apply();
                    Delegate.SetLocalNightMode(AppCompatDelegate.ModeNightNo);
                }).Show();
            }
            else if (id == Resource.Id.nav_comentarios)
            {
                var res = new Intent(this, typeof(AgregarSugerenciaRL));
                StartActivity(res);
            }
            else if (id == Resource.Id.nav_cerrar)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Cerrar sesión");
                alert.SetMessage("¿Desea cerrar sesión?").SetPositiveButton("Sí", (senderAlert, args) =>
                {
                    ISharedPreferences preferencia = Application.GetSharedPreferences("informacion", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = preferencia.Edit();
                    editor.Clear();
                    editor.Apply();
                    this.Finish();

                    Intent intent = new Intent(this, typeof(ActivityLogin));
                    StartActivity(intent);
                }).SetNegativeButton("No", (senderAlert, args) => { }).Show();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

