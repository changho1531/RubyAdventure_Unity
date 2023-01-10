using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;
    float timer;
    //���� ����
    int direction = 1;
    Animator animator;

    //�κ�����
    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        //�ش� ������Ʈ�� �˻� �����ͼ� ����
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }
    // Update is called once per frame
    void Update()
    {

        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        

        Vector2 position = rigidbody2D.position;
        if (vertical)
        {
            //�ִϸ����� �Ķ���� ���
            //Floatž���� �̿��ϹǷ� SetFloat�Լ��� ���� ���
            //ù��° �Ķ���ʹ� �Ķ���� �̸�, �� ��° �Ķ���ʹ� �ش� �Ķ������ ���簪(�ش� ������ �̵���)
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
            
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);


    }

    //Collider �浹�� ȣ��Ǵ� �Լ�
    void OnCollisionEnter2D(Collision2D other)
    {
        //Collisison2Dsms GetComponent �Լ��� ���� �㳪 gameObject�� �浹�� ���� �����������ֵ�
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }


    public void Fix()
    {
        broken = false;
        //���� �ý��� �ùķ��̼ǿ��� �ش� rigidbody�� ���� 
        rigidbody2D.simulated = false;

        animator.SetTrigger("Fixed");
    }


}

