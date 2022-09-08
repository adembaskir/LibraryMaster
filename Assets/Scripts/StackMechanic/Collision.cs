using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Book")
        {
            if (!StackMechanic.Instance.book.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "Untagged";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                
                StackMechanic.Instance.StackCube(other.gameObject, StackMechanic.Instance.book.Count - 1);


            }
        }
     
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "DropArea")
        {
            StartCoroutine(StackMechanic.Instance.DropMoney(other.gameObject));


        }
    }
}
