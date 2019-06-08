using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppSaudeFamilia.DataLocal;
using AppSaudeFamilia.Servico;
using AppSaudeFamilia.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSaudeFamilia
{
    [Activity(Label = "Saúde Família", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PerguntaActivity : Activity, ILocationListener
    {
        public LocationManager locMgr { get; set; }

        public int QuestaoAtual { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool GPSLigado { get; set; }

        public List<ListaPerguntasSaidaDTO> Perguntas { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            await Task.Run(async () =>
            {

                if (UtilAcessibilidade.VerificaAcessoInternet(this))
                {
                    Perguntas = await BuscarPerguntas();

                    SetContentView(Resource.Layout.Pergunta);

                    locMgr = (LocationManager)GetSystemService(LocationService);

                    IsLocationEnabled();
                    CarregaElementosTela();

                    PreencherPergunta(Perguntas[QuestaoAtual]);

                }
                else
                {
                    Modal.ExibirModal(this, GetString(Resource.String.ConexaoInternetTitulo), "", GetString(Resource.String.ConexaoInternetMensagem));
                }
            });



        }

        #region ElementosTela

        private Button btnAnterior;
        private Button btnProximo;
        private Button btnCancelar;
        private Button btnEnviarColeta;
        private TextView txtPergunta;
        private TextView txtResposta;
        private RadioGroup rdOpcoes;
        private RadioButton rbOpcao1;
        private RadioButton rbOpcao2;
        private RadioButton rbOpcao3;
        private RadioButton rbOpcao4;

        private void CarregaElementosTela()
        {
            btnAnterior = FindViewById<Button>(Resource.Id.btnAnterior);
            btnProximo = FindViewById<Button>(Resource.Id.btnProximo);
            btnCancelar = FindViewById<Button>(Resource.Id.btnCancelarQuestionario);
            txtPergunta = FindViewById<TextView>(Resource.Id.txtPergunta);
            txtResposta = FindViewById<TextView>(Resource.Id.txtResposta);
            btnEnviarColeta = FindViewById<Button>(Resource.Id.btnConcluirQuestionario);
            rdOpcoes = FindViewById<RadioGroup>(Resource.Id.rdOpcoes);
            rbOpcao1 = FindViewById<RadioButton>(Resource.Id.rbOpcao1);
            rbOpcao2 = FindViewById<RadioButton>(Resource.Id.rbOpcao2);
            rbOpcao3 = FindViewById<RadioButton>(Resource.Id.rbOpcao3);
            rbOpcao4 = FindViewById<RadioButton>(Resource.Id.rbOpcao4);

            QuestaoAtual = 0;

            btnAnterior.Click += BtnAnterior_Click;

            btnProximo.Click += BtnProximo_Click;

            rbOpcao1.Click += RbAlternativa_Click;
            rbOpcao2.Click += RbAlternativa_Click;
            rbOpcao3.Click += RbAlternativa_Click;
            rbOpcao4.Click += RbAlternativa_Click;
            btnEnviarColeta.Click += BtnEnviarColeta_Click;
            btnCancelar.Click += BtnCancelar_Click;
            txtResposta.AfterTextChanged += TxtResposta_AfterTextChanged;
        }

        #endregion

        #region Eventos
        private void TxtResposta_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            Perguntas[QuestaoAtual].Resposta = txtResposta.Text;
        }

        private void BtnEnviarColeta_Click(object sender, EventArgs e)
        {
            if (Perguntas.Where(p => p.IdAlternativa == 0 && String.IsNullOrEmpty(p.Resposta)).Count() > 0)
            {
                Modal.ExibirModal(this, "Pergunta não preenchida", "", "Nem todas as perguntas foram respondidas. Gentileza verificar.");
            }
            else
            {
                ProgressDialog loading = null;
                RunOnUiThread(() =>
                {
                    loading = ProgressDialog.Show(this, "Salvando dados localmente", "Isso pode demorar um pouco.\nFavor aguardar!", true);
                });

                InserirBancoLocal();

                if (loading.IsShowing && loading != null)
                {
                    loading.Dismiss();
                }

                Finish();

                var activity = new Intent(this, typeof(TelaInicialActivity));
                StartActivity(activity);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void RbAlternativa_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            switch (rb.Id)
            {
                case Resource.Id.rbOpcao1:
                    Perguntas[QuestaoAtual].IdAlternativa = Perguntas[QuestaoAtual].Alternativas[0].Id;
                    break;
                case Resource.Id.rbOpcao2:
                    Perguntas[QuestaoAtual].IdAlternativa = Perguntas[QuestaoAtual].Alternativas[1].Id;
                    break;
                case Resource.Id.rbOpcao3:
                    Perguntas[QuestaoAtual].IdAlternativa = Perguntas[QuestaoAtual].Alternativas[2].Id;
                    break;
                case Resource.Id.rbOpcao4:
                    Perguntas[QuestaoAtual].IdAlternativa = Perguntas[QuestaoAtual].Alternativas[3].Id;
                    break;
            }
        }

        private void BtnProximo_Click(object sender, EventArgs e)
        {
            QuestaoAtual++;  //Atualiza posição pergunta
            VerificarResposta();
            PreencherPergunta(Perguntas[QuestaoAtual]);
            VerificarPosicaoPergunta();
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            QuestaoAtual--; //Atualiza posição pergunta
            VerificarResposta();
            PreencherPergunta(Perguntas[QuestaoAtual]);
            VerificarPosicaoPergunta();
        }

        #endregion

        private async Task<List<ListaPerguntasSaidaDTO>> BuscarPerguntas()
        {
            ProgressDialog loading = null;

            RunOnUiThread(() =>
            {
                loading = ProgressDialog.Show(this, "Buscando perguntas", "Isso pode demorar um pouco.\nFavor aguardar!", true);
            });

            var saida = await WebService.GetAsync<List<ListaPerguntasSaidaDTO>>(CaminhoWebService.LISTAR_PERGUNTAS, Aplicacao.Token);

            if (loading.IsShowing && loading != null)
            {
                loading.Dismiss();
            }

            return saida;
        }

        private void PreencherPergunta(ListaPerguntasSaidaDTO pergunta)
        {
            txtPergunta.Text = pergunta.Descricao;

            if (pergunta.IdTipoPergunta == 1)  //Multipla escolha
            {
                txtResposta.Visibility = Android.Views.ViewStates.Invisible;
                rdOpcoes.Visibility = Android.Views.ViewStates.Visible;
                PreencherAlternativas(pergunta.Alternativas, Perguntas[QuestaoAtual].IdAlternativa);
            }
            else if (pergunta.IdTipoPergunta == 2) //Texto
            {
                txtResposta.Visibility = Android.Views.ViewStates.Visible;
                rdOpcoes.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao1.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao2.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao3.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao4.Visibility = Android.Views.ViewStates.Invisible;
                txtResposta.Text = pergunta.Resposta;
            }
        }

        private int? PreencheAlternativaSelecionada(int idAlternativa)
        {
            if (idAlternativa > 0)
            {
                var alternativas = Perguntas[QuestaoAtual].Alternativas;
                var alternativaSelecionada = alternativas.Where(a => a.Id == idAlternativa).First();
                var elementPos = alternativas.IndexOf(alternativaSelecionada);

                return elementPos;
            }

            return null;
        }

        private void PreencherAlternativas(List<Alternativa> alternativa, int idAlternativa)
        {
            var alternativaSelecionada = PreencheAlternativaSelecionada(idAlternativa);

            rbOpcao1.Visibility = ViewStates.Invisible;
            rbOpcao2.Visibility = ViewStates.Invisible;
            rbOpcao3.Visibility = ViewStates.Invisible;
            rbOpcao4.Visibility = ViewStates.Invisible;

            for (int i = 0; i < alternativa.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        PreencherOpcao(rbOpcao1, alternativa[0].Texto, alternativaSelecionada.HasValue && alternativaSelecionada.Value == 0);
                        break;
                    case 1:
                        PreencherOpcao(rbOpcao2, alternativa[1].Texto, alternativaSelecionada.HasValue && alternativaSelecionada.Value == 1);
                        break;
                    case 2:
                        PreencherOpcao(rbOpcao3, alternativa[2].Texto, alternativaSelecionada.HasValue && alternativaSelecionada.Value == 2);

                        break;
                    case 3:
                        PreencherOpcao(rbOpcao4, alternativa[3].Texto, alternativaSelecionada.HasValue && alternativaSelecionada.Value == 3);
                        break;
                }
            }
        }

        private void PreencherOpcao(RadioButton rbOpcao, string texto, bool opcapSelecionada)
        {
            rbOpcao.Text = texto;
            rbOpcao.Checked = opcapSelecionada;
            rbOpcao.Visibility = ViewStates.Visible;
        }

        private void VerificarPosicaoPergunta()
        {
            if (QuestaoAtual + 1 == Perguntas.Count)
            {
                btnProximo.Visibility = ViewStates.Invisible;
                btnAnterior.Visibility = ViewStates.Visible;
            }
            else if (QuestaoAtual == 0)
            {
                btnAnterior.Visibility = ViewStates.Invisible;
                btnProximo.Visibility = ViewStates.Visible;
            }
            else
            {
                btnAnterior.Visibility = ViewStates.Visible;
                btnProximo.Visibility = ViewStates.Visible;
            }


        }

        private void VerificarResposta()
        {
            var proximaPergunta = Perguntas[QuestaoAtual];

            if (String.IsNullOrEmpty(proximaPergunta.Resposta) && proximaPergunta.IdAlternativa == 0)
            {
                txtResposta.Text = string.Empty;
                rbOpcao1.Checked = false;
                rbOpcao2.Checked = false;
                rbOpcao3.Checked = false;
                rbOpcao4.Checked = false;
            }
        }

        private void InserirBancoLocal()
        {
            if (Latitude == 0 && Longitude == 0)
            {
                Modal.ExibirModal(this, "GPS", "", "Não foi possível identificar sua localização. Verifique se o GPS está ligado e tente novamente.");
            }
            else
            {
                var stringBuilder = new System.Text.StringBuilder();

                var guid = Guid.NewGuid();

                foreach (var item in Perguntas)
                {
                    var questionario = new QuestionarioDB()
                    {
                        Guid = guid.ToString(),
                        Data = DateTime.Now.ToString(),
                        IdPergunta = item.Id,
                        IdQuestionario = 1,
                        IdResposta = item.IdAlternativa,
                        Resposta = item.Resposta,
                        Longitude = Longitude.ToString(),
                        Latitude = Latitude.ToString()
                    };

                    stringBuilder.AppendFormat("{0};", questionario.InsertQuery);
                }

                UtilDataBase.Save(stringBuilder.ToString());
            }

        }

        protected override void OnResume()
        {
            base.OnResume();
            if (locMgr == null)
            {
                locMgr = (LocationManager)GetSystemService(LocationService);
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum]Result resultCode, Intent data)
        {
            if (requestCode == 3)
            {
                IsLocationEnabled();
            }
        }

        #region GPS

        protected void IsLocationEnabled()
        {
            try
            {
                locMgr.RequestLocationUpdates(LocationManager.GpsProvider, 1000, 10, this);
                locMgr.RequestLocationUpdates(LocationManager.NetworkProvider, 0, 0, this);
            }
            catch (Exception)
            {
                ExibirModalLocalizacao("Permite o acesso a sua localização?", "Sua Localização é usada para verificar onde foi realizado a pesquisa.", "Não permitir", "Permitir");
            }
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }

        public void OnProviderDisabled(string provider)
        {
            GPSLigado = false;
        }

        public void OnProviderEnabled(string provider)
        {
            GPSLigado = true;
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {

        }

        public override void OnBackPressed()
        {
            DesligaListenerGps();
            Finish();
        }

        private void DesligaListenerGps()
        {
            locMgr.RemoveUpdates(this);
            locMgr = null;
        }

        #endregion

        #region Modal GPS

        public Dialog dialog { get; set; }

        public void ExibirModalLocalizacao(string titulo, string info, string opcaoEsq, string opcaoDir)
        {
            RunOnUiThread(() =>
            {
                dialog = new Dialog(this, Resource.Style.modal_theme);
                dialog.SetContentView(Resource.Layout.Modal2Opcoes1Msg);

                TextView txtInfoTitulo = (TextView)dialog.FindViewById(Resource.Id.txtInfoTitulo);
                TextView txtInfo = (TextView)dialog.FindViewById(Resource.Id.txtInfo);
                Button esq = (Button)dialog.FindViewById(Resource.Id.btnEsq);
                Button dir = (Button)dialog.FindViewById(Resource.Id.btnDir);

                txtInfoTitulo.Text = titulo;
                txtInfo.Text = info;
                esq.Text = opcaoEsq;
                dir.Text = opcaoDir;

                if (string.IsNullOrEmpty(info))
                {
                    txtInfo.Visibility = ViewStates.Invisible;
                }
                else
                {
                    txtInfo.Text = info;
                }

                esq.Click += Esq_Click;
                dir.Click += Dir_Click;

                dialog.Show();
            });
        }

        private void Dir_Click(object sender, EventArgs e)
        {
            dialog.Dismiss();
            var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
            StartActivityForResult(intent, 3);
        }

        private void Esq_Click(object sender, EventArgs e)
        {
            dialog.Dismiss();
        }

        #endregion

    }
}