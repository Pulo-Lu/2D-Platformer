using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 蹲下等待状态
/// </summary>
public class PlayerCrouchIdleState : PlayerGroundState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerCrouchIdleState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
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
        //设置蹲下时的碰撞盒
        player.SetBoxColliderData(playerData.CrouchColliderOffset, playerData.CrouchColliderSize);

    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateMachine.CurrentState != this) { return; }
        //水平输入不为0
        if (xInput != 0)
        {
            //切换到蹲下移动状态
            stateMachine.ChangeState(player.CrouchMoveState);
        }
        //竖直输入为 0 即 松开S 且 头顶没有接触墙
        else if (yInput == 0 && !isTouchingCeiling)
        {
            Debug.Log(3);
            //切换到等待状态
            stateMachine.ChangeState(player.IdleState);
        }
    }

    /// <summary>
    /// 退出状态
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        //设置站立时的碰撞盒
        player.SetBoxColliderData(playerData.StandColliderOffset, playerData.StandColliderSize);
    }
}
