using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TextFeed))]
public class Messenger : MonoBehaviour
{
    TextFeed m_textFeed = null;

    /// <summary>
    /// 文字送りがスキップされたか
    /// </summary>
    bool m_onSkip = false;

    /// <summary>
    /// 次のメッセージに進むか
    /// </summary>
    bool m_onNextMessage = false;

    /// <summary>
    /// 次のメッセージに進むか
    /// </summary>
    public bool onNextMessage => m_onNextMessage;

    void Awake()
    {
        m_textFeed = GetComponent<TextFeed>();
    }

    /// <summary>
    /// メッセージを進行
    /// </summary>
    /// <returns></returns>
    IEnumerator CoRun(string text)
    {
        m_onSkip = false;
        m_onNextMessage = false;

        // タグ解析
        TextTagAnalyzer.Analyze(ref text);

        // 文字送り開始
        m_textFeed.Run(text);
        // 文字送りが終わるまで待機
        while (m_textFeed.isRunning)
        {
            // 文字送りがスキップされた
            if (m_onSkip)
                m_textFeed.Skip();
            yield return null;
        }
        // メッセージ送り待ち
        yield return new WaitUntil(() => m_onNextMessage);
        // テキストを破棄
        m_textFeed.Clear();
    }

    /// <summary>
    /// メッセージ表示開始
    /// </summary>
    public void Run(string text)
    {
        StartCoroutine(CoRun(text));
    }

    /// <summary>
    /// 何かしら入力があったときに呼び出します
    /// 文字送り中なら文字送りスキップ、メッセージ送り待ちなら次のメッセージへ進みます
    /// </summary>
    public void OnInput()
    {
        if (m_textFeed.isRunning)
            m_onSkip = true;
        else
            m_onNextMessage = true;
    }
}
