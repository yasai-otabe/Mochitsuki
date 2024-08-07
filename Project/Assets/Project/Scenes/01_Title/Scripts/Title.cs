using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KyawaLib;
using UnityEngine.Events;

public class TitleManager : SingletonClass<TitleManager>
{
    Title m_gameObject = null;
    TitleUI m_ui = null;

    public TitleUI UI => m_ui;

    public static void LoadScene(UnityAction<TitleManager> loadedAction = null)
    {
        SceneLoader.instance.LoadSceneAsync("Title", LoadSceneMode.Single,
           () =>
           {
               Create();
               loadedAction?.Invoke(instance);
           });
    }

    public TitleManager()
    {
        m_gameObject = new GameObject("**Title**").AddComponent<Title>();
        m_ui = Object.FindFirstObjectByType<TitleUI>();

        m_ui.homeButton.onClick.AddListener(m_gameObject.GoHome);
    }
}

public class Title : MonoBehaviour
{
    void OnDestroy()
    {
        TitleManager.instance.Destroy();
    }

    IEnumerator Start()
    {
        yield return Fader.instance.CoFadeIn(1f);
    }

    public void GoHome()
    {
        StartCoroutine(CoGoHome());
        IEnumerator CoGoHome()
        {
            yield return Fader.instance.CoFadeOut(1f);
            HomeManager.LoadScene();
        }
    }
}