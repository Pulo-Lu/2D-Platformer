using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家翻滚状态
/// </summary>
public class PlayerScrollState : PlayerAbiilityState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerScrollState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        //设置翻滚时的碰撞盒
        player.SetBoxColliderData(playerData.ScrollColliderOffset, playerData.ScrollColliderSize);
        //设置翻滚速度
        player.SetVelocityX(playerData.ScrollVelocity * player.FaceDir);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //不切换能力行为 且 （动画播放完成 或者 有水平输入 且 水平输入不为人物朝向） 
        if (!isAbilityDone && (isAnimationFinish || xInput != 0 && xInput != player.FaceDir))
        {
            //切换能力行为
            isAbilityDone = true;
        }
    }

    /// <summary>
    /// 动画播放完成
    /// </summary>
    public override void OnAnimationFinish()
    {
        base.OnAnimationFinish();
    }

    /// <summary>
    /// 退出状态
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        //设置站立时的碰撞盒
        player.SetBoxColliderData(playerData.StandColliderOffset, playerData.StandColliderSize);
    }
}
