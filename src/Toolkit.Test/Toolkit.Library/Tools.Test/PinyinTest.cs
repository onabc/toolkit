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
    public class PinyinTest
    {
        private PinyinFormat format;

        [SetUp]
        public void Initialize()
        {
            format = PinyinFormat.WITH_TONE_MARK | PinyinFormat.LOWERCASE | PinyinFormat.WITH_U_UNICODE;
        }

        [Test]
        public void GetPinyinTest()
        {
            string text = "你好";
            string result = Pinyin4Net.GetPinyin(text, format);

            Assert.AreEqual("nĭ hăo", result);
        }
    }
}