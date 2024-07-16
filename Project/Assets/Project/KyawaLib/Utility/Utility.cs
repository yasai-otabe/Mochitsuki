using System;
using System.Collections.Generic;
using UnityEngine;

namespace KyawaLib
{
    static public class Utility
    {
        /// <summary>
        /// 1/2の確率でtrueを返す
        /// </summary>
        /// <returns>true or false</returns>
        static public bool GetRandomBool()
        {
            int rand = UnityEngine.Random.Range(0, 2);
            return rand == 0;
        }

        /// <summary>
        /// weightの確率でtrueを返す
        /// </summary>
        /// <param name="weight">重み（0.0~1.0）</param>
        /// <returns>true or false</returns>
        static public bool GetRandomBool(float weight)
        {
            float w = Mathf.Clamp01(weight);
            float rand = UnityEngine.Random.Range(0f, 1f);
            return rand < w;
        }

        /// <summary>
        /// enumの要素数を取得する
        /// </summary>
        /// <param name="enumType">enum</param>
        /// <returns>要素数</returns>
        static public int GetEnumLength(Type enumType)
        {
            return Enum.GetValues(enumType).Length;
        }

        // --------------------------------------------------------------------
        // https://www.create-forever.games/unity-absolute-path-assets-path/

        /// <summary>
        /// 絶対パスから Assets/ パスに変換する
        /// </summary>
        static public string AbsoluteToAssetsPath(this string self)
        {
            return self.Replace("\\", "/").Replace(Application.dataPath, "Assets");
        }

        /// <summary>
        /// Assets/ パスから絶対パスに変換する
        /// </summary>
        static public string AssetsToAbsolutePath(this string self)
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            return self.Replace("Assets", Application.dataPath).Replace("/", "\\");
#else
            return self.Replace("Assets", Application.dataPath);
#endif
        }

        // --------------------------------------------------------------------
        // https://qiita.com/gushwell/items/a7ea8d02da1f595a1160

        static public void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }

        static public void Sort<T>(this T[] array) where T : IComparable<T>
        {
            Array.Sort(array);
        }

        // --------------------------------------------------------------------
    }
}