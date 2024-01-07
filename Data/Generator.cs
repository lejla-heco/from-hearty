using System.Security.Cryptography;
using System.Text;

public static class Generator
{
    private static int sequence;

    public static Guid NewGuid()
    {
        using MD5 md5 = MD5.Create();
        byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes($"{sequence++}"));
        return new Guid(hash);
    }
}