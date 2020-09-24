using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.WebAssembly.Client.Pages
{
    public class ApiResourceBase :ComponentBase
    {
        [Inject]
        IHttpClientFactory HttpClientFactory { set; get; }

        public string result { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var Client = HttpClientFactory.CreateClient("ServerAPI");
                var baseuri = Client.BaseAddress;
                var result = await Client.GetFromJsonAsync<string>("api/secret");
                
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

    }
}
