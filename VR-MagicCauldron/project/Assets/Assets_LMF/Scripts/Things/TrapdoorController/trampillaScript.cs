using UnityEngine;
using Debug = UnityEngine.Debug;

public class trampillaScript : MonoBehaviour
{
    Rigidbody rb;
    float speed;

    public Transform pos1;
    public Transform pos2;
    float desigtime = 0.0f;
    bool open;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        open = false;
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(pos1.rotation, pos2.rotation, desigtime * speed);
        if (open && desigtime < 2f)
            desigtime += Time.deltaTime;
        if (!open && desigtime > 0)
            desigtime -= Time.deltaTime;
    }
    public void ModifyHatchRotation(bool open)
    {
        this.open = open;
        if (open)
            speed = 1.5f;
        else
            speed = 1.0f;
        // rb.rotation = Quaternion.Euler(0f, 180f, -90f);
        /*
        if (transform.rotation.eulerAngles.z > 271f && desigtime! < 0)
        {
             transform.rotation = Quaternion.Lerp(pos2.rotation, pos1.rotation, desigtime * speed);
        }*/


    }

}
