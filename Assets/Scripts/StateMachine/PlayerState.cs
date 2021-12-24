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
    /// 水平方向输入
    /// </summary>
    protected int xInput;
    /// <summary>
    /// 竖直方向输入
    /// </summary>
    protected int yInput;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
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
        //设置播放动画
        player.animator.SetBool(animBoolName, true);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public virtual void LogicUpdate()
    {
        //获取水平输入
        xInput = player.inputHandler.NormalInputX;
        //获取竖直输入
        yInput = player.inputHandler.NormalInputY;
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
        //设置停止播放动画
        player.animator.SetBool(animBoolName, false);
    }

    /// <summary>
    /// 射线检测
    /// </summary>
    public virtual void OnCheck()
    {

    }
}
