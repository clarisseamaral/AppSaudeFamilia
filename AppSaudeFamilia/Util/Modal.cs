using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppSaudeFamilia
{
    public static class Modal
    {
        static Dialog dialog;

        public static void ExibirModal(Activity activity, string titulo, string info, string info2, Action action = null)
        {
            activity.RunOnUiThread(() =>
            {
                dialog = new Dialog(activity, Resource.Style.modal_theme);
                dialog.SetContentView(Resource.Layout.ModalMensagem);

                TextView txtInfoTitulo = (TextView)dialog.FindViewById(Resource.Id.txtInfoTitulo);
                TextView txtInfo = (TextView)dialog.FindViewById(Resource.Id.txtInfo);
                TextView txtInfo2 = (TextView)dialog.FindViewById(Resource.Id.txtInfo2);
                Button btnOK = (Button)dialog.FindViewById(Resource.Id.btnOK);

                txtInfoTitulo.Text = titulo;
                txtInfo2.Text = info2;

                if (String.IsNullOrEmpty(info))
                    txtInfo.Visibility = ViewStates.Gone;
                else
                    txtInfo.Text = info;

                if (action == null)
                {
                    btnOK.Click += delegate { dialog.Dismiss(); };
                }
                else
                {
                    btnOK.Click += delegate { action.Invoke(); dialog.Dismiss(); };
                }

                dialog.Show();
            });
        }

        public static void ExibirModal1Msg1Opcao(Activity activity, string titulo)
        {
            activity.RunOnUiThread(() =>
            {
                dialog = new Dialog(activity, Resource.Style.modal_theme);
                dialog.SetContentView(Resource.Layout.Modal1Msg1Opcao);

                TextView txtInfoTitulo = (TextView)dialog.FindViewById(Resource.Id.txtTituloModal1Msg1Opcao);
                Button btnOK2 = (Button)dialog.FindViewById(Resource.Id.btnOKModal1Opcao1Msg);

                txtInfoTitulo.Text = titulo;
                btnOK2.Click += delegate { dialog.Dismiss(); };

                dialog.Show();
            });
        }
    }
}