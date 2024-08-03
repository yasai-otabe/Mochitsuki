using UnityEngine;

public class GameStartEndUI : MonoBehaviour
{
    [SerializeField]
    PixelArtGameTextController m_text = null;

    Animator m_animator = null;

    bool m_onFinishAnimation = false;
    public bool onFinishAnimation => m_onFinishAnimation;

    readonly int m_animHash_Enter = Animator.StringToHash("Enter");
    readonly int m_animHash_Exit = Animator.StringToHash("Exit");

    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Enter()
    {
        m_text.SetText("開始");
        m_onFinishAnimation = false;
        m_animator.SetTrigger(m_animHash_Enter);
    }

    public void Exit()
    {
        m_text.SetText("終了");
        m_onFinishAnimation = false;
        m_animator.SetTrigger(m_animHash_Exit);
    }

    /// <summary>
    /// Animationからの呼び出し
    /// </summary>
    public void FinishAnimation()
        => m_onFinishAnimation = true;
}
