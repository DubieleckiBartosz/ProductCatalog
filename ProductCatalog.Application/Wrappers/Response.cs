using System.Text.Json.Serialization;

namespace ProductCatalog.Application.Wrappers;

public class Response<T>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; private set; }
    public bool Success { get; private set; }

    public Response()
    { 
    }

    private Response(T data, string? message)
    {
        Data = data;
        Message = message;
        Success = true;
    }

    public static Response<T> Ok(T data, string? message)
    {
        return new Response<T>(data, message);
    }

    public static Response<T?> Ok(string message)
    {
        return new Response<T?>(default, message);
    }
}