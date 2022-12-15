using UnityEngine;
using UnityEngine.InputSystem;

public class MouseItemData : SlotUI
{ 
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        GameObject canvasObject = GameObject.Find("Canvas");

        transform.SetParent(canvasObject.transform, false);

        transform.position = Vector3.zero;

        canvas = canvasObject.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 newPos = mousePosition / canvas.scaleFactor;

        rectTransform.anchoredPosition = new Vector2(newPos.x - 480, newPos.y - 280);
        //Debug.Log("pos: " + newPos.x + ", " + newPos.y);
    }
}