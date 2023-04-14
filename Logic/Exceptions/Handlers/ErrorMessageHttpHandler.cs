using Marketplace.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Logic.Exceptions.Handlers
{
    public class ErrorMessageHttpHandler:DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            var responseMessage = await base.SendAsync(request,cancellationToken);

            if(responseMessage.IsSuccessStatusCode)
            {
                return responseMessage;
            }

            var statusCode = responseMessage.StatusCode;
            var content = await responseMessage.Content.ReadAsStringAsync();
            var exception = JsonConvert.DeserializeObject<ApiExceptionDTO>(content);
            throw new ApiException(exception.LogMessage,exception.UserMessage,exception.StatusCode);
        }
    }
}
