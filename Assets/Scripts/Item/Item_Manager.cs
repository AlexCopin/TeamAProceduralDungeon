using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Manager : MonoBehaviour
{
    public Item_Stats[] Item;
    public Button[] Slot;
    private Item_Stats ItemStats;
    public int Health;
    private int Damage;
    private int MovementSpeed;
    private int FireRate;

    public Item_Selector ItemSelector;

    public Player Player;


    // Start is called before the first frame update
    void Start()
    {

        
        Player.life = Health;

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
                Time.timeScale = 0;

            }

        }

    }

    void UpdateItem(int slot)
    {

        if(Item[slot] != null)
        {
            Player.life -= Item[slot].Health;

        }


        Item[slot] = ItemSelector.ItemSelected;

        Player.life += Item[slot].Health;

        Slot[slot].image.sprite = Item[slot].GetComponent<SpriteRenderer>().sprite;
        Debug.Log("2");




    }



    public void AddItemToSlot(int slot)
    {
        if (ItemSelector.ItemSelected != null)
        {

        
            if (slot == 1)
            {
                UpdateItem(0);
                Debug.Log("1");
            }
            if (slot == 2)
            {
                UpdateItem(1);

            }
            if (slot == 3)
            {
                UpdateItem(2);

            }
            ItemSelector.ItemSelected = null;
            ItemSelector.gameObject.SetActive(false);


            Time.timeScale = 1;

        }



    }


}
