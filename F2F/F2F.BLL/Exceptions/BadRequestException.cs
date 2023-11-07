namespace F2F.BLL.Exceptions;

[Serializable]
public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message) { }
}
