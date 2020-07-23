using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glattetre.Covid19Data.Web.Tests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {

        readonly Func<string, HttpResponseMessage> _sendAsync;

        public MockHttpMessageHandler(Func<string, HttpResponseMessage> sendAsync)
        {
            _sendAsync = sendAsync;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string input = null;
            if (request.Content != null) // Could be a GET-request without a body
            {
                input = await request.Content.ReadAsStringAsync();
            }
            var retVal = _sendAsync(input);
            return retVal;
        }
    }
}
