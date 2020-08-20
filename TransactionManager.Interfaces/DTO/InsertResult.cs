namespace TransactionManager.Repository
{
    public class InsertResult
    {
        public int Inserted { get; set; }
        public int Updated { get; set; }

        public InsertResult(int inserted = 0, int updated = 0)
        {
            Inserted = inserted;
            Updated = updated;
        }
    }
}