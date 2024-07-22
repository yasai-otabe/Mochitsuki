using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KyawaLib;
using UnityEngine.Events;

public class HomeManager : SingletonClass<HomeManager>
{
    Home m_gameObject = null;
    HomeUI m_ui = null;

    public HomeUI UI => m_ui;

    public static void LoadScene(UnityAction<HomeManager> loadedAction = null)
    {
        SceneLoader.instance.LoadSceneAsync("Result", LoadSceneMode.Single,
           () =>
           {
               Create();
               loadedAction?.Invoke(instance);
           });
    }

    public HomeManager()
    {
        m_gameObject = new GameObject("**Home**").AddComponent<Home>();
        m_ui = Object.FindFirstObjectByType<HomeUI>();

        m_ui.gameButton.onClick.AddListener(m_gameObject.GoGame);
        m_ui.titleButton.onClick.AddListener(m_gameObject.GoTitle);
    }
}

public class Home : MonoBehaviour
{
    void OnDestroy()
    {
        HomeManager.instance.Destroy();
    }

    IEnumerator Start()
    {
        yield return Fader.instance.CoFadeIn(1f);
    }

    public void GoGame()
    {
        StartCoroutine(CoGoGame());
        IEnumerator CoGoGame()
        {
            yield return Fader.instance.CoFadeOut(1f);
            GameManager.LoadScene();
        }
    }

    public void GoTitle()
    {
        StartCoroutine(CoGoTitle());
        IEnumerator CoGoTitle()
        {
            yield return Fader.instance.CoFadeOut(1f);
            TitleManager.LoadScene();
        }
    }
}