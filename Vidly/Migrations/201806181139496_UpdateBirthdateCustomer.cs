namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBirthdateCustomer : DbMigration
    {
        public override void Up()
        {
            Sql(string.Format("UPDATE Customers SET Birthdate={0} WHERE Id=1", new DateTime(1984, 7, 17)));
            Sql(string.Format("UPDATE Customers SET Birthdate={0} WHERE Id=2", new DateTime(1989, 1, 1)));
        }
        
        public override void Down()
        {
        }
    }
}
