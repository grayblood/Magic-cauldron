using OdinSerializer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public enum RecipeType { Potion, Ingredient }
public class GC_Script : MonoBehaviour
{


    Database_Recipes database_Recipes;
    Database_Unlockables database_Unlockables;

    [HideInInspector] public Database_Recipes Database_Recipes { get { return database_Recipes; } }
    [HideInInspector] public Database_Unlockables Database_Unlockables { get { return database_Unlockables; } }

    [Header("Prefab")]

    [SerializeField] GameObject potion_Prefab;

    [Header("Other Objects")]

    [SerializeField] VisualEffect potionExit;
    [SerializeField] PlayerCanvasScript playerCanvasScript;

    [Header("Gold")]

    [SerializeField] public int gold;

    [Header("Events")]

    [SerializeField] GameEvent updateList;
    [SerializeField] GameEvent updateMoney;
    [SerializeField] GameEvent On_Discover;

    public static GC_Script Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        database_Recipes = GetComponent<Database_Recipes>();
        database_Unlockables = GetComponent<Database_Unlockables>();

        foreach (Ingredient_Data d in GetComponent<Database_Ingredients>().ingre_Data)
        {
            d.currentlySpawned = 0;
        }
        Audio_Controller.Initialize();
        gold = 0;
    }
    private void Start()
    {
        if (GMScript.Instance != null)
        {
            if (GMScript.Instance.load)
            {
                LoadData();
            }
        }
    }
    public void ChangeMoneyValues(int amount)
    {
        gold += amount;
        updateMoney.Raise();
    }
    //The COMBINER Main functions

    public bool CheckPotion_R(List<string> data) //check if exist
    {
        if (data.Count != 0)
        {
            data = data.OrderBy(e => e).ToList();
            foreach (Recipe_Data dat in database_Recipes.Recipes)
            {
                if (dat.recipe.ingredients.Count() == data.Count() && dat.recipe.type == RecipeType.Potion)
                {
                    List<string> listdat = new List<string>();
                    for (int i = 0; i < dat.recipe.ingredients.Count(); i++)
                    {
                        listdat.Add(dat.recipe.ingredients[i].i_Name);
                    }
                    listdat = listdat.OrderBy(e => e).ToList();

                    bool isEqual = Enumerable.SequenceEqual(data, listdat);
                    if (isEqual)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void GetPotion(List<string> data)
    {
        if (data.Count != 0)
        {
            data = data.OrderBy(e => e).ToList();
            foreach (Recipe_Data dat in database_Recipes.Recipes)
            {
                if (dat.recipe.ingredients.Count() == data.Count() && dat.recipe.type == RecipeType.Potion)
                {
                    List<string> listdat = new List<string>();

                    for (int i = 0; i < dat.recipe.ingredients.Count(); i++)
                    {
                        listdat.Add(dat.recipe.ingredients[i].i_Name);
                    }
                    listdat = listdat.OrderBy(e => e).ToList();

                    bool isEqual = Enumerable.SequenceEqual(data, listdat);
                    if (isEqual)
                    {
                        GameObject potion = Instantiate(potion_Prefab, potionExit.transform.position, Quaternion.identity);
                        potionExit.Play();
                        potionExit.GetComponent<AudioSource>().Play();
                        if (!dat.recipe.discovered)
                        {
                            DiscoverRecipe(dat);
                            SearchUnlocks();
                        }
                        potion.GetComponent<PotionScript>().SetDataPotion(dat.recipe.o_Data);
                        return;
                    }

                }
                else { print("nope"); }
            }
        }
    }
    public bool CheckIngredient_R(List<string> data) //check if exist
    {
        if (data.Count != 0)
        {
            data = data.OrderBy(e => e).ToList();
            foreach (Recipe_Data dat in database_Recipes.Recipes)
            {
                if (dat.recipe.ingredients.Count() == data.Count() && dat.recipe.type == RecipeType.Ingredient)
                {
                    List<string> listdat = new List<string>();
                    for (int i = 0; i < dat.recipe.ingredients.Count(); i++)
                    {
                        listdat.Add(dat.recipe.ingredients[i].i_Name);
                    }
                    listdat = listdat.OrderBy(e => e).ToList();

                    bool isEqual = Enumerable.SequenceEqual(data, listdat);
                    if (isEqual)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }



    public Object_Data GetIngredient(List<string> data)
    {
        if (data.Count != 0)
        {
            data = data.OrderBy(e => e).ToList();
            foreach (Recipe_Data dat in database_Recipes.Recipes)
            {
                if (dat.recipe.ingredients.Count() == data.Count() && dat.recipe.type == RecipeType.Ingredient)
                {
                    List<string> listdat = new List<string>();

                    for (int i = 0; i < dat.recipe.ingredients.Count(); i++)
                    {
                        listdat.Add(dat.recipe.ingredients[i].i_Name);
                    }
                    listdat = listdat.OrderBy(e => e).ToList();

                    bool isEqual = Enumerable.SequenceEqual(data, listdat);
                    if (isEqual)
                    {
                        if (!dat.recipe.discovered)
                        {
                            DiscoverRecipe(dat);
                            SearchUnlocks();
                        }
                        return dat.recipe.o_Data;
                    }

                }
                else { print("nope"); }
            }
        }
        return null;
    }

    void DiscoverRecipe(Recipe_Data data)
    {
        data.recipe.discovered = true;
        playerCanvasScript.NewDiscover(data);
        On_Discover.Raise();
    }
    void SearchUnlocks()
    {
        foreach (Unlockable_Data uD in database_Unlockables.Unlock)
        {
            if (!uD.unlockable.b_Unlocked)
            {
                int b = 0;
                int c = uD.unlockable.recipes.Count();

                foreach (Recipe_Data rD in uD.unlockable.recipes)
                {
                    if (rD.recipe.discovered)
                    {
                        b++;

                    }
                }
                if (b == c)
                {
                    uD.unlockable.b_Unlocked = true;
                    uD.unlockable.I_Data.avalaible = true;
                    //evento de refresh para la tienda
                    updateList.Raise();

                }
            }
        }
    }
    //Alchemy Table

    public List<Ingredient_Data> DecomposePotion(PotionScript potiondata)
    {
        Debug.Log("Descomponiendo");
        foreach (Recipe_Data rdata in database_Recipes.Recipes)
        {
            Debug.Log("buscando");
            if (potiondata.GetPData() == (Potion_Data)rdata.recipe.o_Data && rdata.recipe.type == RecipeType.Potion)
            {
                return rdata.recipe.ingredients;
            }
        }
        return null;
    }

    public Database_Recipes GetDatabase_Recipes()
    {
        return database_Recipes;
    }


    //Load And Save Zone

    //---------------
    //  Save
    //---------------

    public void Savedata()
    {
        Debug.Log("Guardando");
        //Al main menu no guardem
        if (SceneManager.GetActiveScene().name != "Main_Menu")
        {
            SaveGameData m_SaveGameData = new SaveGameData();


            Dictionary<string, bool> dRecetas = new Dictionary<string, bool>();
            foreach (Recipe_Data data in database_Recipes.Recipes)
            {
                dRecetas.Add(data.recipe.r_Name, data.recipe.discovered);
            }

            m_SaveGameData.playerSaveData.money = gold;
            m_SaveGameData.playerSaveData.recetas = dRecetas;


            byte[] serializedData = SerializationUtility.SerializeValue<SaveGameData>(m_SaveGameData, DataFormat.JSON);
            string base64 = System.Convert.ToBase64String(serializedData);
            File.WriteAllText("savegame.json", base64);
            Debug.Log("Desant el fitxer savegame.json");
            Debug.Log(m_SaveGameData);
            StartCoroutine(WaitToLoadScene());
        }
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main_Menu");
    }

    //---------------
    //  Loads
    //---------------

    public void LoadData()
    {
        Debug.Log("Cargando");
        // try
        //{
        Debug.Log("Carregant el fitxer: savegame.json");
        string newBase64 = File.ReadAllText("savegame.json");
        byte[] serializedData = System.Convert.FromBase64String(newBase64);
        SaveGameData m_SaveGameData = SerializationUtility.DeserializeValue<SaveGameData>(serializedData, DataFormat.JSON);
        Debug.Log(m_SaveGameData);

        gold = m_SaveGameData.playerSaveData.money;
        Dictionary<string, bool> dRecetas = m_SaveGameData.playerSaveData.recetas;
        Debug.Log("dRecetas " + dRecetas);
        foreach (KeyValuePair<string, bool> data in dRecetas)
        {
            Debug.Log(string.Format("01 Saved data name : {0} y data value: {1}", data.Key, data.Value));
            foreach (Recipe_Data data2 in database_Recipes.Recipes)
            {
                Debug.Log(string.Format("02 Saved data name : {0} y data name: {1}", data.Key, data2.recipe.r_Name));
                if (data.Key == data2.recipe.r_Name)
                {
                    data2.recipe.discovered = data.Value;
                }
            }
        }
        SearchUnlocks();
        updateList.Raise();
        updateMoney.Raise();

        Debug.Log("Load Complete");

        //}
        //catch (System.Exception e)
        //{
        //  Debug.Log(e.Message);
        // }
    }

}
