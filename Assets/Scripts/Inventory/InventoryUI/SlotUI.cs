using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour 
{ 
    protected InventorySlot assignedInventorySlot = new InventorySlot();
    public Image icon;
    public TextMeshProUGUI stackSizeText;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;

    public void OnEnable() {
        if (assignedInventorySlot != null) {
            assignedInventorySlot.OnSlotChange += UpdateSlot;
        }
    }

    protected void OnDisable() {
        assignedInventorySlot.OnSlotChange -= UpdateSlot;
    }

    public void assignSlot(InventorySlot slotToAssign) {
        assignedInventorySlot = slotToAssign;
    }

    public void ClearSlot() {
        icon.enabled = false;
        stackSizeText.enabled = false;
    }

    public void DrawSlot(InventorySlot slot) {
        if (slot.isEmpty()) {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        stackSizeText.enabled = true;

        icon.sprite = slot.ItemData.icon;
        stackSizeText.text = slot.StackSize.ToString();
    }

    public void UpdateSlot() {
        DrawSlot(assignedInventorySlot);
    }
}