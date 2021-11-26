using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Champlain_Computer_Science_Tutoring
{
    public class User : INotifyPropertyChanged
    {
        public string Key { get; set; }
        public string UserID { get; set; }
        public string userId
        {
            get => UserID;
            set
            {
                if (value == UserID)
                    return;
                UserID = value;
                this.UserID = value;
                OnPropertyChanged(nameof(userId));
                OnPropertyChanged(nameof(GetUserId));
            }
        }
        public string GetUserId
        {
            get => $"You entered {userId}";
        }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Authentication { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
