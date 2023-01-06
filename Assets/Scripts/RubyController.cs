using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�����Ӵ� ���� �̵� ����
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Input�� "����"�� GetAxis �Լ��� ��Ʈ������( . )�� ����� ȣ��
        //GetAxis �Լ��� �̸��� �ۼ� �� �̸� ȣ��
        //�Ķ���� ��ȣ ���� �ܾ� ����� �ϴ� �� �� �̸��� �Լ����� �˷��� ��.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log(horizontal);
        Debug.Log(vertical);

        Vector2 position = transform.position;

        //deltaTime�� Unity�� �� �������� �������ϴµ� �ɸ��� �ð��� �����´�
        //������ �ð����� ���� �̵�
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;
        transform.position = position;
    }
}