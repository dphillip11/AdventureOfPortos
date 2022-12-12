using Cinemachine;
using UnityEngine;

public class CameraShakeOnCollide : MonoBehaviour
{
    CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        impulseSource.GenerateImpulse();
    }
}
