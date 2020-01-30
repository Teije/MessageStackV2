namespace MessageStack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateMessageChatId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateMessages", "PrivateChat_Id", "dbo.PrivateChats");
            DropIndex("dbo.PrivateMessages", new[] { "PrivateChat_Id" });
            RenameColumn(table: "dbo.PrivateMessages", name: "PrivateChat_Id", newName: "PrivateChatId");
            AlterColumn("dbo.PrivateMessages", "PrivateChatId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PrivateMessages", "PrivateChatId");
            AddForeignKey("dbo.PrivateMessages", "PrivateChatId", "dbo.PrivateChats", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateMessages", "PrivateChatId", "dbo.PrivateChats");
            DropIndex("dbo.PrivateMessages", new[] { "PrivateChatId" });
            AlterColumn("dbo.PrivateMessages", "PrivateChatId", c => c.Guid());
            RenameColumn(table: "dbo.PrivateMessages", name: "PrivateChatId", newName: "PrivateChat_Id");
            CreateIndex("dbo.PrivateMessages", "PrivateChat_Id");
            AddForeignKey("dbo.PrivateMessages", "PrivateChat_Id", "dbo.PrivateChats", "Id");
        }
    }
}
