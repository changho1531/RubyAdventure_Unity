using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //��� ������Ʈ�� Rigidbody�� �����ϴ°� �ƴϴ� ���� ����
    Rigidbody2D rigidbody2D;
    //�ִ�ü��
    //public�� ��ũ��Ʈ �ۿ��� ������ �׼��� �ϴ� �ǹ�, �ν����� â�� ǥ�õȴ�
    public int maxHealth = 5;
    public float speed = 3.0f;
    //����ü��
    int currentHealth;

    //�ٸ� ��ũ��Ʈ������ ���� ����Ǵ� ���� �����ϱ����� ���
    //������Ƽ �ۼ��� �Լ�ó�� ���̰� ���� ������ ���ȴ�
    public int health {get{ return currentHealth; } }

    public float timeInvincible = 2.0f;

    //��� �������� �Ǻ�
    bool isInvincible;
    //���� ���ӽð�
    float invincibleTimer;

    void Start()
    {
        //�����Ӵ� ���� �̵� ����
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        //ĳ���Ϳ������ϴ� Rigidbody2D�� �䱸
        rigidbody2D = GetComponent<Rigidbody2D>();

        //���� ���۽� ü�� �ִ�ġ�� ����
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Input�� "����"�� GetAxis �Լ��� ��Ʈ������( . )�� ����� ȣ��
        //GetAxis �Լ��� �̸��� �ۼ� �� �̸� ȣ��
        //�Ķ���� ��ȣ ���� �ܾ� ����� �ϴ� �� �� �̸��� �Լ����� �˷��� ��.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vector2 position = transform.position;

        //Rigidbody2D ĳ���� ��ġ�� ������
        Vector2 position = rigidbody2D.position;

        //deltaTime�� Unity�� �� �������� �������ϴµ� �ɸ��� �ð��� �����´�
        //������ �ð����� ���� �̵�
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        //transform.position = position;

        //Rigidbody ���ϴ� ��ġ �̵� �� �ٸ� �ݶ��̴��� �浹�� ����
        rigidbody2D.MovePosition(position);


        //���� �����Ͻ�
        if (isInvincible)
        {
            //���ӽð� - Ÿ�̸�(�ð��� �Ųٷμ���)
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }
    //ĳ���� ü�� ���� �Լ�
    //Clamp ü�°�, �ּ� ü��, �ִ�ü�� ���� ü�°��� �ּ�, �ִ븦 ����X
    public void ChangeHealth(int amount)
    {
        //�������� ������
        if (amount < 0)
        {
            //����Ȯ��, ������ ��� �Լ� ����
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth +"/"+maxHealth);
    }
}
