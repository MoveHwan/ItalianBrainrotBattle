using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DirectionalScrollRect : ScrollRect
{
    Vector2 initialTouchPos;
    ScrollRect parentScrollRect;

    float lastVerticalPos;

    bool routeToParent, isDrag;

    protected override void Start()
    {
        base.Start();
        parentScrollRect = transform.parent.GetComponentInParent<ScrollRect>();

        if (parentScrollRect == this)
            parentScrollRect = null;
        else
            lastVerticalPos = verticalNormalizedPosition;
    }

    void Update()
    {
        if (!vertical) return;

        if (isDrag && parentScrollRect)
        {
            // 위로 스크롤 하려는 시도 감지해서 원위치 시키기
            if (verticalNormalizedPosition > lastVerticalPos)
            {
                verticalNormalizedPosition = lastVerticalPos;
            }
        }
        else
        {
            // 드래그가 끝났을 때만 마지막 위치 저장
            lastVerticalPos = verticalNormalizedPosition;
        }
    }



    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        base.OnInitializePotentialDrag(eventData);
        initialTouchPos = eventData.position;
        routeToParent = false;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;

        Vector2 delta = eventData.position - initialTouchPos;

        // 드래그 방향을 확인하여 부모로 전달 여부 결정
        routeToParent = Mathf.Abs(delta.x) > Mathf.Abs(delta.y);

        if (routeToParent && parentScrollRect != null)
        {
            parentScrollRect.OnBeginDrag(eventData);
        }
        else
        {
            base.OnBeginDrag(eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (routeToParent && parentScrollRect != null)
        {
            parentScrollRect.OnDrag(eventData);
        }
        else
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        if (routeToParent && parentScrollRect != null)
        {
            parentScrollRect.OnEndDrag(eventData);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
