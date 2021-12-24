using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家移动状态
/// </summary>
public class PlayerMoveState : PlayerGroundState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerMoveState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //检测是否需要翻转
        player.CheckNeedFlip(xInput);
        //设置水平移动速度
        player.SetVelocityX(playerData.movementVelocity * xInput);

        //水平输入为0
        if (xInput == 0)
        {
            //切换到等待状态
            stateMachine.ChangeState(player.idleState);
        }
    }

}
