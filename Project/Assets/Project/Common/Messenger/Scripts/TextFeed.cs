using System.Collections;
using UnityEngine;
using TMPro;
using KyawaLib;

/// <summary>
/// テキストの文字送りをするクラス
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFeed : MonoBehaviour
{
    TextMeshProUGUI m_tmpro = null;

    /// <summary>
    /// 文字のフェードイン時間(s)
    /// </summary>
    [SerializeField, Min(0)]
    float m_fadeInSpeed = 0.05f;

    /// <summary>
    /// 次の文字を表示し始めるまでの時間(s)
    /// フェードイン時間とは関係なく、現在の文字を表示し始めた瞬間からの時間
    /// </summary>
    [SerializeField, Min(0)]
    float m_feedSpeed = 0.05f;

    /// <summary>
    /// 文字送り中か
    /// </summary>
    bool m_isRunning = false;

    /// <summary>
    /// 文字送りがスキップされたか
    /// </summary>
    bool m_onSkip = false;

    /// <summary>
    /// 文字送りがキャンセルされたか
    /// </summary>
    bool m_onCancel = false;

    public bool isRunning => m_isRunning; // 外部からの取得用

    void Awake()
    {
        m_tmpro = GetComponent<TextMeshProUGUI>();
        m_tmpro.text = string.Empty;
    }

    void OnDisable()
    {
        m_isRunning = false;
    }

    IEnumerator CoRun()
    {
        m_isRunning = true;
        m_onSkip = false;
        m_onCancel = false;

        m_tmpro.ForceMeshUpdate();

        var charCount = m_tmpro.textInfo.characterCount;
        var currentCharCount = 0; // 現在表示している文字位置

        // まず全文字非表示
        for (int i = 0; i < charCount; i++)
        {
            ChangeCharAlpha(i, 0);
        }

        // 1文字ずつ表示
        var onBreak = false;
        while (currentCharCount < charCount)
        {
            // フェードイン開始
            StartCoroutine(CoFadeInChar(currentCharCount));
            // 次の文字を表示し始めるまで待機
            var waitingTime = m_feedSpeed;
            while (0f < waitingTime)
            {
                // 中断されたか
                if (m_onSkip || m_onCancel)
                {
                    onBreak = true;
                    break;
                }
                waitingTime -= Time.deltaTime;
                yield return null;
            }
            // 中断された場合の終了処理
            if (onBreak)
            {
                if (m_onSkip) // 残りの文字を一気に表示
                {
                    break;
                }
                else if (m_onCancel) // 強制終了
                {
                    m_isRunning = false;
                    yield break;
                }
            }
            currentCharCount++;
        }

        // 残りの文字を一気に表示
        for (int i = currentCharCount; i < charCount; i++)
        {
            ChangeCharAlpha(i, 255);
        }

        m_isRunning = false;
    }

    /// <summary>
    /// テキスト内の特定の文字をフェードインする
    /// </summary>
    /// <param name="index">何文字目か</param>
    /// <returns></returns>
    IEnumerator CoFadeInChar(int index)
    {
        if (0 < m_fadeInSpeed)
        {
            var ratio = 0f;
            while (ratio < 1f)
            {
                if (!m_isRunning)
                    break;

                ratio += (Time.deltaTime / m_fadeInSpeed);
                var alpha = (byte)(255 * ratio);
                ChangeCharAlpha(index, alpha);
                yield return null;
            }
        }
        ChangeCharAlpha(index, 255);
    }

    /// <summary>
    /// テキスト内の特定の文字のα値を変更する
    /// </summary>
    /// <param name="index">何文字目か</param>
    /// <param name="alpha">α値</param>
    void ChangeCharAlpha(int index, byte alpha)
    {
        var textInfo = m_tmpro.textInfo;
        KyDebug.AssertIsTrue(0 <= index && index < textInfo.characterInfo.Length);
        var characterInfo = textInfo.characterInfo[index];
        if (!characterInfo.isVisible)
            return;

        // 現在の文字のMaterialと頂点の位置を取得
        var materialIndex = characterInfo.materialReferenceIndex;
        var vertexIndex = characterInfo.vertexIndex;
        // α値変更
        Color32 color = characterInfo.color;
        color.a = alpha;
        var colors = textInfo.meshInfo[materialIndex].colors32;
        for (var i = 0; i < 4; i++)
        {
            colors[vertexIndex + i] = color;
        }
        // Mesh に Color が変更された通知
        m_tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    /// <summary>
    /// 文字送り開始
    /// </summary>
    /// <param name="text"></param>
    public void Run(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        Stop();
        m_tmpro.text = text;
        StartCoroutine(CoRun());
    }

    /// <summary>
    /// 文字送りスキップ（全文字表示）
    /// </summary>
    public void Skip()
    {
        m_onSkip = true;
    }

    /// <summary>
    /// 文字送りストップ（再開不可）
    /// </summary>
    public void Stop()
    {
        m_onCancel = true;
    }

    /// <summary>
    /// 表示している文字を破棄
    /// </summary>
    public void Clear()
    {
        Stop();
        m_tmpro.text = string.Empty;
    }
}