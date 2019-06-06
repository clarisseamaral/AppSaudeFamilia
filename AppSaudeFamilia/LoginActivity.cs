using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AppSaudeFamilia.DataLocal;
using AppSaudeFamilia.Servico;
using AppSaudeFamilia.Util;
using System;
using System.Threading.Tasks;

namespace AppSaudeFamilia
{
    [Activity(Label = "Saúde Família", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var dtUsuario = UtilDataBase.GetItems(UsuarioDB.TableName);
            if (dtUsuario.Rows.Count > 0)
            {
                Aplicacao.Token = dtUsuario.Rows[0]["Token"].ToString();
                ExibirTelaInicial();
            }
            else
            {
                SetContentView(Resource.Layout.Login);

                FindViewById<Button>(Resource.Id.btnLogin).Click += BtnLogin_Click;
            }

            ///if (Aplicacao.Fluxo == Constantes.FLUXO_DESLOGAR)
            //{
            //    ProgressDialog load = ProgressDialog.Show(this, "Deslogando", "Por favor aguarde!", true);
            //    await Task.Run(() =>
            //    {
            //        Aplicacao.ResetarAplicacao();
            //        UtilDataBase.Delete(UsuarioDB.TableName);
            //    });
            //    }

        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            var txtUsuario = (FindViewById<EditText>(Resource.Id.txtUsuario));
            var txtSenha = (FindViewById<EditText>(Resource.Id.txtPassword));
            var mensagem = Resource.String.LoadingTitulo.ToString();

            await Task.Run(async () =>
            {
                if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtSenha.Text))
                {
                    var entrada = new AutenticacaoEntradaDTO() { Usuario = txtUsuario.Text.Trim(), Senha = txtSenha.Text };
                    var saida = await WebService.PostAsync<AutenticacaoEntradaDTO, AutenticacaoSaidaDTO>(entrada, CaminhoWebService.AUTENTICACAO);

                    VerificaLogin(saida.Token);
                }
                else
                {
                    Modal.ExibirModal(this, "Email ou senha em branco", "", "Gentileza informar os campos E-mail e Senha");
                }
            });

            //Esconder o teclado antes de passar para próxima Activity
            InputMethodManager im = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            im.HideSoftInputFromWindow(Window.DecorView.WindowToken, HideSoftInputFlags.NotAlways);

        }

        private void VerificaLogin(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                Aplicacao.Token = token;

                SalvaBancoLocal(token);
                ExibirTelaInicial();
            }
            else
            {
                Modal.ExibirModal(this, GetString(Resource.String.AutenticacaoInvalidaTitulo), "", GetString(Resource.String.AutenticacaoInvalidaMensagem));
            }
        }

        private void ExibirTelaInicial()
        {
            var activity = new Intent(this, typeof(TelaInicialActivity));
            StartActivity(activity);
        }

        private bool VerificaUsuarioLogado()
        {
            //TODO : buscar da base tabbela usuario
            return true;
        }

        private void SalvaBancoLocal(string token)
        {
            var tbUsuario = new UsuarioDB(token);
            var stringBuilder = new System.Text.StringBuilder();

            stringBuilder.AppendFormat("{0};", tbUsuario.InsertQuery);

            UtilDataBase.Save(stringBuilder.ToString());
        }
    }
}