namespace ASassignment2.Model
{
	public class AuditLog
	{
		public int ID { get; set; }
		public string UserID { get; set; }
		public string Activity {  get; set; }
		public DateTime Time { get; set; }
	}
}
