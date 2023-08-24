using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;
    public GameObject[,] postions = new GameObject[9, 10];
    private EPlayer currentPlayer = EPlayer.WHITE;
    private Dictionary<string, Type> pieceTypeMap;
    [SerializeField] private TurnTimer turnTimer;
    [SerializeField] private Text cdText1;
    [SerializeField] private Text cdText2;
    [SerializeField] private GameObject menuText;
    [SerializeField] private GameObject menu;
    private bool gameOver;

    void Start()
    {
        cdText1.transform.gameObject.SetActive(false);
        pieceTypeMap = new Dictionary<string, Type>
        {
            { "white_soldier", typeof(WSoldier) },
            { "white_cannon", typeof(WCannon) },
            { "white_chariot", typeof(WChariot) },
            { "white_horse", typeof(WHorse) },
            { "white_elephant", typeof(WElephant) },
            { "white_guard", typeof(WGuard) },
            { "white_general", typeof(WGeneral) },
            { "black_soldier", typeof(BSoldier) },
            { "black_cannon", typeof(BCannon) },
            { "black_chariot", typeof(BChariot) },
            { "black_horse", typeof(BHorse) },
            { "black_elephant", typeof(BElephant) },
            { "black_guard", typeof(BGuard) },
            { "black_general", typeof(BGeneral) },
        };

        var piecesToCreate = new[]
        {
            ("white_soldier", 0, 6), ("white_soldier", 2, 6),
            ("white_soldier", 4, 6), ("white_soldier", 6, 6),
            ("white_soldier", 8, 6),
            ("white_cannon", 1, 7), ("white_cannon", 7, 7),
            ("white_chariot", 0, 9), ("white_chariot", 8, 9),
            ("white_horse", 1, 9), ("white_horse", 7, 9),
            ("white_elephant", 2, 9), ("white_elephant", 6, 9),
            ("white_guard", 3, 9), ("white_guard", 5, 9),
            ("white_general", 4, 9),
            ("black_soldier", 0, 3), ("black_soldier", 2, 3),
            ("black_soldier", 4, 3), ("black_soldier", 6, 3),
            ("black_soldier", 8, 3),
            ("black_cannon", 1, 2), ("black_cannon", 7, 2),
            ("black_chariot", 0, 0), ("black_chariot", 8, 0),
            ("black_horse", 1, 0), ("black_horse", 7, 0),
            ("black_elephant", 2, 0), ("black_elephant", 6, 0),
            ("black_guard", 3, 0), ("black_guard", 5, 0),
            ("black_general", 4, 0)
        };


        foreach (var pieceInfo in piecesToCreate)
        {
            Create(pieceInfo.Item1, pieceInfo.Item2, pieceInfo.Item3);
        }

        turnTimer.SetGame(this);
        turnTimer.StartCountdown(cdText2);
    }

    private void Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessPiece cm = null;
        Type pieceType;
        if (pieceTypeMap.TryGetValue(name, out var value))
        {
            pieceType = value;
            cm = obj.AddComponent(pieceType) as ChessPiece;
        }

        cm.SetName(name);
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        SetPosition(obj);
    }

    public void SetPosition(GameObject obj)
    {
        ChessPiece cm = obj.GetComponent<ChessPiece>();

        postions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        postions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return postions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= postions.GetLength(0) || y >= postions.GetLength(1)) return false;
        return true;
    }

    public EPlayer GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        HideAllCountDownText();
        Text countDownText = GetCountDownText();
        if (currentPlayer == EPlayer.WHITE)
        {
            currentPlayer = EPlayer.BLACK;
        }
        else
        {
            currentPlayer = EPlayer.WHITE;
        }
        turnTimer.StartCountdown(countDownText);
    }

    private void HideAllCountDownText()
    {
        cdText1.gameObject.SetActive(false);
        cdText2.gameObject.SetActive(false);
    }
    
    public void Winner(EPlayer winner)
    {
        Transform menuTextTransform = menu.transform.Find("MenuText");
        menuTextTransform.GetComponent<TextMeshProUGUI>().text = "Winner: " + winner;
        gameOver = true;
        menu.SetActive(true);
    }

    public void RestartGame()
    {
        if (gameOver)
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
            menu.SetActive(false);
        }
    }

    public Dictionary<string, Type> GetPieceTypeMap()
    {
        return pieceTypeMap;
    }

    private Text GetCountDownText()
    {
        Text cdText = currentPlayer == EPlayer.WHITE ?  cdText1 : cdText2;
        cdText.gameObject.SetActive(true);
        return cdText;
    }
}