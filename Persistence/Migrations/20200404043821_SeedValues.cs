namespace Persistence.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SeedValues : Migration
    {
        #region Methods

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Values", new[] { "Id", "Name" }, new object[] { 1, "Value 101" });

            migrationBuilder.InsertData("Values", new[] { "Id", "Name" }, new object[] { 2, "Value 102" });

            migrationBuilder.InsertData("Values", new[] { "Id", "Name" }, new object[] { 3, "Value 103" });

            migrationBuilder.InsertData("Values", new[] { "Id", "Name" }, new object[] { 4, "Value 104" });

            migrationBuilder.InsertData("Values", new[] { "Id", "Name" }, new object[] { 5, "Value 105" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Values", "Id", 1);

            migrationBuilder.DeleteData("Values", "Id", 2);

            migrationBuilder.DeleteData("Values", "Id", 3);

            migrationBuilder.DeleteData("Values", "Id", 4);

            migrationBuilder.DeleteData("Values", "Id", 5);
        }

        #endregion
    }
}
