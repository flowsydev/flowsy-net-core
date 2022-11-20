using System.Buffers;

namespace Flowsy.Core;

public static class StreamExtensions
{
    public static byte[] ToArray(this Stream stream)
    {
        long? position = null;
        if (stream.CanSeek)
        {
            position = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);
        }

        var buffer = ArrayPool<byte>.Shared.Rent(4096);
        var bytesRead = 0;
        var bytes = new List<byte>();

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            bytes.AddRange(buffer.Take(bytesRead));
        
        ArrayPool<byte>.Shared.Return(buffer);

        if (position is not null)
            stream.Seek(position.Value, SeekOrigin.Begin);
        
        return bytes.ToArray();
    }
    
    public static async Task<byte[]> ToArrayAsync(this Stream stream, CancellationToken cancellationToken)
    {
        long? position = null;
        if (stream.CanSeek)
        {
            position = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);
        }

        var buffer = ArrayPool<byte>.Shared.Rent(4096);
        var bytesRead = 0;
        var bytes = new List<byte>();

        while ((bytesRead = await stream.ReadAsync(buffer, cancellationToken)) > 0)
            bytes.AddRange(buffer.Take(bytesRead));

        ArrayPool<byte>.Shared.Return(buffer);

        if (position is not null)
            stream.Seek(position.Value, SeekOrigin.Begin);
        
        return bytes.ToArray();
    }
}