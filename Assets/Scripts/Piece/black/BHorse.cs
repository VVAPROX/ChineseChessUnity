public class BHorse : ChessBlack
{
    public override void InitiateMovePlates()
    {
        var sc = controller.GetComponent<Game>();

        int[][] moveDirections =
        {
            new[] { 1, 2 },
            new[] { 1, -2 },
            new[] { 2, 1 },
            new[] { 2, -1 },
            new[] { -1, 2 },
            new[] { -1, -2 },
            new[] { -2, 1 },
            new[] { -2, -1 }
        };

        foreach (var direction in moveDirections)
        {
            int xIncrement = direction[0];
            int yIncrement = direction[1];
            int x = xBoard + xIncrement;
            int y = yBoard + yIncrement;
            if (sc.PositionOnBoard(x, y))
            {
                int nextX = xBoard;
                int nextY = yBoard;
                bool isUp = yIncrement == 2;
                bool isDown = yIncrement == -2;
                bool isLeft = xIncrement == -2;
                bool isRight = xIncrement == 2;
                nextX += isRight ? 1 : isLeft ? -1 : 0;
                nextY += isUp ? 1 : isDown ? -1 : 0;
                
                if (sc.PositionOnBoard(nextX, nextY))
                {
                    bool isMoveValid = sc.GetPosition(nextX, nextY) == null;
                    if (isMoveValid)
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
    }
}