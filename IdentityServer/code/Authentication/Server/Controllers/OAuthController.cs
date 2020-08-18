using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string response_type, //authorization flow type
            string client_id,
            string redirect_uri,
            string scope,  // 你要请求的claims 例如email,custum claim etc
            string state)  // 随机生成的字符串，确保返回到相同的客户端
        {
            // 
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);
            return View(model: query.ToString());
        }
        [HttpPost]
        public IActionResult Authorize(string username, string password,
            string redirectUri,
            string state)
        {
            const string code = "fasdfjaieronav;soviu";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);

            return Redirect($"{redirectUri}{query.ToString()}");
        }

        //public async Task<IActionResult> Token(
        public async Task Token(
            string grant_type,  // OAuth的Flow类型，这里是Authorization Code Flow
            string code,
            string client_id,
            string redirect_uri,
            string refresh_token
            )
        {
            // 验证code 


            var claims = new[] {
                new Claim (JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim ("zhusirClaim1", "value1"),
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires:grant_type == "refresh_token" ? DateTime.Now.AddMinutes(5): DateTime.Now.AddMilliseconds(1),
                signingCredentials
                );

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "authTuorial",
                refresh_token = "RefreshTokenSample"
            };

            var responseJson = JsonConvert.SerializeObject(responseObject);
            var responseBytes = Encoding.UTF8.GetBytes(responseJson);
            await Response.Body.WriteAsync(responseBytes,0,responseBytes.Length);

            // 如果使用return和public async Task<IActionResult> Token( 就会报错。不知到为啥。

            //return Redirect(redirect_uri);
        }

        [Authorize]
        public IActionResult TokenValidate()
        {
            if (HttpContext.Request.Query.TryGetValue("access_token", out var accessToken))
            {
                // 做一些验证Token的操作
                return Ok();
            }
            return BadRequest();
            
        }
    }
}
