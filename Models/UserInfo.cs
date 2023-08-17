using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kalumAutManagement.Models
{
    public class UserInfo
    {
        public string Id {get;set;}
        public string UserName {get;set;}
        public string NormalizedUserName {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public List<string> Roles {get;set;}
    }
}