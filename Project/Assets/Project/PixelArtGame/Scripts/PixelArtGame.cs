
using System.Text.RegularExpressions;

public static class PixelArtGame
{
    public const int SCREEN_WIDTH = 512;
    public const int SCREEN_HEIGHT = 288;
    public const int PIXELS_PER_UNIT = 16;

    /// <summary>
    /// 整数値を全角の文字列に変換します
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToStringFullWidth(this int value)
        => Regex.Replace(value.ToString(), "[0-9]", _ => ((char)(_.Value[0] - '0' + '０')).ToString());

    /// <summary>
    /// 実数値を切り捨てて全角の文字列に変換します
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToStringFullWidth(this float value)
        => Regex.Replace(((int)value).ToString(), "[0-9]", _ => ((char)(_.Value[0] - '0' + '０')).ToString());

}