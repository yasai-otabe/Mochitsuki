using System;
using System.Collections.Generic;
using KyawaLib;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipInfo", menuName = "KyawaLib/AudioClipInfo")]
public class AudioClipInfoScrObj : ScriptableObject
{
    [SerializeField]
    List<Data> m_infos = new List<Data>();

    [Serializable]
    class Data
    {
        [SerializeField]
        string name;
        public string Name => name;

        [SerializeField]
        AudioClipInfo info;
        public AudioClipInfo Info => info;
    }

    /// <summary>
    /// クリップ情報を取得
    /// </summary>
    /// <param name="index">インデックス</param>
    /// <returns>クリップ情報</returns>
    public AudioClipInfo Get(int index)
    {
        KyDebug.AssertIsTrue(0 <= index && index < m_infos.Count);
        var info = m_infos[index];
        return info?.Info;
    }

    /// <summary>
    /// クリップ情報を取得
    /// </summary>
    /// <param name="name">名前</param>
    /// <returns>クリップ情報</returns>
    public AudioClipInfo Get(string name)
    {
        var info = m_infos.Find(_ => _.Name == name);
        return info?.Info;
    }

#if UNITY_EDITOR
    public List<string> Dbg_GetNames()
    {
        if (m_infos == null)
            return new();

        var names = new List<string>(m_infos.Count);
        m_infos.ForEach(_ => names.Add(_.Name));
        return names;
    }
#endif
}