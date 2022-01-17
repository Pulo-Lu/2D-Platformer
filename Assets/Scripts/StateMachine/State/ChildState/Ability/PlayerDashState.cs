using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家冲刺状态
/// </summary>
public class PlayerDashState : PlayerAbiilityState
{
    /// <summary>
    /// 冲刺按键松开的时间
    /// </summary>
    private float dashInputFalseTime;
    /// <summary>
    /// 冲刺是否持续中
    /// </summary>
    private bool isHold;

    private Vector2Int dashDir;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerDashState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        //冲刺持续中
        isHold = true;

        //进入冲刺状态后不可再冲刺
        canUseAbility = false;

        player.SetVelocityZero();
        //重新记录进入状态的时间
        stateEnterTime = Time.unscaledTime;
        //残影
        ShadowSpwaner.Instance.ShowShadow(player.gameObject, 5, playerData.DashTime);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //有冲刺输入
        if (dashInput) 
        {
            Debug.Log(1);

            player.SetVelocityX(playerData.dashVelocity.x * player.FaceDir);
            //时间间隔 大于等于 子弹时间最大持续时间
            if (Time.unscaledTime - stateEnterTime >= playerData.MaxDashHoldTime)
            {
                //时间缩放恢复1
                Time.timeScale = 1;
                //无冲刺输入
                dashInput = false;
                //切换到能力行为
                isAbilityDone = true;
            }
        }
        //无冲刺输入
        else
        {
            Debug.Log(2);
            //冲刺持续中
            if (isHold)
            {
                Debug.Log(3);
                //时间缩放恢复1
                Time.timeScale = 1;
                //记录冲刺按键松开的时间
                dashInputFalseTime = Time.unscaledTime;
                //冲刺不在持续中
                isHold = false;
            }

            //当前时间减去 冲刺按键松开的时间 大于等于 冲刺需要的时间
            if (Time.time - dashInputFalseTime >= playerData.DashTime)
            {
                Debug.Log(4);
                //切换到能力行为
                isAbilityDone = true;
            }
           
        }
    }

    /// <summary>
    /// 退出状态
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        //设置速度为0
        player.SetVelocityZero();
        
    }

    /// <summary>
    /// 能否冲刺
    /// </summary>
    /// <returns></returns>
    public bool CanDash()
    {
        //可以冲刺 且 当前时间 减去 上次使用时间 大于等于 冲刺冷却时间
        return canUseAbility && Time.time - lastUseTime >= playerData.DashCoolTime;
    }
}
