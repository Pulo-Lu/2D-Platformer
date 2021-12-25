using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家处在地面时的父类状态
/// </summary>
public class PlayerGroundState : PlayerState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerGroundState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //跳跃
        if (jumpInput)
        {
            //切换到跳跃状态
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
