using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    //새 Rigidbody가 Trigger에 진입할 때마다 이 OnTriggerEnter2D 함수를 호출합니다.
    //ther 파리미터는 트리거에 진입한 Collider를 가지고 있다
    private void OnTriggerEnter2D(Collider2D other)
    {
        //트리거에 진입한 콜라이더의 게임 오브젝트에 있는 RubyController 컴포넌트를 액세스
        //루비만 트리거 작동시 if문이 작동하여 체력이 올라가게끔 하기위함
        //루비 제외 자른 게임 오브젝트가 먹어도 작동 X
        RubyController controller = other.GetComponent<RubyController>();
        if(controller != null)
        {
            if(controller.health < controller.maxHealth)//health는 변수(하얀색), playSound는 함수(노란색)
            {
                controller.ChangeHealth(1);
                //유니티 기본함수 파라미터로 전달한 것을 제거,스크립트가 부착된 게임 오브젝트를제거
                Destroy(gameObject);
                controller.PlaySound(collectedClip);
            }
            
        }
    }
}
