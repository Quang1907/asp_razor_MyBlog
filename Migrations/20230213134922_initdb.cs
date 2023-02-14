using System;
using ASP_RAZOR_5.Models;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPRAZOR5.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                });

            // fake data: Bogus
            Randomizer.Seed = new Random(23234);

            var fakerArticle = new Faker<Article>();
            fakerArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5, 5));
            fakerArticle.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2021, 3, 20), new DateTime(2023, 2, 20)));
            fakerArticle.RuleFor(a => a.Content, f => f.Lorem.Sentence(1, 4));

            for (var i = 0; i < 100; i++)
            {
                Article article = fakerArticle.Generate();
                migrationBuilder.InsertData(
                    table: "articles",
                    columns: new[] { "Title", "Created", "Content" },
                    values: new object[]
                    {
                        article.Title,
                        article.Created,
                        article.Content,
                    }
                );
            }

            // insert data
            // migrationBuilder.InsertData(
            //    table: "articles",
            //    columns: new[] { "Title", "Created", "Content" },
            //    values: new object[]
            //    {
            //         "Bai viet 2",
            //         new DateTime( 2023, 7, 20),
            //         "noi dung 2"
            //    }
            //);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
