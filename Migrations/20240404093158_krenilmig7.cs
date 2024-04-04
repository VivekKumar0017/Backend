using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class krenilmig7 : Migration
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
                    collegeUniqueId = table.Column<int>(type: "int", nullable: false),
                    courseFees = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElegblityCriteria = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    collegeUniqueId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.AdmissionId);
                    table.ForeignKey(
                        name: "FK_Students_Colleges_collegeUniqueId",
                        column: x => x.collegeUniqueId,
                        principalTable: "Colleges",
                        principalColumn: "collegeUniqueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    CoursescourseUniqueId = table.Column<int>(type: "int", nullable: false),
                    StudentsAdmissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.CoursescourseUniqueId, x.StudentsAdmissionId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CoursescourseUniqueId",
                        column: x => x.CoursescourseUniqueId,
                        principalTable: "Courses",
                        principalColumn: "courseUniqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentsAdmissionId",
                        column: x => x.StudentsAdmissionId,
                        principalTable: "Students",
                        principalColumn: "AdmissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentReports",
                columns: table => new
                {
                    studentReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionId = table.Column<int>(type: "int", nullable: false),
                    Attendance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentAdmissionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentReports", x => x.studentReportId);
                    table.ForeignKey(
                        name: "FK_StudentReports_Students_AdmissionId",
                        column: x => x.AdmissionId,
                        principalTable: "Students",
                        principalColumn: "AdmissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentReports_Students_StudentAdmissionId",
                        column: x => x.StudentAdmissionId,
                        principalTable: "Students",
                        principalColumn: "AdmissionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_collegeUniqueId",
                table: "Courses",
                column: "collegeUniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentsAdmissionId",
                table: "StudentCourses",
                column: "StudentsAdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReports_AdmissionId",
                table: "StudentReports",
                column: "AdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReports_StudentAdmissionId",
                table: "StudentReports",
                column: "StudentAdmissionId");

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
                name: "StudentReports");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Colleges");
        }
    }
}
