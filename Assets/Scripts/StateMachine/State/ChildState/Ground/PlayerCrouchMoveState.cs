using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 蹲下移动状态
/// </summary>
public class PlayerCrouchMoveState : PlayerGroundState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerCrouchMoveState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        //设置蹲下时的碰撞盒
        player.SetBoxColliderData(playerData.CrouchColliderOffset, playerData.CrouchColliderSize);

    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //检测是否需要翻转
        player.CheckNeedFlip(xInput);
        //设置蹲下时水平移动速度
        player.SetVelocityX(xInput * playerData.CrouchVelocity);

        //没有水平输入
        if (xInput == 0)
        {
            //切换到蹲下等待状态
            stateMachine.ChangeState(player.CrouchIdleState);
        }
        //竖直输入不为 -1 即 松开S且 头顶没有接触墙
        else if (yInput != -1 && !isTouchingCeiling) 
        {
            //切换到移动状态
            stateMachine.ChangeState(player.MoveState);
        }
    }

}
