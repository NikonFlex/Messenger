namespace MessangerApi.Models
{
   public class Message
   {
      public int Id { get; set; }
      public string Value { get; set; }   
      public int FromId { get; set; }
      public int ToId { get; set; } 
      public DateTime SendTime { get; set; }
   }
}
