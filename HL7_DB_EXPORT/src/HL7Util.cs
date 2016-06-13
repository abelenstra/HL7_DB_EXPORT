using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7_DB_EXPORT
{
    class HL7Util
    {
        private static void al(ref string stream, string line, int ix = 0)
        {
            for (int i = 0; i < ix; i++)
                stream += " ";
            stream += line + "\n";
        }

        private static void a(ref string stream, string line, int ix = 0)
        {
            for (int i = 0; i < ix; i++)
                stream += " ";
            stream += line;
        }

        public static void prepareStringArrayObj(ref string ret, string typeName, StringArray array, int ix)
        {
            bool begin = false;
            for (int i = 0; i < array.Size(); i++)
            {
                if(!begin)
                {
                    begin = true;
                    a(ref ret, String.Format("<{0}", typeName), 8);
                }
                a(ref ret, String.Format(" value=\"{0}\"", array[i]));
            }
            if(begin)
                al(ref ret, "/>");
        }

        public static void prepareSystems(ref string ret, StringArray systems, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "system", systems, ix);
        }

        public static void prepareUses(ref string ret, StringArray uses, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "use", uses, ix);
        }

        public static void prepareTypes(ref string ret, StringArray types, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "type", types, ix);
        }

        public static void prepareDiscricts(ref string ret, StringArray districts, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "district", districts, ix);
        }

        public static void prepareCountries(ref string ret, StringArray countries, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "country", countries, ix);
        }

        public static void prepareCities(ref string ret, StringArray cities, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "city", cities, ix);
        }

        public static void prepareLines(ref string ret, StringArray lines, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "line", lines, ix);
        }

        public static void prepareStates(ref string ret, StringArray states, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "state", states, ix);
        }

        public static void preparePostalCode(ref string ret, StringArray postalCodes, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "postalCode", postalCodes, ix);
        }

        public static void prepareFamilies(ref string ret, StringArray families, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "family", families, ix);
        }

        public static void prepareGivens(ref string ret, StringArray givens, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "given", givens, ix);
        }
        public static void preparePrefixes(ref string ret, StringArray prefixes, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "prefix", prefixes, ix);
        }
        public static void prepareSufixes(ref string ret, StringArray sufixes, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "sufix", sufixes, ix);
        }


        public static void prepareValues(ref string ret, StringArray values, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "value", values, ix);
        }

        public static void prepareTexts(ref string ret, StringArray texts, int ix = 0)
        {
            prepareStringArrayObj(ref ret, "text", texts, ix);
        }

        public static void preparePeroids(ref string ret, Periods peroids, int ix = 0)
        {
            for (int i = 0; i < peroids.Size(); i++)
            {
                Period peroid = peroids[i];
                if (peroid.getStart().Size() + peroid.getEnd().Size() == 0)
                    return;
                al(ref ret, "<peroid>", ix);
                if (peroid.getStart().Size() > 0)
                    al(ref ret, String.Format("<start value = \"{0}\" />", peroid.getStart()[0]), ix + 4);
                if (peroid.getStart().Size() > 0)
                    al(ref ret, String.Format("<end value = \"{0}\" />", peroid.getEnd()[0]), ix + 4);
                al(ref ret, "</peroid>", ix);
            }
            
        }

        public static void prepareAssigners(ref string ret, StringArray assigners, int ix=0)
        {
            for (int i = 0; i < assigners.Size(); i++)
            {
                al(ref ret, "<assigner>", 8);
                al(ref ret, String.Format("   <reference value=\"{0}\"", assigners[i]));
                al(ref ret, "</assigner>", 8);
            }
        }

        public static void processIdentifier(ref string ret, Identifier indentifier)
        {

            al(ref ret, "<identifier>", 4);
            prepareUses(ref ret, indentifier.getUse(), 8);
            //todo indentifier.getType() 
            prepareSystems(ref ret, indentifier.getSystem(), 8);
            prepareValues(ref ret, indentifier.getValue(), 8);
            preparePeroids(ref ret, indentifier.getPeriod(), 8);
            prepareAssigners(ref ret, indentifier.getAssigner(), 8);
            al(ref ret, "</identifier>", 4);
        }

        public static void processHumanName(ref string ret, HumanName humanName)
        {
            al(ref ret, "<name>", 4);
            prepareUses(ref ret, humanName.getUse(), 8);
            prepareTexts(ref ret, humanName.getText(), 8);
            prepareFamilies(ref ret, humanName.getFamily(), 8);
            prepareGivens(ref ret, humanName.getGiven(), 8);
            preparePrefixes(ref ret, humanName.getPrefix(), 8);
            prepareSufixes(ref ret, humanName.getSuffix(), 8);
            preparePeroids(ref ret, humanName.getPeriod(), 8);
            al(ref ret, "</name>", 4);
        }

        public static void processTelecom(ref string ret, ContactPoint contact)
        {
            al(ref ret, "</telecom>", 4);
            prepareSystems(ref ret, contact.getSystem(), 8);
            prepareUses(ref ret, contact.getUse(), 8);
            prepareValues(ref ret, contact.getValue(), 8);
            preparePeroids(ref ret, contact.getPeriod(), 8);
            al(ref ret, "</telecom>", 4);
        }

        public static void processGender(ref string ret, StringArray gender)
        {
            if(gender.Size()>0)
            {
                al(ref ret, String.Format("<gender value=\"{0}\"/>", gender[0]), 4);
            }
        }

        public static void processBirthDate(ref string ret, StringArray birthDate)
        {
            if (birthDate.Size() > 0)
            {
                al(ref ret, String.Format("<birthDate value=\"{0}\"/>", birthDate[0]), 4);
            }
        }

        public static void processAdress(ref string ret, Address address)
        {
            al(ref ret, "<address>", 4);
            prepareUses(ref ret, address.getUse(), 8);
            //prepareTypes(ref ret, address.GetType(), 8);
            prepareTexts(ref ret, address.getText(), 8);
            prepareLines(ref ret, address.getLine(), 8);
            prepareCities(ref ret, address.getCity(), 8);
            prepareDiscricts(ref ret, address.getDistrict(), 8);
            prepareStates(ref ret, address.getState(), 8);
            preparePostalCode(ref ret, address.getPostalCode(), 8);
            prepareCountries(ref ret, address.getCountry(), 8);
            preparePeroids(ref ret, address.getPeriod(), 8);
            al(ref ret, "</address>", 4);
        }

        public static void processPhoto(ref string ret, Attachment photo)
        {
            //TODO
        }

        public static void processLink(ref string ret, Link link)
        {
            //TODO
        }

        public static void processManagingOrganization(ref string ret, Organization organization)
        {
            //TODO
        }

        public static void processActive(ref string ret, BoolArray actives)
        {
            if (actives.Size() > 0)
            {
                al(ref ret, String.Format("<active value=\"{0}\"/>", actives[0]), 4);
            }
        }

        public static void processPatient(ref string ret, Patient Patient)
        {
            al(ref ret, "<Patient xmlns=\"http://hl7.org/fhir\">");
            for (int i = 0; i < Patient.getIdentifier().Size(); i++)
            {
                processIdentifier(ref ret, Patient.getIdentifier()[i]);
            }
            for (int i = 0; i < Patient.getName().Size(); i++)
            {
                processHumanName(ref ret, Patient.getName()[i]);
            }
            for (int i = 0; i < Patient.getTelecom().Size(); i++)
            {
                processTelecom(ref ret, Patient.getTelecom()[i]);
            }
            processGender(ref ret, Patient.getGender());
            processBirthDate(ref ret, Patient.getBirthData());
            for (int i = 0; i < Patient.getAddress().Size(); i++)
            {
                processAdress(ref ret, Patient.getAddress()[i]);
            }
            for (int i = 0; i < Patient.getManagingOrganization().Size(); i++)
            {
                processManagingOrganization(ref ret, Patient.getManagingOrganization()[i]);
            }
            processActive(ref ret, Patient.getActive());
            for (int i = 0; i < Patient.getPhoto().Size(); i++)
            {
                processPhoto(ref ret, Patient.getPhoto()[i]);
            }
            for (int i = 0; i < Patient.getLink().Size(); i++)
            {
                processLink(ref ret, Patient.getLink()[i]);
            }
            
            al(ref ret, "</Patient>");
        }

        public static void processPatients(ref string ret, Patients Patients)
        {
            for (int i = 0; i < Patients.Size(); i++)
            {
                processPatient(ref ret, Patients[i]);
            }
        }

        public static Patient getEmptyPatient()
        {
            Patient Patient = new Patient(new Identifiers(), new HumanNames(), new ContactPoints(),
                new StringArray(), new StringArray(), new Addresses(), new Attachments(), new Organizations(), new Organizations(),
                new BoolArray(), new Links());
            return Patient;
        }

        public static StringArray box(string str)
        {
            StringArray ret = new StringArray();
            ret.Insert(str);
            return ret;
        }

        public static void addIdentifier(ref Patient Patient, string system, string value)
        {
            Identifiers identifiers = Patient.getIdentifier();
            Identifier indentifier = new Identifier(new StringArray(), new CodeableConcepts(), box(system), box(value), new Periods(), new StringArray());
            Patient.getIdentifier().Insert(indentifier);
        }

        public static void setFirstName(ref Patient Patient, string firstName)
        {
            HumanNames names = Patient.getName();
            if(names.Size()==0)
            {
                HumanName name = new HumanName(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new Periods());
                name.getGiven().Insert(firstName);
                Patient.getName().Insert(name);
            } else
            {
                HumanName name = Patient.getName()[0];
                if(name.getGiven().Size() == 0 )
                {
                    name.getGiven().Insert(firstName);
                } else
                {
                    name.getGiven().Insert(name.getGiven()[0]);
                    name.getGiven()[0] = firstName;
                }
                Patient.getName()[0] = name;
            }
        }

        public static void setSecondName(ref Patient Patient, string secondName)
        {
            HumanNames names = Patient.getName();
            if (names.Size() == 0)
            {
                HumanName name = new HumanName(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new Periods());
                name.getGiven().Insert(secondName);
                Patient.getName().Insert(name);
            }
            else
            {
                HumanName name = Patient.getName()[0];
                if (name.getGiven().Size() <= 1)
                {
                    name.getGiven().Insert(secondName);
                }
                else
                {
                    name.getGiven()[1] = secondName;
                }
                Patient.getName()[0] = name;
            }
        }

        public static void setFamily(ref Patient Patient, string family)
        {
            HumanNames names = Patient.getName();
            if (names.Size() == 0)
            {
                HumanName name = new HumanName(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(), new Periods());
                name.getFamily().Insert(family);
                Patient.getName().Insert(name);
            }
            else
            {
                HumanName name = Patient.getName()[0];
                if (name.getFamily().Size() == 0)
                {
                    name.getFamily().Insert(family);
                }
                else
                {
                    name.getFamily()[0] = family;
                }
                Patient.getName()[0] = name;
            }
        }

        public static void setGender(ref Patient Patient, string gender)
        {
            StringArray genders = Patient.getGender();
            if (genders.Size() == 0)
            {
                Patient.getGender().Insert(gender);
            }
            else
            {
                Patient.getGender()[0] = gender;
            }
        }

        public static void addStreetPart(ref Patient Patient, string part)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), box(part), new StringArray(), new StringArray(),
                    new StringArray(), new StringArray(), new StringArray(), new Periods());
                Patient.getAddress().Insert(address);
            } else
            {
                address = Patient.getAddress()[0];
                address.getLine().Insert(part);
                Patient.getAddress()[0] = address;
            }
            
        }

        public static void setPostalCode(ref Patient Patient, string postalCode)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(),
                    new StringArray(), box(postalCode), new StringArray(), new Periods());
                Patient.getAddress().Insert(address);
            }
            else
            {
                address = Patient.getAddress()[0];
                if(address.getPostalCode().Size() == 0)
                    address.getPostalCode().Insert(postalCode);
                else
                    address.getPostalCode()[0]=postalCode;
                Patient.getAddress()[0] = address;
            }

        }

        public static void setCity(ref Patient Patient, string city)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), new StringArray(), box(city), new StringArray(),
                    new StringArray(), new StringArray(), new StringArray(), new Periods());
                Patient.getAddress().Insert(address);
            }
            else
            {
                address = Patient.getAddress()[0];
                if (address.getCity().Size() == 0)
                    address.getCity().Insert(city);
                else
                    address.getCity()[0] = city;
                Patient.getAddress()[0] = address;
            }

        }

        public static void setCountry(ref Patient Patient, string country)
        {
            Address address;
            if (Patient.getAddress().Size() == 0)
            {
                address = new Address(new StringArray(), new StringArray(), new StringArray(), new StringArray(), new StringArray(),
                    new StringArray(), new StringArray(), box(country), new Periods());
                Patient.getAddress().Insert(address);
            }
            else
            {
                address = Patient.getAddress()[0];
                if (address.getCountry().Size() == 0)
                    address.getCountry().Insert(country);
                else
                    address.getCountry()[0] = country;
                Patient.getAddress()[0] = address;
            }

        }

        public static void setVatCode(ref Patient Patient, string vatCode)
        {
            string vatCodeKeyWord = "VatCode";
            Identifiers identifiers = Patient.getIdentifier();
            for(int i=0;i<identifiers.Size();i++)
            {
                if(identifiers[i].getSystem().Size()>0 && identifiers[i].getSystem()[0] == vatCodeKeyWord)
                {
                    if (identifiers[i].getValue().Size() == 0)
                      identifiers[i].getValue().Insert(vatCode);
                    else
                      identifiers[i].getValue()[0] = vatCode;
                    return;
                }
            }
            Identifier vatCodeObj = new Identifier(new StringArray(), new CodeableConcepts(), box(vatCodeKeyWord), box(vatCode), new Periods(), new StringArray());
            Patient.getIdentifier().Insert(vatCodeObj);
        }

        public static void setPatientalCode(ref Patient Patient, string PatientalCode)
        {
            string PatientalCodeKeyWord = "PatientalCode";
            Identifiers identifiers = Patient.getIdentifier();
            for (int i = 0; i < identifiers.Size(); i++)
            {
                if (identifiers[i].getSystem().Size() > 0 && identifiers[i].getSystem()[0] == PatientalCodeKeyWord)
                {
                    if (identifiers[i].getValue().Size() == 0)
                        identifiers[i].getValue().Insert(PatientalCode);
                    else
                        identifiers[i].getValue()[0] = PatientalCode;
                    return;
                }
            }
            Identifier vatCodeObj = new Identifier(new StringArray(), new CodeableConcepts(), box(PatientalCodeKeyWord), box(PatientalCode), new Periods(), new StringArray());
            Patient.getIdentifier().Insert(vatCodeObj);
        }

        public static void setBirthDate(ref Patient Patient, string birthDate)
        {
            if (Patient.getBirthData().Size() == 0)
            {
                Patient.getBirthData().Insert(birthDate);
            }
            else
            {
                Patient.getBirthData()[0] = birthDate;
            }
        }

        public static void addContact(ref Patient Patient, string system, string value)
        {
            Patient.getTelecom().Insert(new ContactPoint(box(system), box(value), new StringArray(), new Periods()));
        }

    }
}
/*




*/
