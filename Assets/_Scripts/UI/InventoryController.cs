using Inventory.UI;
using Inventory.Model;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryPage;

        [SerializeField]
        private InventorySO inventoryData;
        private void Start()
        {
            PrepareUI();
            //inventoryData.Initialize();
        }

        private void PrepareUI()
        {
            inventoryPage.InitializeInventoryUI(inventoryData.Size);
            inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryPage.OnSwapItems += HandleSwapItems;
            inventoryPage.OnStartDragging += HandleDragging;
            inventoryPage.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {

        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {

        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem item = inventoryData.GetItemAt(itemIndex);
            if (item.IsEmpty)
            {
                return;
            }

            ItemSO itemso = item.item;
            inventoryPage.UpdateDescription(itemIndex, itemso.ItemImage, itemso.name, itemso.Description);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!inventoryPage.isActiveAndEnabled)
                {
                    inventoryPage.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryPage.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    inventoryPage.Hide();
                }
            }
        }
    }
}