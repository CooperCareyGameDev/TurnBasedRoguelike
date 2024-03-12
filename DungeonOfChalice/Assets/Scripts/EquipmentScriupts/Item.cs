using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Item 
{
    public abstract string GiveName();
    public virtual void Update(Player player, int stacks)
    {

    }
    public virtual void OnHit(Player player, Enemy enemy, int stacks)
    {

    }
}


public class DamageBuffItem : Item
{
    public override string GiveName()
    {
        return "Damage Buff Item";
    }

    public override void Update(Player player, int stacks)
    {
        Debug.Log("Item testing");
    }

}
