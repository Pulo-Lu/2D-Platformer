using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家在墙角的状态
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

        //设置速度为0
        player.SetVelocityZero();
        //将玩家位置设置为 接触墙面 且 接触墙角 的位置（用于后续计算）
        player.transform.position = detecedPos;
        //墙角位置
        cornerPos = player.CalculateCorner();

        //爬墙开始位置
        startPos = cornerPos + new Vector2(playerData.startOffset.x * player.FaceDir, playerData.startOffset.y);
        //爬墙结束位置
        endPos = cornerPos + new Vector2(playerData.endOffset.x * player.FaceDir, playerData.endOffset.y);

        //将玩家位置设置为爬墙开始位置
        player.transform.position = startPos;
    }

    //退出状态
    public override void Exit()
    {
        base.Exit();

        //不抓着墙角
        isHolding = false;
        //攀爬
        if (isClimbing)
        {
            //将玩家位置设置为爬墙结束位置
            player.transform.position = endPos;
            //不攀爬
            isClimbing = false;
        }
    }

    //设置检测位置
    public void SetdetecedPos(Vector3 detecedPos)
    {
        this.detecedPos = detecedPos;
    }

    //逻辑更新
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

        //抓着墙角 且 没有攀爬
        if (isHolding && !isClimbing)
        {
            //水平输入 为 玩家面向方向
            if (xInput == player.FaceDir)
            {
                //设置正在攀爬
                isClimbing = true;
                //设置攀爬动画
                player.animator.SetBool("ClimbLedge", true);
            }
            //竖直输入为 -1 即 按下W
            else if (yInput == -1)
            {
                //切换到玩家在空中的状态
                stateMachine.ChangeState(player.inAirState);
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

    //结束播放动画
    public override void OnAnimationFinish()
    {
        base.OnAnimationFinish();

        Debug.Log("1");
        //设置攀爬动画
        player.animator.SetBool("ClimbLedge", false);

        //切换到等待状态
        stateMachine.ChangeState(player.idleState);
    }

}
