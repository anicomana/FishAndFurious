using UnityEngine;

public class SectionSpawnManager : MonoBehaviour
{
    GroundMovement groundMovement;
    

    public GameObject[] sectionToSpawn;
    public int nInitialSections = 5;

    public Vector3 firstSectionPos = new Vector3(0, 0, 6);
    public Vector3 addSectionValue = new Vector3(0, 0, 2);
    public Vector3 lastSectionSpawnPos;

    void Start()
    {
        //groundMovement.OnMoved += InstantiateRandomSection;
        lastSectionSpawnPos = firstSectionPos;
        InstantiateRandomSection();

        //for cycle to spawn next sections nCycles times in lastSectionSpawnPos
        for (int i = 0; i < nInitialSections; i++) {
            lastSectionSpawnPos += addSectionValue;
            InstantiateRandomSection();
        }  
    }

    void Update()
    {
        //when ground moves, spawn another section in lastSectionPos
    }

    void InstantiateRandomSection()
    {
        int randomSection = Random.Range(0, sectionToSpawn.Length);
        Instantiate(sectionToSpawn[randomSection], lastSectionSpawnPos, Quaternion.identity);
        Debug.Log("New section spawned in:" +lastSectionSpawnPos);
    }
}
