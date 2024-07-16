using UnityEngine.Events;

namespace KyawaLib
{
    public class StateMachine
    {
        /// <summary>
        /// ステート変更時のコールバック
        /// </summary>
        public UnityAction<int> ChangeStateAction { private get; set; } = null;

        /// <summary>
        /// 現在のステート
        /// </summary>
        public int CurrentStateIndex { private set; get; } = 0;

        /// <summary>
        /// 以前のステート
        /// </summary>
        /// <returns></returns>
        public int PrevStateIndex { private set; get; } = 0;

        /// <summary>
        /// ステートをリセット
        /// </summary>
        public void Reset()
        {
            CurrentStateIndex = 0;
            PrevStateIndex = 0;
            ChangeStateAction = null;
        }

        /// <summary>
        /// ステートを変更する
        /// </summary>
        /// <param name="stateIndex"></param>
        public void ChangeState(int stateIndex)
        {
            if (stateIndex == CurrentStateIndex)
            {
                RedoCurrentState();
            }
            else
            {
                PrevStateIndex = CurrentStateIndex;
                CurrentStateIndex = stateIndex;
                InvokeAction();
            }
        }

        /// <summary>
        /// 現在のステートをやり直す
        /// </summary>
        public void RedoCurrentState()
        {
            InvokeAction();
        }

        /// <summary>
        /// 前回のステートに戻る
        /// </summary>
        public void ReturnPrevState()
        {
            int tmp = PrevStateIndex;
            PrevStateIndex = CurrentStateIndex;
            CurrentStateIndex = tmp;
            InvokeAction();
        }

        void InvokeAction()
        {
            ChangeStateAction?.Invoke(CurrentStateIndex);
        }
    }
}