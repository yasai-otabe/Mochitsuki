using System;
using UnityEngine;

[Serializable]
public class AudioClipInfo
{
    [SerializeField]
    AudioClip m_clip = null;
    public AudioClip Clip => m_clip;

    [SerializeField, Range(0f, 1f)]
    float m_volume = 1f;
    public float Volume => m_volume;

    [SerializeField]
    bool m_onLoop = false;
    public bool OnLoop => m_onLoop;
}