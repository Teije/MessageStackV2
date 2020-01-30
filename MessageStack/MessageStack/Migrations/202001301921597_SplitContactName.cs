namespace MessageStack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SplitContactName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Firstname", c => c.String(nullable: false));
            AddColumn("dbo.Contacts", "Lastname", c => c.String(nullable: false));
            DropColumn("dbo.Contacts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Contacts", "Lastname");
            DropColumn("dbo.Contacts", "Firstname");
        }
    }
}
