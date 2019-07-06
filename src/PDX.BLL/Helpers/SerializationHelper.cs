using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace PDX.BLL.Helpers
{
    public static class SerializationHelper
    {
        public static IList<S> Deserialize<S> (IList<string> data) where S : class, new()
        {
            IList<S> list = new List<S>();
            try
            {
                if (data != null)
                {                    
                    foreach (var d in data)
                    {
                        var desObject = JsonConvert.DeserializeObject<dynamic>(d);
                        var keys = GetPropertyKeysForDynamic(desObject);

                        S s = new S();

                        foreach (var key in keys)
                        {
                            try
                            {
                                var value = desObject[key];
                                var valueConvertible =  value?.Value as IConvertible;
                                PropertyInfo propertyInfo = s.GetType().GetProperty((string)key);
                                propertyInfo.SetValue(s, Convert.ChangeType(valueConvertible, propertyInfo.PropertyType), null);
                            }
                            catch(Exception ex)
                            {
                                s = default(S);
                                continue;
                            }
                        }
                        list.Add(s);
                    }
                    return list;
                }
            }
            catch(Exception ex) {

            }

            return list;
        }

         //Get list of property of a dynamic object
        private static IList<string> GetPropertyKeysForDynamic(dynamic dynamicObject)
        {
            JObject attributesAsObject = dynamicObject;
            Dictionary<string, object> values = attributesAsObject.ToObject<Dictionary<string, object>>();
            return values.Keys.Select(s => s).ToList();
        }
    }
}