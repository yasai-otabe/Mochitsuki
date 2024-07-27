using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftsManData", menuName = "Project/Data/CraftsManData", order = 1)]
public class CraftsManData : ScriptableObject
{
    [SerializeField]
    List<Data> m_data = new();

    [System.Serializable]
    class Data
    {
        public string name = string.Empty;
        public int releaseCount = 0;
    }

    public string GetCraftsManName(int totalMochitsukiCount)
    {
        var result = "EMPTY";
        foreach (var data in m_data)
        {
            if (data.releaseCount <= totalMochitsukiCount)
                result = data.name;
        }
        return result;
    }

    public int GetNextReleaseCount(int totalMochitsukiCount)
    {
        foreach (var data in m_data)
        {
            if (totalMochitsukiCount < data.releaseCount)
                return data.releaseCount;
        }
        return 0; // LvMAXのときここにくる
    }
}
