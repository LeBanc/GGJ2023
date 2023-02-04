using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform
        defaultLayer = null,
        dragLayer = null;

    private Rect boundingBox;

    private DragItem currentDraggedObject = null;
    public DragItem CurrentDraggedObject => currentDraggedObject;

    private void Awake()
    {
        SetBoundingBoxRect(dragLayer);
    }

    public void RegisterDraggedObject(DragItem drag)
    {
        currentDraggedObject = drag;
        drag.transform.SetParent(dragLayer);
    }

    public void UnregisterDraggedObject(DragItem drag)
    {
        drag.transform.SetParent(defaultLayer);
        currentDraggedObject = null;
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return boundingBox.Contains(position);
    }

    private void SetBoundingBoxRect(RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        boundingBox = new Rect(position, size);
    }
}