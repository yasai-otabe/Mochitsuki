using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KyawaLib;
using UnityEngine.Events;

public class GameStartEndManager : SingletonClass<GameStartEndManager>
{
    GameStartEnd m_gameObject = null;
    GameStartEndUI m_ui = null;

    public GameStartEndUI UI => m_ui;

    public static void LoadScene(UnityAction<GameStartEndManager> loadedAction = null)
    {
        SceneLoader.instance.LoadSceneAsync("GameStartEnd", LoadSceneMode.Additive,
           () =>
           {
               Create();
               loadedAction?.Invoke(instance);
           });
    }

    public GameStartEndManager()
    {
        m_gameObject = new GameObject($"**GameStartEnd**").AddComponent<GameStartEnd>();
        m_ui = Object.FindFirstObjectByType<GameStartEndUI>();

        SceneManager.MoveGameObjectToScene(m_gameObject.gameObject, SceneManager.GetSceneByName("GameStartEnd"));
    }
}

public class GameStartEnd : MonoBehaviour
{
    void OnDestroy()
    {
        GameStartEndManager.instance.Destroy();
    }
}