using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GenerateCorps : MonoBehaviour
{
    // Start is called before the first frame update
    class ParentClass
    {
        public GameObject parentObject;
        public string nameLevel;
    }

    [SerializeField] private Character character;
    public GameObject objectGenerateOnDieCharacter;
    private ParentClass parentClasses;
    // Update is called once per frame
    private GameObject[] parents;
    [SerializeField] private LevelManager levelManager;
    private void Start()
    {
        parents = GameObject.FindGameObjectsWithTag("Level");
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        for (int i = 0; i < parents.Length; i++)
        {
            parents[i].SetActive(false);
        }

        if(levelManager != null)
            parents[levelManager.GetCurrentLevel()].SetActive(true);

        parentClasses = new ParentClass();
        SettingParent();
    }
    void Update()
    {
        CheckGenerateCorp();
        
        //ZONA DE TESTEO//
        //if (Input.GetKeyDown(KeyCode.Keypad0))
        //{
        //    if (character == null) return;
        //    character.SetHP(0);
        //}
        //-------------//
    }
    public void SettingParent()
    {
        GameObject currentParent = null;
        int index = 0;
        for (int i = 0; i < parents.Length; i++)
        {
            if (parents[i].activeSelf)
            {
                currentParent = parents[i];
                index = i;
                i = parents.Length;
            }
        }
        if (currentParent != null)
        {
            parentClasses.parentObject = currentParent;
            parentClasses.nameLevel = "Level" + index;
        }
    }
    public void CheckGenerateCorp()
    {
        if (character == null) return;

        if (character.GetHP() <= 0)
            GenerateCorp();
    }
    public void GenerateCorp()
    {
        SettingParent();
        Instantiate(objectGenerateOnDieCharacter, transform.position, Quaternion.identity, parentClasses.parentObject.transform);

        //ZONA DE TESTEO//
        //character.SetHP(100);
    }
}
