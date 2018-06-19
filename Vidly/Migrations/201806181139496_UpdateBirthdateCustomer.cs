namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBirthdateCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET Birthdate=CONVERT(datetime, '17/07/1984', 103) WHERE Id=1");
            Sql("UPDATE Customers SET Birthdate=CONVERT(datetime, '01/01/1989', 103) WHERE Id=2");
        }
        
        public override void Down()
        {
        }
    }
}
