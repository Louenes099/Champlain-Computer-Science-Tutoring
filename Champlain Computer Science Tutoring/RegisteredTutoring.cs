using System;
using System.Collections.Generic;
using System.Text;

namespace Champlain_Computer_Science_Tutoring
{
    public class RegisteredTutoring
    {
        public string Key { get; set; }
        public string StudentID { get; set; }
        public Tutoring[] TutoringSession { get; set; }
    }
}
