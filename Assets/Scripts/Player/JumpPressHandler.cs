using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class JumpPressHandler : MonoBehaviour, IPointerDownHandler

    {
        [SerializeField] private PlayerMover _playerMover;
        
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _playerMover.HandleJumpButton();
        }
    }
}