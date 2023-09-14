using UnityEngine;

public class HammerController : MonoBehaviour
{
    // 控制锤击速度的公共变量
    public float smashSpeed = 10f;

    // 定义锤子在平面上方的高度
    public float hammerHeight = 10f;

    // 锤子锤击地面时下降的距离
    public float smashDistance = 0.5f;

    // 用于存储锤子的初始位置
    private Vector3 originalPosition;

    // 锤子锤击时的目标位置
    private Vector3 smashPosition;

    // 标志，表示是否正在执行锤击操作
    private bool smashing = false;

    private void Start()
    {
        // 在游戏开始时，记录锤子的初始位置
        originalPosition = transform.position;
    }

    void Update()
    {
        // 每一帧都使锤子移动以跟随鼠标
        MoveWithMouse();

        // 当玩家按下鼠标左键，并且锤子当前没有在执行锤击操作
        if (Input.GetMouseButtonDown(0) && !smashing)
        {
            // 设置标志为true，表示开始执行锤击操作
            smashing = true;

            // 计算锤击时锤子的目标位置
            smashPosition = transform.position - new Vector3(0, smashDistance, 0);
        }

        // 当玩家松开鼠标左键，并且锤子当前正在执行锤击操作
        if (Input.GetMouseButtonUp(0) && smashing)
        {
            // 设置标志为false，表示锤击操作结束
            smashing = false;
        }
    }

    // 用于调整插值运算的速度，值越大移动越快，越小移动越平滑
    private float lerpSpeed = 0.1f;

    void MoveWithMouse()
    {
        // 从摄像机通过鼠标当前位置发出一条射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 用于存储射线碰撞的信息
        RaycastHit hit;

        // 如果射线与物体碰撞
        if (Physics.Raycast(ray, out hit))
        {
            // 计算锤子的目标位置，使其跟随鼠标
            Vector3 targetPosition = new Vector3(hit.point.x, hammerHeight, hit.point.z);

            // 如果锤子正在执行锤击操作
            if (smashing)
            {
                // 使锤子平滑地移动到锤击的目标位置
                transform.position = Vector3.Lerp(transform.position, smashPosition, lerpSpeed);
            }
            else
            {
                // 如果锤子没有在执行锤击操作，使其平滑地跟随鼠标移动
                transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
            }
        }
    }
}

