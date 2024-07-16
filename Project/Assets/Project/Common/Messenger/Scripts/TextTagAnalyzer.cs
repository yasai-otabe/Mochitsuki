using System;
using System.Text.RegularExpressions;

static public class TextTagAnalyzer
{
    // @から始まる置換タグ
    enum REPLACEMENT_TAG_NAME
    {
        NAME,
    }

    /// <summary>
    /// @から始まるタグを置換
    /// </summary>
    /// <param name="name">タグの種類</param>
    /// <param name="result">置換結果</param>
    static void ReplaceTag(REPLACEMENT_TAG_NAME name, out string result)
    {
        switch (name)
        {
            case REPLACEMENT_TAG_NAME.NAME:
                result = "xxx";
                break;
            default:
                result = "";
                break;
        }
    }

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="text">解析対象文字列</param>
    static public void Analyze(ref string text)
    {
        // @で始まる1文字以上の文字列を検索
        var reg = new Regex(@"@[A-Z]+");
        var matches = reg.Matches(text);
        foreach (Match match in matches)
        {
            // @を削除した置換タグ文字列をEnumの要素名に変換して置換
            var value = match.Value.Substring(1);
            if (Enum.TryParse(typeof(REPLACEMENT_TAG_NAME), value, out var val))
            {
                ReplaceTag((REPLACEMENT_TAG_NAME)val, out var result);
                text = text.Replace(match.Value, result);
            }
        }
    }

    /// <summary>
    /// 最初に一致した文字列だけを置換
    /// https://baba-s.hatenablog.com/entry/2023/01/25/134558
    /// </summary>
    /// <returns></returns>
    static string ReplaceFirst(this string self, string oldValue, string newValue)
    {
        var startIndex = self.IndexOf(oldValue);
        if (startIndex == -1)
            return self;
        return self
            .Remove(startIndex, oldValue.Length)
            .Insert(startIndex, newValue);
    }

    /// <summary>
    /// {num}を数字に置換（最初に一致した文字列のみ）
    /// </summary>
    /// <param name="num">置換する数字</param>
    /// <returns></returns>
    static public string ReplaceNumber(this string self, int num)
    {
        return self.ReplaceFirst("{num}", num.ToString());
    }
}
