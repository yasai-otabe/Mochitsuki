using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PixelArtGameTextController : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    PixelArtGameFontPreset m_fontPreset = null;

    void OnValidate()
    {
        if (m_fontPreset != null)
        {
            m_text ??= GetComponent<TextMeshProUGUI>();
            m_text.font = m_fontPreset.asset;
            m_text.fontSize = m_fontPreset.fontSize;
        }
    }
#endif

    TextMeshProUGUI m_text = null;

    public TextMeshProUGUI textMeshProUGUI => m_text;

    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 全角の数字をセットします
    /// </summary>
    /// <param name="value"></param>
    public void SetNumber(int value)
    => m_text.text = Regex.Replace(value.ToString(), "[0-9]", _ => ((char)(_.Value[0] - '0' + '０')).ToString());

    public void SetText(string text)
        => m_text.text = text;

    public void SetFontColor(Color color)
        => m_text.color = color;
}

#if UNITY_EDITOR
public static class TextControllerEditor
{
    [UnityEditor.MenuItem("GameObject/PixelArtGame/Text", false, 0)]
    private static void CreatePixelArtGameCanvas()
    {
        var obj = new GameObject("NewText");
        obj.layer = 5; // UI

        if (UnityEditor.Selection.activeObject != null)
        {
            if (UnityEditor.Selection.activeObject is GameObject gameObject)
                obj.transform.SetParent(gameObject.transform);
        }

        var rectTransform = obj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;

        var textController = obj.AddComponent<PixelArtGameTextController>();

        UnityEditorInternal.ComponentUtility.MoveComponentUp(textController);

        UnityEditor.Selection.activeObject = obj;
        UnityEditor.Undo.RegisterCreatedObjectUndo(obj, "PixelArtGameText");
    }
}
#endif
