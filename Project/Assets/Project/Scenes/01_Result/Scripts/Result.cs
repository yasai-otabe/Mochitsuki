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

        // 遷移ボタン
        m_ui.nextButton.onClick.AddListener(() => m_gameObject.GoCraftsManLvUp(m_counts));
    }

    public void SetCount(int counts)
    {
        m_counts = counts;

        // もち情報を取得
        var result = DataManafer.instance.mochiData.GetDataFromMochitsukiCount(m_counts);
        var ID = result.ID;
        var mochiData = result.data;
        // 表示
        var isNew = !PlayerSaveData.MochiRelease.IsRelease(ID);
        m_ui.SetResult(mochiData.name, mochiData.description, mochiData.sprite, isNew);
        // セーブデータ更新
        if (isNew)
            PlayerSaveData.MochiRelease.Release(ID);
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

    public void GoCraftsManLvUp(int counts)
    {
        StartCoroutine(CoGoCraftsManLvUp());
        IEnumerator CoGoCraftsManLvUp()
        {
            yield return Fader.instance.CoFadeOut(0.5f);
            CraftsManLvUpManager.LoadScene(
                _ =>
                {
                    _.SetCount(counts);
                });
        }
    }
}