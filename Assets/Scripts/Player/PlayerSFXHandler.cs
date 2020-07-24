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
            _audio.PlayOneShot(_jumpStartSound);
        }

        private void PlayEndJumpSound()
        {
            _audio.PlayOneShot(_jumpEndSound);
        }

        private void PlayFlameAttackSound()
        {
            _audio.PlayOneShot(_flameSwordSound);
        }

        private void PlayRegularAttackSound()
        {
            _audio.PlayOneShot(_regualAttackSound);
        }

        private void PlayHitSound(int dmg)
        {
            _audio.PlayOneShot(_hitSound);
        }
        
        private void PlayDeathSound()
        {
            _audio.PlayOneShot(_deathSound);
        }
    }
}