using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Toolkit.Library.Tools
{
    /// <summary>
    /// HTTP访问工具类
    /// </summary>
    public class HttpUtil
    {
        /// <summary>
        /// 环境域名
        /// </summary>
        private string baseUri;

        /// <summary>
        /// 超时时间
        /// </summary>
        private int timeout;

        /// <summary>
        /// 客户端工厂
        /// </summary>
        private IHttpClientFactory clientFactory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseUri">环境域名</param>
        /// <param name="timeout">超时时间/秒</param>
        public HttpUtil(string baseUri, int timeout = 6)
        {
            this.baseUri = baseUri;
            this.timeout = timeout;
            this.clientFactory = new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetService<IHttpClientFactory>();
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url, string token = default)
        {
            using var httpClient = CreateClient(token);

            var response = await httpClient
                .GetAsync(url)
                .ConfigureAwait(false);
            return await ReadContentAsync<T>(response);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string url, object content, string token = default)
        {
            using var httpClient = CreateClient(token);

            var response = await httpClient
                .PostAsJsonAsync(url, content)
                .ConfigureAwait(false);
            return await ReadContentAsync<T>(response);
        }

        /// <summary>
        /// Put请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string url, object content, string token = default)
        {
            using var httpClient = CreateClient(token);

            var response = await httpClient
                .PutAsJsonAsync(url, content)
                .ConfigureAwait(false);
            return await ReadContentAsync<T>(response);
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync<T>(string url, string token = default)
        {
            using var httpClient = CreateClient(token);

            var response = await httpClient
                .DeleteAsync(url)
                .ConfigureAwait(false);
            return await ReadContentAsync<T>(response);
        }

        /// <summary>
        /// 构建客户端
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private HttpClient CreateClient(string token = default)
        {
            var httpClient = clientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUri);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
            }

            return httpClient;
        }

        /// <summary>
        /// 解析返回内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<T> ReadContentAsync<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                using var ms = new MemoryStream();
                await response.Content.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var sr = new StreamReader(ms);
                string responseContent = sr.ReadToEnd();

                return JsonSerializer.Deserialize<T>(responseContent);
            }
            return default;
        }
    }
}