using UnityEngine;

/// <summary>
/// サウンドプレイヤー
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    /// <summary>
    /// サウンドを再生するコンポーネント
    /// </summary>
    AudioSource m_source = null;

    /// <summary>
    /// 一時停止中か
    /// </summary>
    bool m_onPause = false;

    /// <summary>
    /// 再生中か
    /// </summary>
    /// <returns></returns>
    public bool isPlaying => m_source.isPlaying;

    /// <summary>
    /// 一時停止中か
    /// </summary>
    public bool isPausing => m_onPause;

    /// <summary>
    /// 停止中か
    /// </summary>
    public bool isStopped => !isPlaying && !m_onPause;

    /// <summary>
    /// ミュート設定
    /// </summary>
    public bool onMute
    {
        get => m_source.mute;
        set => m_source.mute = value;
    }

    /// <summary>
    /// 音量設定
    /// </summary>
    public float volume
    {
        get => m_source.volume;
        set => m_source.volume = value;
    }

    public float GetTime()
        => (m_source == null) ? 0 : m_source.time;

    void Awake()
    {
        m_source = gameObject.GetComponent<AudioSource>();
        m_source.playOnAwake = false;
        m_source.loop = false;
    }

    /// <summary>
    /// 再生（重複可能）
    /// </summary>
    public void PlayOneShot(AudioClip clip, float volume)
    {
        Resume(); // 一時停止中かもしれないので解除

        m_source.PlayOneShot(clip, volume);
    }

    /// <summary>
    /// 再生（重複不可）
    /// </summary>
    public void Play(AudioClip clip, float volume, bool onLoop)
    {
        m_source.clip = clip;
        m_source.volume = volume;
        m_source.loop = onLoop;
        m_source.Play();
    }

    /// <summary>
    /// 一時停止
    /// </summary>
    public void Pause()
    {
        if (!isPlaying)
            return;

        m_source.Pause();
        m_onPause = true;
    }

    /// <summary>
    /// 再開
    /// </summary>
    public void Resume()
    {
        if (!m_onPause)
            return;

        m_source.UnPause();
        m_onPause = false;
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        if (isStopped)
            return;

        Resume(); // 一時停止中かもしれないので解除

        m_source.Stop();
        m_source.clip = null;
    }
}
