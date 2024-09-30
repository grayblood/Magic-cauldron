using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient_Script : MonoBehaviour
{
    [SerializeField]
    Ingredient_Data data;

    public string GetName()
    {
        return data.i_Name;
    }
    public Ingredient_Data GetIData()
    {
        return data;
    }
}
