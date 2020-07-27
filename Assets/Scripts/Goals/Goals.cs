using System;
using UnityEngine;

public class Goals : MonoBehaviour
{
   private Vector3 _endPosition;

   private void Start()
   {
      _endPosition = transform.position;
   }

   public void HideGoalsPanel()
   {
      gameObject.SetActive(false);
   }
}
