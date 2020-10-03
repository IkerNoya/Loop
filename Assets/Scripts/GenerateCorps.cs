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
    public GameObject objectCorpGenerate;
    private ParentClass parentClasses;
    // Update is called once per frame

    private void Start()
    {
        GameObject[] parents = GameObject.FindGameObjectsWithTag("Level");
        parentClasses = new ParentClass();
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
        parentClasses.parentObject = currentParent;
        parentClasses.nameLevel = "Level" + index;
    }
    void Update()
    {
        CheckGenerateCorp();
        //BORRAR LUEGO DEL TESTEO
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (character == null) return;
            character.SetHP(0);
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
        Instantiate(objectCorpGenerate, transform.position, Quaternion.identity, parentClasses.parentObject.transform);
        Destroy(character.gameObject);
    }
}
