using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        public BaseService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {

                HttpClient client = _clientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "applicaton/json");

                  if (!requestDto.Url.StartsWith("http://") && !requestDto.Url.StartsWith("https://"))
                {
                    throw new ArgumentException("Url must start with 'http://' or 'https://'");
                }
                //token
                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {
                   // message.Content = new StringContent(JsonSerializer.Serialize(requestDto), Encoding.UTF8, "application/json");
                     message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");    // Deprecated
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = "Bad Request" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        //var apiResponseDto = JsonSerializer.Deserialize<ResponseDto>(apiContent);  //new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);  // Deprecated

                        return apiResponseDto;
                }

            }
            catch (Exception ex)
            {

                var dto = new ResponseDto
                {
                      Message = ex.Message.ToString(),
                    IsSuccess = false
                  
                };
                return dto;
            }
        }
    }

}