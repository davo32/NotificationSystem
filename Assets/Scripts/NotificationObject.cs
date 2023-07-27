using UnityEngine;
using TMPro;
using System.Collections;

namespace NotificationSystem
{
    public class NotificationObject : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI HeaderText;
        [SerializeField] TextMeshProUGUI SubText;

        public float EmergencyDeleteTime;

        public void CreateNotification(NotificationStruct input)
        {
            HeaderText.text = input.Header;
            SubText.text = input.Description;
            StartCoroutine(EmergencyDelete());
        }

        IEnumerator EmergencyDelete()
        { 
            yield return new WaitForSeconds(EmergencyDeleteTime);
            Destroy(gameObject);
        }
    }
}


