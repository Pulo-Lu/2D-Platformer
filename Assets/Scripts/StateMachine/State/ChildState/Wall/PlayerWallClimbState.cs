using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家抓着墙上爬的状态
/// </summary>
public class PlayerWallClimbState : PlayerTouchingWallState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerWallClimbState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 逻辑更新 
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //设置玩家竖直速度为抓着墙上爬的速度
        player.SetVelocityY(playerData.WallClimbVelocity * yInput);

        //没有Y方向竖直输入
        if(yInput == 0)
        {
            //切换到抓着墙的状态
            stateMachine.ChangeState(player.WallGrabState);
        }
         //竖直输入为-1 ：S
        else if (yInput == -1)
        {
            //切换到下滑状态
            stateMachine.ChangeState(player.WallSlideState);
        }
        //有跳跃输入
        else if (jumpInput)
        {
            //切换到在两面墙之间来回跳状态
            stateMachine.ChangeState(player.WallRoundJumpState);
        }
    }
}
