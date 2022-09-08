using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackMechanic : MonoBehaviour
{
    public static StackMechanic Instance;
    public float movementDelay = 0.25f;
    public List<GameObject> book = new List<GameObject>();
    private Vector3 carryingPoint;
    public int maxCap = 5;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            MoveListElements();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            MoveOrigin();
        }
    }

    public void StackCube(GameObject other, int index)
    {
        if (book.Count <= maxCap )
        {
            //float randomRange = Random.Range(0f, 10f);
            other.transform.parent = transform;
            carryingPoint = book[index].transform.localPosition;
            carryingPoint.y += 0.1f;
            carryingPoint.z = -1;
          
            other.transform.localPosition = carryingPoint;

            book.Add(other);
        }
        StartCoroutine(MakeObjectsBigger());
       
    }
    public IEnumerator MakeObjectsBigger()
    {
        for (int i = book.Count -1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(1,1,1);
            scale *= 1.5f;

            book[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
             book[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            yield return new WaitForSeconds(0.05f);


        }

    }
    public IEnumerator DropMoney(GameObject dropArea)
    {
        for (int i = 1; i < book.Count; i++)
        {
            book[i].transform.parent = dropArea.transform;
            book[i].tag = "Untagged";
            book.RemoveAt(i);
            Debug.Log(i);
            yield return new WaitForSeconds(.1f);
        }
        
    }

    private void MoveListElements()
    {
        for (int i = 1; i < book.Count; i++)
        {
            Vector3 pos = book[i].transform.localPosition;
            pos.x = book[i - 1].transform.localPosition.x;
            book[i].transform.DOLocalMove(pos, movementDelay);

        }
    }
    private void MoveOrigin()
    {
        for (int i = 1; i < book.Count; i++)
        {
            Vector3 pos = book[i].transform.localPosition;
            pos.x = book[0].transform.localPosition.x;
            book[i].transform.DOLocalMove(pos, 0.70f );

        }
    }
}
