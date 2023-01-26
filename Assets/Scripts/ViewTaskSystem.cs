using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ViewTaskSystem : MonoSingleton<ViewTaskSystem>
{
    [SerializeField] Sprite wrongMark;
    [SerializeField] List<Image> WrongImage = new List<Image>();
    [SerializeField] private Image tempImage;
    [SerializeField] private Material redMat;


    public IEnumerator WrongCanvasMove(GameObject obj)
    {
        if (Input.touchCount > 0)
        {
            if (TaskSystem.Instance.wrongCount >= TaskSystem.Instance.maxWrongCount)
            {
                GameManager.Instance.isStart = false;
                Buttons.Instance.failPanel.SetActive(true);
                GameManager.Instance.isStart = false;
                CabinetSystem.Instance.AllObjectClose();
            }

            tempImage.gameObject.SetActive(true);
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            tempImage.gameObject.transform.position = touchPosition;
            tempImage = TaskSystem.Instance.CallWrong(tempImage, wrongMark, redMat);
            tempImage.gameObject.transform.DOMove(WrongImage[TaskSystem.Instance.wrongCount].gameObject.transform.position, 1.5f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(1.5f);
            TaskSystem.Instance.wrongCount++;
            tempImage.gameObject.SetActive(false);
            WrongImage[TaskSystem.Instance.wrongCount - 1] = TaskSystem.Instance.CallWrong(WrongImage[TaskSystem.Instance.wrongCount - 1], wrongMark, redMat);
            obj.SetActive(false);
        }
    }
}
