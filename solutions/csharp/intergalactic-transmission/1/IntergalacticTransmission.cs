public static class IntergalacticTransmission
{
    public static byte[] GetTransmitSequence(byte[] message) =>
        message.SelectMany(num => Convert.ToString(num, 2).PadLeft(8, '0'))
               .Chunk(7)
               .Select(data =>
               {
                   var parity = int.IsEvenInteger(data.Count(bit => bit == '1')) ? '0' : '1';
                   return Convert.ToByte(value: new string(data).PadRight(7, '0') + parity, fromBase: 2);
               })
               .ToArray();

    public static byte[] DecodeSequence(byte[] receivedSeq) =>
        receivedSeq.SelectMany(num =>
                   {
                       var data = Convert.ToString(num, 2).PadLeft(8, '0');
                       return int.IsOddInteger(data.Count(bit => bit == '1')) ? throw new ArgumentException("Message is corrupted!") : data[..^1];
                   }).Chunk(8)
                   .Where(data => data.Length == 8)
                   .Select(data => Convert.ToByte(value: new(data), fromBase: 2))
                   .ToArray();
}