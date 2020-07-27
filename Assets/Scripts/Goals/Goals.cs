using UnityEngine;

public class Goals : MonoBehaviour
{
   private Vector3 _endPosition;
   private Vector3 _startPosition = new Vector3(0, -90, 0);
   private float _speed = 15f;
   
   private bool _isMoving;
   private bool _isPanelOpened;
   private Vector3 _target;

   private void Start()
   {
      _endPosition = transform.localPosition;
   }

   private void FixedUpdate()
   {
      if (!_isMoving) return;

      transform.localPosition = Vector3.MoveTowards(transform.localPosition, _target, _speed);

      if (transform.localPosition == _target)
         _isMoving = false;
   }

   public void ToggleGoalsPanel()
   {
      if (_isMoving) return;
      
      if (!_isPanelOpened)
      {
         _target = _startPosition;
         _isMoving = true;
         _isPanelOpened = true;
      }
      else
      {
         _target = _endPosition;
         _isMoving = true;
         _isPanelOpened = false;
      }
   }
}
