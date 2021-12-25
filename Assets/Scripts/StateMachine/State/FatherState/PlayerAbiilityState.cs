using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家的行为父类状态：冲刺，跳跃，翻滚，反墙跳，攻击
/// </summary>
public class PlayerAbiilityState : PlayerState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerAbiilityState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 是否切换地面父类状态或者空中父类状态
    /// </summary>
    protected bool isAbilityDone;
    /// <summary>
    /// 左脚是否落地
    /// </summary>
    protected bool isLeftFootGround;
    /// <summary>
    /// 右脚是否落地
    /// </summary>
    protected bool isRightFootGround;
    /// <summary>
    /// 是否一只脚落地
    /// </summary>
    protected bool isSingleFootGround;
    /// <summary>
    /// 是否为地面
    /// </summary>
    protected bool isGround;

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        //不切换 由于不同行为：冲刺，跳跃，翻滚，反墙跳，攻击，操作对象也不同，由具体行为控制切换
        isAbilityDone = false;

        
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //是否切换地面父类状态或者空中父类状态
        if (isAbilityDone)
        {
            //地面
            if (isGround && player.CurrentVelocity.y < 0.01f) 
            {
                //切换到等待状态
                stateMachine.ChangeState(player.idleState);
            }
            //不是地面
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
        //左脚是否落地
        isLeftFootGround = player.CheckLeftFootIsGround();
        //右脚是否落地
        isRightFootGround = player.CheckRightFootIsGround();
        //是否为地面
        isGround = isLeftFootGround || isRightFootGround;
        //是否一只脚落地
        isSingleFootGround = isLeftFootGround && !isRightFootGround || !isLeftFootGround && isRightFootGround;
    }

}
