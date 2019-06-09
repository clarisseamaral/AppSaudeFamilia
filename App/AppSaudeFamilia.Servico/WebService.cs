using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppSaudeFamilia.Servico
{
    public static class WebService
    {
        public static string EnderecoBase = "https://coleta.azurewebsites.net/";

        public static U Post<T, U>(T entrada, string caminho)
        {
            var retorno = Activator.CreateInstance<U>();
            var client = new HttpClient();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                client.BaseAddress = new Uri(EnderecoBase);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var param = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(caminho, contentPost).Result;

                if (response.IsSuccessStatusCode)
                {
                    retorno = JsonConvert.DeserializeObject<U>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retorno;
        }

        public static async Task<bool> PostSemSaida<T>(T entrada, string caminho, string token)
        {
            var retorno = Activator.CreateInstance<bool>();
            var client = new HttpClient();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(EnderecoBase);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var param = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(caminho, contentPost);

                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

             return false;
        }

        public static async Task<U> PostAsync<U>(string caminho)
        {
            var retorno = Activator.CreateInstance<U>();
            var client = new HttpClient();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                client.BaseAddress = new Uri(EnderecoBase);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent contentPost = new StringContent("", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(caminho, contentPost);

                if (response.IsSuccessStatusCode)
                {
                    var resposta = await response.Content.ReadAsStringAsync();
                    retorno = JsonConvert.DeserializeObject<U>(resposta);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retorno;
        }

        public static T Get<T>(string caminho, string token)
        {
            var retorno = Activator.CreateInstance<T>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var caminhoCompleto = EnderecoBase + caminho;
                    var content = httpClient.GetStringAsync(caminhoCompleto).Result;

                    var serializer2 = new DataContractJsonSerializer(typeof(T));
                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
                    {
                        retorno = (T)serializer2.ReadObject(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retorno;
        }

        public static async Task<U> PostAsync<T, U>(T entrada, string caminho)
        {
            var retorno = Activator.CreateInstance<U>();
            var client = new HttpClient();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                client.BaseAddress = new Uri(EnderecoBase);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var param = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
                HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(caminho, contentPost);

                if (response.IsSuccessStatusCode)
                {
                    var resposta = await response.Content.ReadAsStringAsync();
                    retorno = JsonConvert.DeserializeObject<U>(resposta);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retorno;
        }

        public static async Task<T> GetAsync<T>(string caminho, string token)
        {
            var retorno = Activator.CreateInstance<T>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var caminhoCompleto = EnderecoBase + caminho;
                    var content = await httpClient.GetStringAsync(caminhoCompleto);

                    retorno = JsonConvert.DeserializeObject<T>(content);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retorno;
        }
    }
}