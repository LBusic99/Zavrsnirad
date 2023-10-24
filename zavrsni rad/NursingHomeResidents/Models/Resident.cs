namespace NursingHomeResidents.Models
{
    public class Resident
    {
        public int ResidentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoomNumber { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string MedicalCondition { get; set; }
    }
}
