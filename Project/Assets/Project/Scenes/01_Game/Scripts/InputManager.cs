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

    // 杵の状態
    private Mochitsuki m_eMochitsuki = Mochitsuki.STAY;

    /// <summary>
    /// 杵の状態を取得
    /// </summary>
    /// <returns>杵の状態</returns>
    public Mochitsuki GetMochitsukiType()
    {
        return m_eMochitsuki;
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
        if (GameManager.instance != null)
        {
            // 入力が許可されているか
            if (GameManager.instance.IsInputEnbale())
            {
                // 右の杵を振る
                if (Input.GetKeyDown(KeyCode.A))
                {
                    switch (m_eMochitsuki)
                    {
                        case Mochitsuki.RIGHT:
                        {
                            m_eMochitsuki = Mochitsuki.LEFT;
                            break;
                        }
                        case Mochitsuki.LEFT:
                        {
                            // 硬直処理
                            break;
                        }
                        case Mochitsuki.STAY:
                        {
                            m_eMochitsuki = Mochitsuki.LEFT;
                            break;
                        }
                    }
                }

                // 左の杵を振る
                if (Input.GetKeyDown(KeyCode.L))
                {
                    switch (m_eMochitsuki)
                    {
                        case Mochitsuki.RIGHT:
                        {
                            // 硬直処理
                            break;
                        }
                        case Mochitsuki.LEFT:
                        {
                            m_eMochitsuki = Mochitsuki.LEFT;
                            break;
                        }
                        case Mochitsuki.STAY:
                        {
                            m_eMochitsuki = Mochitsuki.LEFT;
                            break;
                        }
                    }
                }
            }
        }
    }
}
