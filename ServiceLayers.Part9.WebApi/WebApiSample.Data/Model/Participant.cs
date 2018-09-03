namespace WebApiSample.Data.Model
{
    public class Participant
    {
        public int Id { get; set; }

        public Tournament Tournament { get; set; }
        
        public Player Player { get; set; }
        public int? Postition { get; set; }
    }
}
