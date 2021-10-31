using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FOV_Track : MonoBehaviour
{
    
    [Header("추적된 타겟")]
    //[HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    
    [Header("추적 거리")]
    public float trackRange = 10.0f;
    [Header("업데이트 빈도")]
    public float trackDelay = 0.2f;

    
    [Header("추적할 타겟 레이어")]
    public LayerMask targetMask;    //추적 타겟
    
    [Header("장애물 레이어")]
    public LayerMask obstacleMask;  //장애물

    [Header("추적 시야각")]
    [Range(0, 360)]
    public float viewAngle = 90f;

    [Header("-[고급 옵션]-")]

    [Range(0.0f,50.0f)]
    public float meshResolution = 10;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    

    // Start is called before the first frame update
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine(FindTargetsWithDelay(trackDelay));
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, trackRange, targetMask);
        visibleTargets.Clear();

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            //visibleTargets.Clear();

            Transform target = targetsInViewRadius[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    //print(targetsInViewRadius.Length);
                }
            }
        }
        
    }

    void DrawFieldOfView() 
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i+1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle) {
		Vector3 dir = DirFromAngle (globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast (transform.position, dir, out hit, trackRange, obstacleMask)) {
			return new ViewCastInfo (true, hit.point, hit.distance, globalAngle);
		} else {
			return new ViewCastInfo (false, transform.position + dir * trackRange, trackRange, globalAngle);
		}
	}
    void OnDrawGizmos()
    {
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //enemyPos = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);

        Vector3 forwardV = transform.position + (transform.forward * trackRange);
        /*
        Handles.color = Color.white;
        Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, trackRange);
        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);

        Handles.color = Color.white;
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Handles.DrawLine(transform.position, transform.position + viewAngleA * trackRange);
        Handles.DrawLine(transform.position, transform.position + viewAngleB * trackRange);
        */
        if (_Track())
        {
            Gizmos.color = Color.red;

            foreach(Transform visibleTarget in visibleTargets)
            {
                Gizmos.DrawLine(transform.position, new Vector3(visibleTarget.position.x,0f,visibleTarget.position.z));
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, forwardV);
    }

    bool _Track()
    {
        if(visibleTargets.Count > 0)
            return true;
        else
            return false;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo {
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle) {
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}
}
