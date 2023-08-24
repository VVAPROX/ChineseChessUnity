﻿using UnityEngine;

public abstract class ChessBlack : ChessPiece
{
    public override void Activate()
    {
        SetCoords();
        var sprite = ResourceManager.GetInstance().LoadPieceSprite(name);
        GetComponent<SpriteRenderer>().sprite = sprite;
        player = EPlayer.BLACK;
    }
}