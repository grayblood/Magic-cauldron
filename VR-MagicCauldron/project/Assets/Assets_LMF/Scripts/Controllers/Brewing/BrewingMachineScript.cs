using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BrewingMachineScript : MonoBehaviour
{
    List<string> ingredients = new List<string>();
    [SerializeField] VisualEffect exitpos;

    [SerializeField]
    Renderer pilot;

    bool b_MinigameBM;
    [SerializeField] GameEvent On_StartMinigameBM;

    private void Awake()
    {
        b_MinigameBM = false;
    }
    private void Start()
    {
        pilot.material.color = Color.red;
    }

    public void StartBrewingMachine()
    {
        if (CheckPilot() && !b_MinigameBM)
        {
            b_MinigameBM = true;
            On_StartMinigameBM.Raise();

        }
    }
    public void MinigameEnding()
    {
        if (ingredients.Count > 0)
        {
           
            spawnObject((Ingredient_Data)GC_Script.Instance.GetIngredient(ingredients), exitpos.transform.position);
        }
        RemoveAllIngredients();
        b_MinigameBM = false;
    }


    public void AddIngredient(Ingredient_Script ingredient)
    {
        ingredients.Add(ingredient.GetName());
        CheckPilot();
    }

    public void RemoveIngredient(Ingredient_Script ingredient)
    {
        ingredients.Remove(ingredient.GetName());
        CheckPilot();
    }

    public void RemoveAllIngredients()
    {
        ingredients.Clear();
        CheckPilot();
    }

    public bool CheckPilot()
    {
        Debug.Log("GC_Script.Instance.CheckIngredient_R(ingredients)" + GC_Script.Instance.CheckIngredient_R(ingredients));
        if (GC_Script.Instance.CheckIngredient_R(ingredients))
        {
            pilot.material.color = Color.green;
            return true;
        }
        else
        {
            pilot.material.color = Color.red;
            return false;
        }
    }
    void spawnObject(Ingredient_Data ingre, Vector3 pos)
    {
        Debug.Log("spawneando");
        GameObject a = Instantiate(ingre.ingredient, pos, Quaternion.identity);
        a.name = "Ingredient";
        exitpos.Play();
        exitpos.GetComponent<AudioSource>().Play();
        ingre.currentlySpawned++;
    }
}
