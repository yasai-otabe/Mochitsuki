using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SePlayer))]
public class SePlayerEditor : Editor
{
    float m_fadeDuration = 0f;

    public override bool RequiresConstantRepaint() => true;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10f);

        var tgt = target as SePlayer;
        if (tgt == null)
            return;

        var player = tgt.player;
        if (player == null)
            return;

        var clipNames = tgt.Dbg_GetClipNames();
        tgt.dbg_clipIndex = EditorGUILayout.Popup("Clip Name", tgt.dbg_clipIndex, clipNames.ToArray());

        var clipName = (clipNames.Count == 0) ? string.Empty : clipNames[tgt.dbg_clipIndex];
        var info = tgt.Dbg_GetAudioClipInfo(clipName);

        GUILayout.Space(10f);

        using (new EditorGUI.DisabledGroupScope(!EditorApplication.isPlaying))
        {
            // バー
            var end = (info == null) ? 0f : info.Clip.length;
            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.Slider(player.GetTime(), 0f, end);
            }
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label($"{0.ToString("f3")}s");
                GUILayout.FlexibleSpace();
                GUILayout.Label($"{end.ToString("f3")}s");
            }

            // フェード時間
            m_fadeDuration = EditorGUILayout.FloatField("Fade Duration", m_fadeDuration);

            using (new GUILayout.HorizontalScope())
            {
                // 再生ボタン
                if (GUILayout.Button("PlayOneShot", GUILayout.Width(100), GUILayout.Height(24)))
                {
                    if (info != null)
                        tgt.PlayOneShot(clipName);
                }
                // 再生ボタン
                if (GUILayout.Button("Play", GUILayout.Width(60), GUILayout.Height(24)))
                {
                    if (info != null)
                        tgt.Play(clipName, m_fadeDuration);
                }
                // 停止ボタン
                if (GUILayout.Button("Stop", GUILayout.Width(60), GUILayout.Height(24)))
                {
                    tgt.Stop(m_fadeDuration);
                }
                // 一時停止ボタン
                if (player.isPausing)
                {
                    // 再開ボタン
                    if (GUILayout.Button("Resume", GUILayout.Width(60), GUILayout.Height(24)))
                        tgt.Resume();
                }
                else
                {
                    if (GUILayout.Button("Pause", GUILayout.Width(60), GUILayout.Height(24)))
                        tgt.Pause();
                }
            }

            //GUILayout.Space(5f);

            //// ミュート設定
            //player.onMute = EditorGUILayout.Toggle(new GUIContent("Mute"), player.onMute);

            //using (new GUILayout.HorizontalScope())
            //{
            //    var sec = EditorGUILayout.FloatField("FadeIn Sec", tgt.fadeInPlaySec);
            //    tgt.fadeInPlaySec = Mathf.Max(0, sec);
            //    GUILayout.Space(5f);
            //    // フェードイン再生ボタン
            //    if (GUILayout.Button("FadeInPlay", GUILayout.Width(100), GUILayout.Height(18)))
            //    {
            //        if (info != null)
            //            player.FadeInPlay(info.Clip, info.Volume, false, tgt.fadeInPlaySec);
            //    }
            //}

            //using (new GUILayout.HorizontalScope())
            //{
            //    var sec = EditorGUILayout.FloatField("FadeOut Sec", tgt.fadeOutStopSec);
            //    tgt.fadeOutStopSec = Mathf.Max(0, sec);
            //    GUILayout.Space(5f);
            //    // フェードアウト停止ボタン
            //    if (GUILayout.Button("FadeOutStop", GUILayout.Width(100), GUILayout.Height(18)))
            //    {
            //        if (info != null)
            //            player.FadeOutStop(tgt.fadeOutStopSec);
            //    }
            //}
        }
    }
}
