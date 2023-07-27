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

        bool isRunning = false;

        private void Start()
        {
            CreateNotificationData("Friend Request", "Nova sent a friend request", NotificationEnum.FriendsRequest);
            CreateNotificationData("Quest Added", "Started Quest Shrine Destruction", NotificationEnum.Quest);

            StartCoroutine(BufferNotifications());
        }


        //Call this function when creating new Notifications
        public void CreateNotificationData(string Title, string Information, NotificationEnum condition)
        {
            notificationData.Add(new NotificationStruct(Title, Information,condition));
            if(!isRunning) StartCoroutine(BufferNotifications());
          
        }

        private GameObject CreateCopy()
        {
            return Instantiate(NotificationUI);
        }

        public IEnumerator BufferNotifications()
        {
            isRunning = true;
            while (notificationData.Count > 0)
            {
                CurrentNotification = CreateCopy();
                CurrentNotification.transform.SetParent(this.transform, false);
                CurrentNotification.transform.position = transform.position;
                CurrentNotification.GetComponent<NotificationObject>().EmergencyDeleteTime = DeleteSpeed;
                CurrentNotification.GetComponent<NotificationObject>().CreateNotification(notificationData[0]);
                
                GetComponent<AudioSource>().Play();

                switch(notificationData[0].Condition)
                {
                    case NotificationEnum.Quest:
                        {
                            Debug.Log("QUEST");
                            yield return new WaitForSeconds(DeleteSpeed);
                            break;
                        }
                    case NotificationEnum.NewMessage:
                        {
                            Debug.Log("NewMessage");
                            yield return new WaitForSeconds(DeleteSpeed);
                            break;
                        }
                    case NotificationEnum.FriendsRequest: 
                        {
                            Debug.Log("FriendsRequest");
                            yield return new WaitForSeconds(DeleteSpeed);
                            break;
                        }
                }

                Debug.Log("COUNT: " + notificationData.Count);
                if (notificationData.Count >= 0)
                {
                    notificationData.RemoveAt(0);
                }
                    Destroy(CurrentNotification);
                    CurrentNotification = null;
                

                yield return new WaitForSeconds(Showspeed);
            }
            isRunning = false;
        }
    }
}

//create notification
//use conditon with WaitUntil condition

