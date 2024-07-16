using System;
using System.Collections.Generic;
using System.IO;
using KyawaLib;
using UnityEngine;

[CreateAssetMenu(fileName = "MessageData", menuName = "KyawaLib/MessageData")]
public class MessageDataScrObj : ScriptableObject
{
    Dictionary<string, string[]> m_data = new();
    int m_messageCount = 0;

    public int messageCount => m_messageCount;

    public string GetMessage(string key, int index = 0)
    {
        if (m_data.TryGetValue(key, out var result))
        {
            KyDebug.AssertIsTrue(0 <= index && index < result.Length);
            return result[index];
        }
        KyDebug.LogError($"Not found message key : {key}");
        return "NULL";
    }

#if UNITY_EDITOR
    [SerializeField]
    TextAsset m_csv = null;

    public bool Convert()
    {
        try
        {
            if (m_csv == null)
                throw new Exception("CSVがありません");

            m_data.Clear();

            var reader = new StringReader(m_csv.text);

            var header = reader.ReadLine();
            m_messageCount = header.Split(',').Length - 1;
            if (m_messageCount <= 0)
                throw new Exception("ヘッダーデータに不備があります");

            while (reader.Peek() != -1)
            {
                var line = reader.ReadLine();
                var str = line.Split(',');
                if (str.Length < 2)
                    throw new Exception("データに不備があります");

                var values = new string[str.Length - 1];
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = str[i + 1];
                }
                m_data.Add(str[0], values);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
        return true;
    }
#endif
}
