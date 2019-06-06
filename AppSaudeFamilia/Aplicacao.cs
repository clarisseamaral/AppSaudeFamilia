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
using AppSaudeFamilia.DataLocal;
using AppSaudeFamilia.Util;

namespace AppSaudeFamilia
{
    [Application]
    public class Aplicacao : Application
    {
        public static string Token { get; set; }

        public static void Deslogar()
        {
            //Token = string.Empty;
            //TODO: apaggar registro da tbusuario
        }

        public Aplicacao(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            //UsuarioLogado = new List<UsuarioLogadoDTO>();

            UtilDataBase.CreateDataBase();

            //Carrega Dados Iniciais
            if (!UtilDataBase.TableExist(UsuarioDB.TableName))
                UtilDataBase.CreateTable(UsuarioDB.TableName, UsuarioDB.TableColumns);

            if (!UtilDataBase.TableExist(QuestionarioDB.TableName))
                UtilDataBase.CreateTable(QuestionarioDB.TableName, QuestionarioDB.TableColumns);

        } 

        public static void ResetarAplicacao()
        {
            //TODO: apaggar registro da tbusuario
        }
    }
}