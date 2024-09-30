using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabletReceiptsScript : MonoBehaviour
{
    List<Recipe_Data> recipesUnlocked;
    public int pos = 0;

    [SerializeField] TextMeshProUGUI recipeName;

    [SerializeField] List<IngredientInfoReceta> ingredientInfoReceta = new List<IngredientInfoReceta>();


    private void Start()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        recipesUnlocked = GC_Script.Instance.GetDatabase_Recipes().GetRecipesDiscovered();
        //pos = 0;
        ChangeRecipe(0);
    }
    public void ChangeRecipe(int number)
    {
        if (recipesUnlocked.Count >= 1)
        {
            pos += number;
            if (pos < 0)
            {
                pos = recipesUnlocked.Count - 1;
            }
            else if (pos >= recipesUnlocked.Count)
            {
                pos = 0;
            }
            recipeName.text = recipesUnlocked[pos].recipe.r_Name;
            if (recipesUnlocked[pos].recipe.ingredients.Count == 2)
            {

                ingredientInfoReceta[0].SetInformation(recipesUnlocked[pos].recipe.ingredients[0]);
                ingredientInfoReceta[1].SetInformation(recipesUnlocked[pos].recipe.ingredients[1]);
                ingredientInfoReceta[2].gameObject.SetActive(false);

            }
            else
            {
                ingredientInfoReceta[0].SetInformation(recipesUnlocked[pos].recipe.ingredients[0]);
                ingredientInfoReceta[1].SetInformation(recipesUnlocked[pos].recipe.ingredients[1]);
                ingredientInfoReceta[2].gameObject.SetActive(true);
                ingredientInfoReceta[2].SetInformation(recipesUnlocked[pos].recipe.ingredients[2]);

            }
        }
    }
}
