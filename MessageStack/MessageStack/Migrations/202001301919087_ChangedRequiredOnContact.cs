namespace MessageStack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRequiredOnContact : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Phonenumber", c => c.String());
            AlterColumn("dbo.Contacts", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "Phonenumber", c => c.String(nullable: false));
        }
    }
}
