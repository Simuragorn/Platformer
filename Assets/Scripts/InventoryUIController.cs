using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Cell[] cells;
    [SerializeField] private int cellsCount;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;
    void Init()
    {
        cells = new Cell[cellsCount];
        for (int i = 0; i < cellsCount; ++i)
        {
            cells[i] = Instantiate(cellPrefab, rootParent);
        }
        cellPrefab.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!cells?.Any() ?? false)
        {
            Init();
        }
        var playerInventory = PlayerInventory.Instance;
        for (int i = 0; i < playerInventory.Items.Count; ++i)
        {
            if (i < cells.Length)
                cells[i].Init(playerInventory.Items[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
