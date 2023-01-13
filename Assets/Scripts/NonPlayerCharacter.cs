using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    //대화 상자 표시 할 시간
    public float displayTime = 4.0f;
    //캔버스(대화상자) 활성화/비활성화
    public GameObject dialogBox;
    //대화 상자 표시
    float timerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //대화 상자가 표시중일때
        if(timerDisplay >= 0)
        {   
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
