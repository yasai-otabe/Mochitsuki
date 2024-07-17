using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KyawaLib;

public class GameManager : SingletonClass<GameManager>
{
    Game m_gameObject = null;
    GameUI m_ui = null;

    public GameUI UI => m_ui;

    public GameManager()
    {
        m_gameObject = new GameObject("**Game**").AddComponent<Game>();
        m_ui = Object.FindFirstObjectByType<GameUI>();
    }
}

public class Game : MonoBehaviour
{
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
        yield return Fader.instance.CoFadeIn(1f);
    }
}