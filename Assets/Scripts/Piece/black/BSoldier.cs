public class BSoldier : ChessBlack
{
    public override void InitiateMovePlates()
    {
        PointMovePlate(xBoard, yBoard + 1);
        if (yBoard > 4)
        {
            PointMovePlate(xBoard + 1, yBoard);
            PointMovePlate(xBoard - 1, yBoard);
        }
    }
}