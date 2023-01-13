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

    Animator animator;
    //��� ������ ������ �ٶ󺸴� ���� ����
    //������ ���� ������ ���¸ӽ��� ��� ������ ���ؾ� ���� �𸥴�
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;



    void Start()
    {
        //�����Ӵ� ���� �̵� ����
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        //ĳ���Ϳ������ϴ� Rigidbody2D�� �䱸
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //���� ���۽� ü�� �ִ�ġ�� ����
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input�� "����"�� GetAxis �Լ��� ��Ʈ������( . )�� ����� ȣ��
        //GetAxis �Լ��� �̸��� �ۼ� �� �̸� ȣ��
        //�Ķ���� ��ȣ ���� �ܾ� ����� �ϴ� �� �� �̸��� �Լ����� �˷��� ��.
        //�÷��̾��� �ൿ�� ���� -1 ���� 1���� ���� �Ҽ� �ִ�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        //Vector2 ������ ��ġ�� ���嵵 ������ ���⵵ �����Ѵ�
        Vector2 move = new Vector2(horizontal, vertical);

        //��ǻ�Ͱ� �ε� �Ҽ��� ���ڸ� �����ϴ� ��Ŀ� ���� ���е��� �Ϻ� �սǵǹǷ� == ��� Mathf.Approximately�� ���
        //0.0f��� �Ѵ� ��Ȳ���� 0.00000f�� ���� �� �ִ�
        //Approximately�� �̷��� ����Ȯ���� ���ο� �ΰ� ���� �����ϸ� true���� ��ȯ�մϴ�.
        //��� �̵� �� �϶�
        if (!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //���� ����ȭ
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X",lookDirection.x);
        animator.SetFloat("Look Y",lookDirection.y);
        animator.SetFloat("Speed",move.magnitude);

        //transform ������� ��� �̵��� ������Ʈ �浹�� ���������� �Ͼ��
        //Vector2 position = transform.position;

        //Rigidbody2D ĳ���� ��ġ�� ������
        Vector2 position = rigidbody2D.position;

        //deltaTime�� Unity�� �� �������� �������ϴµ� �ɸ��� �ð��� �����´�
        //������ �ð����� ���� �̵�
        //position.x = position.x + speed * horizontal * Time.deltaTime;
        //position.y = position.y + speed * vertical * Time.deltaTime;
        //transform.position = position;
        position = position + move * speed * Time.deltaTime;

        //Rigidbody2D ���ϴ� ��ġ �̵� �� �ٸ� �ݶ��̴��� �浹�� ����
        rigidbody2D.MovePosition(position);


        //���� �����Ͻ�
        if (isInvincible)
        {
            //���ӽð� �ð��� �Ųٷ� ����
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        //�÷��̾ Ű�� �����°��� ����
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        //����ĳ��Ʈ(NPC�� ��ȭ�ϱ�)
        if (Input.GetKeyDown(KeyCode.X))
        {
            //���̴� ���� ����Ʈ, ���� �� ���̷� �����˴ϴ�
            //����� ���� �ƴ� ��� �߾ӿ��� ����, ��� �ٶ󺸴� ����, �ִ�Ÿ�, Ư�����̾ ������ ���� ����ũ�� ������ ������ ����
            //����ĳ��Ʈ�� �ݶ��̴��� ����� Ȯ��
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                //�浹�� �ִ��� Ȯ���� ���� ����ĳ��Ʈ�� �浹�� ������Ʈ���� NonPlayerCharacter ��ũ��Ʈ�� ã���ϴ�.
                //�� ������Ʈ�� �ش� ��ũ��Ʈ�� �ִ� ��� ��ȭ���ڸ� ǥ���մϴ�.
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if(character != null)
                {
                    character.DisplayDialog();
                }
            }
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
            // hit �ִϸ��̼��� Ʈ����
            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        //maxHealth ��� currentHealth�� �������� UIHealthBar SetValue �Լ��� �����մϴ�.
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        //Instantiate(������Ʈ,��ġ�� ������Ʈ���纻 ����, ȸ����) �Լ�
        //Instantiate(������Ʈ,rigidbody��ġ ���̾ƴ� �տ���ġ �ϱ����� �ణ ���, Quaternionȸ���� ǥ���ϴ� ������.ȸ������) �Լ�
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        //Launch(ȣ��� ĳ���Ͱ� �ٶ󺸴� ����, ���� ��ư����)
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}
