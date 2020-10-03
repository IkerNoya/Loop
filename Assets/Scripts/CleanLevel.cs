using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanLevel : MonoBehaviour
{
    [System.Serializable]
    class ObjectsClean
    {
        public GameObject objectOnDieCharacter;
        public bool recycled;
    }
    // Start is called before the first frame update
    private List<ObjectsClean> objectsClean;
    void Start()
    {
        objectsClean = new List<ObjectsClean>();
    }
    private void OnEnable()
    {
        GenerateCorps.OnCorpGenerate += AddListClean;
    }
    private void OnDisable()
    {
        SettingDestroyObjects();
        GenerateCorps.OnCorpGenerate -= AddListClean;
    }
    public void AddListClean(GenerateCorps gc, GameObject go, bool recycled)
    {
        ObjectsClean objectClean = new ObjectsClean();
        objectClean.objectOnDieCharacter = go;
        objectClean.recycled = recycled;
        objectsClean.Add(objectClean);

        //Debug.Log(objectsClean.Count);
    }
    public void SettingDestroyObjects()
    {
        //if (objectsClean == null) return;
        //if (objectsClean.Count <= 0) return;
        for (int i = 0; i < objectsClean.Count; i++)
        {
            //Debug.Log("DESTRUI AL " + i);
            if (!objectsClean[i].recycled)
            {
                Destroy(objectsClean[i].objectOnDieCharacter);
                //Debug.Log("DESTRUI AL " +i);
            }
        }
        objectsClean.Clear();
    }
}
