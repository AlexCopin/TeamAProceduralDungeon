using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public GameObject Projectile;
    public GameObject Playerattack;

    public Image tooltip;


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
                ItemSelector.RandomChoice(false);
            }
            if (!ItemSelector.gameObject.activeInHierarchy)
            {
                ItemSelector.gameObject.SetActive(true);
                ItemSelector.RandomChoice(false);
                Time.timeScale = 0;

            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Player.attackPrefab = Projectile;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Player.attackPrefab = Playerattack;
        }

    }

    void UpdateItem(int slot)
    {

        if(Item[slot] != null)
        {
            for(int i =0; i< Item[slot].Health; i++)
            {
                Player.Instance.life--;
            }

            for (int i = 0; i < Item[slot].MovementSpeed; i++)
            {
                Player.defaultMovement.speedMax--;
            }
            for (int i = 0; i < Item[slot].Damage; i++)
            {
                Player.Damage--;
            }

            if (Item[slot].RangedAttack)
            {
                Player.attackPrefab = Playerattack;
            }
            if (Item[slot].SpikeImmune)
            {
                Player.SpikeImmune = false;
            }



        }


        Item[slot] = ItemSelector.ItemSelected;
        for (int i = 0; i < Item[slot].Health; i++)
        {
            Player.Instance.life++;  
        }
        for (int i = 0; i < Item[slot].MovementSpeed; i++)
        {
            Player.defaultMovement.speedMax++;
        }
        for (int i = 0; i < Item[slot].Damage; i++)
        {
            Player.Damage++;

        }


        if (Item[slot].RangedAttack)
        {
            Player.attackPrefab = Projectile;

        }
        if (Item[slot].SpikeImmune)
        {
            Player.SpikeImmune = true;
        }
        Slot[slot].image.sprite = Item[slot].GetComponent<SpriteRenderer>().sprite;
        Slot[slot].gameObject.GetComponent<Item_Tooltip>().itemStats = Item[slot];
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
