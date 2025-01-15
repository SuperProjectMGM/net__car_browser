namespace search.api.Messages;

public class MessageWrapper
{
    public  MessageType Type { get; set; }
    public string Message { get; set; } = string.Empty;
}

public enum MessageType
{
    Confirmed = 0,
    Completed = 1,
    UserReturn = 2,
    EmployeeReturn = 3
}
