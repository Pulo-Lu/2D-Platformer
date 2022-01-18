using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家冲刺状态
/// </summary>
public class PlayerDashState : PlayerAbiilityState
{
    /// <summary>
    /// 冲刺方向
    /// </summary>
    private Vector2Int dashDir;

    /// <summary>
    /// 结束位置
    /// </summary>
    private Vector3 endPos;

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

        //进入冲刺状态后不可再冲刺
        canUseAbility = false;
        //设置速度为零
        player.SetVelocityZero();
        //重力为零
        player.rb.gravityScale = 0;
        //检查是否需要翻转
        player.CheckNeedFlip(dashDir.x);
        //残影
        //ShadowSpwaner.Instance.ShowShadow(player.gameObject, 5, playerData.DashTime);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //不切换能力行为
        if (!isAbilityDone)
        {
            //设置速度
            player.SetVelocityX(dashDir.x * playerData.dashVelocity.x);
            player.SetVelocityY(dashDir.y * playerData.dashVelocity.y);
        }
        //不切换能力行为 且 （当前时间减去 进入冲刺状态的时间 大于等于 冲刺需要的时间）||
        //(y冲刺方向为1 且 头顶有墙) || 接触到墙 || (y冲刺方向为-1 且 接触地面)
        if (!isAbilityDone && (Time.time - stateEnterTime >= playerData.DashTime || 
            (dashDir.y == 1 && isTouchingCeiling) || player.CheckIsTouchWall() || (dashDir.y == -1 && isGround)))
        {
            //记录冲刺后的位置
            endPos = player.transform.position;
            //切换能力行为
            isAbilityDone = true;
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
        //玩家位置设置为冲刺后的位置
        player.transform.position = endPos;
        //恢复重力
        player.rb.gravityScale = playerData.gravityScale;
    }

    /// <summary>
    /// 能否冲刺
    /// </summary>
    /// <returns></returns>
    public bool CanDash()
    {
        //可以冲刺 或者 当前时间 减去 上次使用时间 大于等于 冲刺冷却时间
        return canUseAbility || Time.time - lastUseTime >= playerData.DashCoolTime;
    }

    /// <summary>
    /// 设置冲刺方向
    /// </summary>
    /// <param name="dir"></param>
    public void SetDashDirection(Vector2Int dir)
    {
        //记录冲刺方向
        dashDir = dir;
    }
}
