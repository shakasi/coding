using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoCare.WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auth_EntityInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TypeName = table.Column<string>(nullable: false),
                    AuditEnabled = table.Column<bool>(nullable: false),
                    PropertyJson = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_EntityInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Function",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    IsController = table.Column<bool>(nullable: false),
                    IsAjax = table.Column<bool>(nullable: false),
                    AccessType = table.Column<int>(nullable: false),
                    IsAccessTypeChanged = table.Column<bool>(nullable: false),
                    AuditOperationEnabled = table.Column<bool>(nullable: false),
                    AuditEntityEnabled = table.Column<bool>(nullable: false),
                    CacheExpirationSeconds = table.Column<int>(nullable: false),
                    IsCacheSliding = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MED_EXAM_REPORT",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientId = table.Column<string>(maxLength: 60, nullable: true),
                    VisitId = table.Column<string>(maxLength: 60, nullable: true),
                    ExamNo = table.Column<string>(maxLength: 100, nullable: true),
                    ExamClass = table.Column<string>(maxLength: 100, nullable: true),
                    ExamSubClass = table.Column<string>(maxLength: 100, nullable: true),
                    ClinDiag = table.Column<string>(maxLength: 500, nullable: true),
                    ExamMode = table.Column<string>(maxLength: 100, nullable: true),
                    Device = table.Column<string>(maxLength: 100, nullable: true),
                    PerformedBy = table.Column<string>(maxLength: 100, nullable: true),
                    ReqDateTime = table.Column<DateTime>(nullable: true),
                    ReqDept = table.Column<string>(maxLength: 100, nullable: true),
                    ReqPhysician = table.Column<string>(maxLength: 100, nullable: true),
                    ReqMemo = table.Column<string>(maxLength: 1000, nullable: true),
                    Notice = table.Column<string>(maxLength: 60, nullable: true),
                    ExamStartDate = table.Column<DateTime>(nullable: true),
                    ExamEndDate = table.Column<DateTime>(nullable: true),
                    ReportDateTime = table.Column<DateTime>(maxLength: 60, nullable: true),
                    ExamPara = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Recommendation = table.Column<string>(nullable: true),
                    Technician = table.Column<string>(maxLength: 100, nullable: true),
                    Reporter = table.Column<string>(maxLength: 100, nullable: true),
                    resultStatus = table.Column<string>(maxLength: 100, nullable: true),
                    VerifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    VerifiedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_EXAM_REPORT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MED_LAB_REPORT",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientId = table.Column<string>(maxLength: 60, nullable: true),
                    VisitId = table.Column<string>(maxLength: 60, nullable: true),
                    TestNo = table.Column<string>(maxLength: 60, nullable: true),
                    PriorityIndicator = table.Column<string>(maxLength: 60, nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: true),
                    TestCause = table.Column<string>(maxLength: 500, nullable: true),
                    RelevantClinicDiag = table.Column<string>(nullable: true),
                    Specimen = table.Column<string>(nullable: true),
                    SpcmReceivedDateTime = table.Column<DateTime>(nullable: true),
                    ApplyTime = table.Column<DateTime>(nullable: true),
                    OrderingDept = table.Column<string>(maxLength: 60, nullable: true),
                    OrderingProvider = table.Column<string>(maxLength: 60, nullable: true),
                    PerformedBy = table.Column<string>(maxLength: 60, nullable: true),
                    ResultStatus = table.Column<string>(maxLength: 60, nullable: true),
                    ResultRptDateTime = table.Column<DateTime>(maxLength: 60, nullable: true),
                    Transcriptionist = table.Column<string>(maxLength: 60, nullable: true),
                    VerifiedBy = table.Column<string>(maxLength: 60, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 60, nullable: true),
                    ReportItemName = table.Column<string>(maxLength: 100, nullable: true),
                    ReportItemCode = table.Column<string>(maxLength: 100, nullable: true),
                    AbnormalIndicator = table.Column<string>(maxLength: 60, nullable: true),
                    Result = table.Column<string>(nullable: true),
                    Units = table.Column<string>(maxLength: 40, nullable: true),
                    ResultDateTime = table.Column<DateTime>(nullable: true),
                    ReferenceResult = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_LAB_REPORT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MED_PATIENT_REGIST",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientId = table.Column<string>(maxLength: 60, nullable: true),
                    CardId = table.Column<string>(maxLength: 60, nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: true),
                    Nation = table.Column<string>(maxLength: 10, nullable: true),
                    Sex = table.Column<string>(maxLength: 60, nullable: true),
                    DateOfBirth = table.Column<string>(maxLength: 60, nullable: true),
                    IdNo = table.Column<string>(maxLength: 60, nullable: true),
                    MailingAddress = table.Column<string>(nullable: true),
                    PhoneNumberHome = table.Column<string>(maxLength: 60, nullable: true),
                    PhoneNumberBusiness = table.Column<string>(maxLength: 60, nullable: true),
                    VisitId = table.Column<string>(maxLength: 60, nullable: true),
                    REGIST_TIME = table.Column<DateTime>(nullable: true),
                    DeptCode = table.Column<string>(maxLength: 60, nullable: true),
                    DiagDesc = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MED_PATIENT_REGIST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Systems_KeyValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ValueJson = table.Column<string>(nullable: true),
                    ValueType = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems_KeyValue", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "ClassFullNameIndex",
                table: "Auth_EntityInfo",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AreaControllerActionIndex",
                table: "Auth_Function",
                columns: new[] { "Area", "Controller", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "examreport_index",
                table: "MED_EXAM_REPORT",
                columns: new[] { "PatientId", "VisitId" });

            migrationBuilder.CreateIndex(
                name: "labreport_index",
                table: "MED_LAB_REPORT",
                columns: new[] { "TestNo", "ReportItemCode", "ItemNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "cardid_index",
                table: "MED_PATIENT_REGIST",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "idno_index",
                table: "MED_PATIENT_REGIST",
                column: "IdNo");

            migrationBuilder.CreateIndex(
                name: "patientid_visitid_index",
                table: "MED_PATIENT_REGIST",
                columns: new[] { "PatientId", "VisitId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth_EntityInfo");

            migrationBuilder.DropTable(
                name: "Auth_Function");

            migrationBuilder.DropTable(
                name: "MED_EXAM_REPORT");

            migrationBuilder.DropTable(
                name: "MED_LAB_REPORT");

            migrationBuilder.DropTable(
                name: "MED_PATIENT_REGIST");

            migrationBuilder.DropTable(
                name: "Systems_KeyValue");
        }
    }
}
