using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            CREATE OR REPLACE FUNCTION set_updated_at()
            RETURNS TRIGGER AS $$
            BEGIN
                NEW."UpdatedAt" = NOW();
                RETURN NEW;
            END;
            $$ LANGUAGE plpgsql;
        """);

            migrationBuilder.Sql("""
            CREATE TRIGGER trg_set_updated_at
            BEFORE UPDATE ON "Products"
            FOR EACH ROW
            EXECUTE FUNCTION set_updated_at();
        """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            DROP TRIGGER IF EXISTS trg_set_updated_at ON "Products";
        """);

            migrationBuilder.Sql("""
            DROP FUNCTION IF EXISTS set_updated_at();
        """);
        }
    }
}
