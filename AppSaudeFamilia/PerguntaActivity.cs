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
    [Activity(Label = "PerguntaActivity")]
    public class PerguntaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Pergunta);
            // Create your application here
        }

        public List<ListaPerguntasSaidaDTO> Perguntas { get; set; }

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

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Coleta);

            CarregaElementosTela();
        }

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
        }

        private async void ListarPerguntas()
        {
            var saida = await WebService.GetAsync<List<ListaPerguntasSaidaDTO>>(string.Empty);

            var pergunta = saida.First();

            txtPergunta.Text = pergunta.Descricao;

            if (pergunta.TipoPergunta == "1")  //Multipla escolha
            {
                txtResposta.Visibility = Android.Views.ViewStates.Invisible;
                rdOpcoes.Visibility = Android.Views.ViewStates.Visible;
                PreencherAlternativas(pergunta.Alternativas);
            }
            else if (pergunta.TipoPergunta == "2")
            {
                txtResposta.Visibility = Android.Views.ViewStates.Visible;
                rdOpcoes.Visibility = Android.Views.ViewStates.Invisible;
            }
        }

        private void PreencherAlternativas(List<Alternativa> alternativa)
        {
            rbOpcao1.Text = string.Empty;
            rbOpcao2.Text = string.Empty;
            rbOpcao3.Text = string.Empty;
            rbOpcao4.Text = string.Empty;

            for (int i = 0; i < alternativa.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        rbOpcao1.Text = alternativa[i].Texto;
                        break;
                    case 1:
                        rbOpcao2.Text = alternativa[i].Texto;
                        break;
                    case 2:
                        rbOpcao3.Text = alternativa[i].Texto;
                        break;
                    case 3:
                        rbOpcao4.Text = alternativa[i].Texto;
                        break;
                }
            }
        }


        private void BtnProimo_Click(object sender, EventArgs e)
        {
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
        }
    }
}