using System.Collections.Generic;
using UnityEngine;

public class CabinetSystem : MonoSingleton<CabinetSystem>
{
    [System.Serializable]
    public class Cabinet
    {
        public bool[] ObjectGridBool;
        public GameObject[] ObjectGridGameObject;
        public int[] ObjectIDInt;
        public int[] ObjectMaterialCountInt;
        public float objectStartVerticalDistance;
    }
    public List<Cabinet> CabinetClass = new List<Cabinet>();

    [SerializeField] private GameObject _objectPosTemplate;
    [SerializeField] private int _OPObjectCount;

    public float cabinetEmptyDistanceVertical, cabinetEmptyDistanceHorizantal;
    public float cabinetLineDistance, cabinetColumnDistance;
    public int cabinetColumnCount = 8;

    public void StartCabinetSystem()
    {
        ReSizeCabinetClassArray(cabinetColumnCount, CabinetClass);
        ObjectPlacement(_OPObjectCount, cabinetColumnCount, ItemData.Instance.field.taskObjectTypeCount, MateraiSystem.Instance.ObjectMateral.Count, _objectPosTemplate, ScaleSystem.Instance.scaleHorizontalDisctance, ScaleSystem.Instance.scaleVerticalDistance, cabinetLineDistance, cabinetColumnDistance, cabinetEmptyDistanceHorizantal, cabinetEmptyDistanceVertical, CabinetClass);
    }

    public void AllObjectClose()
    {
        for (int i1 = 0; i1 < CabinetClass.Count; i1++)
        {
            for (int i3 = 0; i3 < ItemData.Instance.field.cabineObjectCount; i3++)
            {
                CabinetClass[i1].ObjectGridGameObject[i3].SetActive(false);
            }
        }
    }
    public void ObjectPoolAdd(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        CabinetClass[objectID.cabinetCount].ObjectGridBool[objectID.columnCount] = false;
        CabinetClass[objectID.cabinetCount].ObjectGridGameObject[objectID.columnCount].SetActive(false);
    }

    private void ObjectPlacement(int OPObjectCount, int cabinetColumnCount, int maxObjectCount, int maxObjectMaterialCount, GameObject objectPosTemplate, float scaleColumn, float scaleLine, float cabinetLineDistance, float cabinetColumnDistance, float cabineEmptyColumnDistance, float cabineEmptyLineDistance, List<Cabinet> CabinetClass)
    {
        for (int i1 = 0; i1 < CabinetClass.Count; i1++)
        {
            for (int i3 = 0; i3 < cabinetColumnCount; i3++)
            {
                if (!CabinetClass[i1].ObjectGridBool[i3])
                {
                    GameObject obj = GetObject(OPObjectCount);
                    ObjectID objectID = obj.GetComponent<ObjectID>();

                    ObjectScalePlacement(obj);
                    ObjectIDPlacement(obj, objectID, maxObjectCount, maxObjectMaterialCount, i1, 0, i3, CabinetClass);
                    ObjectPositionPlacement(obj, objectPosTemplate, objectID.cabinetCount, objectID.columnCount, cabinetColumnDistance, cabinetLineDistance, cabineEmptyColumnDistance, cabineEmptyLineDistance, scaleColumn, scaleLine, CabinetClass[objectID.cabinetCount].objectStartVerticalDistance);
                }
            }
        }
    }
    private void ReSizeCabinetClassArray(int cabinetLineCount, List<Cabinet> CabinetClass)
    {
        for (int i = 0; i < CabinetClass.Count; i++)
        {
            CabinetClass[i].ObjectGridBool = new bool[cabinetLineCount];
            CabinetClass[i].ObjectGridGameObject = new GameObject[cabinetLineCount];
        }
    }

    private GameObject GetObject(int OPObjectCount)
    {
        return ObjectPool.Instance.GetPooledObject(OPObjectCount);
    }
    private void ObjectScalePlacement(GameObject obj)
    {
        obj.transform.localScale *= ScaleSystem.Instance.scale;
    }
    private void ObjectIDPlacement(GameObject obj, ObjectID objectID, int maxObjectCount, int maxObjectMaterialCount, int cabinetCount, int lineCount, int columnCount, List<Cabinet> cabinet)
    {
        int ID, materialCount;

        ID = Random.Range(0, maxObjectCount);
        materialCount = Random.Range(0, maxObjectMaterialCount);

        objectID.objectID = ID;
        objectID.materialCount = materialCount;

        GameObject child = obj.transform.GetChild(objectID.objectID).gameObject;

        child.gameObject.SetActive(true);
        child.GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;

        objectID.cabinetCount = cabinetCount;
        objectID.columnCount = columnCount;
        cabinet[objectID.cabinetCount].ObjectGridBool[objectID.columnCount] = true;
        cabinet[objectID.cabinetCount].ObjectGridGameObject[objectID.columnCount] = this.gameObject;

    }
    private void ObjectPositionPlacement(GameObject obj, GameObject objectPosTemplate, int objectCabineCount, int objectColumnCount, float cabineColumnDistance, float cabineLineDistance, float cabineEmptyColumnDistance, float cabineEmptyLineDistance, float objectColumnDistance, float objectLineDistance, float cabineLineMisDistance)
    {
        obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + (objectCabineCount - 1) * (cabineColumnDistance + cabineEmptyColumnDistance) /*+ (objectColumnCount - 1) * (objectColumnDistance / 5) * 7 + objectColumnDistance / 2*/, objectPosTemplate.transform.position.y + (objectColumnCount - 1) * (cabineEmptyLineDistance + cabineLineDistance) + cabineLineDistance / 2 + cabineLineMisDistance, objectPosTemplate.transform.position.z);
    }
}
