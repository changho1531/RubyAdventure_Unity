using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    //모든 오브젝트가 Rigidbody를 보유하는건 아니니 변수 선언
    Rigidbody2D rigidbody2D;
    //최대체력
    //public는 스크립트 밖에서 변수를 액세스 하는 의미, 인스펙터 창에 표시된다
    public int maxHealth = 5;
    public float speed = 3.0f;
    //현재체력
    int currentHealth;

    //다른 스크립트에서도 값이 변경되는 것을 방지하기위해 사용
    //프로퍼티 작성은 함수처럼 보이고 사용시 변수로 사용된다
    public int health {get{ return currentHealth; } }

    public float timeInvincible = 2.0f;

    //루비가 무적인지 판별
    bool isInvincible;
    //무적 지속시간
    float invincibleTimer;

    Animator animator;
    //루비가 가만히 있을시 바라보는 방향 지정
    //지정해 주지 않으면 상태머신은 어느 방향을 취해야 할지 모른다
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;



    void Start()
    {
        //프레임당 유닛 이동 조절
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        //캐릭터에존재하는 Rigidbody2D를 요구
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //게임 시작시 체력 최대치로 설정
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input에 "포함"된 GetAxis 함수를 도트연산자( . )를 사용해 호출
        //GetAxis 함수의 이름을 작성 후 이를 호출
        //파라미터 괄호 안의 단어 얻고자 하는 값 축 이름을 함수에게 알려준 거.
        //플레이어의 행동에 따라 -1 부터 1까지 설정 할수 있다
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        //Vector2 유형은 위치를 저장도 하지만 방향도 저장한다
        Vector2 move = new Vector2(horizontal, vertical);

        //컴퓨터가 부동 소수점 숫자를 저장하는 방식에 의해 정밀도가 일부 손실되므로 == 대신 Mathf.Approximately를 사용
        //0.0f어야 한는 상황에서 0.00000f가 나올 수 있다
        //Approximately는 이러한 비정확성을 염두에 두고 거의 동일하면 true값를 반환합니다.
        //루비가 이동 중 일때
        if (!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //벡터 정규화
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X",lookDirection.x);
        animator.SetFloat("Look Y",lookDirection.y);
        animator.SetFloat("Speed",move.magnitude);

        //transform 사용으로 루비가 이동시 오브젝트 충돌시 떨림현상이 일어난다
        //Vector2 position = transform.position;

        //Rigidbody2D 캐릭터 위치를 가져옴
        Vector2 position = rigidbody2D.position;

        //deltaTime은 Unity가 한 프레임을 렌더링하는데 걸리는 시간을 가져온다
        //렌더링 시간으로 유닛 이동
        //position.x = position.x + speed * horizontal * Time.deltaTime;
        //position.y = position.y + speed * vertical * Time.deltaTime;
        //transform.position = position;
        position = position + move * speed * Time.deltaTime;

        //Rigidbody2D 원하는 위치 이동 및 다른 콜라이더와 충돌시 멈춤
        rigidbody2D.MovePosition(position);


        //무적 상태일시
        if (isInvincible)
        {
            //지속시간 시간을 거꾸로 센다
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        //플레이어가 키를 누르는것을 감지
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        //레이캐스트(NPC와 대화하기)
        if (Input.GetKeyDown(KeyCode.X))
        {
            //레이는 시작 포인트, 방향 및 길이로 구성됩니다
            //루비의 발이 아닌 루비 중앙에서 수행, 루비가 바라보는 방향, 최대거리, 특정레이어만 실행을 위해 마스크에 속하지 않을시 무시
            //레이캐스트가 콜라이더와 닿는지 확인
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                //충돌이 있는지 확인한 다음 레이캐스트가 충돌한 오브젝트에서 NonPlayerCharacter 스크립트를 찾습니다.
                //이 오브젝트에 해당 스크립트가 있는 경우 대화상자를 표시합니다.
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if(character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }
    //캐릭터 체력 변경 함수
    //Clamp 체력값, 최소 체력, 최대체력 설정 체력값이 최소, 최대를 넘지X
    public void ChangeHealth(int amount)
    {
        //데미지를 입을시
        if (amount < 0)
        {
            //상태확인, 무적일 경우 함수 종료
            if (isInvincible)
                return;
            // hit 애니메이션을 트리거
            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        //maxHealth 대비 currentHealth의 비율값을 UIHealthBar SetValue 함수에 제공합니다.
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        //Instantiate(오브젝트,위치에 오브젝트복사본 생성, 회전값) 함수
        //Instantiate(오브젝트,rigidbody위치 발이아닌 손에위치 하기위해 약간 띄움, Quaternion회전을 표현하는 연산자.회전없음) 함수
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        //Launch(호출시 캐릭터가 바라보는 방향, 힘값 뉴튼단위)
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}
