using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Minimal.Core
{
    public class SlugHelper : ISlugHelper
    {
        private static readonly Dictionary<string, Regex> _deleteRegexMap = new Dictionary<string, Regex>();
        private static readonly Lazy<SlugHelperConfiguration> _defaultConfig = new Lazy<SlugHelperConfiguration>(() => new SlugHelperConfiguration());

        protected SlugHelperConfiguration Config { get; set; }

        public SlugHelper() : this(_defaultConfig.Value) { }

        public SlugHelper(SlugHelperConfiguration config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config), "can't be null use default config or empty constructor.");
        }

        public string GenerateSlug(string inputString)
        {
            var sb = new StringBuilder();

            PrepareStringBuilder(inputString.Normalize(NormalizationForm.FormD), sb);
            ApplyStringReplacements(sb);
            RemoveNonSpacingMarks(sb);

            if (Config.DeniedCharactersRegex == null)
            {
                RemoveNotAllowedCharacters(sb);
            }

            if (Config.DeniedCharactersRegex != null)
            {
                if (!_deleteRegexMap.TryGetValue(Config.DeniedCharactersRegex, out var deniedCharactersRegex))
                {
                    deniedCharactersRegex = new Regex(Config.DeniedCharactersRegex, RegexOptions.Compiled);
                    _deleteRegexMap.Add(Config.DeniedCharactersRegex, deniedCharactersRegex);
                }

                var currentValue = sb.ToString();
                sb.Clear();
                sb.Append(DeleteCharacters(currentValue, deniedCharactersRegex));
            }

            if (Config.CollapseDashes)
            {
                CollapseDashes(sb);
            }

            return sb.ToString();
        }

        private void PrepareStringBuilder(string inputString, StringBuilder sb)
        {
            var seenFirstNonWhitespace = false;
            var indexOfLastNonWhitespace = 0;
            for (var i = 0; i < inputString.Length; i++)
            {
                var c = inputString[i];
                var isWhitespace = char.IsWhiteSpace(c);
                if (!seenFirstNonWhitespace && isWhitespace)
                {
                    if (Config.TrimWhitespace)
                    {
                        continue;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    seenFirstNonWhitespace = true;
                    if (!isWhitespace)
                    {
                        indexOfLastNonWhitespace = sb.Length;
                    }
                    else
                    {
                        c = ' ';

                        if (Config.CollapseWhiteSpace)
                        {
                            while ((i + 1) < inputString.Length && char.IsWhiteSpace(inputString[i + 1]))
                            {
                                i++;
                            }
                        }
                    }
                    if (Config.ForceLowerCase)
                    {
                        c = char.ToLower(c);
                    }

                    sb.Append(c);
                }
            }

            if (Config.TrimWhitespace)
            {
                sb.Length = indexOfLastNonWhitespace + 1;
            }
        }

        private void ApplyStringReplacements(StringBuilder sb)
        {
            foreach (var replacement in Config.StringReplacements)
            {
                var search = replacement.Key.Normalize(NormalizationForm.FormD);
                var replace = replacement.Value.Normalize(NormalizationForm.FormD);

                for (var i = 0; i < sb.Length; i++)
                {
                    if (SubstringEquals(sb, i, search))
                    {
                        sb.Remove(i, search.Length);
                        sb.Insert(i, replace);

                        i += replace.Length - 1;
                    }
                }
            }
        }

        private static bool SubstringEquals(StringBuilder sb, int index, string toMatch)
        {
            if (sb.Length - index < toMatch.Length)
            {
                return false;
            }

            for (var i = index; i < sb.Length; i++)
            {
                var matchIndex = i - index;

                if (matchIndex == toMatch.Length)
                {
                    return true;
                }
                else if (sb[i] != toMatch[matchIndex])
                {
                    return false;
                }
            }
            return (sb.Length - index) == toMatch.Length;
        }

        protected static void RemoveNonSpacingMarks(StringBuilder sb)
        {
            for (var ich = 0; ich < sb.Length; ich++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(sb[ich]) == UnicodeCategory.NonSpacingMark)
                {
                    sb.Remove(ich, 1);
                    ich--;
                }
            }
        }

        protected void RemoveNotAllowedCharacters(StringBuilder sb)
        {
            var allowedChars = Config.AllowedChars;
            for (var i = 0; i < sb.Length; i++)
            {
                if (!allowedChars.Contains(sb[i]))
                {
                    sb.Remove(i, 1);
                    i--;
                }
            }
        }

        protected static void CollapseDashes(StringBuilder sb)
        {
            var firstDash = true;
            for (var i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '-')
                {
                    if (firstDash)
                    {
                        firstDash = false;
                    }
                    else
                    {
                        sb.Remove(i, 1);
                        i--;
                    }
                }
                else
                {
                    firstDash = true;
                }
            }
        }

        protected static string DeleteCharacters(string str, Regex deniedCharactersRegex) => deniedCharactersRegex.Replace(str, string.Empty);
    }
}
