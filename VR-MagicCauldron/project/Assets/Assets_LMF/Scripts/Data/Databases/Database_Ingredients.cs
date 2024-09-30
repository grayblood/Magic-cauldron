using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Ingredients : MonoBehaviour
{
    public List<Ingredient_Data> ingre_Data = new List<Ingredient_Data>();
    private void Awake()
    {
        foreach (Ingredient_Data data in ingre_Data)
        {
            data.avalaible = false;

        }
        ingre_Data[0].avalaible = true;
        ingre_Data[1].avalaible = true;
        ingre_Data[2].avalaible = true;
    }

}
