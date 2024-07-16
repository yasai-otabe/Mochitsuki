using System.Collections.Generic;
using DG.Tweening;
using KyawaLib;
using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class SePlayer : SingletonMonoBehaviour<SePlayer>
{
    SoundPlayer m_player = null;
    Tweener m_vplumeFadingTweener = null;

    [SerializeField]
    AudioClipInfoScrObj m_clipInfo = null;

    public SoundPlayer player => m_player;

    public void SetAudioClipInfo(AudioClipInfoScrObj audioClipInfoScrObj)
        => m_clipInfo = audioClipInfoScrObj;

    void Awake()
    {
        m_player = GetComponent<SoundPlayer>();
    }

    /// <summary>
    /// 音量フェード処理
    /// </summary>
    /// <param name="from">初期値</param>
    /// <param name="to">最終値</param>
    /// <param name="duration">所要時間(s)</param>
    /// <param name="ease">イージング</param>
    Tweener GetVolumeFadingTweener(float from, float to, float duration, Ease ease = Ease.Linear)
    {
        Mathf.Clamp01(from);
        Mathf.Clamp01(to);
        return DOVirtual.Float(from, to, duration, value => player.volume = value)
            .SetEase(ease)
            .SetUpdate(UpdateType.Normal);
    }

    /// <summary>
    /// 再生（重複不可）
    /// </summary>
    /// <param name="clipName">再生するクリップの名前</param>
    public void PlayOneShot(string clipName)
    {
        if (m_clipInfo == null)
            return;

        var info = m_clipInfo.Get(clipName);
        if (info == null)
        {
            KyDebug.LogError($"{clipName} という名前の音声クリップが存在しません。");
            return;
        }

        m_vplumeFadingTweener?.Kill(false);

        m_player.PlayOneShot(info.Clip, info.Volume);
    }

    /// <summary>
    /// 再生（重複不可）
    /// </summary>
    /// <param name="clipName">再生するクリップの名前</param>
    public void Play(string clipName,
        float fadeInDuration = 0f, Ease fadeInEase = Ease.Linear)
    {
        if (m_clipInfo == null)
            return;

        var info = m_clipInfo.Get(clipName);
        if (info == null)
        {
            KyDebug.LogError($"{clipName} という名前の音声クリップが存在しません。");
            return;
        }

        m_vplumeFadingTweener?.Kill(false);

        m_vplumeFadingTweener = GetVolumeFadingTweener(0f, 1f, fadeInDuration, fadeInEase)
            .OnStart(() =>
            {
                m_player.Play(info.Clip, info.Volume, info.OnLoop);
            });
    }

    /// <summary>
    /// 一時停止
    /// </summary>
    public void Pause()
    {
        m_player.Pause();
        m_vplumeFadingTweener?.Pause();
    }

    /// <summary>
    /// 再開
    /// </summary>
    public void Resume()
    {
        m_player.Resume();
        m_vplumeFadingTweener?.Play();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop(float fadeInDuration = 0f, Ease fadeInEase = Ease.Linear)
    {
        if (m_clipInfo == null)
            return;

        m_vplumeFadingTweener?.Kill(false);

        m_vplumeFadingTweener = GetVolumeFadingTweener(0f, 1f, fadeInDuration, fadeInEase)
            .OnComplete(() =>
            {
                m_player.Stop();
            });
    }

#if UNITY_EDITOR
    public int dbg_clipIndex { get; set; } = 0;

    /// <summary>
    /// クリップ名配列を取得する
    /// </summary>
    /// <returns></returns>
    public List<string> Dbg_GetClipNames()
    {
        if (m_clipInfo == null)
            return new();
        return m_clipInfo.Dbg_GetNames();
    }

    /// <summary>
    /// クリップ情報を取得する
    /// </summary>
    /// <returns></returns>
    public AudioClipInfo Dbg_GetAudioClipInfo(string name)
        => m_clipInfo?.Get(name);
#endif
}
