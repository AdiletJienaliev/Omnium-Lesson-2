using UnityEngine;
using UnityEngine.EventSystems;

public class HandleControlls : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2 startPosition;
    public Vector2 deltaPosition;

    public Vector2 moveDirection;
    private void Awake()
    {
        NewControls newControls = new NewControls();
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - startPosition;
        direction.Normalize();
        deltaPosition = direction;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        deltaPosition = Vector2.zero;
    }
}
