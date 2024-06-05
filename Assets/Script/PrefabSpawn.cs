using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using System.Linq;
using UnityEngine.XR.ARSubsystems;
using Unity.VisualScripting;

public class PrefabSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] objPrefab;

    [SerializeField] ARSession aRSession;

    [SerializeField] private List<GameObject> spawnPrefabList = new List<GameObject>();
    private ARTrackedImageManager arTrackedIMGmanager;

    private void OnEnable()
    {
        arTrackedIMGmanager = gameObject.GetComponent<ARTrackedImageManager>();
        arTrackedIMGmanager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        arTrackedIMGmanager.trackedImagesChanged -= OnImageChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetAr();
            // arTrackedIMGmanager.referenceLibrary = xRReferenceImages;
        }
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        // if (args.updated.Count > 1)
        // {
        //     aRSession.Reset();
        // }
        // else
        // {

        // }

        foreach (var image in args.added)
        {
            // if (image.referenceImage.name == "Koyuki")
            // {
            //     Debug.Log("Spawn");
            // }

            // XRReferenceImage refImage = image.referenceImage;
            // string imgName = refImage.name; // this is the name in the library
            // Debug.Log(imgName);

            // Debug.Log(image.name);
            // Debug.Log(image.referenceImage);
            // Debug.Log(image.referenceImage.name);
            // Debug.Log(image.trackingState);
            // if (image.name != "Koyuki")
            // {
            // prefab = Instantiate(objPrefab, image.transform);
            //     prefab.transform.position += prefabOffset;
            // }
            // aRSession.Reset();

            Debug.Log($"Image: {image.referenceImage.name}");

            // InstantiatePrefabList(image.referenceImage.name, image.transform);

            foreach (GameObject prefab in objPrefab)
            {
                if (prefab.name.Equals(image.referenceImage.name))
                {
                    GameObject spawnPrefab = Instantiate(prefab, image.transform);
                    spawnPrefab.name = prefab.name;
                    spawnPrefabList.Add(spawnPrefab);
                }
            }

            // foreach (var trackedImage in arTrackedIMGmanager.trackables)
            // {
            //     // InstantiatePrefabList(trackedImage.referenceImage.name, trackedImage.transform);
            //     // Debug.Log($"Image: {image.referenceImage.name}");
            // }
            // ListAllImages();

        }
        foreach (var image in args.removed)
        {

        }
    }

    void ListAllImages()
    {
        Debug.Log(
            $"There are {arTrackedIMGmanager.trackables.count} images being tracked.");

        foreach (var trackedImage in arTrackedIMGmanager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                      $"{trackedImage.transform.position}");
        }
    }

    ARTrackedImage GetImageAt(TrackableId trackableId)
    {
        return arTrackedIMGmanager.trackables[trackableId];
    }

    private void InstantiatePrefabList(string imgName, Transform imgPos)
    {
        foreach (GameObject prefab in objPrefab)
        {
            if (prefab.name.Equals(imgName))
            {
                if (!CheckIsNowSpawn(imgName))
                {
                    GameObject spawnPrefab = Instantiate(prefab, imgPos);
                    spawnPrefab.name = prefab.name;
                    spawnPrefabList.Add(spawnPrefab);
                }
            }
        }
    }

    private bool CheckIsNowSpawn(string imgName)
    {
        if (spawnPrefabList != null)
        {
            foreach (GameObject nowSpawnPrefab in spawnPrefabList)
            {
                if (nowSpawnPrefab.name.Equals(imgName))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ResetAr()
    {
        if (spawnPrefabList != null)
        {
            foreach (GameObject nowSpawnPrefab in spawnPrefabList)
            {
                Destroy(nowSpawnPrefab);
            }
        }
        spawnPrefabList.Clear();
        aRSession.Reset();
    }

}
