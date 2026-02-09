namespace NotedApi.Exceptions;

/// <summary>
/// For handling canvas data errors
/// </summary>
public class InvalidCanvasException : Exception
{
    /// <summary>
    /// Canvas has invalid data format or values
    /// </summary>
    /// <param name="message"></param>
    public InvalidCanvasException(string message)
        : base(message)
    {
    }
}
