using KyawaLib;
using UnityEngine;

public class ShowcaseMochi : MonoBehaviour
{
    [SerializeField, ReadOnly]
    int m_id = 0;

    [SerializeField]
    SpriteRenderer m_renderer = null;

    public int ID => m_id;

    public void Init(bool isUnlock)
    {
        var color = isUnlock ? Color.white : Color.gray;
        m_renderer.color = color;
    }
}
