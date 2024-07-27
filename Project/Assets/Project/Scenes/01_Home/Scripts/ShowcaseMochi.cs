using KyawaLib;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShowcaseMochi : MonoBehaviour
{
    [SerializeField, ReadOnly]
    int m_id = 0;

    SpriteRenderer m_renderer = null;

    public int ID => m_id;

    void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    public void Init(bool isUnlock)
    {
        var color = isUnlock ? Color.white : Color.gray;
        m_renderer.color = color;
    }
}
