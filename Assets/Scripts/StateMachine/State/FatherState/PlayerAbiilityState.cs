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
        stateType = StateType.Ability;
    }

    /// <summary>
    /// 是否切换地面父类状态或者空中父类状态
    /// </summary>
    protected bool isAbilityDone;
    /// <summary>
    /// 切换地面父类状态或者空中父类状态结束
    /// </summary>
    protected bool isAbilityOver;
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
    /// 蹲下检测头顶是否接触到墙
    /// </summary>
    protected bool isTouchingCeiling;
    /// <summary>
    /// 上次使用时间
    /// </summary>
    protected float lastUseTime = -100;
    /// <summary>
    /// 自动刷新冷却
    /// </summary>
    protected bool canUseAbility;

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        //不切换 由于不同行为：冲刺，跳跃，翻滚，反墙跳，攻击，操作对象也不同，由具体行为控制切换
        isAbilityDone = isAbilityOver = false;

        
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //切换地面父类状态或者空中父类状态 且 没有结束
        if (isAbilityDone) 
        {
            //当前时间设置为上次使用能力的时间
            lastUseTime = Time.time;
            //为地面 且 玩家竖直速度接近0
            if (isGround && player.CurrentVelocity.y < 0.01f) 
            {
                //头顶上有墙
                if (isTouchingCeiling)
                {
                    //切换到下蹲等待状态
                    stateMachine.ChangeState(player.CrouchIdleState);
                }
                //头顶上没有墙
                else
                {
                    //切换到等待状态
                    stateMachine.ChangeState(player.IdleState);
                }
            }
            //不是地面
            else
            {
                //切换到空中状态
                stateMachine.ChangeState(player.InAirState);
            }
            //结束切换地面父类状态或者空中父类状态
            isAbilityOver = true;
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
        //蹲下检测头顶是否接触到墙
        isTouchingCeiling = player.CheckIsTouchCeiling();
    }

    /// <summary>
    /// 自动刷新冷却时间
    /// </summary>
    public void RefreshCoolTime()
    {
        canUseAbility = true;
    }

}
