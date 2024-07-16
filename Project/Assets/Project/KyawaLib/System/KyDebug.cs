using System.Diagnostics;

namespace KyawaLib
{
    /// <summary>
    /// UnityEngine.Debugのラッピング
    /// </summary>
    public static class KyDebug
    {
        /// <summary>
        /// Unityエディタ上でのみログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o)
        {
            UnityEngine.Debug.Log(o);
        }

        /// <summary>
        /// Unityエディタ上でのみ警告ログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object o)
        {
            UnityEngine.Debug.LogWarning(o);
        }

        /// <summary>
        /// Unityエディタ上でのみエラーログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void LogError(object o)
        {
            UnityEngine.Debug.LogError(o);
        }

        /// <summary>
        /// Unityエディタ上でのみ、オブジェクトがNullではない場合アサートログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void AssertIsNull(object o, string ms = null)
        {
            UnityEngine.Assertions.Assert.IsNull(o, ms);
        }

        /// <summary>
        /// Unityエディタ上でのみ、オブジェクトがNullの場合アサートログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void AssertIsNotNull(object o, string ms = null)
        {
            UnityEngine.Assertions.Assert.IsNotNull(o, ms);
        }

        /// <summary>
        /// Unityエディタ上でのみ、条件がTrueではない場合アサートログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void AssertIsTrue(bool condition, string ms = null)
        {
            UnityEngine.Assertions.Assert.IsTrue(condition, ms);
        }

        /// <summary>
        /// Unityエディタ上でのみ、条件がFalseではない場合アサートログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void AssertIsFalse(bool condition, string ms = null)
        {
            UnityEngine.Assertions.Assert.IsFalse(condition, ms);
        }

        /// <summary>
        /// Unityエディタ上でのみ、indexが配列の総数を超えていた場合アサートログを表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void AssertIsWithinRange(int arrayCount, int index, string ms = null)
        {
            var condition = (0 <= index && index < arrayCount) ? true : false;
            UnityEngine.Assertions.Assert.IsTrue(condition, ms);
        }
    }
}