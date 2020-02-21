using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiki.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AudioFileTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Disc = table.Column<uint>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AlbumArtists = table.Column<string>(nullable: true),
                    Genres = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    Grouping = table.Column<string>(nullable: true),
                    Track = table.Column<int>(nullable: true),
                    DiscCount = table.Column<uint>(nullable: true),
                    Performers = table.Column<string>(nullable: true),
                    AlbumName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioFileTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaDirectories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DirectoryPath = table.Column<string>(nullable: true),
                    LastScan = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaDirectories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AudioBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AudioBookDirectoryPath = table.Column<string>(nullable: true),
                    IsIdentified = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    SeriesEntry = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    ThumbnailData = table.Column<string>(nullable: true),
                    ISBN10 = table.Column<string>(nullable: true),
                    ISBN13 = table.Column<string>(nullable: true),
                    SeriesId = table.Column<Guid>(nullable: true),
                    PublisherId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GoogleBooksID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioBooks_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioBooks_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeriesAuthors",
                columns: table => new
                {
                    SeriesId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesAuthors", x => new { x.AuthorId, x.SeriesId });
                    table.ForeignKey(
                        name: "FK_SeriesAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesAuthors_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudioFiles",
                columns: table => new
                {
                    AudioBookId = table.Column<Guid>(nullable: false),
                    TrackNumber = table.Column<int>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    AudioTagsId = table.Column<Guid>(nullable: true),
                    Path = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    FileExtension = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MimeType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioFiles", x => new { x.AudioBookId, x.TrackNumber });
                    table.ForeignKey(
                        name: "FK_AudioFiles_AudioBooks_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioFiles_AudioFileTags_AudioTagsId",
                        column: x => x.AudioTagsId,
                        principalTable: "AudioFileTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_AudioBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "AudioBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    AudioBookId = table.Column<Guid>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    CurrentTrack = table.Column<int>(nullable: false),
                    SeriesProgressId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookProgresses_AudioBooks_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookProgresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    AudioFileId = table.Column<Guid>(nullable: false),
                    FileAudioBookId = table.Column<Guid>(nullable: false),
                    FileTrackNumber = table.Column<int>(nullable: false),
                    BookProgressId = table.Column<Guid>(nullable: false),
                    AudioBookProgressId = table.Column<Guid>(nullable: true),
                    IsFinished = table.Column<bool>(nullable: false),
                    Progress = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileProgresses_BookProgresses_AudioBookProgressId",
                        column: x => x.AudioBookProgressId,
                        principalTable: "BookProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileProgresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileProgresses_AudioFiles_FileAudioBookId_FileTrackNumber",
                        columns: x => new { x.FileAudioBookId, x.FileTrackNumber },
                        principalTable: "AudioFiles",
                        principalColumns: new[] { "AudioBookId", "TrackNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    SeriesId = table.Column<Guid>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    CurrentBookProgressId = table.Column<Guid>(nullable: false),
                    CurrentAudioBookProgressId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesProgresses_BookProgresses_CurrentAudioBookProgressId",
                        column: x => x.CurrentAudioBookProgressId,
                        principalTable: "BookProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeriesProgresses_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesProgresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    FileProgressId = table.Column<Guid>(nullable: false),
                    AudioFileProgressId = table.Column<Guid>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    IP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayHistory_FileProgresses_AudioFileProgressId",
                        column: x => x.AudioFileProgressId,
                        principalTable: "FileProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AudioBooks_PublisherId",
                table: "AudioBooks",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBooks_SeriesId",
                table: "AudioBooks",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioFiles_AudioTagsId",
                table: "AudioFiles",
                column: "AudioTagsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookProgresses_AudioBookId",
                table: "BookProgresses",
                column: "AudioBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookProgresses_SeriesProgressId",
                table: "BookProgresses",
                column: "SeriesProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_BookProgresses_UserId",
                table: "BookProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FileProgresses_AudioBookProgressId",
                table: "FileProgresses",
                column: "AudioBookProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_FileProgresses_UserId",
                table: "FileProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FileProgresses_FileAudioBookId_FileTrackNumber",
                table: "FileProgresses",
                columns: new[] { "FileAudioBookId", "FileTrackNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistory_AudioFileProgressId",
                table: "PlayHistory",
                column: "AudioFileProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayHistory_UserId",
                table: "PlayHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_PublisherId",
                table: "Series",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesAuthors_SeriesId",
                table: "SeriesAuthors",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesProgresses_CurrentAudioBookProgressId",
                table: "SeriesProgresses",
                column: "CurrentAudioBookProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesProgresses_SeriesId",
                table: "SeriesProgresses",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesProgresses_UserId",
                table: "SeriesProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookProgresses_SeriesProgresses_SeriesProgressId",
                table: "BookProgresses",
                column: "SeriesProgressId",
                principalTable: "SeriesProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookProgresses_AspNetUsers_UserId",
                table: "BookProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_SeriesProgresses_AspNetUsers_UserId",
                table: "SeriesProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioBooks_Publishers_PublisherId",
                table: "AudioBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Publishers_PublisherId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioBooks_Series_SeriesId",
                table: "AudioBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_SeriesProgresses_Series_SeriesId",
                table: "SeriesProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_BookProgresses_AudioBooks_AudioBookId",
                table: "BookProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_BookProgresses_SeriesProgresses_SeriesProgressId",
                table: "BookProgresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "MediaDirectories");

            migrationBuilder.DropTable(
                name: "PlayHistory");

            migrationBuilder.DropTable(
                name: "SeriesAuthors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FileProgresses");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "AudioFiles");

            migrationBuilder.DropTable(
                name: "AudioFileTags");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "AudioBooks");

            migrationBuilder.DropTable(
                name: "SeriesProgresses");

            migrationBuilder.DropTable(
                name: "BookProgresses");
        }
    }
}
