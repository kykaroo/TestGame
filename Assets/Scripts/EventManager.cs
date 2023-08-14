using System;
using UnityEngine;
using Object = System.Object;

public class EventManager
{
    private EnemySpawner enemySpawner;
    private BackgroundController backgroundController;
    private PlayerController playerController;

    private bool onVictoryOrDefeatScreen;

    public event Action OnVictory;
    public event Action OnDefeat;
    public event Action OnRestart;
    public event Action<bool> OnVictoryOrDefeatScreen;

    public void Initialize(EnemySpawner spawner, BackgroundController background, PlayerController player)
    {
        enemySpawner = spawner;
        backgroundController = background;
        playerController = player;

        playerController.OnEnemyCollision += GameOverEvent;
        playerController.OnVictoryCollision += VictoryEvent;
    }
    
    private void GameOverEvent()
    {
        enemySpawner.gameObject.SetActive(false);
        backgroundController.needMove = false;
        enemySpawner.enemyList.RemoveAll(t => t == null);
        
        foreach (var enemy in enemySpawner.enemyList)
        {
            enemy.GameOver();
        }
        
        OnDefeat?.Invoke();
        OnVictoryOrDefeatScreen?.Invoke(true);
        
        playerController.skeletonAnimation.ClearState();
        playerController.skeletonAnimation.state.AddAnimation(3, "loose", false, 0);
    }

    private void VictoryEvent()
    {
        enemySpawner.gameObject.SetActive(false);
        backgroundController.needMove = false;
        enemySpawner.enemyList.RemoveAll(t => t == null);
        
        foreach (var enemy in enemySpawner.enemyList)
        {
            enemy.PlayerVictory();
        }

        playerController.skeletonAnimation.ClearState();
        playerController.skeletonAnimation.state.AddAnimation(4, "idle", true, 0);
        
        OnVictory?.Invoke();
        OnVictoryOrDefeatScreen?.Invoke(true);
    }

    public void RestartLevel()
    {
        enemySpawner.gameObject.SetActive(true);
        enemySpawner.StartSpawn();
        playerController.readyToShoot = true;
        playerController.reloadTimer = 0f;
        
        foreach (var enemy in enemySpawner.enemyList)
        {
            enemy.DestroyEnemy();
        }
        
        enemySpawner.enemyList.Clear();
        
        playerController.skeletonAnimation.ClearState();
        playerController.StartWalkAnimation();
        
        backgroundController.ResetBackground();
        backgroundController.needMove = true;

        OnRestart?.Invoke();
        OnVictoryOrDefeatScreen?.Invoke(false);
    }
}
