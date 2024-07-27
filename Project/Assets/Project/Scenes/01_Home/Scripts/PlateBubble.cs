using TMPro;
using UnityEngine;

public class PlateBubble : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_nameText = null;

    [SerializeField]
    TextMeshProUGUI m_hintText = null;

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
            m_nameText.text = name;
            m_hintText.text = hint;
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
