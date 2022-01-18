using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家处在地面时的父类状态
/// </summary>
public class PlayerGroundState : PlayerState
{
    /// <summary>
    /// 左脚是否落地
    /// </summary>
    protected bool isLeftFootGround;
    /// <summary>
    /// 右脚是否落地
    /// </summary>
    protected bool isRightFootGround;
    /// <summary>
    /// 是否一只脚落地
    /// </summary>
    protected bool isSingleFootGround;
    /// <summary>
    /// 是否为地面
    /// </summary>
    protected bool isGround;
    /// <summary>
    /// 是否接触到墙
    /// </summary>
    protected bool isTouchingWall;
    /// <summary>
    /// 蹲下检测头顶是否接触到墙
    /// </summary>
    protected bool isTouchingCeiling;
    /// <summary>
    /// 是否检测到墙角
    /// </summary>
    protected bool isTouchingLedge;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerGroundState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
        stateType = StateType.Ground;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        //上一个状态不为空 且 上一个状态不为地面的父类 且 上一个状态不为冲刺状态
        if (player.stateMachine.LastState != null && player.stateMachine.LastState.stateType != StateType.Ground 
            && player.stateMachine.LastState != player.DashState)
        {
            //自动刷新冷却时间
            player.DashState.RefreshCoolTime();
        }
        //重置跳跃次数
        player.JumpState.ResetJumpCount();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //有冲刺输入 且 没有接触墙 且 头顶没有墙 且 可以冲刺
        if (dashInput && !isTouchingWall && !isTouchingCeiling && player.DashState.CanDash()) 
        {
            //设置速度为零
            player.SetVelocityZero();
            //有水平输入 且 竖直输入不为-1 即 S || 竖直输入为1 即 W
            if (xInput != 0 && yInput != -1 || yInput == 1)
            {
                //设置冲刺方向
                player.DashState.SetDashDirection(new Vector2Int(xInput, yInput));
            }
            else
            {
                //设置冲刺方向
                player.DashState.SetDashDirection(new Vector2Int(player.FaceDir, 0));
            }
            //切换到冲刺状态
            stateMachine.ChangeState(player.DashState);
        }
        //不接触墙 且 头顶没有墙 且 有翻滚输入 且 在地面
        else if (!isTouchingWall && !isTouchingCeiling && scrollInput && isGround) 
        {
            //切换到翻滚状态
            stateMachine.ChangeState(player.ScrollState);
        }
        //有抓墙输入 且 接触到墙 且 检测到墙角
        else if (grabInput && isTouchingWall && isTouchingLedge)
        {
            //切换到抓着墙状态
            stateMachine.ChangeState(player.WallGrabState);
        }
        //有跳跃输入 且 跳跃次数不为0 且 头顶上没有墙
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            //切换到跳跃状态
            stateMachine.ChangeState(player.JumpState);
        }
        //不在地面上 且 头顶上没有墙
        else if (!isGround)
        {
            //设置可以延迟跳跃
            player.InAirState.SetCanJumpDelay();
            //切换到空中状态
            stateMachine.ChangeState(player.InAirState);
        }
    }

    /// <summary>
    /// 射线检测
    /// </summary>
    public override void OnCheck()
    {
        base.OnCheck();
        //左脚是否落地
        isLeftFootGround = player.CheckLeftFootIsGround();
        //右脚是否落地
        isRightFootGround = player.CheckRightFootIsGround();
        //是否为地面
        isGround = isLeftFootGround || isRightFootGround;
        //是否一只脚落地
        isSingleFootGround = isLeftFootGround && !isRightFootGround || !isLeftFootGround && isRightFootGround;
        //是否接触到墙
        isTouchingWall = player.CheckIsTouchWall();
        //蹲下检测头顶是否接触到墙
        isTouchingCeiling = player.CheckIsTouchCeiling();
        //检测是否接触到墙角
        isTouchingLedge = player.CheckIsTouchLedge();

    }
}
