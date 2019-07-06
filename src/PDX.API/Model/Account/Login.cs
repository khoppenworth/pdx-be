using PDX.Domain.Account;

namespace PDX.API.Model
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserLogin ClientInfo {get;set;}

    }
}