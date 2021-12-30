using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家处在墙面时的父类状态
/// </summary>
public class PlayerTouchingWallState : PlayerState
{
    /// <summary>
    /// 是否为地面
    /// </summary>
    protected bool isGround;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerTouchingWallState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //不抓墙
        if (!grabInput)
        {
            //为地面
            if (isGround)
            {
                //切换到等待状态
                stateMachine.ChangeState(player.idleState);
            }
            //不为地面
            else
            {
                //切换到空中状态
                stateMachine.ChangeState(player.inAirState);
            }
        }
    }

    /// <summary>
    /// 射线检测
    /// </summary>
    public override void OnCheck()
    {
        base.OnCheck();

        //是否为地面
        isGround = player.CheckLeftFootIsGround() || player.CheckRightFootIsGround();

    }
}
