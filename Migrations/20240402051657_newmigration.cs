using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    collegeUniqueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges", x => x.collegeUniqueId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseUniqueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    collegeUniqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.courseUniqueId);
                    table.ForeignKey(
                        name: "FK_Courses_Colleges_collegeUniqueId",
                        column: x => x.collegeUniqueId,
                        principalTable: "Colleges",
                        principalColumn: "collegeUniqueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    AdmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    collegeUniqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.AdmissionId);
                    table.ForeignKey(
                        name: "FK_Students_Colleges_collegeUniqueId",
                        column: x => x.collegeUniqueId,
                        principalTable: "Colleges",
                        principalColumn: "collegeUniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    AdmissionId = table.Column<int>(type: "int", nullable: false),
                    courseUniqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.AdmissionId, x.courseUniqueId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_courseUniqueId",
                        column: x => x.courseUniqueId,
                        principalTable: "Courses",
                        principalColumn: "courseUniqueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_AdmissionId",
                        column: x => x.AdmissionId,
                        principalTable: "Students",
                        principalColumn: "AdmissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_collegeUniqueId",
                table: "Courses",
                column: "collegeUniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_courseUniqueId",
                table: "StudentCourses",
                column: "courseUniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_collegeUniqueId",
                table: "Students",
                column: "collegeUniqueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Colleges");
        }
    }
}
