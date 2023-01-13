using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //�������
    public static UIHealthBar instance { get; private set; }

    //using UnityEngine.UI; ����Ʈ!
    public Image mask;
    float originalSize;

    //���� �ν��Ͻ��� Awake �Լ��� �����ϴµ�,
    //�̴� "���� �ش� �Լ��� �����ϰ� �ִ� ������Ʈ"�� �ǹ��ϴ� Ư���� C# Ű����
    //������ ���۵Ǹ� ü�� �� ��ũ��Ʈ�� Awake �Լ��� ȣ���Ͽ� "instance"��� ���� ��� �ȿ� �����մϴ�.
    //����, ������ ��ũ��Ʈ���� UIHealthBar.instance�� ȣ���� ��� ��ũ��Ʈ�� ��ȯ�Ǵ� ���� ���� �ִ� ü�� �ٰ� �˴ϴ�.
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //rect.width ȭ��ũ�� �˾Ƴ���
        originalSize = mask.rectTransform.rect.width;
    }

    //Health�� 0~1(1����,0.5����) ���� ������ ��ȭ�� ȣ��
    public void SetValue(float value)
    {
        //ũ�� �� ��Ŀ ����
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
