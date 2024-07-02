using UnityEngine;

namespace Player
{
    public class PlayerCameraFollow : MonoBehaviour
    {  
        [SerializeField] private new Transform  camera;
     
       private  void Update()
        {
            float cameraRotationY = camera.rotation.eulerAngles.y;
            var cameraTransform = transform;
            var cameraRotation = cameraTransform.rotation;
            Vector3 newRotation = new Vector3(cameraRotation.eulerAngles.x, cameraRotationY, cameraRotation.eulerAngles.z);
            Debug.Log(newRotation);
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
