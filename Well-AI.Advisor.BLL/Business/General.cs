using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WellAI.Advisor.BLL.Business
{
    public static class General
    {
        public static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var memorystresm = new MemoryStream();
                SerializeJsonIntoStream(content, memorystresm);
                memorystresm.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(memorystresm);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            return httpContent;
        }

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var stremw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jsonw = new JsonTextWriter(stremw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jsonw, value);
                jsonw.Flush();
            }
        }
    }
}
