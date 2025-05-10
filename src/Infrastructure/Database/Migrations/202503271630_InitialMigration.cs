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
                .WithColumn("logo_url").AsString(500);

            Create.Table("users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(100).NotNullable();

            Create.Table("games")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(100).NotNullable()
                .WithColumn("average_score").AsDouble()
                .WithColumn("developer_id").AsInt32().NotNullable().ForeignKey("developers", "id");

            Create.Table("reviews")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(100)
                .WithColumn("content").AsString(2000)
                .WithColumn("score").AsDouble().NotNullable()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id")
                .WithColumn("game_id").AsInt32().ForeignKey("games", "id");
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
