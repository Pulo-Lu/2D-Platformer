using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 玩家输入接口
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    /// <summary>
    /// 保存水平输入
    /// </summary>
    public Vector2 MovementInput { get; private set; }
    /// <summary>
    /// 限制输入 -1 0 1
    /// </summary>
    public int NormalInputX { get; private set; }
    public int NormalInputY { get; private set; }

    /// <summary>
    /// 保存玩家跳跃输入
    /// </summary>
    public bool JumpInput { get; set; }
    /// <summary>
    /// 停止玩家跳跃输入，用于根据按键时间控制跳跃高度
    /// </summary>
    public bool JumpInputStop { get; private set; }

    /// <summary>
    /// 跳跃开始时间
    /// </summary>
    private float JumpInputStartTime;
    [SerializeField]
    /// <summary>
    /// 跳跃延迟时间
    /// </summary>
    private float JumpInputHoldTime = 0.2f;

    /// <summary>
    /// 保存玩家抓墙输入
    /// </summary>
    public bool GrabInput { get; private set; }

    /// <summary>
    /// 保存玩家翻滚输入
    /// </summary>
    public bool ScrollInput { get; set; }

    /// <summary>
    /// 保存玩家冲刺输入
    /// </summary>
    public bool DashInput { get; private set; }

    private void Update()
    {
        //有玩家跳跃输入
        if (JumpInput)
        {
            //当前时间 与 跳跃开始时间 差 大于等于 跳跃延迟时间
            if (Time.time - JumpInputStartTime >= JumpInputHoldTime)
            {
                //关闭玩家输入
                JumpInput = false;
            }
        }
    }

    /// <summary>
    /// 输入检测事件
    /// </summary>
    /// <param name="context"></param>
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        //默认输入灵敏度为0.5f  
        //Mathf.RoundToInt(X)  X 的值大于0.5f四舍五入为1, 小于0.5f四舍五入为0
        //返回 X 指定的值四舍五入到最近的整数
        //如果数字末尾是.5，因此它是在两个整数中间，不管是偶数或是奇数，将返回偶数
        NormalInputX = Mathf.RoundToInt(MovementInput.x);
        NormalInputY = Mathf.RoundToInt(MovementInput.y);
    }

    /// <summary>
    /// 玩家跳跃事件
    /// </summary>
    /// <param name="context"></param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        //按下按键
        if (context.started)
        {
            //跳跃
            JumpInput = true;
            //开启根据按键时间控制的跳跃
            JumpInputStop = false;
            //记录跳跃开始时间
            JumpInputStartTime = Time.time;
        }
        //松开按键
        if (context.canceled)
        {
            //关闭根据按键时间控制的跳跃
            JumpInputStop = true;
        }
    }

    /// <summary>
    /// 玩家抓墙事件
    /// </summary>
    /// <param name="context"></param>
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        //按下按键
        if (context.started)
        {
            //抓墙
            GrabInput = true;       
        }
        //松开按键
        if (context.canceled)
        {
            //不抓墙
            GrabInput = false;
        }
    }
    
    /// <summary>
    /// 玩家翻滚事件
    /// </summary>
    /// <param name="context"></param>
    public void OnScrollInput(InputAction.CallbackContext context)
    {
        //按下按键
        if (context.started)
        {
            //翻滚
            ScrollInput = true;       
        }
        //松开按键
        if (context.canceled)
        {
            //不翻滚
            ScrollInput = false;
        }
    }   
    
    /// <summary>
    /// 玩家冲刺事件
    /// </summary>
    /// <param name="context"></param>
    public void OnDashInput(InputAction.CallbackContext context)
    {
        //按下按键
        if (context.started)
        {
            //冲刺
            DashInput = true;       
        }
        //松开按键
        if (context.canceled)
        {
            //不冲刺
            DashInput = false;
        }
    }

}
