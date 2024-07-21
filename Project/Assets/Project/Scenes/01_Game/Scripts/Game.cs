using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KyawaLib;
using TMPro;

public class GameManager : SingletonClass<GameManager>
{
    Game m_gameObject = null;
    GameUI m_ui = null;

    private bool m_InputEnable = false;
    private Timer m_Timer = new Timer();
    private InputManager m_InputManager = null;
    private int m_InputCount;

    public GameUI UI => m_ui;

    public GameManager()
    {
        m_gameObject = new GameObject("**Game**").AddComponent<Game>();
        m_ui = Object.FindFirstObjectByType<GameUI>();
        m_InputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        m_Timer.SetTimer(30.0f);
    }

    /// <summary>
    /// 入力の有効・無効を設定する
    /// </summary>
    public void SetEnableInput(bool bEnable)
    {
        m_InputEnable = bEnable;
    }

    /// <summary>
    /// 入力が有効か
    /// </summary>
    /// <returns>
    /// true：有効<br/>
    /// false：無効
    /// </returns>
    public bool IsInputEnbale()
    {
        return m_InputEnable;
    }

    /// <summary>
    /// タイマー取得
    /// </summary>
    /// <returns>タイマー</returns>
    public Timer GetTimer()
    {
        return m_Timer;
    }

    /// <summary>
    /// InputManagerを取得
    /// </summary>
    /// <returns>InputManager</returns>
    public InputManager GetInputManager()
    {
        return m_InputManager;
    }

    /// <summary>
    /// UIを取得
    /// </summary>
    /// <returns>GameUI</returns>
    public GameUI GetUI()
    {
        return m_ui;
    }

    /// <summary>
    /// 入力回数をプラス1
    /// </summary>
    public void AddInputCount()
    {
        m_InputCount++;
    }

    /// <summary>
    /// 入力した回数を取得
    /// </summary>
    /// <returns>入力した回数</returns>
    public int GetInputCount()
    {
        return m_InputCount;
    }

    /// <summary>
    /// 入力した回数をリセット
    /// </summary>
    public void ResetInputCount()
    {
        m_InputCount = 0;
    }
}

public class Game : MonoBehaviour
{
    Timer m_Timer = null;
    InputManager m_InputManager = null;
    public static void LoadScene()
    {
        SceneLoader.instance.LoadSceneAsync("Game", LoadSceneMode.Single, GameManager.Create);
    }

    void OnDestroy()
    {
        GameManager.instance.Destroy();
    }
    
    IEnumerator Start()
    {
        m_InputManager = GameManager.instance.GetInputManager();
        m_Timer = GameManager.instance.GetTimer();
        yield return Fader.instance.CoFadeIn(1f);
        GameManager.instance.SetEnableInput(true);
    }

    private void Update()
    {
        // もちつき待機中はカウントしない
        if(m_InputManager.GetMochitsukiType() != InputManager.Mochitsuki.STAY)
        {
            if(m_Timer.UpdateTimer(Time.deltaTime))
            {
                // 何かしらに遷移
            }
        }

        GameManager.instance.GetUI().TextMeshPro.text = ((int)(m_Timer.GetRemainTime())).ToString() + " s";
        GameManager.instance.GetUI().TextMeshProCount.text = "Count " + GameManager.instance.GetInputCount().ToString();
    }
}