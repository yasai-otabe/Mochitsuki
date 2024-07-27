using System.Collections;
using System.Collections.Generic;
using KyawaLib;
using UnityEngine;

[CreateAssetMenu(fileName = "MochiData", menuName = "Project/Data/MochiData", order = 1)]
public class MochiData : ScriptableObject
{
    [SerializeField]
    List<Data> m_data = new();

    [System.Serializable]
    public class Data
    {
        [SerializeField]
        string _name = string.Empty;
        [SerializeField, Multiline(3)]
        string _description = string.Empty;
        [SerializeField]
        Vector2 _range = Vector2.zero;
        [SerializeField]
        Sprite _sprite = null;

        public string name => _name;
        public string description => _description;
        public Vector2 range => _range;
        public Sprite sprite => _sprite;
    }

    public Data GetDataFromID(int mochiID)
    {
        var index = mochiID - 1;
        KyDebug.AssertIsWithinRange(m_data.Count, index);
        return m_data[index];
    }

    public Data GetDataFromMochitsukiCount(int mochitsukiCount)
    {
        foreach (var data in m_data)
        {
            var min = data.range.x;
            var max = data.range.y;
            if (min <= mochitsukiCount && mochitsukiCount <= max)
                return data;
        }
        KyDebug.AssertIsFalse(true);
        return null;
    }
}
