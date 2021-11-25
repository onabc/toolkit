using Caliburn.Micro;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ReactiveUI;
using Refit;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Toolkit.Client
{
    public class TranslatorViewModel : Screen, IDisplayModule
    {
        private BaduTranslatorConfig baduTranslatorConfig;

        private IBaiduTranslator baiduTranslator;

        public byte SortNumber => 0;
        public Geometry Icon => Application.Current.TryFindResource("translator") as Geometry;

        public override string DisplayName { get; set; } = "翻译";

        /// <summary>
        /// 原文
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// 译文
        /// </summary>
        public string ResultString { get; set; }

        public TranslatorViewModel(ToolkitConfig toolkitConfig)
        {
            baduTranslatorConfig = toolkitConfig.BaduTranslatorConfig;

            baiduTranslator = BuildBaiduTranslator(baduTranslatorConfig.BaseUrl);
        }

        public void Translate()
        {
            BaiduTranslate();
        }

        /// <summary>
        /// 百度翻译接口
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        private IBaiduTranslator BuildBaiduTranslator(string baseUrl) => RestService.For<IBaiduTranslator>(baseUrl,
            new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
            });

        /// <summary>
        /// 调用百度翻译
        /// </summary>
        private void BaiduTranslate()
        {
            if (string.IsNullOrWhiteSpace(QueryString))
            {
                return;
            }
            var queryParam = new BaiduTranslatorQueryParam(QueryString,
                baduTranslatorConfig.AppId,
                baduTranslatorConfig.SecretKey);
            baiduTranslator.Translate(queryParam)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Select(x => string.Join(Environment.NewLine, x.Results.Select(r => r.Dst)))
                .BindTo(this, x => x.ResultString);
        }
    }
}