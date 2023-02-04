using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float speed = 150;
        
    private DragManager manager = null;

    [HideInInspector]
    public Vector2 centerPoint;
    private Vector2 worldCenterPoint => transform.TransformPoint(centerPoint);
    [SerializeField]
    private bool isSet = false;
    [SerializeField]
    private bool isLocked = false;
    private bool moveToOrigin = false;
    private GameObject target;

    // Start is called before the first frame update
    private void Awake()
    {
        manager = FindObjectOfType<DragManager>();
        centerPoint = (transform as RectTransform).anchoredPosition;
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!isLocked)
        {
            manager.RegisterDraggedObject(this);
            if (target != null)
            {
                target.GetComponent<Image>().raycastTarget = true;
                target.GetComponent<Frame>().currentCharItem = null;
                target = null;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        (transform as RectTransform).anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            manager.UnregisterDraggedObject(this);
            checkWhereDrop(eventData);

            if (!isSet)
            {
                moveToOrigin = true;
            }
        }
    }

    private void checkWhereDrop(PointerEventData eventData)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult r in raycastResults)
        {
            if (r.gameObject.CompareTag("Finish"))
            {
                centerPoint = (transform as RectTransform).anchoredPosition;
                isSet = false;
                break;
            }
            else if (r.gameObject.CompareTag("Respawn"))
            {
                target = r.gameObject;
                (transform as RectTransform).anchoredPosition = target.GetComponent<RectTransform>().anchoredPosition + new Vector2(0,-2);
                target.GetComponent<Image>().raycastTarget = false;
                target.GetComponent<Frame>().currentCharItem = this.GetComponent<CharacterItem>();
                
                isSet = true;
            }
            else
            {
                isSet = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(moveToOrigin)
        {
            float deltaX = (transform as RectTransform).anchoredPosition.x - centerPoint.x;
            float signX = Mathf.Sign(deltaX);
            
            float deltaY = (transform as RectTransform).anchoredPosition.y - centerPoint.y;
            float signY = Mathf.Sign(deltaY);

            float maxX = 10;
            float maxY = 10;
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY) && Mathf.Abs(deltaY) > 1)
            {
                maxY = 10 * Mathf.Abs(deltaY / deltaX);
            }
            else if(Mathf.Abs(deltaX) > 1)
            {
                maxX = 10 * Mathf.Abs(deltaX / deltaY);
            }


            float x = Mathf.Min(maxX * speed * Time.fixedDeltaTime, Mathf.Abs(deltaX));
            float y = Mathf.Min(maxY * speed * Time.fixedDeltaTime, Mathf.Abs(deltaY));

            (transform as RectTransform).anchoredPosition -= new Vector2(signX * x, signY * y);

            if(Mathf.Abs(deltaX) < 2 * speed * Time.fixedDeltaTime && Mathf.Abs(deltaY) < 1)
            {
                moveToOrigin = false;
            }
        }
    }

}
