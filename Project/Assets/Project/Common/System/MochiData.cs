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
        Vector2Int _range = Vector2Int.zero;
        [SerializeField]
        Sprite _sprite = null;

        public string name => _name;
        public string description => _description;
        public Vector2Int range => _range;
        public Sprite sprite => _sprite;
    }

    public int DataCount => m_data.Count;

    public Data GetDataFromID(int mochiID)
    {
        var index = mochiID - 1;
        KyDebug.AssertIsWithinRange(m_data.Count, index);
        return m_data[index];
    }

    public (int ID, Data data) GetDataFromMochitsukiCount(int mochitsukiCount)
    {
        for (int i = 0; i < m_data.Count; i++)
        {
            Data data = m_data[i];
            var min = data.range.x;
            var max = data.range.y;
            if (min <= mochitsukiCount && mochitsukiCount <= max)
                return new(i + 1, data);
        }
        KyDebug.AssertIsFalse(true);
        return new(0, null);
    }
}
