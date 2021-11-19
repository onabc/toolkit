#region << 版 本 注 释 >>

/*
 * ----------------------------------------------------------------
 * Copyright @ lumen 2021. All rights reserved.

 * 作    者 ：lumen （DaboQ）

 * 创建时间 ：2021/11/15 15:29:31

 * CLR 版本 ：4.0.30319.42000

 * 命名空间 ：Toolkit.Client.Views.Toolbox

 * 类 名 称 ：RequestPropertyHandler

 * 类 描 述 ：

 * ------------------------------------------------------
 * 历史更新记录

 * 版本 ：  V1.0.0.0        修改时间：2021/11/15 15:29:31         修改人：lumen （DaboQ）

 * 修改内容：
 *
 */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toolkit.Client
{
    internal class RequestPropertyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}