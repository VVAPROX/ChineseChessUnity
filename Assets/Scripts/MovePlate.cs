using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    private GameObject reference = null;
    private int matrixX;
    private int matrixY;
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        Game game = controller.GetComponent<Game>();

        if (attack)
        {
            GameObject cp = game.GetPosition(matrixX, matrixY);
            string cName = cp.name;
            if (cName.Contains("general"))
            {
                EPlayer player = cp.GetComponent<ChessPiece>().player;
                if (player == EPlayer.BLACK)
                {
                    game.Winner(EPlayer.WHITE);
                }
                else
                {
                    game.Winner(EPlayer.BLACK);
                }
            }
            Destroy(cp);
        }

        Dictionary<string, Type> map = game.GetPieceTypeMap();
        

        ChessPiece cm = reference.GetComponent<ChessPiece>();
        if (map.TryGetValue(cm.name, out var value))
        {
            cm = reference.GetComponent(value) as ChessPiece;
        }
        controller.GetComponent<Game>().SetPositionEmpty(cm.GetXBoard(), cm.GetYBoard());
        cm.SetXBoard(matrixX);
        cm.SetYBoard(matrixY);
        cm.SetCoords();
        game.SetPosition(reference);
        cm.DestroyMovePlates();
        
        game.NextTurn();
    }
    
    

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject gameObject)
    {
        reference = gameObject;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}