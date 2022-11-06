namespace Flowsy.Core;

public static class StreamExtensions
{
    public static byte[] ToArray(this Stream stream)
    {
        if (stream.Length == 0)
            return Array.Empty<byte>();

        long? position = null;
        if (stream.CanSeek)
        {
            position = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);
        }
        
        var bytes = new byte[stream.Length];
        int b;
        long i = 0;

        while ((b = stream.ReadByte()) >= 0)
            bytes[i++] = (byte) b;
        
        if (position is not null)
            stream.Seek(position.Value, SeekOrigin.Begin);
        
        return bytes;
    }
}