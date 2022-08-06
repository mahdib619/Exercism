using System;
using System.Collections.Generic;

public static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading)
    {
        var buffer = new byte[9];

        if (reading is >= 4_294_967_296 or <= -2_147_483_649)
        {
            buffer[0] = 256 - 8;
            BitConverter.GetBytes(reading).CopyTo(buffer, 1);
        }
        else if (reading >= 2_147_483_648)
        {
            buffer[0] = 4;
            BitConverter.GetBytes((uint)reading).CopyTo(buffer, 1);
        }
        else if (reading is >= 65_536 or <= -32_769)
        {
            buffer[0] = 256 - 4;
            BitConverter.GetBytes((int)reading).CopyTo(buffer, 1);
        }
        else if (reading >= 0)
        {
            buffer[0] = 2;
            BitConverter.GetBytes((ushort)reading).CopyTo(buffer, 1);
        }
        else
        {
            buffer[0] = 256 - 2;
            BitConverter.GetBytes((short)reading).CopyTo(buffer, 1);
        }

        return buffer;
    }

    public static long FromBuffer(byte[] buffer)
    {
        return buffer[0] switch
        {
            256 - 8 => BitConverter.ToInt64(buffer, 1),
            4 => BitConverter.ToUInt32(buffer, 1),
            256 - 4 => BitConverter.ToInt32(buffer, 1),
            2 => BitConverter.ToUInt16(buffer, 1),
            256 - 2 => BitConverter.ToInt16(buffer, 1),
            _ => 0
        };
    }
}
