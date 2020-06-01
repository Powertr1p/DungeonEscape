using UnityEngine;

namespace Player
{
  public class SwordHitbox : MonoBehaviour
  {
    private void OnTriggerEnter2D(Collider2D other)
    {
      Debug.Log("Hit: " + other.name);
    }
  }
}
