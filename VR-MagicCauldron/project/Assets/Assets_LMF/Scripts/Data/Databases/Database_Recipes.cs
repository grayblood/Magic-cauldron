using System.Collections.Generic;
using UnityEngine;
using static Recipe_Data;

public class Database_Recipes : MonoBehaviour
{
    public List<Recipe_Data> Recipes;
    private void Awake()
    {
        newGame();
    }
    private void newGame()
    {
        foreach (Recipe_Data data in Recipes)
        {
            data.recipe.discovered = false;

        }
    }
    public List<Recipe_Data> GetRecipes()
    {
        return Recipes;
    }
    public List<Recipe_Data> GetRecipesDiscovered()
    {
        List<Recipe_Data> recipesdisco = new List<Recipe_Data>();
        foreach(Recipe_Data data in Recipes)
        {
            if (data.recipe.discovered)
            {
                recipesdisco.Add(data);
            }
        }
        return recipesdisco;
    }
}
