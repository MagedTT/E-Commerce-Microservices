namespace CoreLib.Interfaces;

public class ResponseMessage
{
    public ResponseMessage(bool status, string message)
    {
        Status = status;
        Message = message;
    }
    public bool Status { get; private set; }
    public string? Message { get; private set; }
}