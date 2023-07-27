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

        [SerializeField] private CanvasGroup Single;
        bool SingleToggle = false;

        [SerializeField] private CanvasGroup Multi;
        bool MultiToggle = false;

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

        //-------------------------------------------------------------------------------------

        public void ToggleMulti()
        { 
            MultiToggle = !MultiToggle;

            if (MultiToggle)
            {
                Multi.alpha = 1.0f;
                Multi.interactable = true;
                Multi.blocksRaycasts = true;
            }
            else
            { 
                Multi.alpha = 0.0f; 
                Multi.interactable = false;
                Multi.blocksRaycasts = false;
            }
        }

        public void ToggleSingle()
        {
            SingleToggle = !SingleToggle;

            if (SingleToggle)
            {
                Single.alpha = 1.0f;
                Single.interactable = true;
                Single.blocksRaycasts = true;
            }
            else
            {
                Single.alpha = 0.0f;
                Single.interactable = false;
                Single.blocksRaycasts = false;
            }
        }
    }
}


