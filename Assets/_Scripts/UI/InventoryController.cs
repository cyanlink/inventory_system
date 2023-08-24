using Inventory.UI;
using Inventory.Model;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryPage;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new();
        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach(var item in initialItems)
            {
                if(item.IsEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryPage.ResetAllItems();
            foreach(var item in inventoryState)
            {
                inventoryPage.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
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
            InventoryItem item = inventoryData.GetItemAt(itemIndex);
            if (item.IsEmpty) return;
            inventoryPage.CreateDraggedItem(item.item.ItemImage, item.quantity);
        }


        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem item = inventoryData.GetItemAt(itemIndex);
            if (item.IsEmpty)
            {
                inventoryPage.ResetSelection();
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