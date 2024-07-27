using System.Collections;
using System.Collections.Generic;
using KyawaLib;
using UnityEngine;

public class HomeObject : MonoBehaviour
{
    [SerializeField, ReadOnly]
    List<ShowcaseMochi> m_showcaseMochis = new();

    public List<ShowcaseMochi> showcaseMochis => m_showcaseMochis;

    public ShowcaseMochi GetShowcaseMochi(int id)
    {
        var mochi = m_showcaseMochis.Find(_ => _.ID == id);
        KyDebug.AssertIsNotNull(mochi);
        return mochi;
    }
}
