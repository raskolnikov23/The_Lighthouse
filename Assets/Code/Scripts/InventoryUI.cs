using TMPro;
using UnityEngine;

// to-do:
// ----- change color of title
// ----- dynamically adjust spacing
// ----- redo how position of line is calculated


public class InventoryUI : MonoBehaviour
{
    #region declarations
    
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private int spacingAmount = 20;
    //[SerializeField] private int startingOffsetY = 10;
    [SerializeField] private Color32 lineColor;

    public TextMeshProUGUI[] inventoryLines;
    private int activeSlotIndex = 0;
    
    #endregion
    
    
    private void Start()
    {
        InitializeUI();
    }
    private void Update()
    {

        // for each line..
        for (int i = 1; i < inventoryLines.Length; i++) // remake this based on an event. How??
        {
            // set color from color picker
            inventoryLines[i].color = lineColor;
            
            // set spacing between lines
            inventoryLines[i].rectTransform.anchoredPosition = new Vector2(10, inventoryLines[i - 1].rectTransform.anchoredPosition.y - spacingAmount);
            
            // set color of title
            

        }
    }


    // INITIALIZATION
    private void InitializeUI()
    {
        // initialize default color
        lineColor = new Color32(181, 255, 21, 255);
        
        // get child text objects 
        inventoryLines = GetComponentsInChildren<TextMeshProUGUI>();
        
        // set title text
        inventoryLines[0].text = "Inventory:";
        
        // get currently active slot 
        activeSlotIndex = inventoryManager.currentSlot;
        
        
        // FOR EVERY LINE
        for (int i = 1; i < inventoryLines.Length; i++)
        {
            
            // sets position
            inventoryLines[i].rectTransform.anchoredPosition = new Vector2
                (10, inventoryLines[i - 1].rectTransform.anchoredPosition.y - spacingAmount);
            
            // sets color
            inventoryLines[i].color = lineColor;

            // sets font size
            inventoryLines[i].fontSize = 20;

            // sets line numbers
            inventoryLines[i].text = i.ToString() + " ";
            
            // sets current line bold
            inventoryLines[activeSlotIndex].fontStyle = FontStyles.Bold;
        }
    }
    
    
    private void EditLine(int id)
    {
        // changes line content
        TextMeshProUGUI line = inventoryLines[id+1]; // +1 because we ignore the title
        
        if (inventoryManager.itemArray[id] == null)
        {
            // when item is removed from inventory
            line.text = (id + 1).ToString();
            return;
        }
        line.text = (id + 1) + " " + inventoryManager.itemArray[id].itemName;
    }
    
    // ON INVENTORY SLOT SWITCH
    private void OnSlotSwitch(int id)
    {
        // removes bold from previously active line
        inventoryLines[activeSlotIndex].fontStyle = FontStyles.Normal;
        
        // boldens new line
        inventoryLines[id].fontStyle = FontStyles.Bold;
        
        // re-sets active slot index
        activeSlotIndex = id;
    }
    
    
    private void OnEnable()
    {
        inventoryManager.ItemAdd += EditLine;
        inventoryManager.ToolbarItemDrop += EditLine;
        inventoryManager.SlotSwitch += OnSlotSwitch;
    }
    private void OnDisable()
    {
        inventoryManager.ItemAdd -= EditLine;
        inventoryManager.ToolbarItemDrop -= EditLine;
        inventoryManager.SlotSwitch -= OnSlotSwitch;
    }
}
