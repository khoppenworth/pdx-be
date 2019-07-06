using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace PDX.API.Helpers
{
    public class CustomDecimalFormatter : OutputFormatter
    {
        public string ContentType { get; private set; }
        public CustomDecimalFormatter()
        {
            ContentType = "application/json";
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));

        }

        protected override bool CanWriteType(Type type)
        {
            var isDecimal = type == typeof(decimal);
            return isDecimal;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var decimalValue = (decimal)context.Object;

            var formatted = decimalValue.ToString("F2", CultureInfo.InvariantCulture);

            using (var writer = new StreamWriter(response.Body))
            {
                writer.Write(formatted);
            }

            return Task.FromResult(0);
        }
    }
}