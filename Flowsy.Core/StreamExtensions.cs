namespace Flowsy.Core;

public static class StreamExtensions
{
    public static byte[] ToArray(this Stream stream)
    {
        if (stream.Length == 0)
            return Array.Empty<byte>();
        
        var bytes = new byte[stream.Length];
        int b;
        long i = 0;

        while ((b = stream.ReadByte()) >= 0)
            bytes[i++] = (byte)b;
        
        return bytes;
    }
}