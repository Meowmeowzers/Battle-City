using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.PlayerLoop;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBase;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 basePosition;

    private CinemachineVirtualCamera cam;
    private WaitForSeconds wait;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        wait = new WaitForSeconds(0.1f);
    }

    private void Start()
    {
        playerBase = FindObjectOfType<HealthBase>().gameObject;
        basePosition = playerBase.transform.position;
        basePosition.Set(basePosition.x, basePosition.y, transform.position.z);
        //StartCoroutine(CFollow());
    }

    private void Update()
    {
        SetPlayer();
        Follow();
    }

    private void Follow()
    {
        if (player == null)
        {
            cam.Follow = playerBase.transform;
        }
        else
        {
            cam.Follow = player.transform;
            
        }
    }

    private void SetPlayer()
    {
        if (player == null)
        {
            try
            {
                GameObject temp = FindAnyObjectByType<HealthPlayer>().gameObject;
                if (temp != null)
                    player = temp;
            }
            catch
            {
                Debug.Log("No Player");
            }
            
        }
    }
}