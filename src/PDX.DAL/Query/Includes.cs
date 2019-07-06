using System;
using System.Linq;

namespace PDX.DAL.Query
{
    public class Includes<T>
    {
        public Includes(Func<IQueryable<T>, IQueryable<T>> expression)
        {
            Expression = expression;
        }

        public Func<IQueryable<T>, IQueryable<T>> Expression { get; private set; }
    }
}