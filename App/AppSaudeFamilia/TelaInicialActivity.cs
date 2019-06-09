using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AppSaudeFamilia.DataLocal;
using AppSaudeFamilia.Servico;
using AppSaudeFamilia.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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

            (FindViewById<Button>(Resource.Id.btnNovaColeta)).Click += delegate
            {
                var activity = new Intent(this, typeof(PerguntaActivity));
                StartActivity(activity);
            };

            (FindViewById<Button>(Resource.Id.btnSincronizarColeta)).Click += TelaInicialActivity_Click;

            (FindViewById<Button>(Resource.Id.btnSair)).Click += SairSistema_Click;

        }

        private void TelaInicialActivity_Click(object sender, EventArgs e)
        {
            EnviarRespostas();
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

        private async void EnviarRespostas()
        {
            await Task.Run(async () =>
            {
                if (UtilAcessibilidade.VerificaAcessoInternet(this))
                {

                    ProgressDialog loading = null;

                    RunOnUiThread(() =>
                    {
                        loading = ProgressDialog.Show(this, "Enviando coletas realizadas", "Isso pode demorar um pouco.\nFavor aguardar!", true);
                    });


                    var respostasDB = UtilDataBase.GetItems(QuestionarioDB.TableName);

                    var questionariosInseridos = new List<ColetaEntradaDTO>();


                    var registro = new QuestionarioDB();


                    var request = new ColetaEntradaDTO();

                    string guid = string.Empty;
                    var respostas = new List<RespostaDto>();

                    foreach (DataRow item in respostasDB.Rows)
                    {
                        registro.ConvertDr(item);

                        if (guid != registro.Guid)
                        {
                            if (respostas.Count > 0)
                            {
                                request.Respostas = respostas;
                                await WebService.PostSemSaida<ColetaEntradaDTO>(request, CaminhoWebService.RESPOSTAS, Aplicacao.Token);
                                respostas = new List<RespostaDto>();
                            }

                            request = new ColetaEntradaDTO()
                            {
                                Data = Convert.ToDateTime(registro.Data),
                                Latitude = Convert.ToDouble(registro.Latitude),
                                Longitude = Convert.ToDouble(registro.Longitude),
                            };

                        }

                        respostas.Add(new RespostaDto()
                        {
                            IdOpcaoResposta = registro.IdResposta > 0 ? (int?)registro.IdResposta : null,
                            IdPergunta = registro.IdPergunta,
                            Valor = registro.Resposta
                        });


                        guid = registro.Guid;
                    }

                    if (respostas.Count > 0)
                    {
                        request.Respostas = respostas;
                        await WebService.PostSemSaida<ColetaEntradaDTO>(request, CaminhoWebService.RESPOSTAS, Aplicacao.Token);
                        respostas = new List<RespostaDto>();
                    }

                    UtilDataBase.Delete(QuestionarioDB.TableName);

                    if (loading.IsShowing && loading != null)
                    {
                        loading.Dismiss();
                    }

                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "Coletas sincronizadas com sucesso!", ToastLength.Long).Show();
                        ValidarExibicaoSicronizarColeta();
                    });


                }
                else
                {
                    Modal.ExibirModal(this, GetString(Resource.String.ConexaoInternetTitulo), "", GetString(Resource.String.ConexaoInternetMensagem));
                }
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            ValidarExibicaoSicronizarColeta();
        }

        private void ValidarExibicaoSicronizarColeta()
        {
            var btnSincronizarColeta = FindViewById<Button>(Resource.Id.btnSincronizarColeta);

            var dtQuestionario = UtilDataBase.CountItem(QuestionarioDB.TableName);
            var drawable = dtQuestionario > 0 ? Resource.Drawable.sync : Resource.Drawable.sync_Desabilitado;
            var color = dtQuestionario > 0 ?  Color.ParseColor("#757575") : Color.ParseColor("#ffe0e0e0");
            var enable = dtQuestionario > 0 ? true : false;

            btnSincronizarColeta.SetBackgroundResource(drawable);
            btnSincronizarColeta.Clickable = enable;
            btnSincronizarColeta.Enabled = enable;
            FindViewById<TextView>(Resource.Id.txtSincronizar).SetTextColor(color);
        }
    }
}

