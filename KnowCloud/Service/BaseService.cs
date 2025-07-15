using KnowCloud.Models.Dto;
using KnowCloud.Service.Contract;
using Newtonsoft.Json;
using System.Text;
using static KnowCloud.Utility.Utilities;

namespace KnowCloud.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto> SendAsync<T>(RequestDto requestDto, bool withBearerToken = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("knowCloud");
                HttpRequestMessage message = new();

                // Agrega encabezado Accept
                if (requestDto.ContentType == ContentType.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else
                {
                    message.Headers.Add("Accept", "*/*");
                }

                // Token
                if (withBearerToken)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                // URI
                message.RequestUri = new Uri(requestDto.Url);

                // Contenido del cuerpo
                if (requestDto.ContentType == ContentType.MultipartFormData)
                {
                    var content = new MultipartFormDataContent();

                    foreach (var prop in requestDto.Data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(requestDto.Data);

                        if (value is FormFile file)
                        {
                            if (file != null)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                        }
                        else if (value != null)
                        {
                            content.Add(new StringContent(value.ToString()), prop.Name);
                        }
                    }

                    message.Content = content;
                }
                else
                {
                    // Fix for CS1501 and CS0815
                    var jsonSerializer = JsonSerializer.Create();
                    var stringWriter = new StringWriter();
                    jsonSerializer.Serialize(stringWriter, requestDto.Data);
                    var jsonContent = stringWriter.ToString();

                    message.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                }

                // Método HTTP
                message.Method = requestDto.ApiType switch
                {
                    APIType.POST => HttpMethod.Post,
                    APIType.PUT => HttpMethod.Put,
                    APIType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                // Enviar la solicitud
                HttpResponseMessage response = await client.SendAsync(message);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = responseContent
                    };
                }

                // 👇 Aquí extraemos SOLO el campo `result`
                var rawJson = JsonConvert.DeserializeObject<ResponseDto>(responseContent);

                // Este `rawJson.Result` es el array (o lo que el backend haya devuelto dentro de "result")
                return new ResponseDto
                {
                    IsSuccess = true,
                    Result = rawJson.Result
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearerToken = true)
        {
            return await SendAsync<string>(requestDto, withBearerToken);
        }

    }
}
