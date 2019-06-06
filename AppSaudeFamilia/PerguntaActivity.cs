using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AppSaudeFamilia.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSaudeFamilia
{
    [Activity(Label = "Saúde Família")]
    public class PerguntaActivity : Activity
    {
        public int IdQuestionario {
            get {
                return 1;
            }
        }

        public int QuestaoAtual { get; set; }

        public List<ListaPerguntasSaidaDTO> Perguntas { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Perguntas = await BuscarPerguntas();

            SetContentView(Resource.Layout.Pergunta);

            CarregaElementosTela();

            PreencherPergunta(Perguntas[QuestaoAtual]);



        }

        #region ElementosTela

        private Button btnAnterior;
        private Button btnProximo;
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
        }

        #endregion

        private void BtnEnviarColeta_Click(object sender, EventArgs e)
        {
            InserirBancoLocal();
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

        private void BtnProximo_Click(object sender, EventArgs e)
        {
            QuestaoAtual++;  //Atualiza posição pergunta
            VerificarResposta();
            Perguntas[QuestaoAtual].Resposta = txtResposta.Text;
            PreencherPergunta(Perguntas[QuestaoAtual]);
            VerificarPosicaoPergunta();
            VerificarResposta();
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            QuestaoAtual--; //Atualiza posição pergunta
            VerificarResposta();
            Perguntas[QuestaoAtual].Resposta = txtResposta.Text;
            PreencherPergunta(Perguntas[QuestaoAtual]);
            VerificarPosicaoPergunta();
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

            btnEnviarColeta.Visibility = Perguntas.Where(p => p.IdAlternativa == 0 && String.IsNullOrEmpty(p.Resposta)).Count() > 0 ?
                ViewStates.Invisible : ViewStates.Visible;
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

        }
    }
}