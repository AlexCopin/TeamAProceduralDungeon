using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Selector : MonoBehaviour
{
    public Item_Stats[] ListItems;
    public Item_Stats[] ListItemsArtefact;

    public Button Button1;
    public Button Button2;

    public Item_Stats Item1;
    public Item_Stats Item2;
    public Item_Stats ItemSelected;

    public Item_Manager ItemManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RandomChoice(bool Artefact)
    {
        
        int random = Random.Range(0, ListItems.Length);
        Item1 = ListItems[random];
        
        random = Random.Range(0, ListItems.Length);
        Item2 = ListItems[random];


        Button1.image.sprite = Item1.GetComponent<SpriteRenderer>().sprite;
        Button1.gameObject.GetComponent<Item_Tooltip>().itemStats = Item1;

        Button2.image.sprite = Item2.GetComponent<SpriteRenderer>().sprite;
        Button2.gameObject.GetComponent<Item_Tooltip>().itemStats = Item2;


    }
    public void pickItem1(int item)
    {

        if (item == 1)
        {
            ItemSelected = Item1;
        }

        if (item == 2)
        {
            ItemSelected = Item2;
        }
        
    }
    

    

    
    

}
