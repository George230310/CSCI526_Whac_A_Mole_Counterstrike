using UnityEngine;

public class HammerController : MonoBehaviour
{
    public float smashSpeed = 10f;  // 控制锤击速度
    public float hammerHeight = 10f;  // 锤子在平面上方的高度
    public float smashDistance = 0.5f;  // 锤子锤击地面时的距离

    private Vector3 originalPosition;
    private Vector3 smashPosition;
    private bool smashing = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        MoveWithMouse();

        if (Input.GetMouseButtonDown(0) && !smashing)  // 当按下鼠标左键
        {
            smashing = true;
            smashPosition = transform.position - new Vector3(0, smashDistance, 0);
        }
        if (Input.GetMouseButtonUp(0) && smashing)  // 当松开鼠标左键
        {
            smashing = false;
        }
    }

    private float lerpSpeed = 0.1f;

    void MoveWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, hammerHeight, hit.point.z);

            if (smashing)
            {
                // 如果正在锤击，就移到锤击的位置
                transform.position = Vector3.Lerp(transform.position, smashPosition, lerpSpeed);
            }
            else
            {
                // 如果不在锤击，让锤子跟随鼠标
                transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
            }
        }
    }

}
