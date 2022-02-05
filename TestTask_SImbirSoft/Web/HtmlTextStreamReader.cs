using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NLog;
using TestTask_SimbirSoft.Stat;

namespace TestTask_SimbirSoft.Web
{
    /// <summary>
    /// Simple web-request HTML page reader
    /// </summary>
    public class HtmlTextStreamReader : IDisposable
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private StreamReader _reader;       // Wrappee
        private HttpWebRequest _request;    // Request
        private Uri _uri;                   // Uri to use in request
        private HtmlDocument _doc;          // Doc provided by response
       
        /// <summary>
        /// Create stream from uri
        /// </summary>
        /// <param name="uri">uri of the webpage to process</param>
        public HtmlTextStreamReader([NotNull] Uri uri)
        {
            _uri = uri;
            _request = WebRequest.CreateHttp(_uri);
            _doc = new HtmlDocument()
            {
                OptionFixNestedTags = true,
                OptionCheckSyntax = true,
                OptionAutoCloseOnEnd = true
            };
        }

        /// <summary>
        /// Receive content from the provided URI
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">If trying to receive content more than once</exception>
        public async Task Receive()
        {
            if (_reader != null)
                throw new Exception("Can't receive twice!");
            
            var response = await _request.GetResponseAsync() as HttpWebResponse;

            if (String.Compare(response.Method, "GET") == 0)
            {
                Logger.Info($"GET invoked at {response.ResponseUri} (STATUS: {response.StatusCode})");
            }
            _reader = new StreamReader(response.GetResponseStream());
        }

        /// <summary>
        /// Read entire response into string (with sanitizing and stripping tags)
        /// </summary>
        /// <returns>Text of the loaded page</returns>
        public async Task<string> ReadToEnd()
        {
            using var stat = new ScopedStatCounter("Reading document"); 
                
            var content = await _reader.ReadToEndAsync();
            _doc.LoadHtml(content);
            return _doc.DocumentNode.InnerText.Trim();
        }
        
        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}