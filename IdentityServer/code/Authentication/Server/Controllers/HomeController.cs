using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Authenticate()
        {
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
                expires: DateTime.Now.AddHours(1),
                signingCredentials
                );

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { access_token = tokenJson });
        }

        /// <summary>
        /// 解密JWT
        /// </summary>
        /// <param name="jwt">jwt</param>
        /// <returns></returns>
        public IActionResult Decode(string jwt)
        {
            string[] arrayJwt = jwt.Split('.');

            string decodeJwt = "";
            for (int i = 0; i < arrayJwt.Length; i++)
            {
                if (i == arrayJwt.Length - 1)
                {
                    decodeJwt += "加密签名无法解密";
                }
                else
                {
                    // 不能直接用Convert.FromBase64String，会报错，微软二逼，必须对base64加密字符串按照规则整理一下，具体怎么整理看这个方法的内容
                    var decodeBtyes = ConvertFromBase64String(arrayJwt[i]);
                    decodeJwt += Encoding.UTF8.GetString(decodeBtyes);
                }
            }

            return Ok(decodeJwt);
        }
        private static byte[] ConvertFromBase64String(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return null;
            try
            {
                string working = input.Replace('-', '+').Replace('_', '/'); ;
                while (working.Length % 4 != 0)
                {
                    working += '=';
                }
                return Convert.FromBase64String(working);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult ResetCookie()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
