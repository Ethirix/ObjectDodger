using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBoxSpawner : MonoBehaviour
{
    [SerializeField] private float SpawnIntensity; //x per second
    [SerializeField] private Color BoxColour;
    [SerializeField] private float BoxScale;
    [SerializeField] private GameObject SpawnObject;
    private Plane plane;

    private void Start()
    {
        plane = new Plane(gameObject);
        SpawnObject.GetComponent<Renderer>().sharedMaterial.color = BoxColour;
        SpawnObject.transform.localScale = new Vector3(BoxScale, BoxScale, BoxScale);
        SpawnObject.GetComponent<Rigidbody>().mass = SpawnObject.transform.localScale.x *
                                                     SpawnObject.transform.localScale.y *
                                                     SpawnObject.transform.localScale.z;
        StartCoroutine(LoopCoroutine());
    }

    private IEnumerator LoopCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1 / SpawnIntensity);

        while (true) {
            DoSpawnObject();
            yield return wait;
        }
    }

    private void DoSpawnObject()
    {
        Vector3 spawnPos = RandomPoint();
        GameObject.Instantiate(SpawnObject).transform.position = spawnPos;
    }

    private Vector3 RandomPoint()
    {
        float minX = plane.bounds.min.x * plane.planeScale.x;
        float maxX = plane.bounds.max.x * plane.planeScale.x;
        float maxZ = plane.bounds.max.z * plane.planeScale.z;
        float minZ = plane.bounds.min.z * plane.planeScale.z;
        
        return new Vector3(Random.Range (minX, maxX), plane.transform.position.y, Random.Range (minZ, maxZ) + plane.planePosition.z - plane.planePosition.z / 10);
    }
    
    private class Plane
    {
        public Bounds bounds;
        public Transform transform;
        public readonly Vector3 planePosition;
        public readonly Vector3 planeScale;
        
        public Plane(GameObject gO)
        {
            GameObject plane = gO;
            Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;
            bounds = planeMesh.bounds;
            transform = plane.transform;
            planePosition = plane.transform.position;
            planeScale = plane.transform.localScale;
        }
    }
}
