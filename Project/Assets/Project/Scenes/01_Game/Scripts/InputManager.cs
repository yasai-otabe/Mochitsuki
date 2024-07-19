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

    private Mochitsuki m_eMochitsuki = Mochitsuki.STAY;

    public Mochitsuki GetMochitsukiType()
    {
        return m_eMochitsuki;
    }

    private void Update()
    {
        GetInput();
    }

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
