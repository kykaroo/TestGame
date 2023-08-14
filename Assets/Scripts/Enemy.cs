using System;
using Spine.Unity;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    [SerializeField] private float speed;

    private void Start()
    {
        StartRunAnimation();
    }

    private void StartRunAnimation()
    {
        skeletonAnimation.ClearState();
        skeletonAnimation.state.AddAnimation(1, "run", true, 0);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new(-(Time.deltaTime * speed),0,0));
    }

    public void GameOver()
    {
        skeletonAnimation.ClearState();
        skeletonAnimation.state.AddAnimation(1, "win", true, 0);
        speed = 0f;
    }

    public void PlayerVictory()
    {
        skeletonAnimation.ClearState();
        skeletonAnimation.state.AddAnimation(1, "idle", true, 0);
        speed = 0f;
    }
}