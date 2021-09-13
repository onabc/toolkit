using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Toolkit.Library.Tools
{
    /// <summary>
    /// 本地存储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NativeStorage<T> where T : class
    {
        private readonly string file_name;
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(T));

        public NativeStorage(string file_name)
        {
            this.file_name = file_name;

            CreateStorageFile();
        }

        private void CreateStorageFile()
        {
            var isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (!isoStore.FileExists(file_name))
            {
                var stream = new IsolatedStorageFileStream(file_name, FileMode.Create, isoStore);
                stream.Close();
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T value = default;
            var isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isoStore.FileExists(file_name))
            {
                using (var isoStream = new IsolatedStorageFileStream(file_name, FileMode.Open, isoStore))
                {
                    value = serializer.Deserialize(isoStream) as T;
                }
            }
            return value;
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="data"></param>
        public void Put(T data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            var isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isoStore.FileExists(file_name))
            {
                using (var isoStream = new IsolatedStorageFileStream(file_name, FileMode.Create, isoStore))
                {
                    serializer.Serialize(isoStream, data);
                }
            }
        }
    }
}