using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qiniu.Common;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.RS;
using Qiniu.RS.Model;
using Qiniu.Util;

namespace Toolkit.Library.Tools
{
    /// <summary>
    /// 七牛对象存储工具类
    /// </summary>
    public class QiniuUtil
    {
        /// <summary>
        /// 访问密钥
        /// </summary>
        private string accessKey;

        /// <summary>
        /// 密钥
        /// </summary>
        private string secretKey;

        /// <summary>
        /// 存储空间名
        /// </summary>
        private string bucket;

        /// <summary>
        ///
        /// </summary>
        private Mac mac;

        /// <summary>
        /// Qiniu成功状态码
        /// </summary>
        private readonly int sucessCode = 200;

        /// <summary>
        /// 域名
        /// </summary>
        private string baseUrl;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        /// <param name="bucket"></param>
        public QiniuUtil(
            [NotNull] string accessKey,
            [NotNull] string secretKey,
            [NotNull] string bucket,
            [NotNull] string baseUrl
            )
        {
            this.accessKey = accessKey;
            this.secretKey = secretKey;
            this.bucket = bucket;
            this.baseUrl = baseUrl;

            mac = new(accessKey, secretKey);
        }

        /// <summary>
        /// 简单上传文件
        /// </summary>
        /// <param name="localFile"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public async Task<bool> UploadFileAsync(string localFile)
        {
            if (!File.Exists(localFile))
                throw new FileNotFoundException();

            string saveKey = Path.GetFileNameWithoutExtension(localFile);

            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);

            UploadManager um = new UploadManager();
            HttpResult result = await um.UploadFileAsync(localFile, saveKey, token);

            return result.Code == sucessCode;
        }

        /// <summary>
        /// 获取空间文件列表
        /// </summary>
        public async Task<IEnumerable<ObjectDesc>> ListFilesAsync()
        {
            var list = new List<ObjectDesc>();

            string marker = ""; // 首次请求时marker必须为空
            string prefix = null; // 按文件名前缀保留搜索结果
            string delimiter = null; // 目录分割字符(比如"/")
            int limit = 100; // 单次列举数量限制(最大值为1000)
            BucketManager bm = new BucketManager(mac);
            do
            {
                var result = await bm.ListAsync(bucket, prefix, marker, limit, delimiter);

                var items = result.Result.Items.ConvertAll(s => s.ConvertToObjectDesc());
                list.AddRange(items);

                marker = result.Result.Marker;
            } while (!string.IsNullOrEmpty(marker));

            return list;
        }

        /// <summary>
        /// 获取文件访问链接
        /// </summary>
        /// <returns></returns>
        public string GetAccessUrl(string key)
        {
            string rawUrl = $"{baseUrl}/{key}";
            // 设置下载链接有效期3600秒
            int expireInSeconds = 3600;
            return DownloadManager.CreateSignedUrl(mac, rawUrl, expireInSeconds);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public async Task<bool> DownloadFile(string key, string saveFile)
        {
            var accUrl = GetAccessUrl(key);
            HttpResult result = await DownloadManager.DownloadAsync(accUrl, saveFile);

            return result.Code == sucessCode;
        }
    }
}