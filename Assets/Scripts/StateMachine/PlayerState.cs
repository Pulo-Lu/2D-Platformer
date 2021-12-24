using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态基类脚本
/// </summary>
public class PlayerState
{
    /// <summary>
    /// 玩家脚本
    /// </summary>
    protected Player player;
    /// <summary>
    /// 玩家数据脚本
    /// </summary>
    protected PlayerData playerData;
    /// <summary>
    /// 状态机
    /// </summary>
    protected StateMachine stateMachine;
    /// <summary>
    /// 动画切换名
    /// </summary>
    protected string animBoolName;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player"></param>
    /// <param name="playerData"></param>
    /// <param name="stateMachine"></param>
    /// <param name="animBoolName"></param>
    public PlayerState(Player player, PlayerData playerData,StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.playerData = playerData;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public virtual void LogicUpdate()
    {

    }

    /// <summary>
    /// 物理系统更新
    /// </summary>
    public virtual void PhysicsUpdate()
    {
        OnCheck();
    }

    /// <summary>
    /// 退出状态
    /// </summary>
    public virtual void Exit()
    {

    }

    /// <summary>
    /// 射线检测
    /// </summary>
    public virtual void OnCheck()
    {

    }
}
