using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7_DB_EXPORT
{
    class DBProcessor
    {
        private static StringArray box(string str)
        {
            StringArray array = new StringArray();
            array.Insert(str);
            return array;
        }

        public static Patients processSecret1(DBUtil.DBResult result)
        {
            Patients Patients = new Patients();
            for(int i=0;i<result.Count;i++)
            {
                List < string > row = result[i];
                Patient Patient = HL7Util.getEmptyPatient();
                HL7Util.setFirstName(ref Patient, row[0]);
                HL7Util.setSecondName(ref Patient, row[1]);
                HL7Util.setFamily(ref Patient, row[2]);
                HL7Util.addContact(ref Patient, "email", row[4]);
                HL7Util.addIdentifier(ref Patient, "card", row[5]);
                HL7Util.addIdentifier(ref Patient, "externalCard", row[6]);
                HL7Util.setPatientalCode(ref Patient, row[7]);
                HL7Util.addIdentifier(ref Patient, "identityDocumentNumber", row[8]);
                HL7Util.setGender(ref Patient, row[9]);
                HL7Util.addStreetPart(ref Patient, row[10]);
                HL7Util.addStreetPart(ref Patient, row[11]);
                HL7Util.addStreetPart(ref Patient, row[12]);
                HL7Util.setCity(ref Patient, row[13]);
                HL7Util.setPostalCode(ref Patient, row[14]);
                HL7Util.addIdentifier(ref Patient, "territorialUnitId", row[15]);
                HL7Util.addIdentifier(ref Patient, "agreesForEmailVisitNotifications", row[16]);
                HL7Util.addIdentifier(ref Patient, "guardian", row[17]);
                HL7Util.addIdentifier(ref Patient, "patientGuardianId", row[18]);
                HL7Util.addContact(ref Patient, "phone", row[19]);
                Patients.Insert(Patient);
            }
            return Patients;
        }

        public static Patients processSecret2(DBUtil.DBResult result)
        {
            Patients Patients = new Patients();
            for (int i = 0; i < result.Count; i++)
            {
                List<string> row = result[i];
                Patient Patient = HL7Util.getEmptyPatient();
                HL7Util.addIdentifier(ref Patient, "Card", row[0]);
                HL7Util.setFirstName(ref Patient, row[1]);
                HL7Util.setSecondName(ref Patient, row[2]);
                HL7Util.setFamily(ref Patient, row[3]);
                if(row[4]== "0000000001")
                    HL7Util.setGender(ref Patient, "female"); 
                else
                    HL7Util.setGender(ref Patient, "male"); 
                HL7Util.addStreetPart(ref Patient, row[5]);
                HL7Util.addStreetPart(ref Patient, row[6]);
                HL7Util.addStreetPart(ref Patient, row[7]);
                HL7Util.setPostalCode(ref Patient, row[8]);
                HL7Util.setCity(ref Patient, row[9]);
                HL7Util.addIdentifier(ref Patient, "gmina", row[10]);
                HL7Util.addIdentifier(ref Patient, "miasto", row[11]);
                HL7Util.addIdentifier(ref Patient, "wojewodztwo", row[12]);
                HL7Util.setCountry(ref Patient, row[13]);
                HL7Util.setVatCode(ref Patient, row[14]);
                HL7Util.setPatientalCode(ref Patient, row[15]);
                HL7Util.setBirthDate(ref Patient, row[16]);
                HL7Util.addIdentifier(ref Patient, "birthPlace", row[17]);
                HL7Util.addContact(ref Patient, "email", row[18]);
                HL7Util.addContact(ref Patient, "phone", row[19]);
                HL7Util.addContact(ref Patient, "phone", row[20]);
                HL7Util.addContact(ref Patient, "phone", row[21]);
                HL7Util.addIdentifier(ref Patient, "smsReceiver", row[22]);
                HL7Util.addIdentifier(ref Patient, "emailReceiver", row[22]);
                Patients.Insert(Patient);
            }
            return Patients;
        }
    }
}