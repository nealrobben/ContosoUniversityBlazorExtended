﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

public partial class ContosoUniversityTables : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Instructor",
            columns: table => new
            {
                ID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                ProfilePictureName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Instructor", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "Student",
            columns: table => new
            {
                ID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                ProfilePictureName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Student", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "Department",
            columns: table => new
            {
                DepartmentID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                Budget = table.Column<decimal>(type: "money", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                InstructorID = table.Column<int>(type: "int", nullable: true),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Department", x => x.DepartmentID);
                table.ForeignKey(
                    name: "FK_Department_Instructor_InstructorID",
                    column: x => x.InstructorID,
                    principalTable: "Instructor",
                    principalColumn: "ID");
            });

        migrationBuilder.CreateTable(
            name: "OfficeAssignment",
            columns: table => new
            {
                InstructorID = table.Column<int>(type: "int", nullable: false),
                Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OfficeAssignment", x => x.InstructorID);
                table.ForeignKey(
                    name: "FK_OfficeAssignment_Instructor_InstructorID",
                    column: x => x.InstructorID,
                    principalTable: "Instructor",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Course",
            columns: table => new
            {
                CourseID = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                Credits = table.Column<int>(type: "int", nullable: false),
                DepartmentID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Course", x => x.CourseID);
                table.ForeignKey(
                    name: "FK_Course_Department_DepartmentID",
                    column: x => x.DepartmentID,
                    principalTable: "Department",
                    principalColumn: "DepartmentID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CourseAssignment",
            columns: table => new
            {
                InstructorID = table.Column<int>(type: "int", nullable: false),
                CourseID = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseAssignment", x => new { x.CourseID, x.InstructorID });
                table.ForeignKey(
                    name: "FK_CourseAssignment_Course_CourseID",
                    column: x => x.CourseID,
                    principalTable: "Course",
                    principalColumn: "CourseID",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CourseAssignment_Instructor_InstructorID",
                    column: x => x.InstructorID,
                    principalTable: "Instructor",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Enrollment",
            columns: table => new
            {
                EnrollmentID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CourseID = table.Column<int>(type: "int", nullable: false),
                StudentID = table.Column<int>(type: "int", nullable: false),
                Grade = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Enrollment", x => x.EnrollmentID);
                table.ForeignKey(
                    name: "FK_Enrollment_Course_CourseID",
                    column: x => x.CourseID,
                    principalTable: "Course",
                    principalColumn: "CourseID",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Enrollment_Student_StudentID",
                    column: x => x.StudentID,
                    principalTable: "Student",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Course_DepartmentID",
            table: "Course",
            column: "DepartmentID");

        migrationBuilder.CreateIndex(
            name: "IX_CourseAssignment_InstructorID",
            table: "CourseAssignment",
            column: "InstructorID");

        migrationBuilder.CreateIndex(
            name: "IX_Department_InstructorID",
            table: "Department",
            column: "InstructorID");

        migrationBuilder.CreateIndex(
            name: "IX_Enrollment_CourseID",
            table: "Enrollment",
            column: "CourseID");

        migrationBuilder.CreateIndex(
            name: "IX_Enrollment_StudentID",
            table: "Enrollment",
            column: "StudentID");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CourseAssignment");

        migrationBuilder.DropTable(
            name: "Enrollment");

        migrationBuilder.DropTable(
            name: "OfficeAssignment");

        migrationBuilder.DropTable(
            name: "Course");

        migrationBuilder.DropTable(
            name: "Student");

        migrationBuilder.DropTable(
            name: "Department");

        migrationBuilder.DropTable(
            name: "Instructor");
    }
}
