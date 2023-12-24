public enum GameState { MainMenu, GamePlay, Finish }

public class GameManager : Singleton<GameManager>
{
    private GameState state;

    private void Awake()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }

    public bool InState(GameState gameState)
    {
        return state == gameState;
    }
}
