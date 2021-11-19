using Akavache;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System;
using Refit;
using ReactiveUI;
using System.DirectoryServices;
using System.Collections.ObjectModel;

namespace Toolkit.Client.Views
{
    public class ToolboxViewModel : Screen, IDisplayModule
    {
        private readonly ILogger logger;

        public byte SortNumber { get; init; } = 0;

        public string Citynm { get; set; }
        public CityWeather CurCityWeather { get; set; }

        public override string DisplayName { get; set; } = "ToolKit";

        private readonly ObservableAsPropertyHelper<string> _UrlPathSegment;

        public ToolboxViewModel(ILogger logger)
        {
            this.logger = logger;

            this.ObservableForProperty(vm => vm.CurCityWeather)
                .Throttle(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler)
                .Subscribe(c => Citynm = CurCityWeather.Citynm);
        }

        [TransactionCallHandler]
        public virtual async Task Click()
        {
            var data = new CityWeather { Citynm = "bingo" };
            await BlobCache.UserAccount.InsertObject("data", data);

            BlobCache.UserAccount.GetObject<CityWeather>("data")
                .Subscribe(x => Citynm = x.Citynm, ex => Console.WriteLine("No Key!"));

            logger.Information("*******");
            await Task.Yield();
        }

        public async Task Query()
        {
            //http://api.k780.com/?app=weather.today&weaId=1015&appkey=10003&sign=b59bc3ef6191eb9f747dd4e83c99f2a4&format=json
            QueryParam param = new QueryParam
            {
                app = "weather.today",
                weaId = "1015",
                appkey = "10003",
                sign = "b59bc3ef6191eb9f747dd4e83c99f2a4",
                format = "json",
            };

            RestService.For<IGetWeather>("http://api.k780.com")
                .GetWeather(param)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Select(x => x.Result)
                .BindTo(this, x => x.CurCityWeather);
        }
    }

    public class QueryParam
    {
        public string app { get; set; }
        public string weaId { get; set; }
        public string appkey { get; set; }
        public string sign { get; set; }
        public string format { get; set; }
    }

    public interface IGetWeather
    {
        [Get("")]
        IObservable<Response> GetWeather([Query] QueryParam queryParam);
    }

    public class Response
    {
        public string Success { get; set; }
        public CityWeather Result { get; set; }
    }

    public class CityWeather
    {
        public string Week { get; set; }
        public string Citynm { get; set; }
        public string Weather { get; set; }
        public string Temperature { get; set; }
    }
}