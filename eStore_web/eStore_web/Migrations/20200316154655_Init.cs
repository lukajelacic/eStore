using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore_web.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    DrzavaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.DrzavaID);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GameGenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivZanra = table.Column<string>(maxLength: 50, nullable: false),
                    OznakaZanra = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => x.GameGenreID);
                });

            migrationBuilder.CreateTable(
                name: "IgricaImage",
                columns: table => new
                {
                    IgricaImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IgraID = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgricaImage", x => x.IgricaImageID);
                });

            migrationBuilder.CreateTable(
                name: "RatingCategorie",
                columns: table => new
                {
                    RatingCategorieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivKategorije = table.Column<string>(maxLength: 50, nullable: false),
                    OznakaKategorije = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCategorie", x => x.RatingCategorieID);
                });

            migrationBuilder.CreateTable(
                name: "Regija",
                columns: table => new
                {
                    RegijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    DrzavaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regija", x => x.RegijaID);
                    table.ForeignKey(
                        name: "FK_Regija_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "DrzavaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    GradID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    RegijaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.GradID);
                    table.ForeignKey(
                        name: "FK_Grad_Regija_RegijaID",
                        column: x => x.RegijaID,
                        principalTable: "Regija",
                        principalColumn: "RegijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Igra",
                columns: table => new
                {
                    IgraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Opis = table.Column<string>(maxLength: 1048, nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "Date", nullable: false),
                    TrailerUrl = table.Column<string>(maxLength: 1048, nullable: false),
                    PremiumStatus = table.Column<bool>(nullable: false),
                    Odobrena = table.Column<bool>(nullable: false),
                    GameGenreID = table.Column<int>(nullable: true),
                    RatingCategorieID = table.Column<int>(nullable: true),
                    DeveloperID = table.Column<int>(nullable: true),
                    AdministratorID = table.Column<int>(nullable: true),
                    IgricaImageID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igra", x => x.IgraID);
                    table.ForeignKey(
                        name: "FK_Igra_GameGenre_GameGenreID",
                        column: x => x.GameGenreID,
                        principalTable: "GameGenre",
                        principalColumn: "GameGenreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igra_IgricaImage_IgricaImageID",
                        column: x => x.IgricaImageID,
                        principalTable: "IgricaImage",
                        principalColumn: "IgricaImageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igra_RatingCategorie_RatingCategorieID",
                        column: x => x.RatingCategorieID,
                        principalTable: "RatingCategorie",
                        principalColumn: "RatingCategorieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Popust",
                columns: table => new
                {
                    PopustID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IgraID = table.Column<int>(nullable: false),
                    PocetakPopusta = table.Column<DateTime>(type: "DateTime", nullable: false),
                    KrajPopusta = table.Column<DateTime>(type: "DateTime", nullable: false),
                    PopustProcent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Popust", x => x.PopustID);
                    table.ForeignKey(
                        name: "FK_Popust_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrijavaRecenzije",
                columns: table => new
                {
                    PrijavaRecenzijeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Razlog = table.Column<string>(maxLength: 50, nullable: false),
                    RecenzijaID = table.Column<int>(nullable: false),
                    KupacID = table.Column<int>(nullable: true),
                    AdministratorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaRecenzije", x => x.PrijavaRecenzijeID);
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                columns: table => new
                {
                    RefundID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrijemeKupovine = table.Column<DateTime>(type: "DateTime", nullable: false),
                    VrijemeZahtijeva = table.Column<DateTime>(type: "DateTime", nullable: false),
                    VrijemeOdgovora = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RazlogRefunda = table.Column<string>(maxLength: 50, nullable: true),
                    IgraID = table.Column<int>(nullable: true),
                    KupacID = table.Column<int>(nullable: true),
                    AdministratorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.RefundID);
                    table.ForeignKey(
                        name: "FK_Refund_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Novost",
                columns: table => new
                {
                    NovostID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naslov = table.Column<string>(maxLength: 50, nullable: false),
                    Sadrzaj = table.Column<string>(maxLength: 2048, nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DeveloperID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novost", x => x.NovostID);
                    table.ForeignKey(
                        name: "FK_Novost_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    OsobaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(maxLength: 50, nullable: true),
                    DatumRodenja = table.Column<DateTime>(type: "Date", nullable: false),
                    LoginInfoID = table.Column<int>(nullable: true),
                    EmailAddressID = table.Column<int>(nullable: true),
                    OsobaImageID = table.Column<int>(nullable: true),
                    GradID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_Osoba_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AdministratorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OsobaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdministratorID);
                    table.ForeignKey(
                        name: "FK_Administrator_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    DeveloperID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivKompanije = table.Column<string>(maxLength: 50, nullable: true),
                    OsobaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.DeveloperID);
                    table.ForeignKey(
                        name: "FK_Developer_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddress",
                columns: table => new
                {
                    EmailAddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    OsobaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddress", x => x.EmailAddressID);
                    table.ForeignKey(
                        name: "FK_EmailAddress_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kupac",
                columns: table => new
                {
                    KupacID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 25, nullable: false),
                    PretplacenNaPremium = table.Column<bool>(nullable: false),
                    Reputacija = table.Column<int>(nullable: false),
                    BanStatus = table.Column<bool>(nullable: false),
                    OsobaID = table.Column<int>(nullable: false),
                    WalletID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupac", x => x.KupacID);
                    table.ForeignKey(
                        name: "FK_Kupac_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginInfo",
                columns: table => new
                {
                    LoginInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    TipKorisnika = table.Column<int>(nullable: false),
                    OsobaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginInfo", x => x.LoginInfoID);
                    table.ForeignKey(
                        name: "FK_LoginInfo_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OsobaImage",
                columns: table => new
                {
                    OsobaImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OsobaID = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsobaImage", x => x.OsobaImageID);
                    table.ForeignKey(
                        name: "FK_OsobaImage_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KupacKupuje",
                columns: table => new
                {
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    PopustID = table.Column<int>(nullable: true),
                    VrijemeKupovine = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Cijena = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KupacKupuje", x => new { x.KupacID, x.IgraID });
                    table.ForeignKey(
                        name: "FK_KupacKupuje_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KupacKupuje_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KupacKupuje_Popust_PopustID",
                        column: x => x.PopustID,
                        principalTable: "Popust",
                        principalColumn: "PopustID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prati",
                columns: table => new
                {
                    PratiID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prati", x => x.PratiID);
                    table.ForeignKey(
                        name: "FK_Prati_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prati_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreuzimanjeIgre",
                columns: table => new
                {
                    PreuzimanjeIgreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrijemePreuzimanja = table.Column<DateTime>(type: "DateTime", nullable: false),
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreuzimanjeIgre", x => x.PreuzimanjeIgreID);
                    table.ForeignKey(
                        name: "FK_PreuzimanjeIgre_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreuzimanjeIgre_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrijavaPremium",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KupacId = table.Column<int>(nullable: false),
                    DatumPrijave = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaPremium", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrijavaPremium_Kupac_KupacId",
                        column: x => x.KupacId,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    RecenzijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    RecenzijaText = table.Column<string>(maxLength: 1024, nullable: false),
                    Ocjena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.RecenzijaID);
                    table.ForeignKey(
                        name: "FK_Recenzija_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzija_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RazlogReporta = table.Column<string>(nullable: true),
                    ReportovaniKupacId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Kupac_ReportovaniKupacId",
                        column: x => x.ReportovaniKupacId,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    WalletID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    balance = table.Column<double>(nullable: false),
                    KupacID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.WalletID);
                    table.ForeignKey(
                        name: "FK_Wallet_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    KupacID = table.Column<int>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    DatumDodavanja = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => new { x.KupacID, x.IgraID });
                    table.ForeignKey(
                        name: "FK_WishList_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishList_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupac",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistory",
                columns: table => new
                {
                    WalletHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentBalance = table.Column<double>(nullable: false),
                    TransactionAmount = table.Column<double>(nullable: false),
                    IsIgra = table.Column<bool>(nullable: false),
                    IgraID = table.Column<int>(nullable: false),
                    WalletID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistory", x => x.WalletHistoryID);
                    table.ForeignKey(
                        name: "FK_WalletHistory_Igra_IgraID",
                        column: x => x.IgraID,
                        principalTable: "Igra",
                        principalColumn: "IgraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletHistory_Wallet_WalletID",
                        column: x => x.WalletID,
                        principalTable: "Wallet",
                        principalColumn: "WalletID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_OsobaID",
                table: "Administrator",
                column: "OsobaID");

            migrationBuilder.CreateIndex(
                name: "IX_Developer_OsobaID",
                table: "Developer",
                column: "OsobaID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddress_OsobaID",
                table: "EmailAddress",
                column: "OsobaID",
                unique: true,
                filter: "[OsobaID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Grad_RegijaID",
                table: "Grad",
                column: "RegijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_AdministratorID",
                table: "Igra",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_DeveloperID",
                table: "Igra",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_GameGenreID",
                table: "Igra",
                column: "GameGenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_IgricaImageID",
                table: "Igra",
                column: "IgricaImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_RatingCategorieID",
                table: "Igra",
                column: "RatingCategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_IgricaImage_IgraID",
                table: "IgricaImage",
                column: "IgraID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kupac_OsobaID",
                table: "Kupac",
                column: "OsobaID");

            migrationBuilder.CreateIndex(
                name: "IX_KupacKupuje_IgraID",
                table: "KupacKupuje",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_KupacKupuje_PopustID",
                table: "KupacKupuje",
                column: "PopustID");

            migrationBuilder.CreateIndex(
                name: "IX_LoginInfo_OsobaID",
                table: "LoginInfo",
                column: "OsobaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Novost_DeveloperID",
                table: "Novost",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Novost_IgraID",
                table: "Novost",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Osoba_GradID",
                table: "Osoba",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Osoba_OsobaImageID",
                table: "Osoba",
                column: "OsobaImageID");

            migrationBuilder.CreateIndex(
                name: "IX_OsobaImage_OsobaID",
                table: "OsobaImage",
                column: "OsobaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Popust_IgraID",
                table: "Popust",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Prati_IgraID",
                table: "Prati",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Prati_KupacID",
                table: "Prati",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_PreuzimanjeIgre_IgraID",
                table: "PreuzimanjeIgre",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_PreuzimanjeIgre_KupacID",
                table: "PreuzimanjeIgre",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaPremium_KupacId",
                table: "PrijavaPremium",
                column: "KupacId");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaRecenzije_AdministratorID",
                table: "PrijavaRecenzije",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaRecenzije_KupacID",
                table: "PrijavaRecenzije",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaRecenzije_RecenzijaID",
                table: "PrijavaRecenzije",
                column: "RecenzijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_IgraID",
                table: "Recenzija",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KupacID",
                table: "Recenzija",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_AdministratorID",
                table: "Refund",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_IgraID",
                table: "Refund",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_KupacID",
                table: "Refund",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_Regija_DrzavaID",
                table: "Regija",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReportovaniKupacId",
                table: "Report",
                column: "ReportovaniKupacId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_KupacID",
                table: "Wallet",
                column: "KupacID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistory_IgraID",
                table: "WalletHistory",
                column: "IgraID");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistory_WalletID",
                table: "WalletHistory",
                column: "WalletID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_IgraID",
                table: "WishList",
                column: "IgraID");

            migrationBuilder.AddForeignKey(
                name: "FK_Igra_Administrator_AdministratorID",
                table: "Igra",
                column: "AdministratorID",
                principalTable: "Administrator",
                principalColumn: "AdministratorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Igra_Developer_DeveloperID",
                table: "Igra",
                column: "DeveloperID",
                principalTable: "Developer",
                principalColumn: "DeveloperID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrijavaRecenzije_Administrator_AdministratorID",
                table: "PrijavaRecenzije",
                column: "AdministratorID",
                principalTable: "Administrator",
                principalColumn: "AdministratorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrijavaRecenzije_Kupac_KupacID",
                table: "PrijavaRecenzije",
                column: "KupacID",
                principalTable: "Kupac",
                principalColumn: "KupacID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrijavaRecenzije_Recenzija_RecenzijaID",
                table: "PrijavaRecenzije",
                column: "RecenzijaID",
                principalTable: "Recenzija",
                principalColumn: "RecenzijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Administrator_AdministratorID",
                table: "Refund",
                column: "AdministratorID",
                principalTable: "Administrator",
                principalColumn: "AdministratorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Kupac_KupacID",
                table: "Refund",
                column: "KupacID",
                principalTable: "Kupac",
                principalColumn: "KupacID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Novost_Developer_DeveloperID",
                table: "Novost",
                column: "DeveloperID",
                principalTable: "Developer",
                principalColumn: "DeveloperID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Osoba_OsobaImage_OsobaImageID",
                table: "Osoba",
                column: "OsobaImageID",
                principalTable: "OsobaImage",
                principalColumn: "OsobaImageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OsobaImage_Osoba_OsobaID",
                table: "OsobaImage");

            migrationBuilder.DropTable(
                name: "EmailAddress");

            migrationBuilder.DropTable(
                name: "KupacKupuje");

            migrationBuilder.DropTable(
                name: "LoginInfo");

            migrationBuilder.DropTable(
                name: "Novost");

            migrationBuilder.DropTable(
                name: "Prati");

            migrationBuilder.DropTable(
                name: "PreuzimanjeIgre");

            migrationBuilder.DropTable(
                name: "PrijavaPremium");

            migrationBuilder.DropTable(
                name: "PrijavaRecenzije");

            migrationBuilder.DropTable(
                name: "Refund");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "WalletHistory");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "Popust");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Igra");

            migrationBuilder.DropTable(
                name: "Kupac");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "IgricaImage");

            migrationBuilder.DropTable(
                name: "RatingCategorie");

            migrationBuilder.DropTable(
                name: "Osoba");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "OsobaImage");

            migrationBuilder.DropTable(
                name: "Regija");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
