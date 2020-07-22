using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class EndGame : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameEventsHandler.Instance.HasWinCondition) return;

            LevelLoaderArgs.Load("GameOver", GameEventsHandler.Instance.PlayerDeathCount);
        }
    }
}
