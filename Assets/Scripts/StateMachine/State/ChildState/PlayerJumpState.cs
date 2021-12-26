using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家的跳跃状态
/// </summary>
public class PlayerJumpState : PlayerAbiilityState
{

    /// <summary>
    /// 跳跃次数计数器
    /// </summary>
    private int jumpCounter;

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
        //跳跃键已经按下，设置false
        player.inputHandler.JumpInput = false;
        //设置竖直移动速度
        player.SetVelocityY(playerData.jumpVelocity);
        //切换能力行为
        isAbilityDone = true;
        //进入状态时设置为上升过程中
        player.inAirState.SetIsJump();
        //跳跃次数减一
        jumpCounter--;
    }

    /// <summary>
    /// 能否跳跃
    /// </summary>
    /// <returns></returns>
    public bool CanJump()
    {
        //跳跃次数大于0
        return jumpCounter > 0;
    }

    /// <summary>
    /// 重置跳跃次数
    /// </summary>
    public void ResetJumpCount()
    {
        //跳跃次数设置为最大跳跃次数
        jumpCounter = playerData.maxJumpCount;
    }

    /// <summary>
    /// 跳跃次数设置为0
    /// </summary>
    public void SetJumpCountZero()
    {
        //跳跃次数为0
        jumpCounter = 0;
    }

}
