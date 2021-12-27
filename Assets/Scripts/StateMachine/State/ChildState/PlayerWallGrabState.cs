using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家抓着墙的状态
/// </summary>
public class PlayerWallGrabState : PlayerTouchingWallState
{
    /// <summary>
    /// 抓住墙的位置
    /// </summary>
    private Vector3 grabPos;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerWallGrabState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //保存抓住墙的位置
        grabPos = player.transform.position;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //抓着墙不动
        player.transform.position = grabPos;
        //TODO  体力系统  增加计时器可实现
        //设置速度为0
        player.SetVelocityZero();

        //竖直输入为1 ：W
        if(yInput == 1)
        {
            //切换到上爬状态
            stateMachine.ChangeState(player.wallClimbState);
        }
        //竖直输入为-1 ：S
        else if (yInput == -1)
        {
            //切换到下滑状态
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

}
