using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using PDX.BLL.Helpers;

namespace PDX.BLL.Helpers {
    public static class DataTableHelper {
        public static Expression<Func<T, bool>> ConstructFilter<T> (string search, List<string> columns, bool activeOnly = false) where T : class {
            ParameterExpression argument = Expression.Parameter (typeof (T), "t");
            Expression predicateBody = null;

            if (activeOnly) {
                predicateBody = argument.GetExpression ("IsActive", activeOnly, "Equal", typeof (bool));
            }

            if (!string.IsNullOrEmpty (search) && search.Length > 2) {
                Expression pb = null;
                foreach (var str in columns) {
                    var exp = argument.StringContains (str, search);
                    pb = pb == null ? exp : Expression.OrElse (pb, exp);
                }
                predicateBody = predicateBody == null ? pb : Expression.AndAlso (predicateBody, pb);

            }
            Expression<Func<T, bool>> expression = predicateBody != null ? Expression.Lambda<Func<T, bool>> (predicateBody, new [] { argument }) : null;
            return expression;
        }
        public static Expression<Func<T, bool>> AndAlso<T> (this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2) {
            // need to detect whether they use the same
            // parameter instance; if not, they need fixing
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals (param, expr2.Parameters[0])) {
                // simple version
                return Expression.Lambda<Func<T, bool>> (
                    Expression.AndAlso (expr1.Body, expr2.Body), param);
            }
            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>> (
                Expression.AndAlso (
                    expr1.Body,
                    Expression.Invoke (expr2, param)), param);
        }
    }
}