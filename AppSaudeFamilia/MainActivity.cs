using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AppSaudeFamilia
{
    [Activity(Label = "AppSaudeFamilia", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnNovaColeta);

            button.Click += delegate {
                var activity = new Intent(this, typeof(PerguntaActivity));
                StartActivity(activity);
            };
        }
    }
}

