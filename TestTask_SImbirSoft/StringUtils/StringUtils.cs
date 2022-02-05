using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TestTask_SimbirSoft.StringUtils
{
    /// <summary>
    /// Simple string utils
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        /// Characters that can appear in phone number
        /// </summary>
        private static readonly string PhoneNumberChars = " ()-+";
        
        /// <summary>
        /// Characters that can't appear in normal word
        /// </summary>
        private static readonly string NonWordChars = "<>$*=";
        
        /// <summary>
        /// Check if given string can be a phone number
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>true if given string can be a phone number</returns>
        public static bool IsPhoneNumber([NotNull] string input)
        {
            return input.All(ch => Char.IsDigit(ch) || PhoneNumberChars.Contains(ch, StringComparison.InvariantCulture));
        }

        /// <summary>
        /// Try to sanitize given word (remove invalid characters, example: "he>*$llo worl>d" => "hello world")
        /// </summary>
        /// <param name="word">Word to process</param>
        /// <returns>Processed word</returns>
        public static string SanitizeWord(string word)
        {
            return String.Join(String.Empty, word.Where(ch => !NonWordChars.Contains(ch)));
        }
    }
}