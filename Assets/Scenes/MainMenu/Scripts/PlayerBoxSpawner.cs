using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Scenes.MainMenu.Scripts
{
    public class PlayerBoxSpawner : MonoBehaviour
    {
        [Header("Spawn Intensity")]
        [SerializeField] private float spawnIntensityPerSecond;
    
        [Header("Randomize Spawn Intensity")]
        [SerializeField] private bool randomSpawnIntensityToggle;
        [SerializeField] private float randomSpawnIntensityMinBound; //cannot be less than 0.2
        [SerializeField] private float randomSpawnIntensityMaxBound; //cannot be bigger than 8

        [Header("Randomize Force")] 
        [SerializeField] private bool randomForceToggle;
        [SerializeField] private float randomForceBounds;
        [SerializeField] private float randomTorqueBounds;
    
        [Header("Object Properties")]
        [SerializeField] private Color boxColour;
        [SerializeField] private float boxScale;
        [SerializeField] private GameObject spawnObject;
        private Plane plane;

        private void Start()
        {
            plane = new Plane(gameObject);
            Vector3 scale = spawnObject.transform.localScale;
            spawnObject.GetComponent<Renderer>().sharedMaterial.color = boxColour;
            spawnObject.transform.localScale = new Vector3(boxScale, boxScale, boxScale);
            spawnObject.GetComponent<Rigidbody>().mass = scale.x * scale.y * scale.z;
            spawnObject.tag = "MainMenu_Cube";
            spawnObject.name = "BackgroundCube";

            if (randomSpawnIntensityMinBound < 0.2f) { randomSpawnIntensityMinBound = 0.2f; }
            if (randomSpawnIntensityMinBound > 8.0f) { randomSpawnIntensityMinBound = 8.0f; }
        
            if (randomSpawnIntensityMaxBound < 0.2f) { randomSpawnIntensityMaxBound = 0.2f; }
            if (randomSpawnIntensityMaxBound > 8.0f) { randomSpawnIntensityMaxBound = 8.0f; }

            if (randomSpawnIntensityMinBound > randomSpawnIntensityMaxBound) { randomSpawnIntensityMinBound = randomSpawnIntensityMaxBound; }

            StartCoroutine(LoopCoroutine());
        }

        private IEnumerator LoopCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(1 / spawnIntensityPerSecond);
            bool run = true;
            
            while (run) {
                try {
                    if (randomSpawnIntensityToggle) { wait = new WaitForSeconds(1 / Random.Range(randomSpawnIntensityMinBound, randomSpawnIntensityMaxBound)); }
                    
                    GameObject cube = DoSpawnObject();
                    if (randomForceToggle) { DoRandomForce(cube); }
                } catch {
                    run = false;
                } 
                yield return wait;
            }
        }

        private GameObject DoSpawnObject()
        {
            Vector3 spawnPos = RandomPoint();
            GameObject cube = Instantiate(spawnObject);
            cube.transform.position = spawnPos;
            return cube;
        }

        private void DoRandomForce(GameObject cube)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            float massGravity = rb.mass * 9.81f;
            rb.AddForce(massGravity * Random.Range(-randomForceBounds, randomForceBounds), 
                massGravity * Random.Range(-randomForceBounds, randomForceBounds), 0);
        
            rb.AddTorque(massGravity * Random.Range(-randomTorqueBounds * 10, randomTorqueBounds * 10),
                massGravity * Random.Range(-randomTorqueBounds * 10, randomTorqueBounds * 10),
                massGravity * Random.Range(-randomTorqueBounds * 10, randomTorqueBounds * 10));
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
}
