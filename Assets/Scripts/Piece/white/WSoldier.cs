public class WSoldier : ChessWhite
{
    public override void InitiateMovePlates()
    {
        PointMovePlate(xBoard, yBoard - 1);
        if (yBoard < 5)
        {
            PointMovePlate(xBoard + 1, yBoard);
            PointMovePlate(xBoard - 1, yBoard);
        }
    }
}