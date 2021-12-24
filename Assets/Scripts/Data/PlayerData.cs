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
    ///水平方向移动速度
    public float movementVelocity = 10;
}
