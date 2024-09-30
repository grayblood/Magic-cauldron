using UnityEngine;

public class WhiteBoardScript : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 texturesize = new Vector2(2048, 2048);

    private void Start()
    {
        newtexture();
    }
    public void newtexture()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)texturesize.x, (int)texturesize.y);
        r.material.mainTexture = texture;
    }
}
