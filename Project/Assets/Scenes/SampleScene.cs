using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    [SerializeField]
    GameObject m_sample = null;

    [SerializeField]
    Messenger m_messenger = null;

    // Start is called before the first frame update
    void Start()
    {
        Fader.instance.FadeOut(0f);
        StartCoroutine(CoText());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            m_sample.transform.DOShakePosition(1f, 0.5f);
    }

    IEnumerator CoText()
    {
        yield return Fader.instance.CoFadeIn(1f);
        yield return new WaitForSeconds(1);
        m_messenger.Run("サンプルシーン");
    }
}
