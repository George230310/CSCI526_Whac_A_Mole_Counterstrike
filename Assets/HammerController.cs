using UnityEngine;

public class HammerController : MonoBehaviour
{
    public float smashSpeed = 10f;  // ���ƴ����ٶ�
    public float hammerHeight = 10f;  // ������ƽ���Ϸ��ĸ߶�
    public float smashDistance = 0.5f;  // ���Ӵ�������ʱ�ľ���

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

        if (Input.GetMouseButtonDown(0) && !smashing)  // ������������
        {
            smashing = true;
            smashPosition = transform.position - new Vector3(0, smashDistance, 0);
        }
        if (Input.GetMouseButtonUp(0) && smashing)  // ���ɿ�������
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
                // ������ڴ��������Ƶ�������λ��
                transform.position = Vector3.Lerp(transform.position, smashPosition, lerpSpeed);
            }
            else
            {
                // ������ڴ������ô��Ӹ������
                transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
            }
        }
    }

}
