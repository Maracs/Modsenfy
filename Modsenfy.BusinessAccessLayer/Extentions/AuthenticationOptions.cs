using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsenfy.BusinessAccessLayer.Extentions
{
    internal class AuthenticationOptions
    {
        public const string Issuer = "MyAuthServer"; 
        
        public const string Audience = "MyAuthClient"; 
        
        const string Key = "10fba9e7-bca3-4838-b097-50ae0a66ced32d51ae24-5f12-4309-9770-12015ff2ca81a75e1280-e929-4f84-816a-337337419683";
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
