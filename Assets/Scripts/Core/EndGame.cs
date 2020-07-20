using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class EndGame : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameEventsHandler.Instance.HasWinCondition) return;

            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        }
    }
}
