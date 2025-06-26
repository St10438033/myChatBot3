namespace myChatBot3
{
    public class QuizQuestion
    {

        //getters & setters
        public string Question { get; set; }
        public string CorrectChoice { get; set; }

        //get and set on the List 
        public List<string> Choices { get; set; }
    }
}