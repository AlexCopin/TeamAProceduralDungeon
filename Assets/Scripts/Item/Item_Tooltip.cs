using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class Item_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image tooltip;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public int Corectx;
    public int Corecty;

    public Item_Stats itemStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        

        if(itemStats!= null)
        {
            Debug.Log("lol");
            Name.text = itemStats.Name;
            Description.text = itemStats.Description;
            tooltip.gameObject.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(this.transform.position);
            tooltip.rectTransform.position = new Vector2(this.GetComponent<RectTransform>().position.x + Corectx, this.GetComponent<RectTransform>().position.y + Corecty);
        }
        else {
        }


        
        
        

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(tooltip.gameObject.activeSelf)
        tooltip.gameObject.SetActive(false);
        


    }


}
