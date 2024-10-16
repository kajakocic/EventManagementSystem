using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace projekat_kaja.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KATEGORIJA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KATEGORIJA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOKACIJE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOKACIJE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ime = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Prezime = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Datum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Vreme = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: false),
                    CenaKarte = table.Column<double>(type: "double precision", nullable: false),
                    URLimg = table.Column<string>(type: "text", nullable: false),
                    KategorijaEventID = table.Column<int>(type: "integer", nullable: true),
                    LocationEventID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EVENT_KATEGORIJA_KategorijaEventID",
                        column: x => x.KategorijaEventID,
                        principalTable: "KATEGORIJA",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EVENT_LOKACIJE_LocationEventID",
                        column: x => x.LocationEventID,
                        principalTable: "LOKACIJE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "REGISTRACIJA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsersEventsID = table.Column<int>(type: "integer", nullable: false),
                    EventsUsersID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRACIJA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REGISTRACIJA_EVENT_EventsUsersID",
                        column: x => x.EventsUsersID,
                        principalTable: "EVENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRACIJA_USER_UsersEventsID",
                        column: x => x.UsersEventsID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REVIEW",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ocena = table.Column<double>(type: "double precision", nullable: false),
                    Komentar = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    UserReviewID = table.Column<int>(type: "integer", nullable: true),
                    EventReviewID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEW", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REVIEW_EVENT_EventReviewID",
                        column: x => x.EventReviewID,
                        principalTable: "EVENT",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_REVIEW_USER_UserReviewID",
                        column: x => x.UserReviewID,
                        principalTable: "USER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EVENT_KategorijaEventID",
                table: "EVENT",
                column: "KategorijaEventID");

            migrationBuilder.CreateIndex(
                name: "IX_EVENT_LocationEventID",
                table: "EVENT",
                column: "LocationEventID");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRACIJA_EventsUsersID",
                table: "REGISTRACIJA",
                column: "EventsUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRACIJA_UsersEventsID",
                table: "REGISTRACIJA",
                column: "UsersEventsID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_EventReviewID",
                table: "REVIEW",
                column: "EventReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_UserReviewID",
                table: "REVIEW",
                column: "UserReviewID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REGISTRACIJA");

            migrationBuilder.DropTable(
                name: "REVIEW");

            migrationBuilder.DropTable(
                name: "EVENT");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "KATEGORIJA");

            migrationBuilder.DropTable(
                name: "LOKACIJE");
        }
    }
}
