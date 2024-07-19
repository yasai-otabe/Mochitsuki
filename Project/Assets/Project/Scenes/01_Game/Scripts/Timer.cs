using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// シンプルなタイマークラス
/// </summary>
public class Timer
{
    private float m_Time;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Timer()
    {
        m_Time = 0.0f;
    }

    /// <summary>
    /// タイマーに値を設定する
    /// </summary>
    /// <param name="fTime">設定する値</param>
    public void SetTimer(float fTime)
    {
        Assert.IsTrue(fTime > 0.0f,"タイマーに設定する値は0以上でなければならない。");

        if (fTime > 0.0f)
        {
            m_Time = fTime;
        }
        else
        {
            m_Time = 0.0f;
        }
    }

    /// <summary>
    /// タイマーをリセット
    /// </summary>
    public void ResetTimer()
    {
        m_Time = 0.0f;
    }

    /// <summary>
    /// タイマーが使用中か
    /// </summary>
    /// <returns>
    /// true：使用中<br/>
    /// false：未使用
    /// </returns>
    public bool IsUsedTimer()
    {
        if (m_Time > 0.0f)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// タイマーの残り時間を取得
    /// </summary>
    /// <returns>タイマーの残り時間</returns>
    public float GetRemainTime()
    {
        if(IsUsedTimer())
        {
            return m_Time;
        }

        Assert.IsTrue(false, "タイマーは設定されていません。");
        return 0.0f;
    }

    /// <summary>
    /// タイマーを更新する
    /// </summary>
    /// <param name="fDeltaTime">1フレームの経過時間</param>
    /// <returns>
    /// true：タイマー終了<br/>
    /// false：タイマー継続中
    /// </returns>
    public bool UpdateTimer(float fDeltaTime)
    {
        if (IsUsedTimer())
        {
            m_Time -= fDeltaTime;

            if(m_Time <= 0.0f)
            {
                ResetTimer();
                return true;
            }

            return false;
        }

        Assert.IsTrue(false, "タイマーは設定されていません。");
        return false;
    }
}
