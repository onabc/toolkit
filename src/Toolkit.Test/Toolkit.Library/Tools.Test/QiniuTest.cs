using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Library.Tools;

namespace Toolkit.Test
{
    [TestFixture]
    public class QiniuTest
    {
        private QiniuUtil util;

        [SetUp]
        public void Initialize()
        {
            string accessKey = "AfCvznuKBCL5wNQDzrPrmSLaQXLnr_xBG45UJJyY";
            string secretKey = "HRLk9hU8fVT1xOWOfAJTbCbPyHOvKTWzf9Zbn_VP";
            string bucket = "daboq";
            string baseurl = "http://storage.l-99.space/";

            util = new QiniuUtil(accessKey, secretKey, bucket, baseurl);
        }

        [Test]
        public async Task ListFileTest()
        {
            var list = await util.ListFilesAsync();
            Assert.IsNotEmpty(list);
        }
    }
}