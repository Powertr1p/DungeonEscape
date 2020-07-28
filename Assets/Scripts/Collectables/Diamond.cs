using Core;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public class Diamond : MonoBehaviour, ICollectible
    {
        [SerializeField] private int DiamondValue = 1;
        [SerializeField] private AudioClip _collectSound;
        
        private Rigidbody2D _rb2d;

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player.Player>())
                Collect(DiamondValue);
        }
        
        public void Collect(int value)
        {
            AudioSource.PlayClipAtPoint(_collectSound, transform.position, 0.1f);
            GameEventsHandler.Instance.AddDiamonds(value);
            Destroy(gameObject);
        }

        public void SpawnFromEnemy()
        {
            _rb2d.gravityScale = 1;
            _rb2d.AddForce(new Vector2(Random.Range(-2f,2f), Random.Range(2f,8f)), ForceMode2D.Impulse);
        }
    }
}
