using LiteDB;
using MED.ATTACH.Objects;
using MED.CONTROL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.CONTROL.Objects
{
    public class patientRepos 
    {
        readonly string connectionString = "";

        public patientRepos(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreatePatient(Patient patient)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var patients = db.GetCollection<Patient>("Patient");
                    patients.Insert(patient);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public void ShowPatients()
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var patients = db.GetCollection<Patient>("Patient");
                    List<Patient> patientslist = patients.FindAll().ToList();
                    foreach (var patient in patientslist)
                    {
                        Console.WriteLine($"Полное имя - {patient.FullName} ИИН - {patient.IIN}\t");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Patient FindPatients(string fullname,string IIN)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var patients = db.GetCollection<Patient>("Patient");
                    return patients.FindOne(u => u.FullName == fullname && u.IIN == IIN);
                }
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }
    }
}
