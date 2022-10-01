using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngame : MonoBehaviour
{
    [SerializeField] Transform itemList;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] Player player;

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        foreach(Transform t in itemList)
        {
            Destroy(t.gameObject);
        }

        foreach (Collectable c in player.CollectedItems)
        {
            GameObject ItemUI = Instantiate(ItemPrefab, itemList);
            ItemUI.GetComponentInChildren<Image>().sprite = c.UIImage;
        }
    }
}
