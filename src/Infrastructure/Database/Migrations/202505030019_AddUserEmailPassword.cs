using Domain.Enums;
using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202505030019)]
    public class AddUserEmailPassword : Migration
    {
        public override void Up()
        {
            Execute.Sql("CREATE TYPE user_role AS ENUM ('User', 'Admin')");

            Alter.Table("users")
                .AddColumn("password_hash").AsString(255).Nullable()
                .AddColumn("email").AsString(255).Nullable().Unique()
                .AddColumn("role").AsCustom("user_role").WithDefaultValue("User");
                Insert.IntoTable("users")
                .Row(new
                {
                    name = "doctor",
                    password_hash = "$2a$11$pOz8rIPthqJE2g4HQ6s7L.DmXVbdg58/N7w7/W68d.1oF8uu1Sy3e",
                    email = "dc@dc",
                    role = UserRoles.Admin.ToString()
                });
        }

        public override void Down()
        {
            Delete.Column("password_hash")
                .Column("email")
                .Column("role")
                .FromTable("users");

            Execute.Sql("DROP TYPE user_role");
        }
    }
}
