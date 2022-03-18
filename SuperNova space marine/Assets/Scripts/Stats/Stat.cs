using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    //Variables
    [SerializeField]
    private int baseValue;
    private List<int> modifiers = new List<int>();

    //Functions

    public int GetValue() //returns an int which is the sum of the modifiers
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }
    public void AddModifier(int modifier) //Adds the modifier parameter to the 
    {                                     //list of modifier values
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }
    public void RemoveModifier(int modifier)//Removes modifier parameter from
    {                                       //the list of modifier values
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
