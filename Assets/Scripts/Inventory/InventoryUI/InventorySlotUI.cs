using UnityEngine.EventSystems;

public class InventorySlotUI : SlotUI, IPointerClickHandler 
{ 
    public InventoryUI inventoryUI;

    public void assignInventoryUI(InventoryUI inventoryUIToAssign) {
        inventoryUI = inventoryUIToAssign;
    }

    public void OnPointerClick(PointerEventData eventData) {
        inventoryUI.SlotClicked(this, eventData);
    }
}
