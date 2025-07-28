public abstract class Opponant
{
    public int numMoves;
    public List<int> playerMoves = new List<int>();
    public void updatePlayerMoves(int playerMove)
    {
        playerMoves.Add(playerMove);
    }
    public abstract int getMove();
}

public class RandomOpponant : Opponant
{
    
    private Random generator = new Random();
    public override int getMove() => generator.Next(1, numMoves+1);
    public RandomOpponant(int numMoves)
    {
        this.numMoves = numMoves;
    }

}
public class CopyOpponant : Opponant
{
    public override int getMove()
    {
        if (playerMoves.Count == 0) { return new Random().Next(1, numMoves); }
        else { return playerMoves.Last(); }
    }
    public CopyOpponant(int numMoves)
    {
        this.numMoves = numMoves;
    }
    
}
