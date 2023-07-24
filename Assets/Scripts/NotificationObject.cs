using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace NotificationSystem
{
    public class NotificationObject : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI HeaderText;
        [SerializeField] TextMeshProUGUI SubText;

        public void CreateNotification(NotificationStruct input)
        {
            HeaderText.text = input.Header;
            SubText.text = input.Description;
        }

        //create notificaiton object
        //set Notificaiton into list
        //call CreateNotification
    }
}


