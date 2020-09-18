using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.Client.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class SecretBase : ComponentBase
    {
        [Inject]
        
        private IHttpClientFactory _httpClientFactory { set; get; }

        public string Secret { get; set; } = "这个一个机密内容！";
    }


}
