using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using KyawaLib;

public class Fader : SingletonMonoBehaviour<Fader>
{
    [SerializeField]
    Image m_image = null;
    
    /// <summary>
    /// フェード処理
    /// </summary>
    /// <param name="to">最終値</param>
    /// <param name="duration">所要時間(s)</param>
    /// <param name="color">フェードカラー(RGB)</param>
    /// <param name="ease">イージング</param>
    /// <returns></returns>
    Tweener GetFadingTweener(float to, float duration, Color? color, Ease ease = Ease.Linear)
    {
        if (m_image == null)
            return null;

        color ??= Color.black;

        return m_image.DOFade(to, duration)
            .SetEase(ease)
            .SetUpdate(UpdateType.Normal)
            .OnStart(()=>
            {
                m_image.color = (Color)color;
                m_image.raycastTarget = true;
            })
            .OnComplete(() =>
            {
                m_image.raycastTarget = to != 0f;
            });
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <param name="duration">所要時間(s)</param>
    /// <param name="color">フェードカラー(RGB)</param>
    /// <param name="ease">イージング</param>
    public void FadeIn(float duration, Color? color = null, Ease ease = Ease.Linear)
    {
        GetFadingTweener(0f, duration, color, ease);
    }

    public IEnumerator CoFadeIn(float duration, Color? color = null, Ease ease = Ease.Linear)
    {
        var tweener = GetFadingTweener(0f, duration, color, ease);
        yield return tweener.WaitForKill();
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <param name="duration">所要時間(s)</param>
    /// <param name="color">フェードカラー(RGB)</param>
    /// <param name="ease">イージング</param>
    public void FadeOut(float duration, Color? color = null, Ease ease = Ease.Linear)
    {
        GetFadingTweener(1f, duration, color, ease);
    }

    public IEnumerator CoFadeOut(float duration, Color? color = null, Ease ease = Ease.Linear)
    {
        var tweener = GetFadingTweener(1f, duration, color, ease);
        yield return tweener.WaitForKill();
    }
}
