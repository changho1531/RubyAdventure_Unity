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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        

        Vector2 position = rigidbody2D.position;
        if (vertical)
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        else
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }

        rigidbody2D.MovePosition(position);
    }

    //Collider 충돌시 호출되는 함수
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Collisison2Dsms GetComponent 함수가 없다 허나 gameObject는 충돌에 대한 값을가지고있따
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if(player != null)
        {
            player.ChangeHealth(-1);

        }
        
    }


}

