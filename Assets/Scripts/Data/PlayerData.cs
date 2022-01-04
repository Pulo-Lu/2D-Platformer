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
    /// 水平方向移动速度
    /// </summary>
    public float movementVelocity = 10;
    /// <summary>
    /// 站立时碰撞盒位置
    /// </summary>
    public Vector2 StandColliderOffset;
    /// <summary>
    /// 站立时碰撞盒大小
    /// </summary>
    public Vector2 StandColliderSize;

    [Header("Jump State")]
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

    [Header("Check Data")]
    /// <summary>
    /// 地面球形检测半径
    /// </summary>
    public float GroundCheckRadius = 0.1f;
    /// <summary>
    /// 头顶球形检测半径
    /// </summary>
    public float CeilingCheckRadius = 0.25f;
    /// <summary>
    /// 射线检测墙面的长度
    /// </summary>
    public float WallCheckLength = 0.5f;
    /// <summary>
    /// 地面检测层级
    /// </summary>
    public LayerMask GroundLayer;


    [Header("Wall Data")]
    /// <summary>
    /// 抓着墙上爬的速度
    /// </summary>
    public float wallClimbVelocity = 3f;
    /// <summary>
    /// 抓着墙下滑的速度
    /// </summary>
    public float wallSlideVelocity = 5f;

    [Header("Ledge Climb Data")]
    /// <summary>
    /// 抓着墙角时相对于墙角的偏移量
    /// </summary>
    public Vector2 startOffset;
    /// <summary>
    /// 爬上墙角时相对于墙角的偏移量
    /// </summary>
    public Vector2 endOffset;

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
}
