using UnityEngine;

namespace KyawaLib
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        static T ms_instance;

        /// <summary>
        /// インスタンス取得
        /// </summary>
        static public T instance => ms_instance;

        void Awake()
        {
            if (ms_instance)
            {
                KyDebug.LogWarning($"{GetType().Name} is already exists.");
                Destroy(gameObject);
            }
            else
            {
                ms_instance = this as T;
                Init();
            }
        }

        void OnDestroy()
        {
            Destroy();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// 終了処理
        /// </summary>
        protected virtual void Final() { }

        /// <summary>
        /// インスタンス生成
        /// </summary>
        static public void Create()
        {
            if (ms_instance is null)
            {
                var obj = new GameObject();
                var comp = obj.AddComponent<T>();
                obj.name = comp.GetType().Name;
            }
        }

        /// <summary>
        /// インスタンス生成
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="parent">親</param>
        static public void Create(GameObject obj = null, Transform parent = null)
        {
            if (ms_instance is null)
            {
                if (obj is null)
                    Create();
                else
                    obj.AddComponent<T>();

                if (parent)
                    obj.transform.parent = parent;
            }
        }

        /// <summary>
        /// インスタンス削除
        /// </summary>
        public void Destroy()
        {
            if (ms_instance is not null)
            {
                ms_instance = null;
                Destroy(gameObject);
            }
        }
    }
}
