using Toolkit.Library.Tools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Test
{
    [TestFixture]
    public class HttpTest
    {
        private HttpUtil http;

        [SetUp]
        public void Initialize()
        {
            string baseUri = "https://www.l-99.space/";
            http = new HttpUtil(baseUri);
        }

        [Test(Description = "")]
        public void PostAsyncTest()
        {
            string loginUrl = "api/account/login";
            var data = new
            {
                Phone = "18262722688",
                Password = "e10adc3949ba59abbe56e057f20f883e"
            };
            AsyncTestDelegate @delegate = async ()
                => await http.PostAsync<object>(loginUrl, data);
            Assert.DoesNotThrowAsync(@delegate);
        }
    }
}