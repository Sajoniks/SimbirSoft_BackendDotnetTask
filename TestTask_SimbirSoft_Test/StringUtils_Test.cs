using NUnit.Framework;
using TestTask_SimbirSoft.StringUtils;

namespace TestTask_SimbirSoft_Test
{
    public class Tests
    {
        [Test]
        public void Test_Check_Sanitizer()
        {
            Assert.AreEqual("hello", StringUtils.SanitizeWord("h<ell>o"));
            Assert.AreEqual("world", StringUtils.SanitizeWord("==wo*rld=="));
            Assert.AreEqual("c#", StringUtils.SanitizeWord("c#"));
        }

        [Test]
        public void Test_Check_PhoneNumber()
        {
            Assert.IsTrue(StringUtils.IsPhoneNumber("8 918 123 32 12"));
            Assert.IsTrue(StringUtils.IsPhoneNumber("8 (888) 888 88 8 8"));
            Assert.IsTrue(StringUtils.IsPhoneNumber("+1+2+3+4-(930) 21 12"));
            Assert.IsFalse(StringUtils.IsPhoneNumber("=hello(384)"));
            Assert.IsFalse(StringUtils.IsPhoneNumber("Hello World!"));
            Assert.IsFalse(StringUtils.IsPhoneNumber("**************"));
        }
    }
}