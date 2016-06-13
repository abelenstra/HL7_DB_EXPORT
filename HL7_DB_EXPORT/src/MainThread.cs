using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7_DB_EXPORT
{
    class MainThread
    {
        public void processEstomed(string host, int port)
        {
            DBUtil.MysqlDBConnection db = DBUtil.MysqlDBConnection.Instance();
            db.connect(host, port, "e2demo", "", "");
            DBUtil.DBResult result = db.query("Select  FirstName, SecondName, LastName, BirthDate, Email, CardNo, ExternalCardNo, PeselNo, Sex, AddressPart1, AddressPart2, AddressPart3, City, ZipCode, AgreesForEmailVisitNotifications, Guardian, PatientGuardianId, NormalizedPhoneNumber, TerritorialUnitId, IdentityDocumentType, IdentityDocumentNumber from patient;");
            Patients patients = DBProcessor.processSecret1(result);
            string stream = "";
            HL7Util.processPatients(ref stream, patients);
            File.WriteAllText("Patients.hl7", stream);
        }

        public void processProdentis(string host, int port)
        {
            DBUtil.MSSqlDBConnection db = DBUtil.MSSqlDBConnection.Instance();
            db.connect(host, port, "Prodentis500", "", "");
            DBUtil.DBResult result = db.query("Select nr_kartywew, imie, imie2, nazwisko, plec, ulica, num_domu, num_mieszkania, kod_pocztowy, miasto, g.nazwa, kod_miasta, w.wojewodztwo, c.nazwa, nip, pesel, data_urodzenia, miejsce_ur, email, telefonypraca, telefonydom, komorka, wys_sms, wys_email from Prodentis500.dbo.pacjenci p, Prodentis500.dbo.s_kraje c, Prodentis500.dbo.s_wojewodztwa w, Prodentis500.dbo.s_gminy g where c.id_kraju = p.id_kraju and p.kod_wojew = w.kod and p.kod_gminy = g.kod;");
            Console.WriteLine("Select done!");
            Patients patients = DBProcessor.processSecret2(result);
            string stream = "";
            HL7Util.processPatients(ref stream, patients);
            File.WriteAllText("Patients.hl7", stream);
        }
    }
}
