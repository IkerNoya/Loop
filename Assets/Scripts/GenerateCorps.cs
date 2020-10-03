using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GenerateCorps : MonoBehaviour
{
    // Start is called before the first frame update
    class ParentClass
    {
        public GameObject levelsObjects;
        public string nameLevel;
    }

    [SerializeField] private PlayerController PlayerReference;
    public GameObject objectCorpGenerate;
    private ParentClass[] parentClasses;
    // Update is called once per frame

    private void Start()
    {
        GameObject[] parents = GameObject.FindGameObjectsWithTag("Level");
        parentClasses = new ParentClass[parents.Length];

        for (int i = 0; i < parentClasses.Length; i++)
        {
            parentClasses[i].nameLevel = "Level" + (i + 1);
        }
    }
    void Update()
    {
        
    }

    public void CheckGenerateCorp()
    {
        //Instantiate(objectCorpGenerate,)
    }
}
