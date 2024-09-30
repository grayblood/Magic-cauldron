using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PData", menuName = "ScriptableObjects/Item/Potion", order = 1)]
public class Potion_Data : Object_Data
{
    public string p_Name;
    public Material potion_Material;
    public int money_Sell;
}
 