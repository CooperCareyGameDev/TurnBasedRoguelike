using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public List<ItemList> items = new List<ItemList>();
    // Start is called before the first frame update
    void Start()
    {
        DamageBuffItem item = new DamageBuffItem();
        items.Add(new ItemList(item, item.GiveName(), 1));
        StartCoroutine(CallItemUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CallItemUpdate()
    {
        foreach (ItemList i in items)
        {
            i.item.Update(this, i.stacks);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CallItemUpdate());
    }
}
