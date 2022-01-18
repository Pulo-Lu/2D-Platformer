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
    /// 是否上升过程中
    /// </summary>
    private bool isJumping;
    /// <summary>
    /// 是否可以延迟跳跃
    /// </summary>
    private bool canJumpDelay;
    /// <summary>
    /// 是否接触到墙
    /// </summary>
    protected bool isTouchingWall;
    /// <summary>
    /// 是否接触墙角
    /// </summary>
    protected bool isTouchingLedge;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player">玩家脚本</param>
    /// <param name="playerData">玩家数据脚本</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="animBoolName">动画切换名称</param>
    public PlayerInAirState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
        stateType = StateType.InAir;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //控制延迟跳跃
        ControlJumpDelay();

        //根据按键时间控制的跳跃高度
        ControlJumpHeight();


        //有冲刺输入 且 没有接触墙 且 头顶没有墙 且 可以冲刺
        if (dashInput && !isTouchingWall && player.DashState.CanDash())
        {
            //有水平输入 或者 有竖直输入
            if (xInput != 0 || yInput != 0) 
            {
                //设置冲刺方向
                player.DashState.SetDashDirection(new Vector2Int(xInput, yInput));
            }
            else
            {
                //设置冲刺方向
                player.DashState.SetDashDirection(new Vector2Int(player.FaceDir, 0));
            }
            //切换到冲刺状态
            stateMachine.ChangeState(player.DashState);
        }
        //接触墙面 且 有跳跃输入 且 水平方向输入与玩家朝向一致
        else if (isTouchingWall && jumpInput && xInput == player.FaceDir)
        {
            //切换到玩家单面墙反墙跳状态
            stateMachine.ChangeState(player.WallJumpState);
        }
        //接触墙面 且 有跳跃输入 且 没有水平方向输入
        else if (isTouchingWall && jumpInput && xInput == 0)
        {
            //切换到玩家两面墙之间来回跳的状态
            stateMachine.ChangeState(player.WallRoundJumpState);
        }
        //接触墙面 且 不接触墙角 且 不在地面
        else if (isTouchingWall && !isTouchingLedge && !isGround)
        {
            //切换到玩家在墙角的状态
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        //有抓墙输入 且 接触到墙 且接触墙角
        else if (grabInput && isTouchingWall && isTouchingLedge)
        {
            //切换到抓着墙状态
            stateMachine.ChangeState(player.WallGrabState);
        }
        //有跳跃输入 且 跳跃次数不为0
        else if (jumpInput && player.JumpState.CanJump())
        {
            //切换到跳跃状态
            stateMachine.ChangeState(player.JumpState);
        }
        //接触到墙 且 水平输入与人物朝向相同 且 竖直速度小于0
        else if (isTouchingWall && xInput == player.FaceDir && player.CurrentVelocity.y < playerData.wallSlideThehole)    
        {
            //切换到抓着墙下滑状态
            stateMachine.ChangeState(player.WallSlideState);
        }
        //为地面 且 玩家竖直速度接近0
        else if (isGround && player.CurrentVelocity.y < 0.01f)
        {
            //只有一只脚落地
            if (isSingleFootGround)
            {
                //Debug.Log("单脚落地");
                //切换到单脚落地状态
                stateMachine.ChangeState(player.HardLandState);
            }
            //双脚落地
            else
            {
                //Debug.Log("双脚落地");
                //切换到双脚落地状态
                stateMachine.ChangeState(player.LandState);
            }
        }
        //在空中
        else
        {
            //检测是否需要翻转
            player.CheckNeedFlip(xInput);
            //设置水平方向速度 
            player.SetVelocityX(xInput * playerData.movementVelocity * playerData.movementInAir);

            //设置动画条件
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
        //是否接触到墙
        isTouchingWall = player.CheckIsTouchWall();
        //是否接触到墙角
        isTouchingLedge = player.CheckIsTouchLedge();

        //接触墙面 且 不接触墙角
        if (isTouchingWall && !isTouchingLedge)
        {
            //设置检测位置
            player.LedgeClimbState.SetdetecedPos(player.transform.position);
        }
    }

    /// <summary>
    /// 设置处于上升过程中
    /// </summary>
    public void SetIsJump()
    {
        //上升过程中
        isJumping = true;
    }

    /// <summary>
    /// 根据按键时间控制的跳跃高度
    /// </summary>
    private void ControlJumpHeight()
    {
        //上升过程中
        if (isJumping)
        {
            //时间控制跳跃输入的开关为启动
            if (jumpInputStop)
            {
                //设置竖直移动速度
                player.SetVelocityY(player.CurrentVelocity.y * playerData.jumpHeight);
                //不在上升过程中
                isJumping = false;

                Debug.Log("时间控制跳跃且不为最高点");
            }
            //到达最高点
            else if (Mathf.Abs(player.CurrentVelocity.y) < 0.1f)
            { 
                //不在上升过程中
                isJumping = false;
                Debug.Log("到达最高点");
            }
        }
    }

    /// <summary>
    /// 设置可以延迟跳跃
    /// </summary>
    public void SetCanJumpDelay()
    {
        //可以延迟跳跃
        canJumpDelay = true;
    }

    /// <summary>
    /// 控制延迟跳跃，使用延迟跳跃后，跳跃次数清0
    /// </summary>
    private void ControlJumpDelay()
    {
        //可以延迟跳跃
        if (canJumpDelay)
        {
            //当前时间 与 进入状态时的时间 差 大于等于 跳跃延迟时间
            if (Time.time - stateEnterTime >= playerData.jumpDelay)
            {
                //跳跃次数设置为0
                player.JumpState.SetJumpCountZero();
            }
        }
    }
}
