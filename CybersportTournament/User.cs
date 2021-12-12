namespace CybersportTournament
{
    public class User
    {
        public User(int IDPerson, int Role)
        {
            this.IDPerson = IDPerson;
            this.Role = Role;

        }
        public int IDPerson { get; set; }
        public int Role { get; set; }  
    }
}
