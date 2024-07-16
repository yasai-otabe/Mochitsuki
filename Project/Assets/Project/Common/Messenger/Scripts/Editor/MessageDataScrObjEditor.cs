using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MessageDataScrObj))]
public class MessageDataScrObjEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10f);

        var tgt = target as MessageDataScrObj;
        if (tgt == null)
            return;

        using (new GUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Convert", GUILayout.Width(60), GUILayout.Height(24)))
            {
                if (tgt.Convert())
                    EditorUtility.DisplayDialog("メッセージコンバート成功", "CSVからメッセージをコンバートしました", "OK");
            }
        }
    }
}
