using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotificationSystem
{
    public class NotificationContainer : MonoBehaviour
    {
        public float DeleteSpeed = 3;
        public float Showspeed = 2;
        private GameObject CurrentNotification;
        [SerializeField] private GameObject NotificationUI;
        public List<NotificationStruct> notificationData = new List<NotificationStruct>();

        private void Start()
        {
            CreateNotificationData("Friend Request", "Nova sent a friend request");
            CreateNotificationData("Quest Added", "Started Quest Shrine Destruction");
            CreateNotificationData("Quest Added", "Started Quest Seeing Green");

            StartCoroutine(BufferNotifications());
        }

        //Call this function when creating new Notifications
        public void CreateNotificationData(string Title, string Information)
        {
            notificationData.Add(new NotificationStruct(Title, Information));
        }

        private GameObject CreateCopy()
        {
            return Instantiate(NotificationUI);
        }

        IEnumerator BufferNotifications()
        {
            while (notificationData.Count > 0)
            {
                CurrentNotification = CreateCopy();
                CurrentNotification.transform.SetParent(this.transform, false);
                CurrentNotification.transform.position = transform.position;
                CurrentNotification.GetComponent<NotificationObject>().CreateNotification(notificationData[0]);
                
                GetComponent<AudioSource>().Play();

                yield return new WaitUntil(() => notificationData[0].Condition);

                //yield return new WaitForSeconds(DeleteSpeed);
                notificationData.RemoveAt(0);
                Destroy(CurrentNotification);
                CurrentNotification = null;

                yield return new WaitForSeconds(Showspeed);
            }
        }
    }
}

//create notification
//use conditon with WaitUntil condition

