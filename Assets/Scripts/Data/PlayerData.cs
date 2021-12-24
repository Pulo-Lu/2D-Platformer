using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据脚本
/// </summary>
public class PlayerData : MonoBehaviour
{
    #region State
    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine stateMachine { get; private set; }


    #endregion

    private void Awake()
    {
        stateMachine = new StateMachine();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
