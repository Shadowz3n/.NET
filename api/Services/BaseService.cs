using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace API.Services
{
    public abstract class BaseService
    {

        protected string Url { get; private set; }

        protected BaseService(string url)
        {
            Url = url;
        }

        protected StringContent ToStringContent(object valor)
        {
            return new StringContent(JsonConvert.SerializeObject(valor), Encoding.UTF8, "application/json");
        }

        #region Get
        protected async Task<HttpResponseMessage> GetAsync(string url, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue))
        {
            using (var client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = authorization;
                }
                return await client.GetAsync(Url + url);
            }
        }

        protected async Task<T> GetAsync<T>(string url, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            using (var client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = authorization;
                }

                HttpResponseMessage responseMessage = await client.GetAsync(Url + url);
                if (ensureSuccessStatusCode)
                    responseMessage.EnsureSuccessStatusCode();
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
        }
        #endregion

        #region Post
        protected async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            using (var client = new HttpClient())
                return await client.PostAsync(Url + url, content);
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, object content)
        {
            return await PostAsync(url, ToStringContent(content));
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, AuthenticationHeaderValue authorization, bool ensureSuccessStatusCode = false)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = authorization;
                var retorno = await client.PostAsync(Url + url, content);
                if (ensureSuccessStatusCode)
                    retorno.EnsureSuccessStatusCode();
                return retorno;
            }
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, object content, AuthenticationHeaderValue authorization, bool ensureSuccessStatusCode = false)
        {
            return await PostAsync(url, ToStringContent(content), authorization);
        }



        protected async Task<T> PostAsync<T>(string url, HttpContent content, bool ensureSuccessStatusCode = false)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                var responseMessage = await client.PostAsync(Url + url, content);
                if (ensureSuccessStatusCode)
                    responseMessage.EnsureSuccessStatusCode();
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
        }

        protected async Task<T> PostAsync<T>(string url, object content, bool ensureSuccessStatusCode = false)
        {
            return await PostAsync<T>(url, ToStringContent(content), ensureSuccessStatusCode);
        }

        protected async Task<T> PostAsync<T>(string url, HttpContent content, AuthenticationHeaderValue autorization, bool ensureSuccessStatusCode = false)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = autorization;
                var responseMessage = await client.PostAsync(Url + url, content);

                string responseContent = string.Empty;
                if (ensureSuccessStatusCode)
                    responseMessage.EnsureSuccessStatusCode();
                responseContent = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = JsonConvert.DeserializeObject<BadRequestDTO>(responseContent);
                    return JsonConvert.DeserializeObject<T>(bad.Message);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
        }

        protected async Task<T> PostAsync<T>(string url, object content, AuthenticationHeaderValue autorization, bool ensureSuccessStatusCode = false)
        {
            return await PostAsync<T>(url, ToStringContent(content), autorization, ensureSuccessStatusCode);
        }
        #endregion

        #region Put
        protected async Task<HttpResponseMessage> PutAsync(string url, HttpContent content, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            using (var client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = authorization;
                }
                HttpResponseMessage ret = await client.PutAsync(Url + url, content);
                if (ensureSuccessStatusCode)
                    ret.EnsureSuccessStatusCode();
                return ret;
            }
        }

        protected async Task<HttpResponseMessage> PutAsync(string url, object content, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            return await PutAsync(url, ToStringContent(content), authorization, ensureSuccessStatusCode);
        }

        protected async Task<T> PutAsync<T>(string url, HttpContent content, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            using (var client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = authorization;
                }
                HttpResponseMessage responseMessage = await client.PutAsync(Url + url, content);
                if (ensureSuccessStatusCode)
                    responseMessage.EnsureSuccessStatusCode();
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
        }

        protected async Task<T> PutAsync<T>(string url, object content, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            return await PutAsync<T>(url, ToStringContent(content), authorization, ensureSuccessStatusCode);
        }
        #endregion

        #region Delete
        protected async Task<HttpResponseMessage> DeleteAsync(string url, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            using (var client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = authorization;
                }
                HttpResponseMessage ret = await client.DeleteAsync(Url + url);
                if (ensureSuccessStatusCode)
                    ret.EnsureSuccessStatusCode();
                return ret;
            }
        }

        protected async Task<T> DeleteAsync<T>(string url, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            using (var client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = authorization;
                }
                HttpResponseMessage ret = await client.DeleteAsync(Url + url);
                if (ensureSuccessStatusCode)
                    ret.EnsureSuccessStatusCode();
                string responseContent = await ret.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
        }
        #endregion

        #region Head
        protected async Task<HttpResponseMessage> HeadAsync(string url, AuthenticationHeaderValue authorization = default(AuthenticationHeaderValue), bool ensureSuccessStatusCode = default(bool))
        {
            using (var client = new HttpClient())
            {
                if (authorization != null)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = authorization;
                }
                var requestMensagem = new HttpRequestMessage
                {
                    Method = HttpMethod.Head,
                    RequestUri = new Uri(Url + url)
                };
                HttpResponseMessage ret = await client.SendAsync(requestMensagem);
                if (ensureSuccessStatusCode)
                    ret.EnsureSuccessStatusCode();
                return ret;
            }
        }
        #endregion

    }
}