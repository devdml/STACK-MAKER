using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    private Player player;
    private Level currentLevel;

    private void Start()
    {
        LoadLevel(1);
        OnInit();
        UIManager.Instance.OpenMainMenuUI();
    }

    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.position;
        player.OnInit();
    }

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }
}
