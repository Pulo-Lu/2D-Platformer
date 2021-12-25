using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  玩家处在空中时的父类状态
/// </summary>
public class PlayerInAirState : PlayerState
{
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
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerInAirState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //为地面 且 玩家竖直速度接近0
        if(isGround && player.CurrentVelocity.y < 0.01f)
        {
            //只有一只脚落地
            if (isSingleFootGround)
            {
                //切换到单脚落地状态
                stateMachine.ChangeState(player.hardLandState);
            }
            //双脚落地
            else
            {
                //切换到双脚落地状态
                stateMachine.ChangeState(player.landState);
            }
        }
        //在空中
        else
        {
            //可以翻转
            player.CheckNeedFlip(xInput);
            //设置水平方向速度 
            player.SetVelocityX(xInput * playerData.movementVelocity * playerData.movementInAir);

            //设置动画条件
            player.animator.SetFloat("XVelocity", Mathf.Abs(player.CurrentVelocity.x));
            player.animator.SetFloat("YVelocity", player.CurrentVelocity.y);

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
