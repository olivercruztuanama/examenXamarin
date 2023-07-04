using Examen.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Examen.Services
{
    public class ApiService
    {
        public async Task<Response> CallApi<T>(string apiPath, string request, bool isGet = false)
        {
            var resultado = new Response();
            try
            {
                using (var handler = new HttpClientHandler { UseCookies = false })
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    var client2 = new HttpClient(handler);
                    client2.Timeout = Timeout.InfiniteTimeSpan;
                    var requestData = new HttpRequestMessage(isGet ? HttpMethod.Post : HttpMethod.Get, apiPath);
                    if (request != string.Empty)
                    {
                        requestData.Content = new StringContent(request, Encoding.UTF8, "application/json");
                    }

                    requestData.Headers.TryAddWithoutValidation("Authorization", String.Format("Bearer {0}", ""));
                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client2.SendAsync(requestData).ConfigureAwait(false);
                    if (!response.IsSuccessStatusCode)
                    {
                        resultado = new Response
                        {
                            IsSuccess = false,
                            Message = response.StatusCode.ToString()
                        };
                    }
                    else
                    {
                        var data = response.Content.ReadAsStringAsync().Result;
                        resultado = new Response
                        {
                            IsSuccess = true,
                            Message = "Ok",
                        };
                        resultado.Result = JsonConvert.DeserializeObject<List<T>>(data);
                    }
                    response.Dispose();
                    requestData.Dispose();
                    client2.Dispose();
                    handler.Dispose();
                }
            }
            catch (Exception ex)
            {
                resultado = new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

            return resultado;
        }
    }
}
