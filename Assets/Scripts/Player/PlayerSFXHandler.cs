using System;
using UnityEngine;

namespace Player
{
    public class PlayerSFXHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private AudioClip _jumpStartSound;
        [SerializeField] private AudioClip _jumpEndSound;
        [SerializeField] private AudioClip _regualAttackSound;
        [SerializeField] private AudioClip _flameSwordSound;
        private float _soundVolume = 0.1f;
        
        private AudioSource _audio;
        private Player _player;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _player = GetComponentInParent<Player>();
        }

        private void OnEnable()
        {
            _player.DamageTaken += PlayHitSound;
        }

        private void PlayStartJumpSound()
        {
            _audio.PlayOneShot(_jumpStartSound, _soundVolume);
        }

        private void PlayEndJumpSound()
        {
            _audio.PlayOneShot(_jumpEndSound, _soundVolume);
        }

        private void PlayFlameAttackSound()
        {
            _audio.PlayOneShot(_flameSwordSound, _soundVolume);
        }

        private void PlayRegularAttackSound()
        {
            _audio.PlayOneShot(_regualAttackSound, _soundVolume);
        }

        private void PlayHitSound(int dmg)
        {
            _audio.PlayOneShot(_hitSound, _soundVolume);
        }
        
        private void PlayDeathSound()
        {
            _audio.PlayOneShot(_deathSound, _soundVolume);
        }
    }
}