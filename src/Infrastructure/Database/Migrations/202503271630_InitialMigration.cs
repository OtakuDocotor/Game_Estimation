using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503271630)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("developers")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(100).NotNullable()
                .WithColumn("description").AsString(1000).NotNullable()
                .WithColumn("logourl").AsString(500);

            Create.Table("users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(100).NotNullable();

            Create.Table("games")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(100).NotNullable()
                .WithColumn("averagescore").AsDouble()
                .WithColumn("developerid").AsInt32().NotNullable().ForeignKey("developers", "id");

            Create.Table("reviews")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(100)
                .WithColumn("content").AsString(2000)
                .WithColumn("score").AsDouble().NotNullable()
                .WithColumn("userid").AsInt32().ForeignKey("users", "id")
                .WithColumn("gameid").AsInt32().ForeignKey("games", "id");

            Insert.IntoTable("developers")
                .Row(new
                {
                    name = "Arcane",
                    description = "Good dev",
                    logourl = "Logo"
                });

            Insert.IntoTable("users")
                .Row(new
                {
                    name = "Ilya"
                });

            Insert.IntoTable("games")
                .Row(new
                {
                    name = "Prey",
                    averagescore = 8,
                    developerid = 1
                });

            Insert.IntoTable("reviews")
                .Row(new
                {
                    name = "First review",
                    content = "Good game",
                    score = 8,
                    userid = 1,
                    gameid = 1
                });
        }

        public override void Down()
        {
            Delete.Table("reviews");
            Delete.Table("games");
            Delete.Table("developers");
            Delete.Table("users");
        }
    }
}
