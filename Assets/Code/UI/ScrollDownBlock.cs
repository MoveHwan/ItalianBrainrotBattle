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

        // 이전보다 스크롤이 위로 가려는 시도일 때 → 원래 값으로 되돌림
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

