using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum Mochitsuki
    {
        RIGHT,
        LEFT,
        STAY,
    }

    [SerializeField]
    private float m_OperationCoolTime;

    [SerializeField]
    private float m_PenaltyCoolTime;

    // 杵の状態
    private Mochitsuki m_eMochitsuki = Mochitsuki.STAY;
    private Timer m_CoolTimer = null;

    /// <summary>
    /// 杵の状態を取得
    /// </summary>
    /// <returns>杵の状態</returns>
    public Mochitsuki GetMochitsukiType()
    {
        return m_eMochitsuki;
    }

    private void Start()
    {
        m_CoolTimer = new Timer();
    }

    private void Update()
    {
        GetInput();
    }

    /// <summary>
    /// 入力を行う
    /// </summary>
    private void GetInput()
    {
        // ゲームマネージャーNULLチェック
        if (GameManager.instance == null)   { return; }

        // 入力が許可されているか
        if (!(GameManager.instance.IsInputEnbale()))    { return; }

        // クールタイムが終了しているか
        if (!(m_CoolTimer.UpdateTimer(Time.deltaTime)))
        {
            GameManager.instance.GetUI().TextMeshProPenalty.text = "CoolTime " + m_CoolTimer.GetRemainTime().ToString("f1") + "s";
            return;
        }

        // 右の杵を振る
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.instance.AddInputCount();

            switch (m_eMochitsuki)
            {
                case Mochitsuki.RIGHT:
                {
                    m_eMochitsuki = Mochitsuki.LEFT;
                    m_CoolTimer.SetTimer(m_OperationCoolTime);
                    GameManager.instance.GetUI().TextMeshProA.alpha = 0.0f;
                    GameManager.instance.GetUI().TextMeshProL.alpha = 1.0f;
                    break;
                }
                case Mochitsuki.LEFT:
                {
                    // 硬直処理
                    m_CoolTimer.SetTimer(m_PenaltyCoolTime);
                    break;
                }
                case Mochitsuki.STAY:
                {
                    m_eMochitsuki = Mochitsuki.LEFT;
                    m_CoolTimer.SetTimer(m_OperationCoolTime);
                    GameManager.instance.GetUI().TextMeshProA.alpha = 0.0f;
                    GameManager.instance.GetUI().TextMeshProL.alpha = 1.0f;
                    GameManager.instance.GetUI().TextMeshProTutorial.alpha = 0.0f;
                    break;
                }
            }
        }

        // 左の杵を振る
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.instance.AddInputCount();

            switch (m_eMochitsuki)
            {
                case Mochitsuki.RIGHT:
                {
                    // 硬直処理
                    m_CoolTimer.SetTimer(m_PenaltyCoolTime);
                    break;
                }
                case Mochitsuki.LEFT:
                {
                    m_eMochitsuki = Mochitsuki.RIGHT;
                    m_CoolTimer.SetTimer(m_OperationCoolTime);
                    GameManager.instance.GetUI().TextMeshProL.alpha = 0.0f;
                    GameManager.instance.GetUI().TextMeshProA.alpha = 1.0f;
                    break;
                }
                case Mochitsuki.STAY:
                {
                    m_eMochitsuki = Mochitsuki.RIGHT;
                    m_CoolTimer.SetTimer(m_OperationCoolTime);
                    GameManager.instance.GetUI().TextMeshProL.alpha = 0.0f;
                    GameManager.instance.GetUI().TextMeshProA.alpha = 1.0f;
                    GameManager.instance.GetUI().TextMeshProTutorial.alpha = 0.0f;
                    break;
                }
            }
        }
    }
}
