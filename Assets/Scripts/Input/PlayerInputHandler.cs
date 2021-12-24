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
    /// -1 0 1
    /// </summary>
    public int NormalInputX { get; private set; }
    public int NormalInputY { get; private set; }

    /// <summary>
    /// 输入检测事件
    /// </summary>
    /// <param name="context"></param>
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        //默认输入灵敏度为0.5f  
        //Mathf.RoundToInt(X)  X 的绝对值大于0.5f四舍五入为1, 小于0.5f四舍五入为0
        NormalInputX = Mathf.RoundToInt(MovementInput.x);
        NormalInputY = Mathf.RoundToInt(MovementInput.y);
    }

}
