using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家在墙角攀爬的状态
/// </summary>
public class PlayerLedgeClimbState : PlayerState
{
    /// <summary>
    /// 是否正在抓着墙角
    /// </summary>
    private bool isHolding;
    /// <summary>
    /// 是否正在攀爬
    /// </summary>
    private bool isClimbing;
    /// <summary>
    /// 蹲下检测头顶是否接触到墙
    /// </summary>
    protected bool isTouchingCeiling;

    /// <summary>
    /// 检测位置
    /// </summary>
    private Vector2 detecedPos;
    /// <summary>
    /// 检测墙角位置
    /// </summary>
    private Vector2 cornerPos;
    /// <summary>
    /// 爬墙开始位置(相对于墙角坐标)
    /// </summary>
    private Vector2 startPos;
    /// <summary>
    /// 爬墙结束位置(相对于墙角坐标)
    /// </summary>
    private Vector2 endPos;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="player"></param>
    /// <param name="playerData"></param>
    /// <param name="stateMachine"></param>
    /// <param name="animBoolName"></param>
    public PlayerLedgeClimbState(Player player, PlayerData playerData, StateMachine stateMachine, string animBoolName) : base(player, playerData, stateMachine, animBoolName)
    {
    }

    //进入状态
    public override void Enter()
    {
        base.Enter();
        //正在抓着墙角
        isHolding = true;
        //没有攀爬
        isClimbing = false;
        //设置速度为0
        player.SetVelocityZero();
        //将玩家位置设置为 接触墙面 且 接触墙角 的位置（用于后续计算）
        player.transform.position = detecedPos;
        //墙角位置
        cornerPos = player.CalculateCorner();

        //爬墙开始位置
        startPos = cornerPos + new Vector2(playerData.StartOffset.x * player.FaceDir, playerData.StartOffset.y);
        //爬墙结束位置
        endPos = cornerPos + new Vector2(playerData.EndOffset.x * player.FaceDir, playerData.EndOffset.y);

        //将玩家位置设置为爬墙开始位置
        player.transform.position = startPos;
    }

    /// <summary>
    /// 设置检测位置
    /// </summary>
    /// <param name="detecedPos"></param>
    public void SetdetecedPos(Vector3 detecedPos)
    {
        this.detecedPos = detecedPos;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //画出墙角水平竖直发射的线
        Debug.DrawLine(cornerPos, cornerPos + new Vector2(0, 0.1f));
        Debug.DrawLine(cornerPos, cornerPos + new Vector2(0.1f * -player.FaceDir, 0));

        //爬墙时设置速度为0
        player.SetVelocityZero();
        //将玩家位置设置为爬墙开始位置
        player.transform.position = startPos;

        //抓着墙角
        if (isHolding)
        {
            //水平输入 为 玩家面向方向
            if (xInput == player.FaceDir)
            {
                //攀爬
                isClimbing = true;
                //没有抓着墙角
                isHolding = false;
                //设置攀爬动画
                player.animator.SetBool("ClimbLedge", true);
            }
            //竖直输入为-1 即 W 
            else if (yInput == -1)
            {
                //切换玩家在空中的状态
                stateMachine.ChangeState(player.InAirState);
            }
            //有跳跃输入
            else if (jumpInput)
            {
                //切换到在两面墙之间来回跳状态
                stateMachine.ChangeState(player.WallRoundJumpState);
            }
        }

        //正在攀爬
        if (isClimbing)
        {
            //动画结束
            if (isAnimationFinish)
            {
                //将玩家位置设置为爬墙结束位置
                player.transform.position = endPos;
                //头顶有墙
                if (CheckIsTouchCeiling())
                {
                    //切换到蹲下状态
                    stateMachine.ChangeState(player.CrouchIdleState);
                }
                //没有墙
                else
                {
                    //切换到等待状态
                    stateMachine.ChangeState(player.IdleState);
                }
            }
        }
    }

    /// <summary>
    /// 动画事件
    /// </summary>
    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();

        //没有抓着墙角
        isHolding = true;
    }

    /// <summary>
    /// 结束播放动画
    /// </summary>
    public override void OnAnimationFinish()
    {
        base.OnAnimationFinish();

        //设置攀爬动画
        player.animator.SetBool("ClimbLedge", false);
        //动画结束
        isAnimationFinish = true;
    }

    /// <summary>
    /// 检测头顶是否有墙
    /// </summary>
    private bool CheckIsTouchCeiling()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(cornerPos + new Vector2(0.015f * player.FaceDir, 0), Vector2.up, 1.6f, playerData.GroundLayer);
        Debug.DrawLine(cornerPos, cornerPos + Vector2.up * 1.6f, Color.red);
        return hit2D;
    }

    /// <summary>
    /// 射线检测
    /// </summary>
    public override void OnCheck()
    {
        base.OnCheck();
        //蹲下检测头顶是否接触到墙
        isTouchingCeiling = CheckIsTouchCeiling();
    }
}
