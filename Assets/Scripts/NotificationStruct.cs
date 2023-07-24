using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotificationSystem
{
    public struct NotificationStruct
    {
        public string Header;
        public string Description;
        public bool Condition;

        public NotificationStruct(string a, string b,bool c = false)
        {
            Header = a;
            Description = b;
            Condition = c;
        }

    }
}
