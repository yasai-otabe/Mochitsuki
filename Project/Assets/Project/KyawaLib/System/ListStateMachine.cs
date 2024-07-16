using System.Diagnostics;

namespace KyawaLib
{
    /// <summary>
    /// リストのステート管理
    /// </summary>
    public class ListStateMachine
    {
        /// <summary>
        /// リストの項目数
        /// </summary>
        public int ItemCount { private get; set; } = 0;

        /// <summary>
        /// 現在選択している項目のインデックスを
        /// </summary>
        public int SelectIndex { private get; set; } = 0;

        bool m_loop = false;

        [Conditional("UNITY_EDITOR")]
        void Check()
        {
            UnityEngine.Assertions.Assert.IsTrue(0 < ItemCount);
            UnityEngine.Assertions.Assert.IsTrue(0 <= SelectIndex && SelectIndex < ItemCount);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="itemCount">リストの項目数</param>
        /// <param name="selectIndex">現在選択している項目のインデックス</param>
        /// <param name="loop">項目をそれ以上移動できなくなったとき（最初や最後）ループするか</param>
        public ListStateMachine(int itemCount, int selectIndex, bool loop = false)
        {
            ItemCount = itemCount;
            SelectIndex = selectIndex;
            m_loop = loop;
            Check();
        }

        /// <summary>
        /// 次の項目へ移動
        /// </summary>
        public bool Next()
        {
            Check();
            SelectIndex++;
            if (ItemCount <= SelectIndex)
            {
                if (m_loop)
                {
                    SelectIndex = 0;
                }
                else
                {
                    SelectIndex = ItemCount - 1;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 前の項目へ移動
        /// </summary>
        public bool Prev()
        {
            Check();
            SelectIndex--;
            if (SelectIndex < 0)
            {
                if (m_loop)
                {
                    SelectIndex = ItemCount - 1;
                }
                else
                {
                    SelectIndex = 0;
                    return false;
                }
            }
            return true;
        }
    }
}