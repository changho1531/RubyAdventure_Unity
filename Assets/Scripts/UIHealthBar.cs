using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //정적멤버
    public static UIHealthBar instance { get; private set; }

    //using UnityEngine.UI; 임포트!
    public Image mask;
    float originalSize;

    //정적 인스턴스에 Awake 함수를 보관하는데,
    //이는 "현재 해당 함수를 실행하고 있는 오브젝트"를 의미하는 특수한 C# 키워드
    //게임이 시작되면 체력 바 스크립트는 Awake 함수를 호출하여 "instance"라는 정적 멤버 안에 보관합니다.
    //따라서, 임의의 스크립트에서 UIHealthBar.instance를 호출할 경우 스크립트에 반환되는 값은 씬에 있는 체력 바가 됩니다.
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //rect.width 화면크기 알아내기
        originalSize = mask.rectTransform.rect.width;
    }

    //Health가 0~1(1완충,0.5반피) 사이 값으로 변화시 호출
    public void SetValue(float value)
    {
        //크기 및 앵커 설정
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
