using UnityEngine;

public class BGeneral : ChessBlack
{
    public override void InitiateMovePlates()
    {
        var sc = controller.GetComponent<Game>();

        int[][] moveDirections =
        {
            new[] { 1, 0 },
            new[] { -1, 0 },
            new[] { 0, 1 },
            new[] { 0, -1 }
        };

        foreach (var direction in moveDirections)
        {
            var xIncrement = direction[0];
            var yIncrement = direction[1];
            Debug.Log(yIncrement);
            var x = xBoard + xIncrement;
            var y = yBoard + yIncrement;
            bool isValidMove = x < 3 || x > 5 || y < 3;
            if (isValidMove && sc.PositionOnBoard(x, y))
            {
                var destination = sc.GetPosition(x, y);

                if (destination == null)
                {
                    MovePlateSpawn(x, y);
                }
                else if (destination.GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }
}