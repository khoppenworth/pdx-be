using System.Collections.Generic;

namespace PDX.BLL.Model
{
    public class UserEventData
    {
        public string Userguid { get; set; }
        public string FullName { get; set; }
        public string Data { get; set; }
        public List<UserAddress> Addresses { get; set; }
        public bool IsRequired { get; set; }
    }
}