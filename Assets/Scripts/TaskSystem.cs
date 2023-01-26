using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSystem : MonoSingleton<TaskSystem>
{
    public int searchMaterialCount, searchTypeCount, columnCount, lineCount;
    public int wrongCount = 0, maxWrongCount = 2;

    public void NewSearchMaterial()
    {
        do
        {
            columnCount = Random.Range(0, 8);
            lineCount = Random.Range(0, 3);
        }
        while (!CabinetSystem.Instance.CabinetClass[lineCount].ObjectGridBool[0, columnCount]);

        ObjectID objectID = CabinetSystem.Instance.CabinetClass[lineCount].ObjectGridGameObject[0, columnCount].GetComponent<ObjectID>();

        searchMaterialCount = objectID.materialCount;
        searchTypeCount = objectID.objectID;
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
                if (CabinetSystem.Instance.CabinetClass[i1].ObjectGridBool[0, i2])
                    isFinish = false;

        return isFinish;
    }
}
