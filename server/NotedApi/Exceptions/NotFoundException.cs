namespace NotedApi.Exceptions;

/// <summary>
/// For handling missing database items
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Database record not found
    /// </summary>
    /// <param name="message"></param>
    public NotFoundException(string message)
        : base(message)
    {
    }
}
