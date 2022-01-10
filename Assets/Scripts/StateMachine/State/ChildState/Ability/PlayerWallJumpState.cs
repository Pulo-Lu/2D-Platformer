using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家单面墙反墙跳状态
/// </summary>
public class PlayerWallJumpState : PlayerAbiilityState
{

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerWallJumpState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        //设置反墙跳速度
        player.SetVelocityX(playerData.WallJumpVelocity.x * -player.FaceDir);
        player.SetVelocityY(playerData.WallJumpVelocity.y);
       
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //设置动画条件
        player.animator.SetFloat("XVelocity", Mathf.Abs(player.CurrentVelocity.x));
        player.animator.SetFloat("YVelocity", player.CurrentVelocity.y);

        //时间间隔大于等于单面墙反墙跳的时间
        if (Time.time - stateEnterTime >= playerData.WallJumpTime) 
        {
            //切换能力行为
            isAbilityDone = true;
        }
    }
}
