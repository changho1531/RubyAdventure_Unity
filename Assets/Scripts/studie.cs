using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class studie : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    //플레이어가 가만히 있을시 바라보는 방향
    //지정해 주지 않으면 상태머신은 어느 방향을 취해야 할지 모른다
    Vector2 lookDirection = new Vector2(1, 0);

    //던질 오브젝트
    public GameObject projectilePrefab;

    public float speed;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
        
    // Update is called once per frame
    void Update()
    {
        //파라미터 괄호 안의 단어 얻고자 하는 값 축 이름을 저장
        //누른 방향키를 가져와 저장
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //이동 방향 저장
        Vector2 move = new Vector2(horizontal, vertical);

        //transform 사용으로 이동시 오브젝트 충돌시 떨림현상이 일어난다
        //캐릭터 위치를 가져온다
        Vector2 position = rigidbody2D.position;

        //deltaTime은 Unity가 한 프레임을 렌더링하는데 걸리는 시간을 가져온다
        //없을시 속도가 매 프레임마다 가속이 붙는다
        //렌더링 시간으로 유닛 이동
        position = position + move * speed * Time.deltaTime;

        //Rigidbody2D 원하는 위치 이동 및 다른 콜라이더와 충돌시 멈춤
        rigidbody2D.MovePosition(position);


        //플레이어가 키를 누르는것을 감지
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    void Launch()
    {
        //오브젝트 복사
        //Instantiate(오브젝트,위치에 오브젝트복사본 생성, 회전값) 함수
        //Instantiate(오브젝트,rigidbody위치 발이아닌 손에위치 하기위해 약간 띄움, Quaternion회전을 표현하는 연산자.회전없음) 함수
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        //Launch(호출시 캐릭터가 바라보는 방향, 힘값 뉴튼단위)
        projectile.Launch(lookDirection, 300);
    }
}
