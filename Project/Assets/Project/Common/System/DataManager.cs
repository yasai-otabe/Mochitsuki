using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KyawaLib;

public class DataManafer : SingletonMonoBehaviour<DataManafer>
{
    [SerializeField]
    MochiData m_mochiData = null;

    [SerializeField]
    CraftsManData m_craftsManData = null;

    /// <summary>
    /// もちデータ
    /// </summary>
    public MochiData mochiData => m_mochiData;
    /// <summary>
    /// 職人レベルデータ
    /// </summary>
    public CraftsManData craftsManData => m_craftsManData;
}