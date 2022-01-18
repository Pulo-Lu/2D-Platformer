using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家等待状态
/// </summary>
public class PlayerIdleState : PlayerGroundState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerIdleState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        //设置速度为0
        player.SetVelocityZero();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.transform.position = enterPos;

        //竖直输入为 -1 即 按下S
        if (yInput == -1 || isTouchingCeiling)
        {
            //切换到蹲下等待状态
            stateMachine.ChangeState(player.CrouchIdleState);
        }
        //单脚落地
        else if (isSingleFootGround)
        {
            //切换到单脚落地状态
            stateMachine.ChangeState(player.HardLandState);
        }
        //水平输入不为0
        else if (xInput != 0) 
        {
            //切换到移动状态
            stateMachine.ChangeState(player.MoveState);
        }
    }

}
