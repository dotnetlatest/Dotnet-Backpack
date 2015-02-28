using System;
using System.Text;

namespace Minecloud.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string item)
        {
            return Encoding.UTF8.GetBytes(item);
        }

        //This converts the 64 byte hash into the string hex representation of byte values 
        // (shown by default as 2 hex characters per byte)that looks like 
        // "FB-2F-85-C8-85-67-F3...", each pair represents
        // the byte value of 0-255.  Removing the "-" separator creates a 128 character string 
        // representation in hex
        public static string ToHexString(this byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "");
        }
    }
}