using System.Text.Json;

namespace GroupApiProject.Models.Responses;

public record TextResponse
{
    public string? Message {get; set;}

    public TextResponse(string message)
    {
        Message = message;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}