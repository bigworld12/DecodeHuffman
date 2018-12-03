using NGenerics.DataStructures.General;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DecodeHuffman
{
    class Program
    {

        static void Main(string[] args)
        {

            var x = new byte[] { 0b0001_0110, 0b1110_1100 };
            var res = GetRelativeNumber(x, 0, 4, 7);
            Console.WriteLine(Convert.ToString(res, 2));

            Console.ReadKey();
        }

        public static List<byte> DecodeFile(byte[] file, Dictionary<byte, byte> encodingTable)
        {
            var resList = new List<byte>();
            int byteIndex = 0;
            byte curCount = 1, //max 8
                relativeBitIndex = 0;//max 7
            while (byteIndex < file.Length)
            {
                start:
                var res = GetRelativeNumber(file, byteIndex, relativeBitIndex, curCount);

                if (encodingTable.TryGetValue(res, out var ascii))
                {
                    resList.Add(ascii);
                    var newRelativeBitIndex = (relativeBitIndex + curCount) % 8;
                    var newByteIndex = byteIndex + ((relativeBitIndex + curCount) / 8);
                    curCount = 1;
                }
                else
                {
                    curCount++;
                    goto start;
                }
            }
            return resList;
        }

        //byte index
        //relative bit index
        //absolute bit index = 8*byteIndex + relativeIndex;
        static byte GetRelativeNumber(byte[] arr, int byteIndex, byte relativeBitIndex, byte Count)
        {
            unchecked
            {
                var currentByte = arr[byteIndex];
                if (relativeBitIndex + Count >= 9)
                {
                    var extraByte = arr[byteIndex + 1];

                    byte n1 = (byte)(8 - relativeBitIndex);
                    byte n2 = (byte)(Count - n1);



                    var b1 = (byte)(255 >> (8 - n1));
                    var b2 = (byte)~(255 >> n2);

                    var res1 = (byte)(currentByte & b1);
                    var res2 = (byte)((extraByte & b2) >> (8 - n2));
                    var res = (res1 << n2) + res2;
                    return (byte)res;

                }
                else
                {
                    //all in a single byte

                    var b1 = 255 >> relativeBitIndex;
                    var res1 = currentByte & b1;
                    var res = res1 >> (8 - Count - relativeBitIndex);
                    return (byte)res;

                }
            }
        }
    }


}
