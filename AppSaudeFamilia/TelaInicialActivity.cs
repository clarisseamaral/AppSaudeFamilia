using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AppSaudeFamilia.Util;
using AppSaudeFamilia.DataLocal;

namespace AppSaudeFamilia
{
    [Activity(Label = "Saúde Família")]
    public class TelaInicialActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var dtQuestionario = UtilDataBase.CountItem(QuestionarioDB.TableName);
            if(dtQuestionario > 0)
            {
                FindViewById<Button>(Resource.Id.btnSincronizarColeta).Visibility = ViewStates.Visible;
            }

            (FindViewById<Button>(Resource.Id.btnNovaColeta)).Click += delegate {
                var activity = new Intent(this, typeof(PerguntaActivity));
                StartActivity(activity);
            };
        }

        //protected override void OnResume()
        //{
        //    var dtQuestionario = UtilDataBase.CountItem(QuestionarioDB.TableName);
        //    if (dtQuestionario > 0)
        //    {
        //        FindViewById<Button>(Resource.Id.btnSincronizarColeta).Visibility = ViewStates.Visible;
        //    }
        //}
    }
}

