using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    Button m_nextButton = null;
    public Button nextButton => m_nextButton;

    [SerializeField]
    TextMeshProUGUI m_nameText = null;

    [SerializeField]
    TextMeshProUGUI m_descriptionText = null;

    [SerializeField]
    Image m_mochiImage = null;

    [SerializeField]
    GameObject m_newObject = null;

    public void SetResult(string name, string description, Sprite mochi, bool isNew)
    {
        m_nameText.text = name;
        m_descriptionText.text = description;
        m_mochiImage.sprite = mochi;
        m_newObject.SetActive(isNew);
    }
}
