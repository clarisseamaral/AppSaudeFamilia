using Android.App;
using Android.OS;
using Android.Widget;
using AppSaudeFamilia.Servico;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Views;

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
            rdOpcoes = FindViewById<RadioGroup>(Resource.Id.rdOpcoes);
            rbOpcao1 = FindViewById<RadioButton>(Resource.Id.rbOpcao1);
            rbOpcao2 = FindViewById<RadioButton>(Resource.Id.rbOpcao2);
            rbOpcao3 = FindViewById<RadioButton>(Resource.Id.rbOpcao3);
            rbOpcao4 = FindViewById<RadioButton>(Resource.Id.rbOpcao4);

            QuestaoAtual = 0;

            btnAnterior.Click += BtnAnterior_Click;

            btnProximo.Click += BtnProximo_Click;
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
                PreencherAlternativas(pergunta.Alternativas);
            }
            else if (pergunta.IdTipoPergunta == 2)
            {
                txtResposta.Visibility = Android.Views.ViewStates.Visible;
                rdOpcoes.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao1.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao2.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao3.Visibility = Android.Views.ViewStates.Invisible;
                rbOpcao4.Visibility = Android.Views.ViewStates.Invisible;
            }
        }

        private void PreencherAlternativas(List<Alternativa> alternativa)
        {
            rbOpcao1.Visibility = ViewStates.Invisible;
            rbOpcao2.Visibility = ViewStates.Invisible;
            rbOpcao3.Visibility = ViewStates.Invisible;
            rbOpcao4.Visibility = ViewStates.Invisible;

            for (int i = 0; i < alternativa.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        rbOpcao1.Text = alternativa[i].Texto;
                        rbOpcao1.Visibility = ViewStates.Visible;
                        break;
                    case 1:
                        rbOpcao2.Text = alternativa[i].Texto;
                        rbOpcao2.Visibility = ViewStates.Visible;
                        break;
                    case 2:
                        rbOpcao3.Text = alternativa[i].Texto;
                        rbOpcao3.Visibility = ViewStates.Visible;
                        break;
                    case 3:
                        rbOpcao4.Text = alternativa[i].Texto;
                        rbOpcao4.Visibility = ViewStates.Visible;
                        break;
                }
            }
        }

        private void BtnProximo_Click(object sender, EventArgs e)
        {
            QuestaoAtual++;
            PreencherPergunta(Perguntas[QuestaoAtual]);
            VerificarPosicaoPergunta();
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            QuestaoAtual--;
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

        }
    }
}