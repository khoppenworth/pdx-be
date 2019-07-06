using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PDX.API.Helpers
{
    /// <summary>
    /// Helpers on HttpContext
    /// </summary>
    public static class HttpContextHelper
    {
        /// <summary>
        /// Read UserID from ClaimsPrincipal
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        public static int GetUserID(this ClaimsPrincipal claimsIdentity){
            var claim = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.PrimarySid);            
            return Convert.ToInt32(claim.Value);
        }

        /// <summary>
        /// Read UserID from HttpContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetUserID(this Microsoft.AspNetCore.Http.HttpContext context){
            var claimsIdentity = context.User;
            var claim = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.PrimarySid);            
            return Convert.ToInt32(claim.Value);
        }
    }
}