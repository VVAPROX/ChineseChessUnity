public class BChariot : ChessBlack
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
            var x = xBoard + xIncrement;
            var y = yBoard + yIncrement;

            while (sc.PositionOnBoard(x, y))
            {
                if (sc.GetPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                }
                else
                {
                    if (sc.GetPosition(x, y).GetComponent<ChessPiece>().player != player) MovePlateAttackSpawn(x, y);
                    break;
                }

                x += xIncrement;
                y += yIncrement;
            }
        }
    }
}