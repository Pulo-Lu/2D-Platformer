using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家脚本
/// </summary>
public class Player : MonoBehaviour
{
    #region State
    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine stateMachine { get; private set; }
    /// <summary>
    /// 玩家等待状态
    /// </summary>
    public PlayerIdleState idleState { get; private set; }
    /// <summary>
    /// 玩家移动状态
    /// </summary>
    public PlayerMoveState moveState { get; private set; }
    #endregion

    #region Data
    /// <summary>
    /// 玩家数据
    /// </summary>
    public PlayerData playerData;
    /// <summary>
    /// 玩家当前速度
    /// </summary>
    public Vector2 CurrentVelocity { get; private set; }
    /// <summary>
    /// 临时变量，用于保存速度
    /// </summary>
    private Vector2 Temp;
    /// <summary>
    /// 当前玩家面向方向 1右 -1左
    /// </summary>
    public int FaceDir { get; private set; }
    #endregion

    #region Component
    /// <summary>
    /// 输入组件
    /// </summary>
    public PlayerInputHandler inputHandler { get; private set; }
    /// <summary>
    /// 动画组件
    /// </summary>
    public Animator animator { get; private set; }
    /// <summary>
    /// 刚体组件
    /// </summary>
    public Rigidbody2D rb { get; private set; }
    #endregion

    private void Awake()
    {
        ///创建状态机
        stateMachine = new StateMachine();
        //创建等待状态
        idleState = new PlayerIdleState(this, playerData, stateMachine, "Idle");
        //创建移动状态
        moveState = new PlayerMoveState(this, playerData, stateMachine, "Move");
    }

    // Start is called before the first frame update
    void Start()
    {
        //获取 PlayerInputHandler 输入组件
        inputHandler = GetComponent<PlayerInputHandler>();
        //获取 动画 组件
        animator = GetComponent<Animator>();
        //获取 刚体 组件
        rb = GetComponent<Rigidbody2D>();
        //状态机初始化
        stateMachine.Init(idleState);

        //玩家面向右
        FaceDir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //状态机调用逻辑更新方法
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        //状态机调用物理更新方法
        stateMachine.CurrentState.PhysicsUpdate();
    }

    /// <summary>
    /// 设置水平方向速度 
    /// </summary>
    /// <param name="xVelocity"></param>
    public void SetVelocityX(float xVelocity)
    {
        //保存设置速度
        Temp = new Vector2(xVelocity, rb.velocity.y);
        //给刚体设置速度
        rb.velocity = CurrentVelocity = Temp;
    }

    /// <summary>
    /// 检测是否需要翻转
    /// </summary>
    /// <param name="xInput"></param>
    public void CheckNeedFlip(int xInput)
    {
        //有输入且玩家面向不为右
        if (xInput != 0 && xInput != FaceDir) 
        {
            //翻转
            Flip();
        }
    }

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        //180翻转
        transform.Rotate(0, 180, 0);
        //改变玩家面向方向
        FaceDir = -FaceDir;
    }
}
