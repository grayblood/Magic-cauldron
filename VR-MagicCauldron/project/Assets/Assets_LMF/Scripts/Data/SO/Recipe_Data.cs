using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RData", menuName = "ScriptableObjects/data/Recipe", order = 1)]
public class Recipe_Data : ScriptableObject
{
    [Serializable]
    public class Recipe
    {
        public string r_Name;
        public RecipeType type;
        public Object_Data o_Data;
        public List<Ingredient_Data> ingredients;
        
        public bool discovered;
    }
    [SerializeField]
    public Recipe recipe;
}
