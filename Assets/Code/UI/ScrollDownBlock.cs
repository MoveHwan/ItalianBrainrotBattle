using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollDownBlock : MonoBehaviour
{
    ScrollRect scrollRect;

    float lastScrollValue, current;

    void OnEnable()
    {
        scrollRect = GetComponent<ScrollRect>();

        lastScrollValue = scrollRect.verticalNormalizedPosition;
    }

    void Update()
    {
        current = scrollRect.verticalNormalizedPosition;

        // �������� ��ũ���� ���� ������ �õ��� �� �� ���� ������ �ǵ���
        if (current > lastScrollValue)
        {
            scrollRect.verticalNormalizedPosition = lastScrollValue;
        }
        else
        {
            lastScrollValue = current;
        }
    }
}

