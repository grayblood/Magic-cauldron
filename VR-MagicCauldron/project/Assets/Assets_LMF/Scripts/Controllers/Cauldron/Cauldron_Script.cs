using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron_Script : MonoBehaviour
{
    //---------------------------------------------------------------------
    //---------------------------Variables---------------------------------
    //---------------------------------------------------------------------

    //Ingredients list
    List<string> ingredients = new List<string>();

    //Minigame GameObjects
    [SerializeField] List<PointsScriptCauldron> points = new List<PointsScriptCauldron>();

    bool playing;


    [SerializeField] Light light;
    [Header("GameEvents")]
    [SerializeField] GameEvent On_WaitToStart;
    [SerializeField] GameEvent Wrong;
    [SerializeField] GameEvent On_MinigameEnd;
    [SerializeField] GameEvent On_Explosion;
    [SerializeField] GameEvent On_Item_Insert;



    //---------------------------------------------------------------------
    //----------------------------Functions--------------------------------
    //---------------------------------------------------------------------
    private void Awake()
    {
        ingredients.Clear();
        playing = false;
    }

    //Detection

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ingredient_Script>())
        {
            ingredients.Add(other.gameObject.GetComponent<Ingredient_Script>().GetName());
            other.gameObject.GetComponent<Ingredient_Script>().GetIData().currentlySpawned--;
            On_Item_Insert.Raise();
            Destroy(other.gameObject);

        }
        if (GC_Script.Instance.CheckPotion_R(ingredients))
        {
            light.gameObject.SetActive(true);
        }
        else
        {
            light.gameObject.SetActive(false);
        }
        if (other.gameObject.name == "Wand")
        {
            //test if there's a receipt
            if (GC_Script.Instance.CheckPotion_R(ingredients))
            {
                StartPuzzleTime();
            }
            else
            {
                if (ingredients.Count != 0)
                {
                    On_Explosion.Raise();
                }
                ingredients.Clear();
            }
        }
        if (ingredients.Count >= 3 && !GC_Script.Instance.CheckPotion_R(ingredients))
        {
            On_Explosion.Raise();
            ingredients.Clear();
        }
    }


    //---------------------------------------------------------------------
    //----------------------------Puzzle minigame--------------------------
    //---------------------------------------------------------------------


    [SerializeField] List<Sequence_Data> combos = new List<Sequence_Data>();

    List<int> current_Puzzle = new List<int>();
    int currentPuzzlePos;



    void StartPuzzleTime()
    {
        On_WaitToStart.Raise();
    }
    public void StartTheMinigame()
    {
        playing = true;
        foreach (PointsScriptCauldron point in points)
        {
            point.changeState(0);
        }

        StartCoroutine("WaitForNextPuzzle", 0.5f);
    }
    public void CancelMinigame()
    {
        playing = false;
        ingredients.Clear();
        On_MinigameEnd.Raise();
    }
    void randomPuzzle()
    {
        current_Puzzle = combos[Random.Range(0, combos.Count)].sequence.order;
        currentPuzzlePos = 0;
        searchthepoint(current_Puzzle[0]).ShineState();
    }

    public void receivePointData(int iD)
    {
        if (iD == current_Puzzle[currentPuzzlePos])
        {
            //Correct go next
            searchthepoint(iD).CorrectState();
            Debug.Log(string.Format("Currentpuzzle = {0} , position = {1}", current_Puzzle[currentPuzzlePos], currentPuzzlePos));
            currentPuzzlePos++;
            if (currentPuzzlePos >= current_Puzzle.Count)
            {
                GC_Script.Instance.GetPotion(ingredients);
                ingredients.Clear();
                light.gameObject.SetActive(false);
                On_MinigameEnd.Raise();
            }
            else
            {
                searchthepoint(current_Puzzle[currentPuzzlePos]).ShineState();
                StartCoroutine("WaitForNextOrb", 1.5f);
            }
        }
        else
        {
            Wrong.Raise();
            //reset
            ResetMinigame();
        }
    }

    public void ResetMinigame()
    {
        if (playing)
        {
            playing = false;
            On_WaitToStart.Raise();
        }
    }

    PointsScriptCauldron searchthepoint(int id)
    {
        //Debug.Log("searching " + id);
        foreach (PointsScriptCauldron p in points)
        {
            if (p.pointID == id)
            {
                return p;
            }
        }
        return null;
    }

    IEnumerator WaitForNextPuzzle(float ftime)
    {
        yield return new WaitForSeconds(ftime);
        randomPuzzle();
    }
    IEnumerator WaitForNextOrb(float ftime)
    {
        yield return new WaitForSeconds(ftime);

    }
}
