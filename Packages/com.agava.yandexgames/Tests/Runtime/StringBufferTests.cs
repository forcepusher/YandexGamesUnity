using System;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;

namespace YandexGames.Utility.Tests
{
    public class StringBufferTests
    {
        [Test]
        public void ShouldConvertString()
        {
            string testString =
                "henlo developer\n" +
                "helllo you STINKY DEVELOPER\n" +
                "go code in javascript ugly\n";

            byte[] testStringBytes = Encoding.UTF8.GetBytes(testString);
            IntPtr testStringBufferPointer = Marshal.AllocHGlobal(testStringBytes.Length);
            Marshal.Copy(testStringBytes, 0, testStringBufferPointer, testStringBytes.Length);

            var stringBuffer = new StringBuffer(testStringBufferPointer, testStringBytes.Length);
            Assert.AreEqual(testString, stringBuffer.ToString());

            Marshal.FreeHGlobal(testStringBufferPointer);
        }
    }
}
