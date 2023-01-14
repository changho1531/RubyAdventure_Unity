using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    RubyController controller;
    //Startȣ�� ������ ������Ʈ ������ ���� �����ӿ� ȣ��ȴ�
    //Awake�� ������Ʈ ���� ���(Instantiate�� ȣ��� ��) ȣ��ǹǷ�
    //Launch ȣ�� ���� Rigidbody2D�� ���������� �غ�˴ϴ�.
    //null reference exception error - Rigidbody2D ����ִ� null ����
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    //������Ʈ�� ������ ��
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }


    void Update()
    {
        //position�� ������ �߽ɿ������� ������Ʈ�� ��ġ, magnitude�� �ش� ������ ���̰� �ȴ�
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    //������Ʈ�� �浹 �� ȣ��
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
            
        }
        Destroy(gameObject);
        
    }
}
