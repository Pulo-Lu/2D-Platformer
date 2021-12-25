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
    //水平方向移动速度
    public float movementVelocity = 10;

    [Header("Jump State")]
    //竖直方向移动速度
    public float jumpVelocity = 20;

    [Header("Check Data")]
    //地面球形检测半径
    public float GroundCheckRadius = 0.1f;
    //地面检测层级
    public LayerMask GroundLayer;
}
