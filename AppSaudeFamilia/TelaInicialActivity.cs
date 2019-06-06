﻿using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using AppSaudeFamilia.Util;
using AppSaudeFamilia.DataLocal;
using Android.Content.PM;

namespace AppSaudeFamilia
{
    [Activity(Label = "Saúde Família", ScreenOrientation = ScreenOrientation.Portrait)]
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

            (FindViewById<Button>(Resource.Id.btnSair)).Click += SairSistema_Click;
          
        }

        private void SairSistema_Click(object sender, EventArgs e)
        {
            var dialog = new Dialog(this, Resource.Style.modal_theme);
            dialog.SetContentView(Resource.Layout.Modal2Opcoes1Msg);
            TextView txtTitulo = dialog.FindViewById<TextView>(Resource.Id.txtInfoTitulo);
            txtTitulo.Text = "Tem certeza que deseja realmente sair?";

            TextView txtInfo = (TextView)dialog.FindViewById(Resource.Id.txtInfo);
            txtInfo.Text = "Questionários não sincronizados serão perdidos.";

            Button btNao = dialog.FindViewById<Button>(Resource.Id.btnDir);

            btNao.Text = "Não";

            btNao.Click += delegate
            {
                dialog.Dismiss();
            };

            Button btSim = dialog.FindViewById<Button>(Resource.Id.btnEsq);

            btSim.Text = "Sim";

            btSim.Click += delegate
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                Aplicacao.Fluxo = Constantes.FLUXO_DESLOGAR;
                StartActivity(intent);
            };
            dialog.Show();
        }


        protected override void OnResume()
        {
            base.OnResume();
            var dtQuestionario = UtilDataBase.CountItem(QuestionarioDB.TableName);
            if (dtQuestionario > 0)
            {
                FindViewById<Button>(Resource.Id.btnSincronizarColeta).Visibility = ViewStates.Visible;
            }
        }

    }
}

