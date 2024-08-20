using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StickableObject : MonoBehaviour
{
    [HideInInspector] 
    public bool isConnected = false;
    Rigidbody _rigidbody;
    private fuckFuckScaling _scaling;
    MeshCollider _meshCollider;
    BoxCollider _boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (null == _rigidbody)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        
        _scaling = GetComponent<fuckFuckScaling>();

        _meshCollider = GetComponent<MeshCollider>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float FindFarthestPoint(MeshCollider meshCollider, Vector3 center)
    {
        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        Vector3 farthestPoint = Vector3.zero;
        float maxDistance = 0f;

        foreach (Vector3 vertex in vertices)
        {
            Vector3 worldVertex = meshCollider.transform.TransformPoint(vertex);
            float distance = Vector3.Distance(worldVertex, center);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestPoint = worldVertex;
            }
        }

        return maxDistance;
    }

    float FindFarthestPointBox(BoxCollider boxCollider, Vector3 center)
    {
        Vector3[] corners = new Vector3[8];
        Vector3 size = boxCollider.size;
        Vector3 extents = size * 0.5f;

        // Calculate the 8 corners of the box collider
        corners[0] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-extents.x, -extents.y, -extents.z));
        corners[1] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(extents.x, -extents.y, -extents.z));
        corners[2] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-extents.x, extents.y, -extents.z));
        corners[3] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(extents.x, extents.y, -extents.z));
        corners[4] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-extents.x, -extents.y, extents.z));
        corners[5] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(extents.x, -extents.y, extents.z));
        corners[6] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-extents.x, extents.y, extents.z));
        corners[7] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(extents.x, extents.y, extents.z));

        float maxDistance = 0f;

        foreach (Vector3 corner in corners)
        {
            float distance = Vector3.Distance(corner, center);
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }

        return maxDistance;
    }

    float CalculateMeshArea(MeshCollider meshCollider)
    {
        Mesh mesh = meshCollider.sharedMesh;
        float area = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            area += Vector3.Cross(v1 - v0, v2 - v0).magnitude * 0.5f;
        }

        return area;
    }

    float CalculateBoxVolume(BoxCollider boxCollider, Vector3 scale)
    {
        Vector3 size = boxCollider.size;
        return size.x * scale.x * size.y * scale.y * size.z * scale.z * 10;
    }

    private void StickObjectTo(GameObject obj)
    {

        transform.SetParent(obj.transform);

        if(_meshCollider)
            obj.GetComponent<AdjustBallCollider>().Expand(FindFarthestPoint(_meshCollider, obj.transform.position));
        else if(_boxCollider)
            obj.GetComponent<AdjustBallCollider>().Expand(FindFarthestPointBox(_boxCollider, obj.transform.position));

        // remove for chicken
        FollowPath removeMe = GetComponent<FollowPath>();
        if (removeMe)
            removeMe.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isConnected)
        {
            return;
        }

        if (null != _scaling && _scaling.GetScaleBracket() > fuckFuckScaling.playerScaleBracket)
        {
            return;
        }

        if (_meshCollider)
        {
            float meshArea = CalculateMeshArea(_meshCollider) * transform.localScale.x * transform.localScale.y * transform.localScale.z;
            // sphere formula
            float ballArea = 4 / 3 * Mathf.PI;
            ballArea *= collision.gameObject.GetComponent<SphereCollider>().radius * collision.gameObject.transform.localScale.x;
            ballArea *= collision.gameObject.GetComponent<SphereCollider>().radius * collision.gameObject.transform.localScale.x;
            ballArea *= collision.gameObject.GetComponent<SphereCollider>().radius * collision.gameObject.transform.localScale.x;

            if (meshArea > ballArea)
                return;
        }

        if (_boxCollider)
        {
            float boxArea = CalculateBoxVolume(_boxCollider, transform.localScale);

            // sphere formula
            float ballArea = 4 / 3 * Mathf.PI;
            ballArea *= collision.gameObject.GetComponent<SphereCollider>().radius * collision.gameObject.transform.localScale.x;
            ballArea *= collision.gameObject.GetComponent<SphereCollider>().radius * collision.gameObject.transform.localScale.x;
            ballArea *= collision.gameObject.GetComponent<SphereCollider>().radius * collision.gameObject.transform.localScale.x;

            if (boxArea > ballArea)
                return;
        }

        var stickableObject = collision.gameObject.GetComponent<StickableObject>();
        var stickyBall = collision.gameObject.GetComponent<StickyBall>();

        if ((null != stickableObject && stickableObject.isConnected) || 
            null != stickyBall)
        {
            if (stickableObject != null)
                StickObjectTo(stickableObject.gameObject);

            if (stickyBall != null)
            {
                StickObjectTo(stickyBall.gameObject);
                stickyBall.AddObjectToAverageDistance(transform.position);
            }


            // bro who put this shit in here?
#if UNITY_EDITOR
            var mov = transform.parent.GetComponent<SimpleMovement>();
            if (null != mov)
            {
                mov.movementModifier += 25.0f;
            }
#endif
            isConnected = true;
            _rigidbody.isKinematic = true;
            Component.Destroy(_rigidbody);
            
        }
    }

}
