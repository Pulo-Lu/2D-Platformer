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

   
}
