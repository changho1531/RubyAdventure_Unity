using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //OnTriggerStay2D 함수는 매프레임마다 호출
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
