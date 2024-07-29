using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KyawaLib;
using UnityEngine.Events;
using DG.Tweening;

public class CraftsManLvUpManager : SingletonClass<CraftsManLvUpManager>
{
    CraftsManLvUp m_gameObject = null;
    CraftsManLvUpUI m_ui = null;
    int m_totalCounts = 0;
    int m_counts = 0;

    public CraftsManLvUpUI UI => m_ui;

    public static void LoadScene(UnityAction<CraftsManLvUpManager> loadedAction = null)
    {
        SceneLoader.instance.LoadSceneAsync("CraftsManLvUp", LoadSceneMode.Single,
           () =>
           {
               Create();
               loadedAction?.Invoke(instance);
           });
    }

    public CraftsManLvUpManager()
    {
        m_gameObject = new GameObject($"**CraftsManLvUp**").AddComponent<CraftsManLvUp>();
        m_ui = Object.FindFirstObjectByType<CraftsManLvUpUI>();

        m_gameObject.ui = m_ui;

        m_ui.nextButton.onClick.AddListener(() => m_gameObject.GoHome());
        m_ui.nextButton.gameObject.SetActive(false);
    }

    public void SetCount(int counts)
    {
        m_counts = counts;
        m_totalCounts = PlayerSaveData.MochitsukiCount.value;
        PlayerSaveData.MochitsukiCount.AddAndSaveMochitsukiCount(m_counts);

        var craftsManName = DataManafer.instance.craftsManData.GetCraftsManName(m_totalCounts);
        m_ui.SetCraftsManName(craftsManName);

        m_ui.Init(m_totalCounts, m_counts);
    }
}

public class CraftsManLvUp : MonoBehaviour
{
    Tweener m_countUpTweener = null;

    CraftsManLvUpUI m_ui = null;
    public CraftsManLvUpUI ui { set => m_ui = value; }

    void OnDestroy()
    {
        CraftsManLvUpManager.instance.Destroy();
    }

    IEnumerator Start()
    {
        yield return Fader.instance.CoFadeIn(0.5f);

        m_countUpTweener = m_ui.GetCountUpTweener();
        yield return m_countUpTweener.WaitForKill();

        m_ui.nextButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            m_countUpTweener?.Kill(true);
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