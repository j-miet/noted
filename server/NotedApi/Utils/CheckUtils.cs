namespace NotedApi.Utils;

public static class CheckUtils
{
    public static string NameCheck(string name)
    {
        if (name == null ||
            name.Trim() == "" ||
            name.Length > 50)
        {
            return "";
        }
        return name;
    }
}