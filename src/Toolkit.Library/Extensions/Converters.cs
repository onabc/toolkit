﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Library.Extensions
{
    public static class Converters
    {
        public static string ToFileSizeString(this long size)
        {
            if (size < 1024)
            {
                return size.ToString("F0") + " bytes";
            }
            else if ((size >> 10) < 1024)
            {
                return (size / 1024F).ToString("F1") + " KB";
            }
            else if ((size >> 20) < 1024)
            {
                return ((size >> 10) / 1024F).ToString("F1") + " MB";
            }
            else if ((size >> 30) < 1024)
            {
                return ((size >> 20) / 1024F).ToString("F1") + " GB";
            }
            else if ((size >> 40) < 1024)
            {
                return ((size >> 30) / 1024F).ToString("F1") + " TB";
            }
            else if ((size >> 50) < 1024)
            {
                return ((size >> 40) / 1024F).ToString("F1") + " PB";
            }
            else
            {
                return ((size >> 50) / 1024F).ToString("F0") + " EB";
            }
        }
    }
}