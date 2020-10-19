using AlertToCareApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlertToCareApi.EntriesValidator
{
    public class PatientInfoValidator
    {
        public static bool CheckIfPatientIdIsValid(int patientId)
        {
            ConfigDbContext _context = new ConfigDbContext();
            var patientStore = _context.Patients.ToList();
            var findPatientWithId = patientStore.FirstOrDefault(item => item.PatientId == patientId);
            if (findPatientWithId == null)
            {
                return false;
            }
            return true;
        }
        private static bool ValidatePatientInfo(Patients patient)
        {
            if (CheckIfLengthIsValid(patient.ContactNo, 10) && CheckIfNameIsValid(patient.PatientName))
            {
                return true;
            }
            return false;

        }
        private static bool CheckForBedsAvailability(int countOfAvailableBeds)
        {
            if (countOfAvailableBeds == 0)
                return false;
            else
                return true;
        }

        public static void ValidateInfoAndCheckForAvailability(Patients patient, int countOfAvailableBeds, ref bool validInfo, ref string message)
        {
            if (ValidatePatientInfo(patient))
            {
                if (CheckForBedsAvailability(countOfAvailableBeds))
                {
                    validInfo = true;
                    message = "";
                }
                else
                {
                    validInfo = false;
                    message = "All Beds Are Occupied, Cannot Add More Patients";
                }
            }
            else
            {
                validInfo = false;
                message = "Patient Information Is Incorrect, Please Recheck";
            }
        }
        private static bool CheckIfLengthIsValid(string contactNo, int length)
        {
            if (contactNo.Length == length)
            {
                return true;
            }
            return false;
        }

        private static bool CheckIfNameIsValid(string patientName)
        {
            if (patientName == "" || !Regex.Match(patientName, @"^[A-Za-z]+[\s][A-Za-z]+$").Success)
            {
                return false;
            }
            return true;

        }
    }
}
