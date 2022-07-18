namespace BornToMove.DAL;

public class MoveRating : Base
{    
    public Move? Move
    {
        get;
        set; 
    }
    
    public double Rating
    {
        get;
        set;
    }

    public double Intensity
    {
        get;
        set;
    }
}