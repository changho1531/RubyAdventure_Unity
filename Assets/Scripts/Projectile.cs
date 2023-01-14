using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    RubyController controller;
    //Start호출 시점은 오브젝트 생성후 다음 프레임에 호출된다
    //Awake는 오브젝트 생성 즉시(Instantiate가 호출될 때) 호출되므로
    //Launch 호출 전에 Rigidbody2D가 정상적으로 준비됩니다.
    //null reference exception error - Rigidbody2D 비어있다 null 포함
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    //오브젝트에 가해진 힘
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }


    void Update()
    {
        //position은 월드의 중심에서부터 오브젝트의 위치, magnitude는 해당 벡터의 길이가 된다
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    //오브젝트에 충돌 시 호출
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
