using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机状态类型枚举
/// </summary>
public enum StateType
{
    Ground,
    InAir,
    TouchingWall,
    Ability,
    Global,
    Climb,
}

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
    /// 状态机状态类型
    /// </summary>
    public StateType stateType { get; protected set; }

    /// <summary>
    /// 限制水平方向输入
    /// </summary>
    protected int xInput;
    /// <summary>
    /// 限制竖直方向输入
    /// </summary>
    protected int yInput;
    /// <summary>
    /// 跳跃输入
    /// </summary>
    protected bool jumpInput;
    /// <summary>
    /// 抓墙输入
    /// </summary>
    protected bool grabInput;
    /// <summary>
    /// 翻滚输入
    /// </summary>
    protected bool scrollInput;
    /// <summary>
    /// 冲刺输入
    /// </summary>
    protected bool dashInput;

    /// <summary>
    /// 是否上升过程中
    /// </summary>
    protected bool jumpInputStop;

    /// <summary>
    /// 动画是否播放完成
    /// </summary>
    protected bool isAnimationFinish;
    /// <summary>
    /// 进入状态时的时间
    /// </summary>
    protected float stateEnterTime;
    /// <summary>
    /// 进入状态的位置
    /// </summary>
    protected Vector3 enterPos;

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
        //射线检测
        OnCheck();
        //记录进入状态时的时间
        stateEnterTime = Time.time;
        //设置播放动画
        player.animator.SetBool(animBoolName, true);
        //结束动画播放
        isAnimationFinish = false;
        //记录进入状态的位置
        enterPos = player.transform.position;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public virtual void LogicUpdate()
    {
        //限制水平输入
        xInput = player.inputHandler.NormalInputX;
        //限制竖直输入
        yInput = player.inputHandler.NormalInputY;
        //获取跳跃输入
        jumpInput = player.inputHandler.JumpInput;
        //获取根据时间控制跳跃输入的开关
        jumpInputStop = player.inputHandler.JumpInputStop;
        //获取抓墙输入
        grabInput = player.inputHandler.GrabInput;
        //获取翻滚输入
        scrollInput = player.inputHandler.ScrollInput;
        //获取冲刺输入
        dashInput = player.inputHandler.DashInput;
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

    /// <summary>
    /// 动画事件
    /// </summary>
    public virtual void OnAnimationTrigger()
    {
    
    }

    /// <summary>
    ///动画结束 
    /// </summary>
    public virtual void OnAnimationFinish()
    {
        isAnimationFinish = true;
    }
}
