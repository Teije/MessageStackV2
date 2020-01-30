namespace MessageStack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.ChatAccount1", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.ChatAccount1", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.ChatAccounts", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.ChatAccounts", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Contacts", "OwnerAccountId", "dbo.Accounts");
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Contacts", new[] { "OwnerAccountId" });
            DropIndex("dbo.ChatAccounts", new[] { "ChatId" });
            DropIndex("dbo.ChatAccounts", new[] { "AccountId" });
            DropIndex("dbo.ChatAccount1", new[] { "Chat_Id" });
            DropIndex("dbo.ChatAccount1", new[] { "Account_Id" });
            RenameColumn(table: "dbo.Contacts", name: "OwnerAccountId", newName: "OwnerAccount_Id");
            RenameColumn(table: "dbo.Contacts", name: "ContactAccountId", newName: "TargetAccount_Id");
            RenameIndex(table: "dbo.Contacts", name: "IX_ContactAccountId", newName: "IX_TargetAccount_Id");
            CreateTable(
                "dbo.GroupChats",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Subject = c.String(),
                        ImageUrl = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupMessages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        SendDate = c.DateTime(nullable: false),
                        GChat_Id = c.Guid(),
                        Sender_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupChats", t => t.GChat_Id)
                .ForeignKey("dbo.Accounts", t => t.Sender_Id)
                .Index(t => t.GChat_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.PrivateChats",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        FirstUser_Id = c.Guid(),
                        SecondUser_Id = c.Guid(),
                        Account_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.FirstUser_Id)
                .ForeignKey("dbo.Accounts", t => t.SecondUser_Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.FirstUser_Id)
                .Index(t => t.SecondUser_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        SendDate = c.DateTime(nullable: false),
                        PrivateChat_Id = c.Guid(),
                        Sender_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrivateChats", t => t.PrivateChat_Id)
                .ForeignKey("dbo.Accounts", t => t.Sender_Id)
                .Index(t => t.PrivateChat_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.GroupChatAccounts",
                c => new
                    {
                        GroupChat_Id = c.Guid(nullable: false),
                        Account_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupChat_Id, t.Account_Id })
                .ForeignKey("dbo.GroupChats", t => t.GroupChat_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.GroupChat_Id)
                .Index(t => t.Account_Id);
            
            AddColumn("dbo.Accounts", "Firstname", c => c.String());
            AddColumn("dbo.Accounts", "Lastname", c => c.String());
            AddColumn("dbo.Accounts", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Contacts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "Phonenumber", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "OwnerAccount_Id", c => c.Guid());
            CreateIndex("dbo.Contacts", "OwnerAccount_Id");
            AddForeignKey("dbo.Contacts", "OwnerAccount_Id", "dbo.Accounts", "Id");
            DropColumn("dbo.Accounts", "Name");
            DropTable("dbo.AccountRoles");
            DropTable("dbo.Chats");
            DropTable("dbo.Messages");
            DropTable("dbo.ChatAccounts");
            DropTable("dbo.ChatAccount1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ChatAccount1",
                c => new
                    {
                        Chat_Id = c.Guid(nullable: false),
                        Account_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Chat_Id, t.Account_Id });
            
            CreateTable(
                "dbo.ChatAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChatId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChatId = c.Guid(nullable: false),
                        SenderId = c.Guid(nullable: false),
                        SenderName = c.String(),
                        Text = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Accounts", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.Contacts", "OwnerAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.PrivateChats", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.PrivateChats", "SecondUser_Id", "dbo.Accounts");
            DropForeignKey("dbo.PrivateMessages", "Sender_Id", "dbo.Accounts");
            DropForeignKey("dbo.PrivateMessages", "PrivateChat_Id", "dbo.PrivateChats");
            DropForeignKey("dbo.PrivateChats", "FirstUser_Id", "dbo.Accounts");
            DropForeignKey("dbo.GroupChatAccounts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.GroupChatAccounts", "GroupChat_Id", "dbo.GroupChats");
            DropForeignKey("dbo.GroupMessages", "Sender_Id", "dbo.Accounts");
            DropForeignKey("dbo.GroupMessages", "GChat_Id", "dbo.GroupChats");
            DropIndex("dbo.GroupChatAccounts", new[] { "Account_Id" });
            DropIndex("dbo.GroupChatAccounts", new[] { "GroupChat_Id" });
            DropIndex("dbo.Contacts", new[] { "OwnerAccount_Id" });
            DropIndex("dbo.PrivateMessages", new[] { "Sender_Id" });
            DropIndex("dbo.PrivateMessages", new[] { "PrivateChat_Id" });
            DropIndex("dbo.PrivateChats", new[] { "Account_Id" });
            DropIndex("dbo.PrivateChats", new[] { "SecondUser_Id" });
            DropIndex("dbo.PrivateChats", new[] { "FirstUser_Id" });
            DropIndex("dbo.GroupMessages", new[] { "Sender_Id" });
            DropIndex("dbo.GroupMessages", new[] { "GChat_Id" });
            AlterColumn("dbo.Contacts", "OwnerAccount_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Contacts", "Phonenumber", c => c.String());
            AlterColumn("dbo.Contacts", "Name", c => c.String());
            DropColumn("dbo.Contacts", "Email");
            DropColumn("dbo.Accounts", "Email");
            DropColumn("dbo.Accounts", "Lastname");
            DropColumn("dbo.Accounts", "Firstname");
            DropTable("dbo.GroupChatAccounts");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.PrivateChats");
            DropTable("dbo.GroupMessages");
            DropTable("dbo.GroupChats");
            RenameIndex(table: "dbo.Contacts", name: "IX_TargetAccount_Id", newName: "IX_ContactAccountId");
            RenameColumn(table: "dbo.Contacts", name: "TargetAccount_Id", newName: "ContactAccountId");
            RenameColumn(table: "dbo.Contacts", name: "OwnerAccount_Id", newName: "OwnerAccountId");
            CreateIndex("dbo.ChatAccount1", "Account_Id");
            CreateIndex("dbo.ChatAccount1", "Chat_Id");
            CreateIndex("dbo.ChatAccounts", "AccountId");
            CreateIndex("dbo.ChatAccounts", "ChatId");
            CreateIndex("dbo.Contacts", "OwnerAccountId");
            CreateIndex("dbo.Messages", "ChatId");
            AddForeignKey("dbo.Contacts", "OwnerAccountId", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatAccounts", "ChatId", "dbo.Chats", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatAccounts", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatAccount1", "Account_Id", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatAccount1", "Chat_Id", "dbo.Chats", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "ChatId", "dbo.Chats", "Id", cascadeDelete: true);
        }
    }
}
