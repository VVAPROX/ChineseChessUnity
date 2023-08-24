public class BElephant : ChessBlack
{
    public override void InitiateMovePlates()
    {
        var sc = controller.GetComponent<Game>();

        int[][] moveDirections =
        {
            new[] { 2, 2 },
            new[] { 2, -2 },
            new[] { -2, 2 },
            new[] { -2, -2 }
        };

        foreach (var direction in moveDirections)
        {
            var xIncrement = direction[0];
            var yIncrement = direction[1];
            var x = xBoard + xIncrement;
            var y = yBoard + yIncrement;
            if (sc.PositionOnBoard(x, y))
            {
                if (sc.GetPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                }
                else if (sc.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }
}