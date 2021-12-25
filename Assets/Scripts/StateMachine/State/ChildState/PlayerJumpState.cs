using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家的跳跃状态
/// </summary>
public class PlayerJumpState : PlayerAbiilityState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerJumpState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        //设置竖直移动速度
        player.SetVelocityY(playerData.jumpVelocity);
        //切换能力行为
        isAbilityDone = true;
    }

}
