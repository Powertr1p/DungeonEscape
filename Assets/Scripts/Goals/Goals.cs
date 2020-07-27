using UnityEngine;

public class Goals : MonoBehaviour
{
   private Vector3 _endPosition;
   private Vector3 _startPosition = new Vector3(0, -90, 0);
   private float _speed = 15f;
   
   private bool isMoving;
   private Vector3 _target;

   private void Start()
   {
      _endPosition = transform.localPosition;
   }

   private void FixedUpdate()
   {
      if (!isMoving) return;

      transform.localPosition = Vector3.MoveTowards(transform.localPosition, _target, _speed);

      if (transform.localPosition == _target)
         isMoving = false;
   }

   public void HideGoalsPanel()
   {
      _target = _endPosition;
      isMoving = true;
   }
   
   public void ShowGoalPanel()
   {
      _target = _startPosition;
      isMoving = true;
   }
}
