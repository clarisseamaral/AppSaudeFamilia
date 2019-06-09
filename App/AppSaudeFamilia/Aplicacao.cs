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
        public static int Fluxo { get; set; }

        public static string Token { get; set; }

        public Aplicacao(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            UtilDataBase.CreateDataBase();

            //Carrega Dados Iniciais
            if (!UtilDataBase.TableExist(UsuarioDB.TableName))
                UtilDataBase.CreateTable(UsuarioDB.TableName, UsuarioDB.TableColumns);

            if (!UtilDataBase.TableExist(QuestionarioDB.TableName))
                UtilDataBase.CreateTable(QuestionarioDB.TableName, QuestionarioDB.TableColumns);

        } 

        public static void ResetarAplicacao()
        {
            Token = string.Empty;
            UtilDataBase.Delete(UsuarioDB.TableName);
            UtilDataBase.Delete(QuestionarioDB.TableName);
        }
    }

    public class Constantes
    {
        public static int FLUXO_DESLOGAR { get { return 9159; } }
    }
}