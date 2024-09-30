using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI name_Text;
    [SerializeField] private TextMeshProUGUI count_Text;

    [Header("Ignore this text")]
    [SerializeField] private TextMeshProUGUI unlocked_Text;
    [SerializeField] private TextMeshProUGUI new_Discover_Text;


    private void Start()
    {
        image.CrossFadeAlpha(0, 0, false);
        name_Text.CrossFadeAlpha(0, 0, false);
        count_Text.CrossFadeAlpha(0, 0, false);
        unlocked_Text.CrossFadeAlpha(0, 0, false);
        new_Discover_Text.CrossFadeAlpha(0, 0, false);
    }
    public void NewDiscover(Recipe_Data data)
    {
        image.CrossFadeAlpha(0.3f, 2.0f, false);
        name_Text.CrossFadeAlpha(1f, 2.0f, false);
        count_Text.CrossFadeAlpha(1f, 2.0f, false);
        unlocked_Text.CrossFadeAlpha(1f, 2.0f, false);
        new_Discover_Text.CrossFadeAlpha(1f, 2.0f, false);

        count_Text.text = GC_Script.Instance.Database_Recipes.GetRecipesDiscovered().Count + " / " + GC_Script.Instance.Database_Recipes.GetRecipes().Count;
        name_Text.text = data.recipe.r_Name;

        StartCoroutine(FadeOff());
    }
    IEnumerator FadeOff()
    {
        yield return new WaitForSeconds(3);
        image.CrossFadeAlpha(0.0f, 2.0f, false);
        name_Text.CrossFadeAlpha(0.0f, 2.0f, false);
        count_Text.CrossFadeAlpha(0.0f, 2.0f, false);
        unlocked_Text.CrossFadeAlpha(0.0f, 2.0f, false);
        new_Discover_Text.CrossFadeAlpha(0.0f, 2.0f, false);
    }

}
