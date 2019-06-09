using Android.App;

namespace AppSaudeFamilia.Util
{
    public static class UtilAcessibilidade
    {
        public static bool VerificaAcessoInternet(this Activity activityAtual)
        {
            Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)activityAtual.GetSystemService("connectivity");

            Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            return isOnline;
        }
    }
}