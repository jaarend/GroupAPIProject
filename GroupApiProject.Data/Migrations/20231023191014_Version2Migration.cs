using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupApiProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class Version2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Classes_ClassId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_OwnerId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gear_ArmorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gear_WeaponId",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "AttackTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    HitValue = table.Column<int>(type: "int", nullable: false),
                    APCost = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_AttackTypes_Type",
                        column: x => x.Type,
                        principalTable: "AttackTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRole",
                table: "Users",
                column: "UserRole");

            migrationBuilder.CreateIndex(
                name: "IX_Gear_Type",
                table: "Gear",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AttackSlot_1",
                table: "Classes",
                column: "AttackSlot_1");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AttackSlot_2",
                table: "Classes",
                column: "AttackSlot_2");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Type",
                table: "Characters",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_Type",
                table: "Attacks",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_CharacterTypes_Type",
                table: "Characters",
                column: "Type",
                principalTable: "CharacterTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Classes_ClassId",
                table: "Characters",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_OwnerId",
                table: "Characters",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Attacks_AttackSlot_1",
                table: "Classes",
                column: "AttackSlot_1",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Attacks_AttackSlot_2",
                table: "Classes",
                column: "AttackSlot_2",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gear_ArmorId",
                table: "Classes",
                column: "ArmorId",
                principalTable: "Gear",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gear_WeaponId",
                table: "Classes",
                column: "WeaponId",
                principalTable: "Gear",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Gear_GearTypes_Type",
                table: "Gear",
                column: "Type",
                principalTable: "GearTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserRole",
                table: "Users",
                column: "UserRole",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_CharacterTypes_Type",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Classes_ClassId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_OwnerId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Attacks_AttackSlot_1",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Attacks_AttackSlot_2",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gear_ArmorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gear_WeaponId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Gear_GearTypes_Type",
                table: "Gear");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserRole",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "CharacterTypes");

            migrationBuilder.DropTable(
                name: "GearTypes");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "AttackTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Gear_Type",
                table: "Gear");

            migrationBuilder.DropIndex(
                name: "IX_Classes_AttackSlot_1",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_AttackSlot_2",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Characters_Type",
                table: "Characters");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Classes_ClassId",
                table: "Characters",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_OwnerId",
                table: "Characters",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gear_ArmorId",
                table: "Classes",
                column: "ArmorId",
                principalTable: "Gear",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gear_WeaponId",
                table: "Classes",
                column: "WeaponId",
                principalTable: "Gear",
                principalColumn: "Id");
        }
    }
}
