using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TaskSystem : MonoSingleton<TaskSystem>
{
    public GameObject tempObject, tempObjectPos;
    public int searchMaterialCount, searchTypeCount, columnCount, lineCount;
    public int wrongCount = 0, maxWrongCount = 2;

    public void NewSearchMaterial()
    {
        do
        {
            columnCount = Random.Range(0, 8);
            lineCount = Random.Range(0, 3);
        }
        while (!CabinetSystem.Instance.CabinetClass[lineCount].ObjectGridBool[columnCount]);

        ObjectID objectID = CabinetSystem.Instance.CabinetClass[lineCount].ObjectGridGameObject[columnCount].GetComponent<ObjectID>();

        tempObject.transform.GetChild(searchTypeCount).gameObject.SetActive(false);
        searchMaterialCount = objectID.materialCount;
        searchTypeCount = objectID.objectID;
        tempObject.transform.GetChild(searchTypeCount).gameObject.SetActive(true);
        tempObject.transform.GetChild(searchTypeCount).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.ObjectMateral[searchMaterialCount];
    }

    public IEnumerator TrueObjectTaskMove()
    {
        tempObject.transform.DOMove(CabinetSystem.Instance.CabinetClass[lineCount].ObjectGridGameObject[columnCount].transform.position, 1.5f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.8f);
        //partical
        CabinetSystem.Instance.CabinetClass[lineCount].ObjectGridGameObject[columnCount].SetActive(false);
        tempObject.transform.position = tempObjectPos.transform.position;
        TaskSystem.Instance.NewSearchMaterial();

    }

    public Image CallWrong(Image freeImage, Sprite freeSprite, Material RedMat)
    {
        Image tempImage = freeImage;
        tempImage.sprite = freeSprite;
        Material mat = new Material(MateraiSystem.Instance.Mat2D.shader);
        tempImage.material = mat;
        tempImage.color = RedMat.color;
        return tempImage;
    }

    public bool CheckFinish()
    {
        bool isFinish = true;

        for (int i1 = 0; i1 < 3; i1++)
            for (int i2 = 0; i2 < 8; i2++)
                if (CabinetSystem.Instance.CabinetClass[i1].ObjectGridBool[i2])
                    isFinish = false;

        return isFinish;
    }
}
