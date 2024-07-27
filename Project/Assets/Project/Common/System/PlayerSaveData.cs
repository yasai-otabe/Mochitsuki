using System;
using System.Text;
using KyawaLib;
using UnityEngine;

public static class PlayerSaveData
{
    /// <summary>
    /// セーブデータをロード
    /// </summary>
    public static void LoadAll()
    {
        MochitsukiCount.Load();
        MochiRelease.Load();

#if UNITY_EDITOR
        var strBuilder = new StringBuilder();
        strBuilder.AppendLine("SaveData Loded Log:");

        strBuilder.AppendLine($"mochitsukiCount : {MochitsukiCount.value}");
        strBuilder.AppendLine($"mochiRelease : {Convert.ToString(MochiRelease.value, 2)}");

        KyDebug.Log(strBuilder.ToString());
#endif
    }
    /// <summary>
    /// セーブデータをセーブ
    /// </summary>
    public static void SaveAll()
    {
        MochitsukiCount.Save();
        MochiRelease.Save();
    }
    /// <summary>
    /// セーブデータをリセット
    /// </summary>
    public static void ResetAll()
    {
        MochitsukiCount.Reset();
        MochiRelease.Reset();
    }
    /// <summary>
    /// セーブデータを削除
    /// </summary>
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static class MochitsukiCount
    {
        static readonly (string key, int defaultValue) ms_keyValue = new("mochitsukiCount", 0);

        /// <summary>
        /// 餅つき回数
        /// </summary>
        public static int value { get; private set; } = ms_keyValue.defaultValue;

        /// <summary>
        /// 餅つき回数を加算してセーブ
        /// </summary>
        /// <param name="counts">加算数</param>
        public static void AddAndSaveMochitsukiCount(int counts)
        {
            value += counts;
            Save();
        }

        public static void Load()
            => value = PlayerPrefs.GetInt(ms_keyValue.key, ms_keyValue.defaultValue);

        public static void Save()
            => PlayerPrefs.SetInt(ms_keyValue.key, value);

        public static void Reset()
        {
            value = ms_keyValue.defaultValue;
            PlayerPrefs.SetInt(ms_keyValue.key, ms_keyValue.defaultValue);
        }
        public static void Delete()
        {
            value = ms_keyValue.defaultValue;
            PlayerPrefs.DeleteKey(ms_keyValue.key);
        }
    }


    public static class MochiRelease
    {
        static readonly (string key, int defaultValue) ms_keyValue = new("mochiRelease", 0);

        /// <summary>
        /// 餅の解放状態
        /// ビットで管理（0で未解放、1で解放済）
        /// </summary>
        public static int value = ms_keyValue.defaultValue;

        /// <summary>
        /// 餅が解放されているか
        /// </summary>
        /// <param name="mochiID">ID（1~32）</param>
        /// <returns>true:解放済、false:未解放</returns>
        public static bool IsRelease(int mochiID)
        {
            KyDebug.AssertIsTrue(0 < mochiID && mochiID <= 32);
            var x = value >> (mochiID - 1); // チェックしたいビットを1桁目に右シフトする
            return (x & 1) == 1; // 1桁目が0か1か
        }
        /// <summary>
        /// 餅を解放する
        /// </summary>
        /// <param name="mochiID">ID（1~32）</param>
        public static void Release(int mochiID)
        {
            KyDebug.AssertIsTrue(0 < mochiID && mochiID <= 32);
            value |= 1 << (mochiID - 1);
            Save();
        }

        public static void Load()
            => value = PlayerPrefs.GetInt(ms_keyValue.key, ms_keyValue.defaultValue);

        public static void Save()
            => PlayerPrefs.SetInt(ms_keyValue.key, value);

        public static void Reset()
        {
            value = ms_keyValue.defaultValue;
            PlayerPrefs.SetInt(ms_keyValue.key, ms_keyValue.defaultValue);
        }
        public static void Delete()
        {
            value = ms_keyValue.defaultValue;
            PlayerPrefs.DeleteKey(ms_keyValue.key);
        }
    }
}
