using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// /// <summary>
/// 玩家抓着墙下滑的状态
/// </summary>
public class PlayerWallSlideState : PlayerTouchingWallState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerWallSlideState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {

    }

    /// <summary>
    /// 逻辑更新 
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //设置玩家竖直速度为抓着墙下滑的速度
        player.SetVelocityY(playerData.WallSlideVelocity * -1);

        //有抓墙输入
        if (grabInput)
        {
            //竖直输入为1 ：W
        
            if (yInput == 1)
            {
                //切换到上爬状态
                stateMachine.ChangeState(player.WallClimbState);
              
            }
            //没有Y方向竖直输入
            else if (yInput == 0)
            {
                //切换到抓着墙的状态
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
        //接触墙面 且 有跳跃输入 且 水平方向输入与玩家朝向一致
        else if (isTouchingWall && jumpInput && xInput == player.FaceDir)
        {
            Debug.Log(2);
            //切换到玩家单面墙反墙跳状态
            stateMachine.ChangeState(player.WallJumpState);
        }
    }

}
