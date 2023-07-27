using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace NotificationSystem
{
    public class NotificationGenerator : MonoBehaviour
    {
        [SerializeField] NotificationContainer NotifyContainer;

        [SerializeField] private TMP_InputField Title;
        [SerializeField] private TMP_InputField Info;
        [SerializeField] private TMP_Dropdown Type;

        [SerializeField] private Button GenerateBtn;
        [SerializeField] private Button ClearBtn;
        readonly List<string> m_options = new List<string> { "None", "Quest", "Friend Request", "LevelProgression", "New Message" };

        bool CheckFields()
        {
            string _title = Title.text;
            string _info = Info.text;

            return (string.IsNullOrWhiteSpace(_title)) && (string.IsNullOrWhiteSpace(_info) && (Type.value == 0));
        }

        void ClearFields()
        {
            Title.text = "";
            Info.text = "";
            Type.value = 0;
            Type.ClearOptions();
            Type.AddOptions(m_options);
        }

        void SendGeneration()
        {
            NotificationEnum typeData = NotificationEnum.None;

            switch (Type.value)
            {
                case 0:
                    {
                        Debug.Log("[ERROR] - Empty Type");
                        break;
                    }
                case 1:
                    {
                        typeData = NotificationEnum.Quest;
                        break;
                    }
                case 2:
                    {
                        typeData = NotificationEnum.FriendsRequest;
                        break;
                    }
                case 3:
                    {
                        typeData = NotificationEnum.LevelProgression;
                        break;
                    }
                case 4:
                    {
                        typeData = NotificationEnum.NewMessage;
                        break;
                    }
            }



            if(CheckFields())
            {
                Debug.Log("[ERROR] - All Fields were not filled out!");
                ClearFields();
            }
            else if (!CheckFields())
            {
                //send the generation to the right place
                NotifyContainer.CreateNotificationData(Title.text, Info.text, typeData);
                ClearFields();
            }
            

        }


        // Start is called before the first frame update
        void Start()
        {
            ClearBtn.onClick.AddListener(ClearFields);
            GenerateBtn.onClick.AddListener(SendGeneration);
            Type.ClearOptions();
            Type.AddOptions(m_options);
        }

        // Update is called once per frame
        void Update()
        {
            GenerateBtn.interactable = !CheckFields();
            ClearBtn.interactable = !CheckFields();
        }
    }
}
