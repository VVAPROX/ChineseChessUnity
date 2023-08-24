using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    protected GameObject controller;

    protected int xBoard = -1;
    protected int yBoard = -1;
    public EPlayer player;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    public abstract void Activate();

    public void SetCoords()
    {
        float x = xBoard * 8.1f - 32.4f;
        float y = yBoard * 8.1f - 36f;

        transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public virtual void SetYBoard(int y)
    {
        yBoard = y;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    private void OnMouseUp()
    {
        Game sc = controller.GetComponent<Game>();
        if (!sc.IsGameOver() && sc.GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
    }

    public abstract void InitiateMovePlates();

    protected void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 8.1f;
        y *= 8.1f;
        x += -32.4f;
        y += -36f;
        GameObject movePlate = PrefabManager.GetInstance().GetMovePlate();
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    protected void MovePlateSpawn(int matrixX, int matrixY, bool isAttack = false)
    {
        float x = matrixX;
        float y = matrixY;
        x *= 8.1f;
        y *= 8.1f;
        x += -32.4f;
        y += -36f;
        GameObject movePlate = PrefabManager.GetInstance().GetMovePlate();
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<ChessPiece>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
        else
        {
            Debug.Log("move invalid " + x + " " + y);
        }
    }
}