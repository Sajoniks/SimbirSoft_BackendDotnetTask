using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TestTask_SimbirSoft.Stat;

namespace TestTask_SimbirSoft.StringUtils
{
    /// <summary>
    /// Class that extracts word from given string using delimiters.
    /// </summary>
    public class WordExtractor
    {
        private string _content;    // Content to process
        private string _pattern;    // Regex pattern that is used for splitting
        private char[] _delimiters; // Delimiters to use in splitting
 
        private IEnumerable<string> _extractCache;        // Cached extracted words collection
        private Dictionary<string, int> _statisticsCache; // Cached histogram data  

        /// <summary>
        /// Clear all cached data
        /// </summary>
        private void ClearCache()
        {
            _extractCache = null;
            _statisticsCache = null;
        }
        
        /// <summary>
        /// Set delimiters that will be used in split 
        /// </summary>
        public char[] Delimiters
        {
            get
            {
                return _delimiters;
            }
            set
            {
                _delimiters = value ?? throw new Exception("Null delimiters array was passed");
                _pattern = String.Join('|', 
                    _delimiters.Select(
                            del => Regex.Escape(del.ToString())
                        )
                    );

                ClearCache();
            }
        }

        /// <summary>
        /// Create word extractor
        /// </summary>
        /// <param name="content">Content to process</param>
        public WordExtractor(string content)
        {
            _content = content;
            ClearCache();
        }

        /// <summary>
        /// Make split
        /// </summary>
        /// <returns>Collection of words</returns>
        public IEnumerable<string> GetWords()
        {
            if (_extractCache == null)
            {
                using (var stat = new ScopedStatCounter("Extracting words"))
                {
                    _extractCache
                        = Regex
                            .Split(_content, _pattern, RegexOptions.CultureInvariant | RegexOptions.Compiled)
                            .Where(word => !string.IsNullOrEmpty(word) && !StringUtils.IsPhoneNumber(word))
                            .Select(word => StringUtils.SanitizeWord(word.ToLowerInvariant()));
                }
            }

            return _extractCache;
        }
        
        /// <summary>
        /// Calculate words histogram (using provided content)
        /// </summary>
        /// <returns>Words histogram as dictionary word:frequency</returns>
        public Dictionary<string, int> GetHistogram()
        {
            if (_statisticsCache == null)
            {
                using (var stat = new ScopedStatCounter("Histogram"))
                {
                    var result = new Dictionary<string, int>();
                    var words = GetWords();
                    foreach (var word in words)
                    {
                        int value = 0;
                        if (!result.TryGetValue(word, out value))
                        {
                            result.Add(word, 0);
                        }

                        result[word] += 1;
                    }
                    _statisticsCache = result;
                }
            }
            return _statisticsCache;
        }
    }
}