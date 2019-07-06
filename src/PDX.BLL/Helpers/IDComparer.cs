using System.Collections.Generic;
using PDX.Domain;

namespace PDX.BLL.Helpers
{
    public class IDComparer : IEqualityComparer<BaseEntity>
    {
        public bool Equals(BaseEntity x, BaseEntity y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (object.ReferenceEquals(x, null) ||
                object.ReferenceEquals(y, null))
            {
                return false;
            }
            return x.ID == y.ID;
        }

        public int GetHashCode(BaseEntity obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.ID.GetHashCode();
        }
    }
}