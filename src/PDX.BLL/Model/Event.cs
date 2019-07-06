using System;
using System.Collections.Generic;

namespace PDX.BLL.Model {
    public class Event {
       public string ApplicationCode { get; set; }
        public string EventTypeCode { get; set; }
        public string EnvironmentCode { get; set; }
        public List<EnvironmentData> EnvironmentDatas { get; set; }
        public List<UserEventData> UserEventDatas { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}