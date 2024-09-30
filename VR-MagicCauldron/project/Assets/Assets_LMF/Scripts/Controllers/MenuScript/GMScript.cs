using OdinSerializer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour
{
    public static GMScript Instance { get; private set; }

    public bool load;

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
        load = false;
    }

    public void LoadScenes(bool b_continue)
    {
        load = b_continue;
        StartCoroutine(Wait(3, b_continue));
    }
    IEnumerator Wait(float a,bool b)
    {
        yield return new WaitForSeconds(a);
        if (b)
        {
            SceneManager.LoadScene("Laboratory");
            //Hacer un wait de 2 segundos
            //Cargar datos con gcontroller de la escena
        }
        else
        {
            SceneManager.LoadScene("Laboratory");
        }
    }



    //Load And Save Zone

    //---------------
    //  Save
    //---------------
    /*
    public void Savedata(int gold)
    {
        //Al main menu no guardem
        if (SceneManager.GetActiveScene().name != "Main_Menu")
        {
            SaveGameData m_SaveGameData = new SaveGameData();


            Dictionary<string, bool> dRecetas = new Dictionary<string, bool>();
            foreach (Recipe_Data data in GetComponent<Database_Recipes>().Recipes)
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
        }
    }
    //---------------
    //  Loads
    //---------------

    public void LoadGame()
    {
        // try
        //{
        Debug.Log("Carregant el fitxer: savegame.json");
        string newBase64 = File.ReadAllText("savegame.json");
        byte[] serializedData = System.Convert.FromBase64String(newBase64);
        SaveGameData m_SaveGameData = SerializationUtility.DeserializeValue<SaveGameData>(serializedData, DataFormat.JSON);
        
        Debug.Log(m_SaveGameData);
        m_playerPos = m_SaveGameData.player.position;
        List<int> pokenames = m_SaveGameData.player.partymName;
        List<int> pokelevels = m_SaveGameData.player.partyLevel;
        Debug.Log("Carregant Pokemons: ");
        Debug.Log("Carregant pokenames: " + pokenames);
        Debug.Log("Carregant pokelevels: " + pokelevels);
        int i = 0;
        foreach (int a in pokenames)
        {
            print(GetComponent<PokedexScript>().SearchReferencePokemon(a));
            loadPokemons(GetComponent<PokedexScript>().SearchReferencePokemon(a), pokelevels[i]);
            i++;
        }
        List<string> itemName = m_SaveGameData.player.BackpackItemName;
        List<int> itemQuantity = m_SaveGameData.player.BackpackQuantity;
        print("item name: " + itemName);
        print("itemQuantity: " + itemQuantity);
        int b = 0;
        Debug.Log("Carregant items: ");
        foreach (string a in itemName)
        {
            print("item: " + a);
            CreateAndAddItem(GetComponent<ItemBDScript>().SearchReferenceItems(a), itemQuantity[b]);
            b++;
        }
        Debug.Log("Canviant a escena: " + m_SaveGameData.level);
        SceneManager.LoadScene(m_SaveGameData.level);
        playerCanMove = true;
        On_SceneChange.Raise();

        //}
        //catch (System.Exception e)
        //{
        //  Debug.Log(e.Message);
        // }
    }*/
}

