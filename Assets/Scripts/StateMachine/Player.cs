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
    /// <summary>
    /// 玩家跳跃状态
    /// </summary>
    public PlayerJumpState jumpState { get; private set; }
    /// <summary>
    /// 玩家空中状态
    /// </summary>
    public PlayerInAirState inAirState { get; private set; }
    /// <summary>
    /// 正常落地状态（双脚落地）
    /// </summary>
    public PlayerLandState landState { get; private set; }
    /// <summary>
    /// 单脚落地状态
    /// </summary>
    public PlayerHardLandState hardLandState { get; private set; }
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

    #region Transform
    /// <summary>
    /// 玩家左脚 Transform
    /// </summary>
    public Transform LetfFoot;
    /// <summary>
    /// 玩家右脚 Transform
    /// </summary>
    public Transform RightFoot;
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
        ///初始化状态机
        stateMachine = new StateMachine();
        //初始化等待状态
        idleState = new PlayerIdleState(this, playerData, stateMachine, "Idle");
        //初始化移动状态
        moveState = new PlayerMoveState(this, playerData, stateMachine, "Move");  
        //初始化跳跃状态
        jumpState = new PlayerJumpState(this, playerData, stateMachine, "InAir");
        //初始化空中状态
        inAirState = new PlayerInAirState(this, playerData, stateMachine, "InAir");
        //初始化正常落地状态
        landState = new PlayerLandState(this, playerData, stateMachine, "Land");
        //初始化单脚落地状态
        hardLandState = new PlayerHardLandState(this, playerData, stateMachine, "HardLand");
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
    /// 设置竖直方向速度 
    /// </summary>
    /// <param name="yVelocity"></param>
    public void SetVelocityY(float yVelocity)
    {
        //保存设置速度
        Temp = new Vector2(rb.velocity.x, yVelocity);
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
    /// 检测左脚是否在地面上
    /// </summary>
    /// <returns></returns>
    public bool CheckLeftFootIsGround()
    {
        //检测到返回true,否则返回false
        return Physics2D.OverlapCircle(LetfFoot.position, playerData.GroundCheckRadius, playerData.GroundLayer);
    }

    /// <summary>
    /// 检测右脚是否在地面上
    /// </summary>
    /// <returns></returns>
    public bool CheckRightFootIsGround()
    {
        //检测到返回true,否则返回false
        return Physics2D.OverlapCircle(RightFoot.position, playerData.GroundCheckRadius, playerData.GroundLayer);
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

    /// <summary>
    ///动画结束 
    /// </summary>
    private void OnAnimationFinish()
    {
        //当前状态设置为动画播放完成
        stateMachine.CurrentState.OnAnimationFinish();
    }

    /// <summary>
    /// 画圆
    /// </summary>
    private void OnDrawGizmos()
    {
        //画出左脚的球形检测
        Gizmos.DrawWireSphere(LetfFoot.position, playerData.GroundCheckRadius);
        //画出右脚的球形检测
        Gizmos.DrawWireSphere(RightFoot.position, playerData.GroundCheckRadius);
    }
}
