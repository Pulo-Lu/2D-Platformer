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
    public PlayerIdleState IdleState { get; private set; }
    /// <summary>
    /// 玩家移动状态
    /// </summary>
    public PlayerMoveState MoveState { get; private set; }
    /// <summary>
    /// 玩家跳跃状态
    /// </summary>
    public PlayerJumpState JumpState { get; private set; }
    /// <summary>
    /// 玩家空中状态
    /// </summary>
    public PlayerInAirState InAirState { get; private set; }
    /// <summary>
    /// 正常落地状态（双脚落地）
    /// </summary>
    public PlayerLandState LandState { get; private set; }
    /// <summary>
    /// 单脚落地状态
    /// </summary>
    public PlayerHardLandState HardLandState { get; private set; }
    /// <summary>
    /// 玩家抓着墙的状态
    /// </summary>
    public PlayerWallGrabState WallGrabState { get; private set; }
    /// <summary>
    /// 玩家抓着墙上爬的状态
    /// </summary>
    public PlayerWallClimbState WallClimbState { get; private set; }
    /// /// <summary>
    /// 玩家抓着墙下滑的状态
    /// </summary>
    public PlayerWallSlideState WallSlideState { get; private set; }
    /// <summary>
    /// 玩家在墙角的状态
    /// </summary>
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    /// <summary>
    /// 玩家蹲下等待的状态
    /// </summary>
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    /// <summary>
    /// 玩家蹲下移动的状态
    /// </summary>
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    /// <summary>
    /// 玩家单面墙反墙跳状态
    /// </summary>
    public PlayerWallJumpState WallJumpState { get; private set; }
    /// <summary>
    /// 玩家两面墙之间来回反墙跳状态
    /// </summary>
    public PlayerWallRoundJumpState WallRoundJumpState { get; private set; }
    /// <summary>
    /// 玩家翻滚状态
    /// </summary>
    public PlayerScrollState ScrollState { get; private set; }
    /// <summary>
    /// 玩家冲刺状态
    /// </summary>
    public PlayerDashState DashState { get; private set; }

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

    #region Check Transform
    /// <summary>
    /// 检测玩家左脚 Transform
    /// </summary>
    public Transform LetfFoot;
    /// <summary>
    /// 检测玩家右脚 Transform
    /// </summary>
    public Transform RightFoot;
    /// <summary>
    /// 检测墙面 Transform
    /// </summary>
    public Transform WallCheckCenter;
    /// <summary>
    /// 检测墙角 Transform
    /// </summary>
    public Transform LedgeCheckCenter;
    /// <summary>
    /// 检测头顶 Transform
    /// </summary>
    public Transform CeilingCheckCenter;

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
    /// <summary>
    /// 碰撞盒组件
    /// </summary>
    private BoxCollider2D box;
    #endregion

    private void Awake()
    {
        ///初始化状态机
        stateMachine = new StateMachine();
        //初始化等待状态
        IdleState = new PlayerIdleState(this, playerData, stateMachine, "Idle");
        //初始化移动状态
        MoveState = new PlayerMoveState(this, playerData, stateMachine, "Move");  
        //初始化跳跃状态
        JumpState = new PlayerJumpState(this, playerData, stateMachine, "InAir");
        //初始化空中状态
        InAirState = new PlayerInAirState(this, playerData, stateMachine, "InAir");
        //初始化正常落地状态
        LandState = new PlayerLandState(this, playerData, stateMachine, "Land");
        //初始化单脚落地状态
        HardLandState = new PlayerHardLandState(this, playerData, stateMachine, "HardLand");
        //初始化玩家抓着墙状态
        WallGrabState = new PlayerWallGrabState(this, playerData, stateMachine, "WallGrab");
        //初始化玩家抓着墙上爬状态
        WallClimbState = new PlayerWallClimbState(this, playerData, stateMachine, "WallClimb");
        //初始化玩家抓着墙下滑状态
        WallSlideState = new PlayerWallSlideState(this, playerData, stateMachine, "WallSlide");
        //初始化玩家在墙角的状态
        LedgeClimbState = new PlayerLedgeClimbState(this, playerData, stateMachine, "LedgeClimb");
        //初始化玩家蹲下等待的状态
        CrouchIdleState = new PlayerCrouchIdleState(this, playerData, stateMachine, "CrouchIdle");
        //初始化玩家蹲下移动的状态
        CrouchMoveState = new PlayerCrouchMoveState(this, playerData, stateMachine, "CrouchMove");
        //初始化玩家单面墙反墙跳的状态
        WallJumpState = new PlayerWallJumpState(this, playerData, stateMachine, "WallJump");
        //初始化玩家两面墙之间来回反墙跳状态
        WallRoundJumpState = new PlayerWallRoundJumpState(this, playerData, stateMachine, "WallRoundJump");
        //初始化玩家翻滚状态
        ScrollState = new PlayerScrollState(this, playerData, stateMachine, "Scroll");
        //初始化玩家冲刺状态
        DashState = new PlayerDashState(this, playerData, stateMachine, "Dash");


        //获取 PlayerInputHandler 输入组件
        inputHandler = GetComponent<PlayerInputHandler>();
        //获取 动画 组件
        animator = GetComponent<Animator>();
        //获取 刚体 组件
        rb = GetComponent<Rigidbody2D>();
        //获取 碰撞盒 组件
        box = GetComponent<BoxCollider2D>();
        //状态机初始化
        stateMachine.Init(IdleState);
    }

    // Start is called before the first frame update
    void Start()
    {
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
    /// 设置速度为0 
    /// </summary>
    public void SetVelocityZero()
    {
        //给刚体设置速度
        rb.velocity = CurrentVelocity = Vector2.zero;
    }

    /// <summary>
    /// 设置碰撞盒参数
    /// </summary>
    /// <param name="offset">位置</param>
    /// <param name="size">大小</param>
    public void SetBoxColliderData(Vector2 offset, Vector2 size)
    {
        //设置位置
        box.offset = offset;
        //设置大小
        box.size = size;
    }

    /// <summary>
    /// 检测是否需要翻转
    /// </summary>
    /// <param name="xInput"></param>
    public void CheckNeedFlip(int xInput)
    {
        //有输入 且 玩家面向不为右
        if (xInput != 0 && xInput != FaceDir) 
        {
            //翻转
            Flip();
        }
    }

    /// <summary>
    /// 检测左脚是否在地面上（矩形检测）
    /// </summary>
    /// <returns></returns>
    public bool CheckLeftFootIsGround()
    {
        //检测到返回true,否则返回false
        return Physics2D.OverlapBox(LetfFoot.position, playerData.GroundCheckBorder, 0, playerData.GroundLayer);
    }

    /// <summary>
    /// 检测右脚是否在地面上（矩形检测）
    /// </summary>
    /// <returns></returns>
    public bool CheckRightFootIsGround()
    {
        //检测到返回true,否则返回false
        return Physics2D.OverlapBox(RightFoot.position, playerData.GroundCheckBorder, 0, playerData.GroundLayer);
    }

    /// <summary>
    /// 检测是否接触到墙面（射线检测）
    /// </summary>
    /// <returns></returns>
    public bool CheckIsTouchWall()
    {
        //检测到返回true,否则返回false
        return Physics2D.Raycast(WallCheckCenter.position, Vector2.right * FaceDir, playerData.WallCheckLength, playerData.GroundLayer);
    }

    /// <summary>
    /// 检测是否接触到墙角（射线检测）
    /// </summary>
    /// <returns></returns>
    public bool CheckIsTouchLedge()
    {
        //检测到返回true,否则返回false
        return Physics2D.Raycast(LedgeCheckCenter.position, Vector2.right * FaceDir, playerData.WallCheckLength, playerData.GroundLayer);
    }

    /// <summary>
    /// 蹲下检测头顶是否接触到墙（射线检测）
    /// </summary>
    /// <returns></returns>
    public bool CheckIsTouchCeiling()
    {
        //检测到返回true,否则返回false
        //return Physics2D.OverlapCircle(CeilingCheckCenter.position, playerData.CeilingCheckRadius, playerData.GroundLayer);
        //射线检测
        return Physics2D.Raycast(CeilingCheckCenter.position, Vector2.up, playerData.CeilingCheckRadius, playerData.GroundLayer);
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
    /// 计算墙角位置（射线检测）
    /// </summary>
    /// <returns></returns>
    public Vector2 CalculateCorner()
    {
        //获取从 检测墙面 Transform 发射到墙面的射线 ；射线与墙面接触x值即为墙角x坐标
        RaycastHit2D hit_x = Physics2D.Raycast(WallCheckCenter.position, Vector2.right * FaceDir, playerData.WallCheckLength, playerData.GroundLayer);
        //计算  检测点
        Vector2 yCenter = new Vector2(WallCheckCenter.position.x + (hit_x.distance + 0.015f) * FaceDir, LedgeCheckCenter.position.y);
        //获取从 检测点 向下发射到墙面的射线 ；射线与墙面接触y值即为墙角y坐标
        RaycastHit2D hit_y = Physics2D.Raycast(yCenter, Vector2.down, LedgeCheckCenter.position.y - WallCheckCenter.position.y, playerData.GroundLayer);
        //返回墙角坐标
        return new Vector2(WallCheckCenter.position.x + hit_x.distance * FaceDir, LedgeCheckCenter.position.y - hit_y.distance);
    }

    /// <summary>
    /// 动画事件
    /// </summary>
    private void OnAnimationTrigger()
    {
        //动画事件触发
        stateMachine.CurrentState.OnAnimationTrigger();
    }

    /// <summary>
    ///动画结束 
    /// </summary>
    private void OnAnimationFinish()
    {
        //动画播放完成
        stateMachine.CurrentState.OnAnimationFinish();
    }

    /// <summary>
    /// 画圆
    /// </summary>
    private void OnDrawGizmos()
    {
        //画出左脚的矩形检测
        Gizmos.DrawWireCube(LetfFoot.position, playerData.GroundCheckBorder);
        //画出右脚的矩形检测
        Gizmos.DrawWireCube(RightFoot.position, playerData.GroundCheckBorder);

        //画出头顶的球形检测
        //Gizmos.DrawWireSphere(CeilingCheckCenter.position, playerData.CeilingCheckRadius);

        //画出接触墙面的射线检测
        Gizmos.DrawLine(WallCheckCenter.position, WallCheckCenter.position + Vector3.right * FaceDir * playerData.WallCheckLength);
        //画出接触墙角的射线检测
        Gizmos.DrawLine(LedgeCheckCenter.position, LedgeCheckCenter.position + Vector3.right * FaceDir * playerData.WallCheckLength);
        //画出头顶接触墙的射线检测
        Gizmos.DrawLine(CeilingCheckCenter.position,(Vector2)CeilingCheckCenter.position + Vector2.up * playerData.CeilingCheckRadius);

    }
}
