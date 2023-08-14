using System;
using Spine.Unity;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] internal SkeletonAnimation skeletonAnimation;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private TextMeshProUGUI reloadTimerText;
    
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
    
    public void Initialize(MenuManager menu, AudioManager audio, InputManager input, EventManager manager, PauseService pause)
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
        
        reloadTimerText.text = Math.Round(reloadTimer, 2).ToString();
    }
    
    private void TryToShoot()
    {
        if (readyToShoot && !onPause && !onVictoryOrDefeatScreen)
            Shoot();
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) 
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                readyToShoot = false;
                reloadTimer = reloadTime;
                
                skeletonAnimation.state.AddAnimation(0, "shoot", false, 0).TimeScale = 1.5f;
                muzzle.Play();
                audioManager.PlaySfx("Shoot");

                hit.collider.gameObject.GetComponent<Enemy>().Die();
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