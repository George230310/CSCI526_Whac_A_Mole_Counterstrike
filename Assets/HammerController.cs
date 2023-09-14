using UnityEngine;

public class HammerController : MonoBehaviour
{
    // ���ƴ����ٶȵĹ�������
    public float smashSpeed = 10f;

    // ���崸����ƽ���Ϸ��ĸ߶�
    public float hammerHeight = 10f;

    // ���Ӵ�������ʱ�½��ľ���
    public float smashDistance = 0.5f;

    // ���ڴ洢���ӵĳ�ʼλ��
    private Vector3 originalPosition;

    // ���Ӵ���ʱ��Ŀ��λ��
    private Vector3 smashPosition;

    // ��־����ʾ�Ƿ�����ִ�д�������
    private bool smashing = false;

    private void Start()
    {
        // ����Ϸ��ʼʱ����¼���ӵĳ�ʼλ��
        originalPosition = transform.position;
    }

    void Update()
    {
        // ÿһ֡��ʹ�����ƶ��Ը������
        MoveWithMouse();

        // ����Ұ��������������Ҵ��ӵ�ǰû����ִ�д�������
        if (Input.GetMouseButtonDown(0) && !smashing)
        {
            // ���ñ�־Ϊtrue����ʾ��ʼִ�д�������
            smashing = true;

            // ���㴸��ʱ���ӵ�Ŀ��λ��
            smashPosition = transform.position - new Vector3(0, smashDistance, 0);
        }

        // ������ɿ������������Ҵ��ӵ�ǰ����ִ�д�������
        if (Input.GetMouseButtonUp(0) && smashing)
        {
            // ���ñ�־Ϊfalse����ʾ������������
            smashing = false;
        }
    }

    // ���ڵ�����ֵ������ٶȣ�ֵԽ���ƶ�Խ�죬ԽС�ƶ�Խƽ��
    private float lerpSpeed = 0.1f;

    void MoveWithMouse()
    {
        // �������ͨ����굱ǰλ�÷���һ������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ���ڴ洢������ײ����Ϣ
        RaycastHit hit;

        // ���������������ײ
        if (Physics.Raycast(ray, out hit))
        {
            // ���㴸�ӵ�Ŀ��λ�ã�ʹ��������
            Vector3 targetPosition = new Vector3(hit.point.x, hammerHeight, hit.point.z);

            // �����������ִ�д�������
            if (smashing)
            {
                // ʹ����ƽ�����ƶ���������Ŀ��λ��
                transform.position = Vector3.Lerp(transform.position, smashPosition, lerpSpeed);
            }
            else
            {
                // �������û����ִ�д���������ʹ��ƽ���ظ�������ƶ�
                transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
            }
        }
    }
}

