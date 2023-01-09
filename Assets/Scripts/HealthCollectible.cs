using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //�� Rigidbody�� Trigger�� ������ ������ �� OnTriggerEnter2D �Լ��� ȣ���մϴ�.
    //ther �ĸ����ʹ� Ʈ���ſ� ������ Collider�� ������ �ִ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Ʈ���ſ� ������ �ݶ��̴��� ���� ������Ʈ�� �ִ� RubyController ������Ʈ�� �׼���
        //��� Ʈ���� �۵��� if���� �۵��Ͽ� ü���� �ö󰡰Բ� �ϱ�����
        //��� ���� �ڸ� ���� ������Ʈ�� �Ծ �۵� X
        RubyController controller = other.GetComponent<RubyController>();
        if(controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                //����Ƽ �⺻�Լ� �Ķ���ͷ� ������ ���� ����,��ũ��Ʈ�� ������ ���� ������Ʈ������
                Destroy(gameObject);
            }
            
        }
    }
}
