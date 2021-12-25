using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单脚落地状态
/// </summary>
public class PlayerHardLandState : PlayerGroundState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerHardLandState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //水平方向输入不为0
        if (xInput != 0)
        {
            //切换到移动状态
            stateMachine.ChangeState(player.moveState);
        }
    }

}
