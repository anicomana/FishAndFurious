using UnityEngine;

public class SectionSpawnManager : MonoBehaviour
{
    public GameObject[] sectionToSpawn;
    //coordinates for first section to spawn = firstSectionPos
    //lastSectionPos
    //how many times shoould for cycle be done = nCycles
    //how much to add to lastSectionPos = addSectionValue = Vector3 (0, 0, 2)

    void Start()
    {
        //LastSectionPos = firstSectionPos
        //spawn first section
        //lastSectionPos += addSectionValue;
        //for cycle to spawn next sections n times in lastSectionSpawnPos
            //lastSectionPos += addSectionValue;
            //spawn next section in lastSectionSpawnPos
            //repeat cycle
            
        
    }

    void Update()
    {
        //when world moves, spawn another section in lastSectionPos
    }
}
