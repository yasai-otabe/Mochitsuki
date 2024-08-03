using UnityEngine;

public class PlateBubble : MonoBehaviour
{
    [SerializeField]
    PixelArtGameTextController m_nameText = null;

    [SerializeField]
    PixelArtGameTextController m_hintText = null;

    RectTransform rectTransform = null;

    void Awake()
    {
        gameObject.SetActive(false);
        rectTransform = GetComponent<RectTransform>();
    }

    public void Show(string name, string hint, Vector2 position)
    {
        if (!gameObject.activeSelf)
        {
            m_nameText.SetText(name);
            m_hintText.SetText(hint);
            rectTransform.anchoredPosition = position;
            gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
