using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Manager : MonoBehaviour
{
    public Item_Stats[] Item;
    public Button[] Slot;
    private Item_Stats ItemStats;
    private int Health;
    private int Damage;
    private int MovementSpeed;
    private int FireRate;

    public Item_Selector ItemSelector;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            if(ItemSelector.gameObject.activeInHierarchy)
            {
                ItemSelector.RandomChoice();
            }
            if (!ItemSelector.gameObject.activeInHierarchy)
            {
                ItemSelector.gameObject.SetActive(true);
                ItemSelector.RandomChoice();

            }

        }

    }

    void UpdateItem()
    {
        Health = 0;
        Damage = 0;
        MovementSpeed = 0;
        FireRate = 0;
        for (int i = 0; i < 3; i++)
        {
            if(Item[i] != null)
            {

                Health += Item[i].Health;
                Damage += Item[i].Damage;
                MovementSpeed += Item[i].MovementSpeed;
                FireRate += Item[i].FireRate;
            }
            

        }
    }

    public void AddItemToSlot(int slot)
    {
        if (ItemSelector.ItemSelected != null)
        {

        
            if (slot == 1)
            {
                Item[0] = ItemSelector.ItemSelected;
                Slot[0].image.sprite = Item[0].GetComponent<SpriteRenderer>().sprite;
            }
            if (slot == 2)
            {
                Item[1] = ItemSelector.ItemSelected;
                Slot[1].image.sprite = Item[1].GetComponent<SpriteRenderer>().sprite;
            }
            if (slot == 3)
            {
                Item[2] = ItemSelector.ItemSelected;
                Slot[2].image.sprite = Item[2].GetComponent<SpriteRenderer>().sprite;
            }
            ItemSelector.ItemSelected = null;
            ItemSelector.gameObject.SetActive(false);


            UpdateItem();
        }



    }


}
