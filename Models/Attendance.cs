namespace FIsrtMVCapp.Models
{
    public class Attendance
    {

        
        public static void AddAttendant(Person person)
        {
           PeopleContext db = new PeopleContext();
            db.People.Add(person);
            db.SaveChanges(); 
        }

        public static List<Person> GetAttendants()
        {
            PeopleContext db = new PeopleContext();
            List<Person> people = db.People.ToList();
            return people;
        }


    }
}
