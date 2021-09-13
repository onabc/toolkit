using Toolkit.Library.Extensions;
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
    public class EnumExtensionsTest
    {
        [Test]
        public void GetDescriptionTest()
        {
            var day = Week.Friday;
            string description = day.GetDescription();

            Assert.AreEqual(description, "星期五");
        }
    }

    public enum Week
    {
        [System.ComponentModel.Description("星期一")]
        Monday,

        [System.ComponentModel.Description("星期二")]
        Tuesday,

        [System.ComponentModel.Description("星期三")]
        Wednesday,

        [System.ComponentModel.Description("星期四")]
        Thursday,

        [System.ComponentModel.Description("星期五")]
        Friday,

        [System.ComponentModel.Description("星期六")]
        Saturday,

        [System.ComponentModel.Description("星期日")]
        Sunday
    }
}