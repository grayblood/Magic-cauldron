using System.Linq;
using UnityEngine;

public class PenScript : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int pensize = 5;

    

    private Renderer ren;
    private Color[] colors;
    private float tipheight;
    [SerializeField]
    WhiteBoardScript whiteBoardScript;
    Vector2 touchpos;
    Vector2 lasttouchpos;
    private RaycastHit touch;

    private bool touchedlastframe;
    private Quaternion lasttouchrot;

    bool holding;

    private void Start()
    {
        ren = tip.GetComponent<Renderer>();
        colors = Enumerable.Repeat(ren.material.color, pensize * pensize).ToArray();
        //tipheight = tip.localScale.y;
        tipheight = 2.5f;
        holding = false;
    }
    private void Update()
    {
        Draw();
    }

    public void drawingbool(bool a)
    {
        holding = a;
    }

    public void Draw()
    {
        if (Physics.Raycast(tip.position, transform.forward, out touch, tipheight) && holding)
        {
            if (touch.transform.CompareTag("WhiteBoard"))
            {
                if (whiteBoardScript == null)
                {
                    whiteBoardScript = touch.transform.GetComponent<WhiteBoardScript>();
                }

                touchpos = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                var x = (int)(touchpos.x * whiteBoardScript.texturesize.x - (pensize / 2));
                var y = (int)(touchpos.y * whiteBoardScript.texturesize.y - (pensize / 2));

                if (y < 0 || y > whiteBoardScript.texturesize.y || x < 0 || x > whiteBoardScript.texturesize.x) return;

                if (touchedlastframe)
                {
                    whiteBoardScript.texture.SetPixels(x, y, pensize, pensize, colors);
                    for (float f = 0.01f; f < 1.00f; f += 0.03f)
                    {
                        var lerpX = (int)Mathf.Lerp(lasttouchpos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(lasttouchpos.y, y, f);
                        whiteBoardScript.texture.SetPixels((int)lerpX, (int)lerpY, pensize, pensize, colors);

                    }

                    // transform.rotation = lasttouchrot;

                    whiteBoardScript.texture.Apply();
                }

                lasttouchpos = new Vector2(x, y);
                lasttouchrot = transform.rotation;
                touchedlastframe = true;
                return;
            }
        }
        else
        {

            whiteBoardScript = null;
            touchedlastframe = false;
        }
    }
}
