using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_PlayerPhaseState : MonoBehaviour
{
    public enum State
    {
        Init,
        Liquid, // 水
        Gas,    // 気体
        Solid,  // 固体
        Slime,  // スライム
    }

    private State m_state;

    public MM_PlayerPhaseState()
    {
        m_state = State.Init;
    }
        
    public void ChangeState(MM_PlayerPhaseState.State state)
    {
        m_state=state;
    }

    public State GetState()
    {
        return m_state;
    }
}
