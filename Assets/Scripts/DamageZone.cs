using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public AudioClip collectedClip;
    //OnTriggerStay2D 함수는 트리거 작동 시 매프레임마다 호출
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent <RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
