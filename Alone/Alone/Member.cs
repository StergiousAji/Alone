using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Alone
{
    [Serializable]

    public class Member
    {
        //Attributes
        private string firstName, lastName, username, email, password, errorMessage;
        private DateTime dateOfBirth;
        private int score, highScore;

        //Default Constructor
        public Member() {}

        //Properties
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (Validate_Name(value, "first"))
                    firstName = value;
                else
                    throw new CustomException(errorMessage);
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (Validate_Name(value, "last"))
                    lastName = value;
                else
                    throw new CustomException(errorMessage);
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (Validate_Username(value))
                    username = value;
                else
                    throw new CustomException(errorMessage);
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (Validate_Email(value))
                    email = value;
                else
                    throw new CustomException(errorMessage);
            }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                if (Validate_DateOfBirth(value))
                    dateOfBirth = value;
                else
                    throw new CustomException(errorMessage);
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (Validate_Password(value))
                    password = value;
                else
                    throw new CustomException(errorMessage);
            }
        }

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                if (score > highScore)
                    highScore = score;
            }
        }

        public int HighScore
        {
            get { return highScore; }
        }

        //Validation Methods
        public bool Validate_Name(string n, string type)
        {
            //If text field is empty then ERROR.
            if (String.IsNullOrEmpty(n))
            {
                errorMessage = $"Please provide your {type} name.";
                return false;
            }

            //If any of the characters in the names is a number, spaces or punctuation then ERROR.
            foreach (char c in n)
            {
                if (Char.IsNumber(c) || Char.IsWhiteSpace(c) || Char.IsPunctuation(c))
                {
                    errorMessage = $"This {type} name is not valid.";
                    return false;
                }
            }

            return true;
        }

        public bool Validate_Username(string u)
        {
            //If text field is empty then ERROR.
            if (String.IsNullOrEmpty(u))
            {
                errorMessage = $"Please provide a username.";
                return false;
            }

            //If text field contains whitespaces then ERROR.
            if (u.Contains(" "))
            {
                errorMessage = $"This username is invalid.";
                return false;
            }

            ReadFromFile(u);

            //Check if the user already exists.
            foreach (Member m in FrmRegistration.members)
            {
                if (m == null)
                    break;

                if (u == m.Username)
                {
                    errorMessage = $"This username already exists.";
                    return false;
                }
            }

            return true;
        }

        public bool Validate_Email(string e)
        {
            //If text field is empty then ERROR.
            if (String.IsNullOrEmpty(e))
            {
                errorMessage = "Please provide your email address.";
                return false;
            }

            //If email address does not contain '@' and '.' or contains whitespaces then ERROR.
            if (!(e.Contains("@") && e.Contains(".")) || e.Contains(" "))
            {
                errorMessage = "This e-mail address is not valid.";
                return false;
            }

            return true;
        }

        public bool Validate_DateOfBirth(DateTime dob)
        {
            int age = DateTime.Now.Year - dob.Year;
            //Check to see if an extra year has been added to the age due to the difference in months. If so decrement age.
            if (dob > DateTime.Now.AddYears(-age))
                age--;

            //If user is under 7 years then ERROR.
            if (age < 7)
            {
                errorMessage = "You have to be above 7 years to register an account.";
                return false;
            }

            return true;
        }

        public bool Validate_Password(string p)
        {
            //If text field is empty then ERROR.
            if (String.IsNullOrEmpty(p))
            {
                errorMessage = "Please provide a password.";
                return false;
            }

            //If password is less than 6 characters then ERROR.
            if (p.Length < 6)
            {
                errorMessage = "Your password is too short.";
                return false;
            }

            //Count how many digits there are in the user's password.
            int digits = 0;
            foreach (char c in p)
            {
                if (Char.IsNumber(c))
                    digits++;
            }

            //If password does not contain at least 1 digit then ERROR.
            if (digits < 1)
            {
                errorMessage = "Your password should contain at least 1 digit.";
                return false;
            }

            //If text field contains whitespaces then ERROR.
            if (p.Contains(" "))
            {
                errorMessage = $"This password contains whitespaces.";
                return false;
            }

            return true;
        }

        public void WriteToFile(int numOfMembers)
        {
            //Open the file in create mode to overwrite the first member in the members array to the file.
            Stream sw = File.Open(@".\MemberData.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(sw, FrmRegistration.members[0]);
            sw.Close();

            //Open the file in append mode to add the subsequent members in the members array.
            sw = File.Open("MemberData.bin", FileMode.Append);
            for (int i = 1; i < numOfMembers; i++)
                bf.Serialize(sw, FrmRegistration.members[i]);

            sw.Close();
        }

        public void ReadFromFile(string username)
        {
            string cwd = Directory.GetCurrentDirectory();
            //Open the file to read every member registered and place them in the members array.
            Stream sr = File.OpenRead(@".\MemberData.bin");
            BinaryFormatter bf = new BinaryFormatter();
            int i = 0;

            while (sr.Position < sr.Length)
            {
                FrmRegistration.members[i] = (Member)bf.Deserialize(sr);
                i++;
            }

            sr.Close();
        }

        public int CountMembers()
        {
            //Returns the total number of members that are not null in the array.
            int n = 0;
            foreach (Member m in FrmRegistration.members)
            {
                if (m == null)
                    break;
                else
                    n++;
            }

            return n;
        }
    }
}
