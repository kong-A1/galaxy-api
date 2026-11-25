using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace galaxy_api.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class AddMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "master_menus",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name_th = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name_en = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    route = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_master_menus", x => x.id);
                    table.CheckConstraint("ck_master_menus_parent_not_self", "parent_id <> id");
                    table.ForeignKey(
                        name: "fk_master_menus_master_menus_parent_id",
                        column: x => x.parent_id,
                        principalTable: "master_menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_master_menus_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_master_menus_users_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_master_menus_users_updated_by",
                        column: x => x.updated_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "menu_permissions",
                columns: table => new
                {
                    menu_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_permissions", x => new { x.menu_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_menu_permissions_master_menus_menu_id",
                        column: x => x.menu_id,
                        principalTable: "master_menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_menu_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_master_menus_code",
                table: "master_menus",
                column: "code",
                unique: true,
                filter: "deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_master_menus_created_by",
                table: "master_menus",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_master_menus_deleted_by",
                table: "master_menus",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "ix_master_menus_parent_id",
                table: "master_menus",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_master_menus_updated_by",
                table: "master_menus",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "ix_menu_permissions_permission_id",
                table: "menu_permissions",
                column: "permission_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_permissions");

            migrationBuilder.DropTable(
                name: "master_menus");
        }
    }
}
