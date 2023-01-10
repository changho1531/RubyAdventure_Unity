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
    //적의 방향
    int direction = 1;
    Animator animator;

    //로봇상태
    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        //해당 컴포넌트를 검색 가져와서 저장
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
            //애니메이터 파라미터 사용
            //Float탑입을 이용하므로 SetFloat함수를 통해 사용
            //첫번째 파라미터는 파라미터 이름, 두 번째 파라미터는 해당 파라미터의 현재값(해당 방향의 이동량)
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

    //Collider 충돌시 호출되는 함수
    void OnCollisionEnter2D(Collision2D other)
    {
        //Collisison2Dsms GetComponent 함수가 없다 허나 gameObject는 충돌에 대한 값을가지고있따
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }


    public void Fix()
    {
        broken = false;
        //물리 시스템 시뮬레이션에서 해당 rigidbody를 제거 
        rigidbody2D.simulated = false;

        animator.SetTrigger("Fixed");
    }


}

