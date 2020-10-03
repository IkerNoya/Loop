using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
public class GenerateCorps : MonoBehaviour
{
    // Start is called before the first frame update
    class ParentClass
    {
        public GameObject parentObject;
        public string nameLevel;
    }

    [System.Serializable]
    class ObjectInstanciate
    {
        public GameObject objectOnDieCharacter;
        public bool recycled;
    }
    [SerializeField] private Character character;
    [SerializeField] private ObjectInstanciate objectBloodCharacter;
    [SerializeField] private ObjectInstanciate objectCorpCharacter;
    public static event Action<GenerateCorps, GameObject, bool> OnCorpGenerate;
    private ParentClass parentClasses;
    [SerializeField] GameObject[] parents;
    [SerializeField] private LevelManager levelManager;
    private int inLevel;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
 
        parentClasses = new ParentClass();
        SettingParent();
        inLevel = levelManager.GetCurrentLevel();
    }
    void Update()
    {
        CheckGenerateCorp();

        //ZONA DE TESTEO//
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (character == null) return;
            character.SetHP(0);
        }
        //-------------//
    }
    public void SettingParent() {
        if (parents[levelManager.GetCurrentLevel()] != null) {
            parentClasses.parentObject = parents[levelManager.GetCurrentLevel()];
            parentClasses.nameLevel = "Level" + levelManager.GetCurrentLevel();
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
        GameObject go = null;
        go = Instantiate(objectCorpCharacter.objectOnDieCharacter, transform.position, Quaternion.identity, parentClasses.parentObject.transform);
        if (OnCorpGenerate != null)
        {
            //Debug.Log(objectCorpCharacter.recycled);
            OnCorpGenerate(this, go, objectCorpCharacter.recycled);
        }
        go = Instantiate(objectBloodCharacter.objectOnDieCharacter, transform.position, Quaternion.identity, parentClasses.parentObject.transform);
        if (OnCorpGenerate != null)
        {
            //Debug.Log(objectBloodCharacter.recycled);
            OnCorpGenerate(this, go, objectBloodCharacter.recycled);
        }
        //ZONA DE TESTEO//
        character.SetHP(100);
    }
}