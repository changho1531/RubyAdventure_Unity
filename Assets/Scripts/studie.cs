using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class studie : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    //�÷��̾ ������ ������ �ٶ󺸴� ����
    //������ ���� ������ ���¸ӽ��� ��� ������ ���ؾ� ���� �𸥴�
    Vector2 lookDirection = new Vector2(1, 0);

    //���� ������Ʈ
    public GameObject projectilePrefab;

    public float speed;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
        
    // Update is called once per frame
    void Update()
    {
        //�Ķ���� ��ȣ ���� �ܾ� ����� �ϴ� �� �� �̸��� ����
        //���� ����Ű�� ������ ����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //�̵� ���� ����
        Vector2 move = new Vector2(horizontal, vertical);

        //transform ������� �̵��� ������Ʈ �浹�� ���������� �Ͼ��
        //ĳ���� ��ġ�� �����´�
        Vector2 position = rigidbody2D.position;

        //deltaTime�� Unity�� �� �������� �������ϴµ� �ɸ��� �ð��� �����´�
        //������ �ӵ��� �� �����Ӹ��� ������ �ٴ´�
        //������ �ð����� ���� �̵�
        position = position + move * speed * Time.deltaTime;

        //Rigidbody2D ���ϴ� ��ġ �̵� �� �ٸ� �ݶ��̴��� �浹�� ����
        rigidbody2D.MovePosition(position);


        //�÷��̾ Ű�� �����°��� ����
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    void Launch()
    {
        //������Ʈ ����
        //Instantiate(������Ʈ,��ġ�� ������Ʈ���纻 ����, ȸ����) �Լ�
        //Instantiate(������Ʈ,rigidbody��ġ ���̾ƴ� �տ���ġ �ϱ����� �ణ ���, Quaternionȸ���� ǥ���ϴ� ������.ȸ������) �Լ�
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        //Launch(ȣ��� ĳ���Ͱ� �ٶ󺸴� ����, ���� ��ư����)
        projectile.Launch(lookDirection, 300);
    }
}
