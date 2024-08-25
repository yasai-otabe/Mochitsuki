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

    [SerializeField]
    private GameObject m_Kine1P;
    private float m_1PTargetAngle = 0.0f;
    private float m_1PCurrentAngle;

    [SerializeField]
    private GameObject m_Kine2P;
    private float m_2PTargetAngle = 0.0f;
    private float m_RotationSpeed = 20.0f;
    private float m_2PCurrentAngle;

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
        m_2PCurrentAngle = m_Kine2P.transform.position.z;
        m_1PCurrentAngle = m_Kine1P.transform.position.z;
    }

    private void Update()
    {
       
        m_1PCurrentAngle = Mathf.LerpAngle(m_1PCurrentAngle, m_1PTargetAngle, m_RotationSpeed * Time.deltaTime);
        m_2PCurrentAngle = Mathf.LerpAngle(m_2PCurrentAngle, m_2PTargetAngle, m_RotationSpeed * Time.deltaTime);
        m_Kine1P.transform.rotation = Quaternion.Euler(m_Kine1P.transform.rotation.x, m_Kine1P.transform.rotation.y, m_1PCurrentAngle);
        m_Kine2P.transform.rotation = Quaternion.Euler(m_Kine2P.transform.rotation.x, m_Kine2P.transform.rotation.y, m_2PCurrentAngle);
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
                    //m_CoolTimer.SetTimer(m_OperationCoolTime);
                    var Color1P = GameManager.instance.GetUI().UIGuide1P.color;
                    var Color2P = GameManager.instance.GetUI().UIGuide2P.color;
                    Color1P.a = 0.0f;
                    Color2P.a = 1.0f;
                    GameManager.instance.GetUI().UIGuide1P.color = Color1P;
                    GameManager.instance.GetUI().UIGuide2P.color = Color2P;
                    m_1PTargetAngle = -38.0f;
                    m_2PTargetAngle = -40.0f;
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
                    //m_CoolTimer.SetTimer(m_OperationCoolTime);
                    var Color1P = GameManager.instance.GetUI().UIGuide1P.color;
                    var Color2P = GameManager.instance.GetUI().UIGuide2P.color;
                    Color1P.a = 0.0f;
                    Color2P.a = 1.0f;
                    GameManager.instance.GetUI().UIGuide1P.color = Color1P;
                    GameManager.instance.GetUI().UIGuide2P.color = Color2P;
                    GameManager.instance.GetUI().TextMeshProTutorial.alpha = 0.0f;
                    m_1PTargetAngle = -38.0f;
                    m_2PTargetAngle = -40.0f;
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
                    //m_CoolTimer.SetTimer(m_OperationCoolTime);
                    var Color1P = GameManager.instance.GetUI().UIGuide1P.color;
                    var Color2P = GameManager.instance.GetUI().UIGuide2P.color;
                    Color1P.a = 1.0f;
                    Color2P.a = 0.0f;
                    GameManager.instance.GetUI().UIGuide1P.color = Color1P;
                    GameManager.instance.GetUI().UIGuide2P.color = Color2P;
                    m_1PTargetAngle = 40.0f;
                    m_2PTargetAngle = 38.0f;
                    break;
                }
                case Mochitsuki.STAY:
                {
                    m_eMochitsuki = Mochitsuki.RIGHT;
                    //m_CoolTimer.SetTimer(m_OperationCoolTime);
                    var Color1P = GameManager.instance.GetUI().UIGuide1P.color;
                    var Color2P = GameManager.instance.GetUI().UIGuide2P.color;
                    Color1P.a = 1.0f;
                    Color2P.a = 0.0f;
                    GameManager.instance.GetUI().UIGuide1P.color = Color1P;
                    GameManager.instance.GetUI().UIGuide2P.color = Color2P;
                    GameManager.instance.GetUI().TextMeshProTutorial.alpha = 0.0f;
                    m_1PTargetAngle = 40.0f;
                    m_2PTargetAngle = 38.0f;
                    break;
                }
            }
        }
    }
}
