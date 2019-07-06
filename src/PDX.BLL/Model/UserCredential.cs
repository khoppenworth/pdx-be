using System.Collections.Generic;
using PDX.Domain.Account;

namespace PDX.BLL.Model
{
    public class UserCredential
    {
        public User User { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}