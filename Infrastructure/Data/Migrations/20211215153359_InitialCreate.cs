using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallsTaskInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WebLetRecId = table.Column<int>(type: "int", nullable: true),
                    CallNo = table.Column<int>(type: "int", nullable: false),
                    ExecNo = table.Column<int>(type: "int", nullable: false),
                    SrvManNo = table.Column<int>(type: "int", nullable: false),
                    SrvCarNo = table.Column<int>(type: "int", nullable: false),
                    ClosingTm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingFualtTp = table.Column<int>(type: "int", nullable: true),
                    ClosingReasonCd = table.Column<int>(type: "int", nullable: true),
                    ClosingRem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SrvTp = table.Column<int>(type: "int", nullable: false),
                    ExtraPriceExists = table.Column<int>(type: "int", nullable: true),
                    ExecExtraPrice = table.Column<double>(type: "float", nullable: true),
                    BatterySold = table.Column<int>(type: "int", nullable: true),
                    BatteryPrice = table.Column<double>(type: "float", nullable: true),
                    BatteryPicPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OfficeRemExists = table.Column<int>(type: "int", nullable: true),
                    OfficeRem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TowingAccident = table.Column<int>(type: "int", nullable: true),
                    SbcCarLocation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SbcCarCrossroadLoc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CarInvolved = table.Column<int>(type: "int", nullable: true),
                    InvolvedCarNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CarLocOffRoad = table.Column<int>(type: "int", nullable: true),
                    OnMyWayRem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SubCarKm = table.Column<int>(type: "int", nullable: true),
                    SubCarFuelSts = table.Column<int>(type: "int", nullable: true),
                    SubDrvNm1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubDrvIdCrtNo1 = table.Column<int>(type: "int", nullable: true),
                    SubDrvBirthDt1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubDrvLicenseNo1 = table.Column<int>(type: "int", nullable: true),
                    DrvLicenseVldEndDt1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubDrvNm2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubDrvIdCrtNo2 = table.Column<int>(type: "int", nullable: true),
                    SubDrvBirthDt2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubDrvLicenseNo2 = table.Column<int>(type: "int", nullable: true),
                    DrvLicenseVldEndDt2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceNo = table.Column<int>(type: "int", nullable: true),
                    CreditCardTp = table.Column<int>(type: "int", nullable: true),
                    CardHolderTz = table.Column<int>(type: "int", nullable: true),
                    CardHolderNm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardCvv = table.Column<int>(type: "int", nullable: true),
                    SignerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SignerTz = table.Column<int>(type: "int", nullable: true),
                    ArrivalX = table.Column<double>(type: "float", nullable: true),
                    ArrivalY = table.Column<double>(type: "float", nullable: true),
                    Pic1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic6 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic7 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic8 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic9 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic10 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic11 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic12 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic13 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pic14 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecSts = table.Column<int>(type: "int", nullable: true),
                    CardVldDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallsTaskInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClosingReason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreadirCardTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShortDsc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreditCmpNote = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CardIdentifire = table.Column<int>(type: "int", nullable: true),
                    VisaNoLength = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreadirCardTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaultTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fnc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fnc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LockedIps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lockedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockedIps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LockedSrvMans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lockedSrvManNo = table.Column<long>(type: "bigint", nullable: false),
                    SrvManNM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSupplier = table.Column<bool>(type: "bit", nullable: false),
                    lockedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockedSrvMans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sessionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    ip = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SrvManName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SrvManNo = table.Column<int>(type: "int", nullable: false),
                    phoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    smsCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    smsCodeCounter = table.Column<int>(type: "int", nullable: true),
                    lastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    smsCodeRetriesCounter = table.Column<int>(type: "int", nullable: true),
                    smsCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isSupplier = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestStsTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStsTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hebrewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stringValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    intValue = table.Column<int>(type: "int", nullable: true),
                    decimalValue = table.Column<double>(type: "float", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrcSrv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrcSrv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrvManAttnTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvManAttnTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrvManExecStsTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvManExecStsTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrvManShiftTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvManShiftTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrvManTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvManTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrvTp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dsc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrcSrvCd = table.Column<int>(type: "int", nullable: false),
                    ActivityTp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvTp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Security",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "XYLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CallNo = table.Column<int>(type: "int", nullable: false),
                    ExecNo = table.Column<int>(type: "int", nullable: false),
                    SrvManNo = table.Column<int>(type: "int", nullable: false),
                    SrvCarNo = table.Column<int>(type: "int", nullable: false),
                    LocX = table.Column<double>(type: "float", nullable: false),
                    LocY = table.Column<double>(type: "float", nullable: false),
                    RepTm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecSts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XYLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SrvManExecSts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Call_No = table.Column<int>(type: "int", nullable: false),
                    Exec_No = table.Column<int>(type: "int", nullable: false),
                    Web_Let_Rec_Id = table.Column<int>(type: "int", nullable: false),
                    SrvManExecStsTpId = table.Column<int>(type: "int", nullable: false),
                    Rep_Tm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rep_X = table.Column<double>(type: "float", nullable: true),
                    Rep_Y = table.Column<double>(type: "float", nullable: true),
                    Srv_Man_No = table.Column<int>(type: "int", nullable: false),
                    Srv_Car_No = table.Column<int>(type: "int", nullable: false),
                    Rec_Sts = table.Column<int>(type: "int", nullable: false),
                    External_Oprr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvManExecSts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SrvManExecSts_SrvManExecStsTp_SrvManExecStsTpId",
                        column: x => x.SrvManExecStsTpId,
                        principalTable: "SrvManExecStsTp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SrvManAttn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SrvManNo = table.Column<int>(type: "int", nullable: false),
                    SrvManShiftTpId = table.Column<int>(type: "int", nullable: false),
                    ShiftDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftX = table.Column<double>(type: "float", nullable: true),
                    ShiftY = table.Column<double>(type: "float", nullable: true),
                    SrvCarNo = table.Column<int>(type: "int", nullable: false),
                    SrvCarKm = table.Column<float>(type: "real", nullable: true),
                    RecSts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvManAttn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SrvManAttn_SrvManShiftTp_SrvManShiftTpId",
                        column: x => x.SrvManShiftTpId,
                        principalTable: "SrvManShiftTp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SrvMan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstNm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNm = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SrvManTpId = table.Column<int>(type: "int", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrvMan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SrvMan_SrvManTp_SrvManTpId",
                        column: x => x.SrvManTpId,
                        principalTable: "SrvManTp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FleetCarRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CallNo = table.Column<int>(type: "int", nullable: true),
                    ExecNo = table.Column<int>(type: "int", nullable: true),
                    SrcSrvId = table.Column<int>(type: "int", nullable: true),
                    SrvTpId = table.Column<int>(type: "int", nullable: true),
                    SrvTpDsc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SrvManNo = table.Column<int>(type: "int", nullable: true),
                    ExternalOprr = table.Column<int>(type: "int", nullable: true),
                    ExecApxTm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SbcCarNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrvCarNo = table.Column<int>(type: "int", nullable: false),
                    SrcAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SrcX = table.Column<double>(type: "float", nullable: true),
                    SrcY = table.Column<double>(type: "float", nullable: true),
                    DestAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DestX = table.Column<double>(type: "float", nullable: true),
                    DestY = table.Column<double>(type: "float", nullable: true),
                    GarageNm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CltGivenExecTm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SbcCarTpDsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SbcCarInfo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FaultTpDsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CallReceiverEmpNm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DispatcherEmpNm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SbcNm = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SbcCmpNm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SbcIdCrtNo = table.Column<int>(type: "int", nullable: true),
                    WaitNm = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    SbcMobilePhoneNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    SbcContactPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    SbcCarLocDsc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    KeyLocDsc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ExecReminderRem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OfficeRem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ExecPrice = table.Column<double>(type: "float", nullable: true),
                    SubCar = table.Column<int>(type: "int", nullable: true),
                    SubContractNo = table.Column<int>(type: "int", nullable: true),
                    SubAdmissionTm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubReturnTm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubReturnDestAddrr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubDailyRentPrice = table.Column<double>(type: "float", nullable: true),
                    SubDrvNm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubDrvIdCrtNo = table.Column<int>(type: "int", nullable: true),
                    SubDrvBirthDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubDrvLicenseNo = table.Column<int>(type: "int", nullable: true),
                    DrvLicenseReceiptDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrvLicenseVldEndDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sts = table.Column<int>(type: "int", nullable: true),
                    ExecSeq = table.Column<int>(type: "int", nullable: true),
                    DipatcherEmpNo = table.Column<int>(type: "int", nullable: true),
                    SbcCarAreaId = table.Column<int>(type: "int", nullable: false),
                    SbcCarAreaDsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DestAreaId = table.Column<int>(type: "int", nullable: true),
                    DestAreaDsc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CallCustomer = table.Column<int>(type: "int", nullable: true),
                    CreateTm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowCallInfo = table.Column<int>(type: "int", nullable: true),
                    UpdateNotification = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FleetCarRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FleetCarRequest_Area_DestAreaId",
                        column: x => x.DestAreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCarRequest_Area_SbcCarAreaId",
                        column: x => x.SbcCarAreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FleetCarRequest_SrcSrv_SrcSrvId",
                        column: x => x.SrcSrvId,
                        principalTable: "SrcSrv",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FleetCarRequest_SrvTp_SrvTpId",
                        column: x => x.SrvTpId,
                        principalTable: "SrvTp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FleetCarRequest_DestAreaId",
                table: "FleetCarRequest",
                column: "DestAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCarRequest_SbcCarAreaId",
                table: "FleetCarRequest",
                column: "SbcCarAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCarRequest_SrcSrvId",
                table: "FleetCarRequest",
                column: "SrcSrvId");

            migrationBuilder.CreateIndex(
                name: "IX_FleetCarRequest_SrvTpId",
                table: "FleetCarRequest",
                column: "SrvTpId");

            migrationBuilder.CreateIndex(
                name: "IX_SrvMan_SrvManTpId",
                table: "SrvMan",
                column: "SrvManTpId");

            migrationBuilder.CreateIndex(
                name: "IX_SrvManAttn_SrvManShiftTpId",
                table: "SrvManAttn",
                column: "SrvManShiftTpId");

            migrationBuilder.CreateIndex(
                name: "IX_SrvManExecSts_SrvManExecStsTpId",
                table: "SrvManExecSts",
                column: "SrvManExecStsTpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallsTaskInfo");

            migrationBuilder.DropTable(
                name: "ClosingReason");

            migrationBuilder.DropTable(
                name: "CreadirCardTp");

            migrationBuilder.DropTable(
                name: "FaultTp");

            migrationBuilder.DropTable(
                name: "FleetCarRequest");

            migrationBuilder.DropTable(
                name: "Fnc");

            migrationBuilder.DropTable(
                name: "LockedIps");

            migrationBuilder.DropTable(
                name: "LockedSrvMans");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "RequestStsTp");

            migrationBuilder.DropTable(
                name: "SiteParams");

            migrationBuilder.DropTable(
                name: "SrvMan");

            migrationBuilder.DropTable(
                name: "SrvManAttn");

            migrationBuilder.DropTable(
                name: "SrvManAttnTp");

            migrationBuilder.DropTable(
                name: "SrvManExecSts");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "XYLocation");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "SrcSrv");

            migrationBuilder.DropTable(
                name: "SrvTp");

            migrationBuilder.DropTable(
                name: "SrvManTp");

            migrationBuilder.DropTable(
                name: "SrvManShiftTp");

            migrationBuilder.DropTable(
                name: "SrvManExecStsTp");
        }
    }
}
