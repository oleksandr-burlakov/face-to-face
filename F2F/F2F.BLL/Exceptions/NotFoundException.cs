namespace F2F.BLL.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message) { }
}
