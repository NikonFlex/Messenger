namespace MessangerApi.DTOs
{
   public class MessageDTO
   {
      public string Value { get; set; }
      public int FromId { get; set; }
      public int ToId { get; set; }
      public DateTime SendTime { get; set; }
   }
}
