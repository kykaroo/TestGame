using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject gameOverPanel;
    
    public PlayerController playerController;
    public BackgroundController backgroundController;

    public void Start()
    {
        backgroundController.needMove = true;
        playerController.OnVictoryCollision += VictoryEvent;
        playerController.OnEnemyCollision += GameOver;
    }
    
    private void GameOver()
    {
        enemySpawner.gameObject.SetActive(false);
        backgroundController.needMove = false;
        enemySpawner.enemyList.RemoveAll(t => t == null);
        
        foreach (var enemy in enemySpawner.enemyList)
        {
            enemy.GameOver();
        }
        
        gameOverPanel.SetActive(true);
        
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
        
        victoryPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        enemySpawner.gameObject.SetActive(true);
        enemySpawner.StartSpawn();
        
        foreach (var enemy in enemySpawner.enemyList)
        {
            Destroy(enemy.gameObject);
        }
        
        enemySpawner.enemyList.Clear();
        
        playerController.skeletonAnimation.ClearState();
        playerController.StartWalkAnimation();
        
        backgroundController.ResetBackground();
        backgroundController.needMove = true;

        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }
}