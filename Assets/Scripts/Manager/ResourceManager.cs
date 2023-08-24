using UnityEngine;


public class ResourceManager
{
    private static ResourceManager instance;

    public static ResourceManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ResourceManager();
        }

        return instance;
    }

    public Sprite LoadPieceSprite(string name)
    {
        string path = "Piece/" + name;
        return Resources.Load<Sprite>(path);
    }
}