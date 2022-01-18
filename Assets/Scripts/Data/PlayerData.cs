using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据脚本
/// </summary>
[CreateAssetMenu(fileName ="newPlayerData",menuName ="Data/Player Data/Move Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    /// <summary>
    /// 站立时碰撞盒位置
    /// </summary>
    public Vector2 StandColliderOffset;
    /// <summary>
    /// 站立时碰撞盒大小
    /// </summary>
    public Vector2 StandColliderSize;
    /// <summary>
    /// 水平方向移动速度
    /// </summary>
    public float movementVelocity = 10;



    [Header("Jump State")]
    /// <summary>
    /// 重力
    /// </summary>
    public float gravityScale = 6f;
    /// <summary>
    /// 竖直方向移动速度
    /// </summary>
    public float jumpVelocity = 20;
    /// <summary>
    /// 跳跃次数
    /// </summary>
    public int maxJumpCount = 2;
    /// <summary>
    /// 按键时间控制的跳跃高度系数
    /// </summary>
    public float jumpHeight = 0.5f;
    /// <summary>
    /// 跳跃的延迟时间
    /// </summary>
    public float jumpDelay = 0.2f;

    [Header("InAir State")]
    /// <summary>
    /// 空中方向移动速度
    /// </summary>
    public float movementInAir = 0.8f;
    /// <summary>
    /// 下滑状态持续时间（空中状态转换到单面墙反墙跳时间大小）
    /// </summary>
    public float wallSlideThehole = -3f;
    /// <summary>
    /// 空中下落的最大速度
    /// </summary>
    public float maxFallVelocity = -20f;

    [Header("Check Data")]
    /// <summary>
    /// 地面矩形检测宽高
    /// </summary>
    public Vector2 GroundCheckBorder = new Vector2(0.1f, 0.3f);
    /// <summary>
    /// 检测墙面的宽高
    /// </summary>
    public Vector2 wallCheckBorder = new Vector2(0.5f, 0.3f);
    /// <summary>
    /// 头顶球形检测半径
    /// </summary>
    public float CeilingCheckRadius = 0.25f;
    /// <summary>
    /// 地面检测层级
    /// </summary>
    public LayerMask GroundLayer;
    /// <summary>
    /// 墙面检测层级
    /// </summary>
    public LayerMask WallLayer;


    [Header("Wall Data")]
    /// <summary>
    /// 抓着墙上爬的速度
    /// </summary>
    public float WallClimbVelocity = 3f;
    /// <summary>
    /// 抓着墙下滑的速度
    /// </summary>
    public float WallSlideVelocity = 5f;

    [Header("Ledge Climb Data")]
    /// <summary>
    /// 抓着墙角时相对于墙角的偏移量
    /// </summary>
    public Vector2 StartOffset;
    /// <summary>
    /// 爬上墙角时相对于墙角的偏移量
    /// </summary>
    public Vector2 EndOffset;

    [Header("Crouch Data")]
    /// <summary>
    /// 蹲下时水平方向移动速度
    /// </summary>
    public float CrouchVelocity = 5f;
    /// <summary>
    /// 蹲下时起跳高度系数
    /// </summary>
    public float CrouchJump = 1.5f;
    /// <summary>
    /// 蹲下时碰撞盒位置
    /// </summary>
    public Vector2 CrouchColliderOffset;
    /// <summary>
    /// 蹲下时碰撞盒大小
    /// </summary>
    public Vector2 CrouchColliderSize;

    [Header("Wall Jump")]
    /// <summary>
    /// 单面墙反墙跳速度大小
    /// </summary>
    public Vector2 WallJumpVelocity = new Vector2(7, 20);
    /// <summary>
    /// 单面墙反墙跳间隔时间大小
    /// </summary>
    public float WallJumpTime = 0.3f;

    [Header("Wall Round Jump")]
    /// <summary>
    /// 两面墙之间来回跳的速度大小
    /// </summary>
    public Vector2 WallRoundJumpVelocity = new Vector2(10, 15);
    /// <summary>
    /// 两面墙之间来回跳的间隔时间大小
    /// </summary>
    public float WallRoundJumpTime = 0.4f;

    [Header("Scroll")]
    /// <summary>
    /// 翻滚速度大小
    /// </summary>
    public float ScrollVelocity = 10f;
    /// <summary>
    /// 翻滚时碰撞盒偏移
    /// </summary>
    public Vector2 ScrollColliderOffset;
    /// <summary>
    /// 翻滚时碰撞盒大小
    /// </summary>
    public Vector2 ScrollColliderSize;

    [Header("Dash")]
    /// <summary>
    /// 冲刺速度
    /// </summary>
    public Vector2 dashVelocity = new Vector2(20, 10);
    /// <summary>
    /// 冲刺需要的时间
    /// </summary>
    public float DashTime = 0.5f;
    /// <summary>
    /// 冲刺冷却时间
    /// </summary>
    public float DashCoolTime = 2f;


}
