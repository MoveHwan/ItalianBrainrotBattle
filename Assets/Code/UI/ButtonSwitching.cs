using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitching : MonoBehaviour
{
    public ScrollHelper ScrollHelper;

    public GameObject[] Buttons;

    int prevIdx = -1;

    private void Update()
    {
        if (ScrollHelper.targetIdx != prevIdx)
        {
            prevIdx = ScrollHelper.targetIdx;

            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(i == prevIdx);
            }
        }

    }
}
