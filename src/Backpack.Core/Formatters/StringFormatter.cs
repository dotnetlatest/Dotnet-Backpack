/*
 * -----------------------------------------------------------------
 * © 2006-2015 MineCloud, Inc (http://www.minecloud.com)
 * -----------------------------------------------------------------
 */   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Backpack.Core.Formatters
{
    /// <summary>
    /// String Helper Class
    /// </summary>
    public static class StringFormatter
    {
        private static readonly Regex _tokenRegEx = new Regex(@"({)(?<token>[^}]+)(})", RegexOptions.IgnoreCase);

        /// <summary>
        /// Determines if the incoming word contains only Alpha characters with no spaces.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool ContainsAlphaOnlyNoSpaces(string word)
        {
            Regex regEx = new Regex(@"^[a-zA-Z]*$");
            return regEx.IsMatch(word);
        }

        /// <summary>
        /// Determines if the incoming word contains only Numeric characters with no spaces.  
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool ContainsNumericOnlyNoSpaces(string word)
        {
            Regex regEx = new Regex(@"^[0-9]*$");
            return regEx.IsMatch(word);
        }

        /// <summary>
        /// Determines if the incoming word contains only Alpha-Numeric characters with no spaces.  
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool ContainsAlphaNumericOnlyNoSpaces(string word)
        {
            Regex regEx = new Regex(@"^[a-zA-Z0-9]*$");
            return regEx.IsMatch(word);
        }

        /// <summary>
        /// Appends a delimiter followed by the second String to the first String if both are not null/empty.
        /// Otherwise, the first or second String is returned if either is not null/empty.
        /// Optionally checks to see if the first String ends with the delimiter or the second String 
        /// starts with the delimiter before appending the delimiter.
        /// </summary>
        /// <param name="targetStr1">The first string.</param>
        /// <param name="targetStr2">The second string.</param>
        /// <param name="delim">The delimiter to use when appending.</param>
        /// <param name="checkDelimExists">Determines if the strings should be checked for delimiters.</param>
        /// <returns>The resulting string.</returns>
        public static string AppendWithDelim(string targetStr1, string targetStr2, string delim, bool checkDelimExists)
        {
            string result;
            targetStr1 = (string.IsNullOrEmpty(targetStr1)) ? string.Empty : targetStr1.Trim();
            targetStr2 = (string.IsNullOrEmpty(targetStr2)) ? string.Empty : targetStr2.Trim();

            if (!targetStr1.Equals(string.Empty))
            {
                result = targetStr1;
                if (!targetStr2.Equals(string.Empty))
                {
                    if (checkDelimExists)
                    {
                        if (!targetStr1.EndsWith(delim) && !targetStr2.StartsWith(delim))
                        {
                            result = result + delim + targetStr2;
                        }
                        else
                        {
                            result += targetStr2;
                        }
                    }
                    else
                    {
                        result = result + delim + targetStr2;
                    }
                }
            }
            else
            {
                result = targetStr2;
            }

            return result;
        }

        /// <summary>
        /// Appends the strings in valueList one after another with delimiters in between.
        /// </summary>
        /// <param name="valueList">The list of values to concatenate.</param>
        /// <param name="delim">The delimiter to use when appending.</param>
        /// <param name="checkDelimExists">Determines if the strings should be checked for delimiters.</param>
        /// <returns>The resulting string.</returns>
        public static string AppendWithDelim(string[] valueList, string delim, bool checkDelimExists)
        {
            string result = string.Empty;

            if ((valueList != null) && (valueList.Length > 0))
            {
                for (int i = 0; i < valueList.Length; i++)
                {
                    if (valueList[i] != null)
                    {
                        result = AppendWithDelim(result, valueList[i], delim, checkDelimExists);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Appends the suffix to the target String if the said String does not already end with the suffix.
        /// If the suffix is null, the target String is returned.
        /// </summary>
        /// <param name="targetStr">The target String.</param>
        /// <param name="targetSuffix">The target suffix to append.</param>
        /// <returns>The target string ending with the target suffix.</returns>
        public static string AppendSuffixIfNotFound(string targetStr, string targetSuffix)
        {
            if ((targetSuffix != null) && (!targetStr.EndsWith(targetSuffix)))
            {
                return targetStr + targetSuffix;
            }
            return targetStr;
        }

        /// <summary>
        /// Appends the prefix to the target String if the said String does not already start with the prefix.
        /// If the prefix is null, the target String is returned.
        /// </summary>
        /// <param name="targetStr">The target String.</param>
        /// <param name="targetPrefix">The target prefix to append.</param>
        /// <returns>The target string ending with the target suffix.</returns>
        public static string AppendPrefixIfNotFound(string targetStr, string targetPrefix)
        {
            if ((targetPrefix != null) && (!targetStr.StartsWith(targetPrefix)))
            {
                return targetPrefix + targetStr;
            }
            return targetStr;
        }

        /// <summary>
        /// Checks to see if the given <code>string</code> is not null nor empty.
        /// If it is, an empty string is returned.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <returns>The <code>string</code> if it is neither null nor empty, else an empty string.</returns>
        public static string GetStringValue(string value)
        {
            return GetStringValue(value, String.Empty);
        }

        /// <summary>
        /// Checks to see if the given <code>string</code> is not null nor empty.
        /// If it is, default value is returned.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <param name="defaultValue">A value to return if the String is null or empty.</param>
        /// <returns>The <code>string</code> if it is neither null nor empty, else the default value.</returns>
        public static string GetStringValue(string value, string defaultValue)
        {
            if (!String.IsNullOrEmpty(value))
            {
                if (!value.Trim().Equals(String.Empty))
                {
                    return value;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the <code>bool</code> value of a <code>string</code>; or false if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <returns>The <code>bool</code> value of a <code>string</code>; or false if an error occurs.</returns>
        public static bool GetBoolValue(string value)
        {
            return GetBoolValue(value, false);
        }

        /// <summary>
        /// Gets the <code>bool</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <param name="defaultValue">Default value to return.</param>
        /// <returns>The <code>bool</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.</returns>
        public static bool GetBoolValue(string value, bool defaultValue)
        {
            bool result = defaultValue;

            if (!string.IsNullOrEmpty(value))
            {
                // Check for null and trim
                value = string.IsNullOrEmpty(value) ? "" : value.Trim();

                if (value.Length > 0)
                {
                    switch (value.ToLower())
                    {
                        case "true": result = true; break;
                        case "yes": result = true; break;
                        case "1": result = true; break;
                        default: result = false; break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the <code>int</code> value of a <code>string</code>; or 0 if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <returns>The <code>int</code> value of a <code>string</code>; or 0 if an error occurs.</returns>
        public static int GetIntValue(string value)
        {
            return GetIntValue(value, 0);
        }

        /// <summary>
        /// Gets the <code>int</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <param name="defaultValue">The default value to return in case of error.</param>
        /// <returns>The <code>int</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.</returns>
        public static int GetIntValue(string value, int defaultValue)
        {
            int returnValue = defaultValue;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    returnValue = Int32.Parse(value);
                }
                catch (Exception)
                {
                    returnValue = defaultValue;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the <code>long</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <param name="defaultValue">The default value to return in case of error.</param>
        /// <returns>The <code>long</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.</returns>
        public static long GetLongValue(string value, long defaultValue)
        {
            long returnValue = defaultValue;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    returnValue = long.Parse(value);
                }
                catch (Exception)
                {
                    returnValue = defaultValue;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the <code>decimal</code> value of a <code>string</code>; or <code>0.0</code> if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <returns>The <code>decimal</code> value of a <code>string</code>; or <code>0.0</code> if an error occurs.</returns>
        public static decimal GetDecimalValue(string value)
        {
            return GetDecimalValue(value, 0.0m);
        }

        /// <summary>
        /// Gets the <code>decimal</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <param name="defaultValue">The default value to return in case of error.</param>
        /// <returns>The <code>decimal</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.</returns>
        public static decimal GetDecimalValue(string value, decimal defaultValue)
        {
            decimal returnValue = defaultValue;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    returnValue = Decimal.Parse(value);
                }
                catch (Exception)
                {
                    returnValue = defaultValue;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the <code>double</code> value of a <code>string</code>; or <code>0.0</code> if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <returns>The <code>double</code> value of a <code>string</code>; or <code>0.0</code> if an error occurs.</returns>
        public static double GetDoubleValue(string value)
        {
            return GetDoubleValue(value, 0.0);
        }

        /// <summary>
        /// Gets the <code>double</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.
        /// </summary>
        /// <param name="value">The <code>string</code> value.</param>
        /// <param name="defaultValue">The default value to return in case of error.</param>
        /// <returns>The <code>double</code> value of a <code>string</code>; or <paramref name="defaultValue"/> if an error occurs.</returns>
        public static double GetDoubleValue(string value, double defaultValue)
        {
            double returnValue = defaultValue;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    returnValue = Double.Parse(value);
                }
                catch (Exception)
                {
                    returnValue = defaultValue;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Checks to see if the given String is a valid Guid.
        /// If so, the equivalent Guid is returned.
        /// Otherwise, the emptry Guid is returned.
        /// </summary>
        /// <param name="value">The target String.</param>
        /// <returns>The equivalent Guid if the String was a valid Guid, else the empty Guid.</returns>
        public static Guid GetGuidValue(string value)
        {
            return GetGuidValue(value, Guid.Empty);
        }

        /// <summary>
        /// Checks to see if the given String is a valid Guid.
        /// If so, the equivalent Guid is returned.
        /// Otherwise, the default value is returned.
        /// </summary>
        /// <param name="value">The target String.</param>
        /// <param name="defaultValue">A value to return if the check fails.</param>
        /// <returns>The equivalent Guid if the String was a valid Guid, else the default value.</returns>
        public static Guid GetGuidValue(string value, Guid defaultValue)
        {
            Guid result = defaultValue;

            if (!string.IsNullOrEmpty(value))
            {
                //Attempt to form GUID
                Guid.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// Replaces the named token in the target String with the replacement value.
        /// The comparison for named tokens are not case sensitive.
        /// </summary>
        /// <param name="targetString">The target string.</param>
        /// <param name="tokenName">The named token.</param>
        /// <param name="replacementValue">The replacement value.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatNamedToken(string targetString, string tokenName, string replacementValue)
        {
            Dictionary<string, string> replacementList = new Dictionary<string, string> { { tokenName, replacementValue } };
            return FormatNamedToken(targetString, replacementList);
        }

        /// <summary>
        /// Replaces the named token in the target String with the equivalent replacement value in the replacement list.
        /// The comparison for named tokens are not case sensitive.
        /// </summary>
        /// <param name="targetString">The target string.</param>
        /// <param name="replacementList">A collection of keys and corresponding replacement values.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatNamedToken(string targetString, Dictionary<string, string> replacementList)
        {
            NamedReplaceEvaluator targetEval = new NamedReplaceEvaluator(replacementList);
            MatchEvaluator evalMatch = targetEval.EvalMatch;
            return _tokenRegEx.Replace(targetString, evalMatch);
        }

        /// <summary>
        /// Returns the first occurence of a non-common subsequence of characters.
        /// Comparison is done in a non-case sensitive manner.
        /// </summary>
        /// <param name="str1">The first string to check.</param>
        /// <param name="str2">The second string to check.</param>
        /// <returns>The index of the first differing character, -1 if they are the same, or 0 if either string is empty or null.</returns>
        public static int GetIndexOfNonCommonChar(string str1, string str2)
        {
            int currIndex = 0;

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return currIndex;
            }

            str1 = str1.ToLower();
            str2 = str2.ToLower();

            while ((currIndex < str1.Length) && (currIndex < str2.Length))
            {
                if (str1[currIndex] != str2[currIndex])
                {
                    break;
                }

                currIndex++;
            }

            if ((currIndex >= str1.Length) && (currIndex >= str2.Length))
            {
                currIndex = -1;
            }

            return currIndex;
        }

        /// <summary>
        /// Checks if string is null and trims spaces on both ends.
        /// </summary>
        /// <param name="text">Text to trim.</param>
        /// <returns>Trimmed text or an emptry string if null was passed in.</returns>
        public static string Trim(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text.Trim();
            }

            return string.Empty;
        }

        /// <summary>
        /// Truncates the string to the specified length if it is longer.
        /// Appends the specified suffix afterwards.
        /// </summary>
        /// <param name="targetStr">The target String.</param>
        /// <param name="targetSuffix">The target suffix to append.</param>
        /// <param name="targetLength">The target maximum length. A value equals or less than 0 means no maximum.</param>
        /// <returns>The resulting string after a possible truncation.</returns>
        public static string Truncate(string targetStr, string targetSuffix, int targetLength)
        {
            string result = GetStringValue(targetStr, String.Empty);

            if ((!String.IsNullOrEmpty(targetStr)) && (targetLength > 0) && (targetStr.Length > targetLength))
            {
                result = result.Substring(0, targetLength + 1);

                //truncated right before a space (punctuation marks are not considered), all words are complete, just trim.
                if (result.EndsWith(" "))
                {
                    result = result.Trim();
                }
                else
                {
                    //Remove extra character that was left for space check
                    if (result.Length > 1)
                    {
                        result = result.Substring(0, result.Length - 1);
                    }

                    //Find last full word based on the last occurence of a space
                    int index = result.LastIndexOf(" ");
                    if (index > -1)
                    {
                        //Space found - if no spaces are found, there is only one word, do not truncate further
                        result = result.Substring(0, index);
                    }
                }

                if (!String.IsNullOrEmpty(targetSuffix))
                {
                    result += targetSuffix;
                }
            }

            return result;
        }

        /// <summary>
        /// Truncates the string to the specified length and if in the middle of a work it removes the partial word based on any of the separators.
        /// Appends the specified suffix afterwards.
        /// </summary>
        /// <param name="targetStr">The target String.</param>
        /// <param name="targetSuffix">The target suffix to append.</param>
        /// <param name="targetLength">The target maximum length. A value equals or less than 0 means no maximum.</param>
        /// <param name="separators">The separators to use to remove the partial words.</param>
        /// <returns>The resulting string after a possible truncation.</returns>
        public static string TruncateWithSeparators(string targetStr, string targetSuffix, int targetLength, char[] separators)
        {
            string result = GetStringValue(targetStr, String.Empty);

            if ((!String.IsNullOrEmpty(targetStr)) && (targetLength > 0) && (targetStr.Length > targetLength))
            {
                result = result.Substring(0, targetLength + 1);

                //truncated right before a space (punctuation marks are not considered), all words are complete, just trim.
                if (result.EndsWith(" "))
                {
                    result = result.Trim();
                }
                else
                {
                    //Remove extra character that was left for space check
                    if (result.Length > 1)
                    {
                        result = result.Substring(0, result.Length - 1);
                    }

                    //Find last full word based on the last occurence of any separator
                    int index = result.LastIndexOfAny(separators);
                    if (index > -1)
                    {
                        //Space found - if no spaces are found, there is only one word, do not truncate further
                        result = result.Substring(0, index);
                    }
                }

                if (!String.IsNullOrEmpty(targetSuffix))
                {
                    result += targetSuffix;
                }
            }

            return result;
        }

        /// <summary>
        /// Truncates the string to the specified length and it removes the content based on the separator text.
        /// Appends the specified suffix afterwards.
        /// </summary>
        /// <param name="targetStr">The target String.</param>
        /// <param name="targetSuffix">The target suffix to append.</param>
        /// <param name="targetLength">The target maximum length. A value equals or less than 0 means no maximum.</param>
        /// <param name="separator">The separator to use to remove the partial words.</param>
        /// <returns>The resulting string after a possible truncation.</returns>
        public static string TruncateAtString(string targetStr, string targetSuffix, int targetLength, string separator)
        {
            string result = GetStringValue(targetStr, String.Empty);

            if ((!String.IsNullOrEmpty(targetStr)) && (targetLength > 0) && (targetStr.Length > targetLength))
            {
                result = result.Substring(0, targetLength + 1);

                //truncated right before a space (punctuation marks are not considered), all words are complete, just trim.
                if (result.EndsWith(" "))
                {
                    result = result.Trim();
                }
                else
                {
                    //Remove extra character that was left for space check
                    if (result.Length > 1)
                    {
                        result = result.Substring(0, result.Length - 1);
                    }

                    //Find last full word based on the last occurence of any separator
                    int index = result.LastIndexOf(separator, StringComparison.Ordinal);
                    if (index > -1)
                    {
                        //Space found - if no spaces are found, there is only one word, do not truncate further
                        result = result.Substring(0, index);
                    }
                    else
                    {
                        index = result.LastIndexOf(" ", StringComparison.Ordinal);
                        if (index > -1)
                        {
                            //Space found - if no spaces are found, there is only one word, do not truncate further
                            result = result.Substring(0, index);
                        }
                    }
                }

                if (!String.IsNullOrEmpty(targetSuffix))
                {
                    result += targetSuffix;
                }
            }

            return result;
        }

        /// <summary>
        /// Accepts a list of replacements and performs a single pass replace to avoid overlapping replacements.
        /// </summary>
        /// <param name="targetText">The target string.</param>
        /// <param name="replacementList">The list of tokens to replace.</param>
        /// <returns>The new string with the tokens replaced.</returns>
        public static string ReplaceFromList(this string targetText, Dictionary<string, string> replacementList)
        {
            return ReplaceFromList(targetText, replacementList, RegexOptions.None);
        }

        /// <summary>
        /// Accepts a list of replacements and performs a single pass replace to avoid overlapping replacements.
        /// </summary>
        /// <param name="targetText">The target string.</param>
        /// <param name="replacementList">The list of tokens to replace.</param>
        /// <param name="options">Regular expression options.</param>
        /// <returns>The new string with the tokens replaced.</returns>
        public static string ReplaceFromList(this string targetText, Dictionary<string, string> replacementList, RegexOptions options)
        {
            if (String.IsNullOrEmpty(targetText) || (replacementList == null) || (replacementList.Count < 1))
            {
                return targetText;
            }

            List<string> escapedKeys = new List<string>();
            foreach (string key in replacementList.Keys)
            {
                escapedKeys.Add(Regex.Escape(key));
            }

            return Regex.Replace(targetText,
                                    "(" + String.Join("|", escapedKeys.ToArray()) + ")",
                                    delegate(Match m)
                                    {
                                        return replacementList[m.Value];
                                    },
                                    options
                                );
        }

        /// <summary>
        /// Transforms a Dictionary instance into a query string.
        /// </summary>
        /// <param name="targetParamList">The Dictionary.</param>
        /// <returns>The query string.</returns>
        public static string ToQueryString(this Dictionary<string, string> targetParamList)
        {
            if ((targetParamList != null) && (targetParamList.Count > 0))
            {
                string[] paramList = (string[])targetParamList.Select
                    (x => String.Concat(Uri.EscapeDataString(x.Key), "=", Uri.EscapeDataString(x.Value ?? String.Empty))).ToArray();

                return String.Join("&", paramList);
            }

            return String.Empty;
        }

        /// <summary>
        /// Strips out the Html from a string
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string StripHtml(string original)
        {
            Regex htmlParser = new Regex("<[^\\<]+\\>", RegexOptions.IgnoreCase);
            string body = htmlParser.Replace(original, " ");

            return body;
        }
    }

    internal class NamedReplaceEvaluator
    {
        private readonly Dictionary<string, string> _replacementList;

        /// <summary>
        /// Creates a NamedReplaceEvaluator.
        /// </summary>
        /// <param name="replacementList">The list of replacement tokens.</param>
        internal NamedReplaceEvaluator(Dictionary<string, string> replacementList)
        {
            _replacementList = replacementList;
            if (_replacementList == null)
            {
                _replacementList = new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Adds a token name and replacement value to the replacement list.
        /// </summary>
        /// <param name="key">The token name.</param>
        /// <param name="replacementValue">The related replacement value.</param>
        internal void AddReplacement(string key, string replacementValue)
        {
            key = (string.IsNullOrEmpty(key)) ? string.Empty : key.Trim().ToLower();
            if (!string.IsNullOrEmpty(key) && (replacementValue != null))
            {
                _replacementList[key] = replacementValue;
            }
        }

        /// <summary>
        /// Evaluates a match for the regular expression: ({)([^}]+)(})
        /// Replaces named tokens with values from the replacement list.
        /// </summary>
        /// <param name="match">The current regular expression match.</param>
        /// <returns>The string to replace the token with if found in the replacement list. Otherwise, the match value.</returns>
        internal string EvalMatch(Match match)
        {
            string result = match.Value;
            string tokenName = match.Groups["token"].Value;
            tokenName = (string.IsNullOrEmpty(tokenName)) ? string.Empty : tokenName.Trim().ToLower();

            if (_replacementList.ContainsKey(tokenName))
            {
                result = _replacementList[tokenName];
            }

            return result;
        }
    }
}