using System;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] internal SkeletonAnimation skeletonAnimation;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private ParticleSystem explosionPrefab;

    private AudioManager audioManager;
    private InputManager inputManager;
    private EventManager eventManager;
    
    private bool onPause;
    private bool onVictoryOrDefeatScreen;
    
    public float reloadTimer;
    public bool readyToShoot;
    
    public event Action OnEnemyCollision;
    public event Action OnVictoryCollision;

    private void Start()
    {
        readyToShoot = true;
        StartWalkAnimation();
    }
    
    public void Initialize(AudioManager audio, InputManager input, EventManager manager, PauseService pause)
    {
        audioManager = audio;
        inputManager = input;
        eventManager = manager;
        
        inputManager.OnMouseButtonClicked += TryToShoot;
        pause.OnPause += b => onPause = b;
        eventManager.OnVictoryOrDefeatScreen += b => onVictoryOrDefeatScreen = b;
        
    }

    public void StartWalkAnimation()
    {
        skeletonAnimation.state.AddAnimation(1, "walk", true, 0);
    }

    private void Update()
    {
        if (onPause) return;
        
        ShootingTimer();
    }

    private void ShootingTimer()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0)
        {
            readyToShoot = true;
            reloadTimer = 0;
        }
    }
    
    private void TryToShoot()
    {
        if (readyToShoot && !onPause && !onVictoryOrDefeatScreen)
            Shoot();
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        var hitCollider = hit.collider;
        if (hitCollider != null) 
            if (hitCollider.TryGetComponent(out Enemy enemy))
            {
                readyToShoot = false;
                reloadTimer = reloadTime;
                
                // Анимация ускорена для соответствия времени перезарядки
                skeletonAnimation.state.AddAnimation(0, "shoot", false, 0).TimeScale = reloadTime * 1.5f;
               
                muzzle.Play();
                // В настройках префаба взрыва он автоматически активируется при создании и уничтожается после конца анимации
                Instantiate(explosionPrefab, hit.transform.position, Quaternion.identity);
                    
                audioManager.PlaySfx("Shoot");

                hitCollider.gameObject.GetComponent<Enemy>().Die();
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            OnEnemyCollision?.Invoke();
            return;
        }
        
        if (other.gameObject.TryGetComponent(out VictoryTrigger victory))
        {
            OnVictoryCollision?.Invoke();
        }
    }
}