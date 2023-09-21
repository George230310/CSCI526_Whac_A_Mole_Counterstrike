using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    private float smashDelay = 0.25f; // 每0.25秒检查一次是否有鼹鼠需要锤击
    private float lastSmashTime = 0;

    // 控制锤击速度的公共变量
    public float smashSpeed = 10f;

    // 定义锤子在平面上方的高度
    public float hammerHeight = 10f;

    // 锤子锤击地面时下降的距离
    public float smashDistance = 0.5f;

    public List<GameObject> moles;

    // 用于存储锤子的初始位置
    private Vector3 originalPosition;

    // 锤子锤击时的目标位置
    private Vector3 smashPosition;

    // 标志，表示是否正在执行锤击操作
    private bool smashing = false;
    

    private void Awake()
    {
        // 在游戏初始化时，隐藏鼠标
        Cursor.visible = false;
    }

    private void Start()
    {
        // 在游戏开始时，记录锤子的初始位置
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Time.time - lastSmashTime > smashDelay)
        {
            lastSmashTime = Time.time;
            SmashRandomMole();
        }

        // 直接移动锤子，不依赖于鼠标位置
        MoveToTarget();
    }
    void MoveToTarget()
    {
        // 如果锤子正在执行锤击操作
        if (smashing)
        {
            // 使锤子移动到锤击的目标位置
            transform.position = smashPosition;
        }
        else
        {
            // 如果锤子没有在执行锤击操作，使其回到原始位置
            transform.position = originalPosition;
        }
    }
 


    void SmashRandomMole()
    {
        // 获取所有正在上升的鼹鼠
        List<GameObject> risingMoles = moles.FindAll(mole => mole.GetComponent<Mole>().isRising);

        // 如果有正在上升的鼹鼠
        if (risingMoles.Count > 0)
        {
            // 从列表中随机选择一个
            GameObject randomMole = risingMoles[Random.Range(0, risingMoles.Count)];

            // 计算锤击位置
            smashPosition = new Vector3(randomMole.transform.position.x, hammerHeight, randomMole.transform.position.z) - new Vector3(0, smashDistance, 0);

            // 执行锤击
            smashing = true;
        }
    }
}



