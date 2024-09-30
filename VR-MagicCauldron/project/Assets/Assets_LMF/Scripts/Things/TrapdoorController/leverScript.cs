using UnityEngine;


public class leverScript : MonoBehaviour
{
    [SerializeField] trampillaScript trampilla;

    bool open;

    private void Start()
    {
        open = false;
    }

    private void Update()
    {
        //Debug.Log("rotation" + transform.rotation.eulerAngles.x);
        if (transform.rotation.eulerAngles.x < 55f)
        {
            
            if (!open)
            {
                open = true;
                trampilla.ModifyHatchRotation(open);
                //trampilla.OpenHatch();
            }
        }
        else
        {
           
            if (open)
            {
                open = false;
                trampilla.ModifyHatchRotation(open);
                //trampilla.CloseHatch();
            }
        }
    }
}
