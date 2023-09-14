using UnityEngine;

public class HammerController : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDown = false;

    void Start()
    {
        // ��¼���ӵĳ�ʼλ��
        originalPosition = transform.position;
    }

    void Update()
    {
        // ʹ���Ӹ������
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        transform.position = new Vector3(mousePos.x, transform.position.y, mousePos.z);

        if (Input.GetMouseButtonDown(0) && !isDown) // ����������������׶�Ӳ�������
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z); // ���Ӵ�����ƽ��
            isDown = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDown) // ���ͷ�����������׶��������
        {
            transform.position = originalPosition; // ���ӷ��ص�ԭʼλ��
            isDown = false;
        }
    }
}
