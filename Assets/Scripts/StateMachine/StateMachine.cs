using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机脚本
/// </summary>
public class StateMachine
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public PlayerState CurrentState { get; private set; }

    /// <summary>
    /// 上个状态
    /// </summary>
    public PlayerState LastState { get; private set; }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="InitState"></param>
    public void Init(PlayerState InitState)
    {
        //设置当前状态
        CurrentState = InitState;
        //进入当前状态
        CurrentState.Enter();
    }

    /// <summary>
    /// 更改状态
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeState(PlayerState nextState)
    {
        //退出状态
        CurrentState.Exit();
        //将当前记录为上个状态
        LastState = CurrentState;
        //将当前状态设置为下一状态
        CurrentState = nextState;
        //进入当前状态
        CurrentState.Enter();
    }
}
