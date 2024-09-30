using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "IData", menuName = "ScriptableObjects/Item/Ingredient", order = 1)]
public class Ingredient_Data : Object_Data
{
    public string i_Name;
    public GameObject ingredient;
    public Sprite image;
    public int price;
    public int currentlySpawned;
    public bool avalaible;

}
