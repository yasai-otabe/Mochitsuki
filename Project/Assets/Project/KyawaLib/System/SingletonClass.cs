namespace KyawaLib
{
    public class SingletonClass<T> where T : new()
    {
        static T ms_instance;

        /// <summary>
        /// インスタンス取得
        /// </summary>
        static public T instance
            => ms_instance ??= new T();

        /// <summary>
        /// インスタンス削除
        /// </summary>
        public void Destroy()
        {
            if (ms_instance is not null)
            {
                OnDestroy();
                ms_instance = default;
            }
        }

        /// <summary>
        /// Destroy時に呼び出されます.
        /// </summary>
        protected virtual void OnDestroy()
        {

        }
    }
}