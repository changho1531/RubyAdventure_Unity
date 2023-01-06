using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //프레임당 유닛 이동 조절
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Input에 "포함"된 GetAxis 함수를 도트연산자( . )를 사용해 호출
        //GetAxis 함수의 이름을 작성 후 이를 호출
        //파라미터 괄호 안의 단어 얻고자 하는 값 축 이름을 함수에게 알려준 거.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log(horizontal);
        Debug.Log(vertical);

        Vector2 position = transform.position;

        //deltaTime은 Unity가 한 프레임을 렌더링하는데 걸리는 시간을 가져온다
        //렌더링 시간으로 유닛 이동
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;
        transform.position = position;
    }
}
