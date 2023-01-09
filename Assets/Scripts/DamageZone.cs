using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //OnTriggerStay2D �Լ��� Ʈ���� �۵� �� �������Ӹ��� ȣ��
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent <RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}