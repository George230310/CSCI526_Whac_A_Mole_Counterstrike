using UnityEngine;

public class HammerController : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDown = false;

    void Start()
    {
        // 记录锤子的初始位置
        originalPosition = transform.position;
    }

    void Update()
    {
        // 使锤子跟随鼠标
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        transform.position = new Vector3(mousePos.x, transform.position.y, mousePos.z);

        if (Input.GetMouseButtonDown(0) && !isDown) // 如果点击鼠标左键并且锥子不在下面
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z); // 锤子触碰到平面
            isDown = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDown) // 当释放鼠标左键并且锥子在下面
        {
            transform.position = originalPosition; // 锤子返回到原始位置
            isDown = false;
        }
    }
}
