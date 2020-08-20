namespace TransactionManager.Repository
{
    public class InsertResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public int Inserted { get; set; }
        public int Updated { get; set; }

        public InsertResult(bool success, string error, int inserted = 0, int updated = 0)
        {
            Success = success;
            Error = error;
            Inserted = inserted;
            Updated = updated;
        }
    }
}