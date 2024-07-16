using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace KyawaLib
{
    /// <summary>
    /// UnityEngine.SceneManagementのラッピング
    /// </summary>
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        /// <summary>
        /// シーンをロード
        /// </summary>
        /// <param name="name">シーン名</param>
        /// <param name="mode">シーンモード</param>
        public void LoadScene(string name, LoadSceneMode mode)
            => SceneManager.LoadScene(name, mode);

        /// <summary>
        /// シーンをロード
        /// </summary>
        /// <param name="name">シーン名</param>
        /// <param name="mode">シーンモード</param>
        /// <param name="onLoadedAction">シーンロード後のアクション</param>
        public void LoadSceneAsync(string name, LoadSceneMode mode, UnityAction onLoadedAction)
            => StartCoroutine(CoLoadScene(name, mode, onLoadedAction));

        /// <summary>
        /// シーンをアンロード
        /// </summary>
        /// <param name="name">シーン名</param>
        /// <param name="onUnloadedAction">シーンアンロード後のアクション</param>
        /// <param name="releaseUnusedResouces">未使用リソースをアンロードするか</param>
        public void UnloadSceneAsync(string name, UnityAction onUnloadedAction, bool releaseUnusedResouces = false)
            => StartCoroutine(CoUnloadScene(name, onUnloadedAction, releaseUnusedResouces));

        /// <summary>
        /// シーンをロード
        /// </summary>
        /// <param name="name">シーン名</param>
        /// <param name="mode">シーンモード</param>
        /// <param name="onLoadedAction">シーンロード後のアクション</param>
        public IEnumerator CoLoadScene(string name, LoadSceneMode mode, UnityAction onLoadedAction)
        {
            yield return SceneManager.LoadSceneAsync(name, mode);
            yield return null; // 1フレーム待つ
            KyDebug.Log($"{name} scene loaded.");
            onLoadedAction?.Invoke();
        }

        /// <summary>
        /// シーンをアンロード
        /// </summary>
        /// <param name="name">シーン名</param>
        /// <param name="onUnloadedAction">シーンアンロード後のアクション</param>
        /// <param name="releaseUnusedResouces">未使用リソースをアンロードするか</param>
        public IEnumerator CoUnloadScene(string name, UnityAction onUnloadedAction, bool releaseUnusedResouces = false)
        {
            var scene = SceneManager.GetSceneByName(name);
            yield return SceneManager.UnloadSceneAsync(scene);
            if (releaseUnusedResouces)
                yield return Resources.UnloadUnusedAssets();
            KyDebug.Log($"{scene.name} scene unloaded.");
            onUnloadedAction?.Invoke();
        }
    }
}