using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KyawaLib
{
    /// <summary>
    /// 編集不可のパラメータ表示
    /// [SerializeField, ReadOnly] と書く
    /// </summary>
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUI.PropertyField(_position, _property, _label);
            }
        }
    }
#endif
}
