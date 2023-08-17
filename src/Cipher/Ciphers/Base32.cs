using System.Text;
using Hopex.Cipher.Interfaces;

namespace Hopex.Cipher.Ciphers;

internal class Base32 : ICipher
{
    /// <inheritdoc />
    public string Encode(string input)
    {
        var enc = Encoding.Unicode.GetBytes(input);

        var charCount = (int)Math.Ceiling(enc.Length / 5d) * 8;
        var returnArray = new char[charCount];

        byte nextChar = 0, bitsRemaining = 5;
        var arrayIndex = 0;

        foreach (var b in enc)
        {
            nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
            returnArray[arrayIndex++] = ValueToChar(nextChar);

            if (bitsRemaining < 4)
            {
                nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                bitsRemaining += 5;
            }

            bitsRemaining -= 3;
            nextChar = (byte)((b << bitsRemaining) & 31);
        }

        if (arrayIndex == charCount) return new string(returnArray);
        returnArray[arrayIndex++] = ValueToChar(nextChar);
        while (arrayIndex != charCount) returnArray[arrayIndex++] = '=';

        return string.Concat(returnArray);
    }

    /// <inheritdoc />
    public string Decode(string input)
    {
        input = input.TrimEnd('=');
        var byteCount = input.Length * 5 / 8;
        var returnArray = new byte[byteCount];

        byte curByte = 0, bitsRemaining = 8;
        var arrayIndex = 0;

        foreach (var c in input)
        {
            var cValue = CharToValue(c);

            var mask = 0;
            if (bitsRemaining > 5)
            {
                mask = cValue << (bitsRemaining - 5);
                curByte = (byte)(curByte | mask);
                bitsRemaining -= 5;
            }
            else
            {
                mask = cValue >> (5 - bitsRemaining);
                curByte = (byte)(curByte | mask);
                returnArray[arrayIndex++] = curByte;
                curByte = (byte)(cValue << (3 + bitsRemaining));
                bitsRemaining += 3;
            }
        }

        if (arrayIndex != byteCount) returnArray[arrayIndex] = curByte;

        return Encoding.Unicode.GetString(returnArray);
    }

    private static int CharToValue(char c)
    {
        int value = c;

        return value switch
        {
            < 91 and > 64 => value - 65,
            < 56 and > 49 => value - 24,
            < 123 and > 96 => value - 97,
            _ => throw new ArgumentException("Character is not a Base32 character.", nameof(c))
        };
    }

    private static char ValueToChar(byte b)
    {
        return b switch
        {
            < 26 => (char)(b + 65),
            < 32 => (char)(b + 24),
            _ => throw new ArgumentException("Byte is not a value Base32 value.", nameof(b))
        };
    }
}