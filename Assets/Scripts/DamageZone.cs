using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //OnTriggerStay2D �Լ��� �������Ӹ��� ȣ��
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent <RubyController>();
        if(controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(-1);
            }
        }
    }
}
