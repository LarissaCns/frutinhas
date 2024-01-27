using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSpawnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float[] spawnPos;
    private int index;
    [SerializeField]
    private float maxTime, minTime;
    [SerializeField]
    private List <GameObject> weaponsList = new List<GameObject>();
    [SerializeField]
    private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    private void Update()
    {
        for (int i = 0; i < weaponsList.Count; i++)
        {
            if (weaponsList[i] == null)
            {
                weaponsList.Remove(weaponsList[i]);
            }
        }
    }

    IEnumerator SpawnWeapon()
    {
        index = Random.Range(0, spawnPos.Length);
        canSpawn = true;

        for (int i = 0; i < weaponsList.Count; i++)
        {
            if (weaponsList[i] != null && weaponsList[i].transform.position.x == spawnPos[index])
            {
                canSpawn = false;
            }
        }

        if (canSpawn)
        {
            weaponsList.Add(Instantiate(prefab, new Vector3(spawnPos[index], 7f, 0f), Quaternion.identity));
        }

        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        yield return SpawnWeapon();
    }
}
