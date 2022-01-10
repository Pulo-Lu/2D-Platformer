using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家处在墙面时的父类状态
/// </summary>
public class PlayerTouchingWallState : PlayerState
{
    /// <summary>
    /// 是否为地面
    /// </summary>
    protected bool isGround;
    /// <summary>
    /// 是否接触墙面
    /// </summary>
    protected bool isTouchingWall;
    /// <summary>
    /// 是否接触墙角
    /// </summary>
    protected bool isTouchingLedge;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerTouchingWallState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //接触墙面 且 有跳跃输入 且 水平方向输入与玩家朝向一致
        if (isTouchingWall && jumpInput && xInput == player.FaceDir)
        {
            //切换到玩家单面墙反墙跳状态
            stateMachine.ChangeState(player.WallJumpState);
        }
        //接触墙面 且 不接触墙角 且 不在地面
        else if (isTouchingWall && !isTouchingLedge && !isGround)
        {
            //切换到玩家在墙角的状态
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        //没有抓墙输入 且 接触地面
        else if (!grabInput && isGround)
        {
            //切换到等待状态
            stateMachine.ChangeState(player.IdleState);
        }
        //不接触墙 或者 没有抓墙输入 且 水平输入与人物朝向不相同
        else if (!isTouchingWall || !grabInput && xInput != player.FaceDir) 
        {
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

        //是否为地面
        isGround = player.CheckLeftFootIsGround() || player.CheckRightFootIsGround();
        //是否接触到墙
        isTouchingWall = player.CheckIsTouchWall();
        //是否接触到墙角
        isTouchingLedge = player.CheckIsTouchLedge();

        //接触墙面 且 不接触墙角
        if (isTouchingWall && !isTouchingLedge)
        {
            //设置检测位置
            player.LedgeClimbState.SetdetecedPos(player.transform.position);
        }
    
    }
}
