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


    private void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    IEnumerator SpawnWeapon()
    {
        index = Random.Range(0, spawnPos.Length);

        Instantiate(prefab, new Vector3 (spawnPos[index], 7f, 0f), Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        yield return SpawnWeapon();
    }
}
