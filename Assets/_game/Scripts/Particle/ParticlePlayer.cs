using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Play()
    {
        _particleSystem.Play();
    }
}