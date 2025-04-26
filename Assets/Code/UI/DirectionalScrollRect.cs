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
            // ���� ��ũ�� �Ϸ��� �õ� �����ؼ� ����ġ ��Ű��
            if (verticalNormalizedPosition > lastVerticalPos)
            {
                verticalNormalizedPosition = lastVerticalPos;
            }
        }
        else
        {
            // �巡�װ� ������ ���� ������ ��ġ ����
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

        // �巡�� ������ Ȯ���Ͽ� �θ�� ���� ���� ����
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
