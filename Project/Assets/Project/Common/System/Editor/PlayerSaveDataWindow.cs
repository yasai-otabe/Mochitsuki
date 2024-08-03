using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlayerSaveDataWindow : EditorWindow
{
    int m_mochitsukiCount = 0;
    List<(string name, bool isUnlocked)> m_mochiReleases = new();

    /// <summary>
    /// ウィンドウ作成
    /// </summary>
    [MenuItem("Mochitsuki/セーブデータ管理")]
    static public void Create()
    {
        var window = GetWindow<PlayerSaveDataWindow>();
        window.titleContent = new GUIContent("セーブデータ管理");
        window.minSize.Set(400, 200);
        window.maxSize.Set(800, 400);
        window.Show();
    }

    void OnEnable()
    {
        Init();
    }

    void OnGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("セーブデータをリセット", GUILayout.Width(150)))
            {
                if (EditorUtility.DisplayDialog("セーブデータをリセット", "全てのセーブデータを初期値にリセットしてもよろしいですか？", "OK", "Cancel"))
                {
                    PlayerSaveData.ResetAll();
                    Init();
                }
            }
            if (GUILayout.Button("セーブデータを上書き", GUILayout.Width(150)))
            {
                if (EditorUtility.DisplayDialog("セーブデータに反映", "現在の状態でセーブデータを上書きしてもよろしいですか？", "OK", "Cancel"))
                {
                    PlayerSaveData.MochitsukiCount.SetAndSaveMochitsukiCount(m_mochitsukiCount);
                    for (var i = 0; i < m_mochiReleases.Count; i++)
                    {
                        PlayerSaveData.MochiRelease.SetOnRelease(i + 1, m_mochiReleases[i].isUnlocked);
                    }
                }
            }
        }

        GUILayout.Space(10);

        m_mochitsukiCount = EditorGUILayout.IntField("総もちつき回数", m_mochitsukiCount, GUILayout.Width(210));

        GUILayout.Space(10);

        GUILayout.Label("もち解放：");
        for (var i = 0; i < m_mochiReleases.Count; i++)
        {
            var data = m_mochiReleases[i];
            data.isUnlocked = EditorGUILayout.Toggle("　　" + data.name, data.isUnlocked);
            m_mochiReleases[i] = data;
        }
    }

    void Init()
    {
        PlayerSaveData.LoadAll();

        m_mochitsukiCount = PlayerSaveData.MochitsukiCount.value;

        m_mochiReleases.Clear();
        var mochiData = AssetDatabase.LoadAssetAtPath<MochiData>("Assets/Project/Data/MochiData.asset");
        if (mochiData != null)
        {
            for (var id = 1; id <= mochiData.DataCount; id++)
            {
                var name = mochiData.GetDataFromID(id).name;
                var isUnlocked = PlayerSaveData.MochiRelease.IsRelease(id);
                m_mochiReleases.Add(new(name, isUnlocked));
            }
        }
    }
}