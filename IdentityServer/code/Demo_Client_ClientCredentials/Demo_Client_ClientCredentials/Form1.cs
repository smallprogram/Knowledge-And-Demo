using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Client_ClientCredentials
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private async void Button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                richTextBox1.Text = disco.Error;
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "Api1"
            });

            if (tokenResponse.IsError)
            {
                richTextBox1.Text = disco.Error;
                return;
            }

            richTextBox1.Text = tokenResponse.Json.ToString();

            //调用API
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:6001/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                richTextBox2.Text = response.StatusCode.ToString();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                richTextBox2.Text = JArray.Parse(content).ToString();
            }
        }
    }
}
