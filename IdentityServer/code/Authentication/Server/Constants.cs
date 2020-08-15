using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public static class Constants
    {
        // 受众
        public const string Audiance = "https://localhost:6021";
        // 发布者
        public const string Issuer = Audiance;

        public const string Secret = "特别长的秘密字符串";
    }
}
