using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KyawaLib;
using UnityEngine.Events;

public class ResultManager : SingletonClass<ResultManager>
{
    Result m_gameObject = null;
    ResultUI m_ui = null;
    int m_counts = 0;

    public ResultUI UI => m_ui;
    public int Counts { set => m_counts = value; }

    public static void LoadScene(UnityAction<ResultManager> loadedAction = null)
    {
        SceneLoader.instance.LoadSceneAsync("Result", LoadSceneMode.Single,
           () =>
           {
               Create();
               loadedAction?.Invoke(instance);
           });
    }

    public ResultManager()
    {
        m_gameObject = new GameObject($"**Result**").AddComponent<Result>();
        m_ui = Object.FindFirstObjectByType<ResultUI>();

        m_ui.homeButton.onClick.AddListener(m_gameObject.GoHome);
    }
}

public class Result : MonoBehaviour
{
    void OnDestroy()
    {
        ResultManager.instance.Destroy();
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