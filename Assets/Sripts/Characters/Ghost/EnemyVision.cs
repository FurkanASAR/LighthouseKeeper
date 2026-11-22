using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public bool CanSeePlayer { get; private set; }
    [SerializeField] private Ghost ghost;


    [SerializeField] private Transform eyePoint;
    [SerializeField] private float viewDistance = 15f;
    [Range(0, 360)]
    [SerializeField] private float viewAngle = 90f;

    
    [SerializeField] private LayerMask targetMask;        
    [SerializeField] private LayerMask obstacleMask;

    private Transform playerTransform;


    private void Start()
    {
        playerTransform = ghost.GetPlayerTransform();
    }

    private void Update()
    {
        CanSeePlayer = CheckVision();
    }


    private bool CheckVision()
    {
        // Oyuncunun gövdesine bak (ayak değil), yoksa eğilince/engebede hata verir
        Vector3 targetPos = playerTransform.position + Vector3.up * 1.0f;
        Vector3 dirToTarget = targetPos - eyePoint.position;
        float distance = dirToTarget.magnitude;

        // 1) Mesafe kontrolü
        if (distance > viewDistance) return false;

        // 2) Açı kontrolü
        if (Vector3.Angle(eyePoint.forward, dirToTarget) > viewAngle * 0.5f) return false;

        // 3) Engel kontrolü: arada duvar varsa görmez
        if (Physics.Raycast(eyePoint.position, dirToTarget.normalized, distance, obstacleMask))
            return false;

        return true;
    }





}
