using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryPage;

    public int inventorySize = 10;
    private void Start()
    {
        inventoryPage.InitializeInventoryUI(inventorySize);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!inventoryPage.isActiveAndEnabled)
            {
                inventoryPage.Show();

            }
            else
            {
                inventoryPage.Hide();
            }
        }
    }
}
