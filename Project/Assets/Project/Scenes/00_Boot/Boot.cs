using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    /// <summary>
    /// 最初に開始するシーン名
    /// </summary>
    static string ms_firstSceneName = "SampleScene";

#if UNITY_EDITOR
    const int BUILD_INDEX_BOOT = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeOnLoad()
    {
        // エディタで開いているシーンを取得します
        var firstScene = SceneManager.GetActiveScene();

        if (firstScene.buildIndex != BUILD_INDEX_BOOT)
        {
            ms_firstSceneName = firstScene.name;

            // まずBootシーンから開始します
            SceneManager.LoadScene(BUILD_INDEX_BOOT);
        }
    }
#endif

    void LoadFirstScene()
    {
        // 開始シーンから開始します
        switch (ms_firstSceneName)
        {
            case "Title":
                //TitleManager.instance.LoadScene();
                break;
            case "Game":
                //GameManager.instance.LoadScene();
                break;
            case "Result":
                //ResultManager.instance.LoadScene();
                break;
            default:
                // 現在開いているシーンを開始
                SceneManager.LoadScene(ms_firstSceneName);
                return;
        }
        Fader.instance.FadeOut(0f);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Initialize();

        LoadFirstScene();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize()
    {

    }
}
