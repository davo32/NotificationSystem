namespace NotificationSystem
{
    public struct NotificationStruct
    {
        public string Header;
        public string Description;
        public NotificationEnum Condition;

        public NotificationStruct(string a, string b,NotificationEnum c)
        {
            Header = a;
            Description = b;
            Condition = c;
        }

    }
}
