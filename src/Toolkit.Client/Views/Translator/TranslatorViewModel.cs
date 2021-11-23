using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ReactiveUI;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Toolkit.Client
{
    public class TranslatorViewModel : Screen, IDisplayModule
    {
        public byte SortNumber => 0;
        public Geometry Icon => Application.Current.TryFindResource("translator") as Geometry;

        public override string DisplayName { get; set; } = "翻译";

        private BaduTranslatorConfig baduTranslatorConfig;

        private IBaiduTranslator baiduTranslator;

        /// <summary>
        /// 查询文本
        /// </summary>
        public string QueryString { get; set; }

        public TranslatorViewModel(ToolkitConfig toolkitConfig)
        {
            baduTranslatorConfig = toolkitConfig.BaduTranslatorConfig;

            baiduTranslator = BuildBaiduTranslator(baduTranslatorConfig.BaseUrl);
        }

        private IBaiduTranslator BuildBaiduTranslator(string baseUrl) => RestService.For<IBaiduTranslator>(baseUrl,
            new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
    )
            });

        public void Translate()
        {
            BaiduTranslate();
        }

        public IEnumerable<BaiduTranslatorResult> Results { get; set; }

        private void BaiduTranslate()
        {
            string salt = new Random().Next(100000).ToString();
            string sign = EncryptString(baduTranslatorConfig.AppId + QueryString + salt + baduTranslatorConfig.SecretKey);
            var queryParam = new
            {
                q = QueryString,
                appid = baduTranslatorConfig.AppId,
                from = "en",
                to = "zh",
                salt = salt,
                sign = sign
            };
            baiduTranslator.Translate(queryParam)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Subscribe(TEST);

            // 计算MD5值
            static string EncryptString(string str)
            {
                MD5 md5 = MD5.Create();
                // 将字符串转换成字节数组
                byte[] byteOld = Encoding.UTF8.GetBytes(str);
                // 调用加密方法
                byte[] byteNew = md5.ComputeHash(byteOld);
                // 将加密结果转换为字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in byteNew)
                {
                    // 将字节转换成16进制表示的字符串，
                    sb.Append(b.ToString("x2"));
                }
                // 返回加密的字符串
                return sb.ToString();
            }
        }

        private void TEST(object OBJ)
        {
        }
    }
}