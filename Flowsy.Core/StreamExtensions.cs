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

        var memoryOwner = MemoryPool<byte>.Shared.Rent();
        var memory = memoryOwner.Memory;

        int b, i = 0;
        while ((b = stream.ReadByte()) >= 0)
            memory.Span[i++] = (byte) b; 
        
        if (position is not null)
            stream.Seek(position.Value, SeekOrigin.Begin);
        
        return memory.ToArray();
    }
}