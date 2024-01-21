namespace Rainfall.Application.Shared;

public class Detail
{
    public string Target { get; set; }
    public string Message { get; set; }

    public Detail(string target, string message)
    {
        Target = target;
        Message = message;
    }
}