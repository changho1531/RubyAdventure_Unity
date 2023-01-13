using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    //��ȭ ���� ǥ�� �� �ð�
    public float displayTime = 4.0f;
    //ĵ����(��ȭ����) Ȱ��ȭ/��Ȱ��ȭ
    public GameObject dialogBox;
    //��ȭ ���� ǥ��
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
        //��ȭ ���ڰ� ǥ�����϶�
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
