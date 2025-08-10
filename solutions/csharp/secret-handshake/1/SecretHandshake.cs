using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

public static class SecretHandshake
{
    private static readonly string[] _actions = { "wink", "double blink", "close your eyes", "jump" };

    public static string[] Commands(int commandValue)
    {
        var bits = new BitVector32(commandValue);
        var cm = BitVector32.CreateMask();

        var handShake = new LinkedList<string>();
        for (var i = 0; i <= 3; i++)
        {
            if (bits[cm])
                handShake.AddLast(_actions[i]);

            cm = BitVector32.CreateMask(cm);
        }

        return bits[cm] ? handShake.Reverse().ToArray() : handShake.ToArray();
    }
}
