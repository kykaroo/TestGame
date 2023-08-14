using System;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] internal SkeletonAnimation skeletonAnimation;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;

    private bool readyToShoot;
    
    public event Action OnEnemyCollision;
    public event Action OnVictoryCollision;

    private void Start()
    {
        readyToShoot = true;
        StartWalkAnimation();
    }

    public void StartWalkAnimation()
    {
        skeletonAnimation.state.AddAnimation(1, "walk", true, 0);
    }

    private void Update()
    { 
        if (Input.GetMouseButtonDown(0) && readyToShoot) Shoot();
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) 
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                readyToShoot = false;
                skeletonAnimation.state.AddAnimation(0, "shoot", false, 0).TimeScale = 1.5f;
                muzzle.Play();
                AudioManager.Instance.PlaySfx("Shoot");

                Destroy(hit.collider.gameObject);
                Invoke(nameof(ResetShot),reloadTime);
            }
    }

    private void ResetShot()
    {
        readyToShoot = true;
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