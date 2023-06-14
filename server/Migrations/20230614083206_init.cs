using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Bid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Bid);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Pid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bid = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Pid);
                    table.ForeignKey(
                        name: "FK_Product_Brand_Bid",
                        column: x => x.Bid,
                        principalTable: "Brand",
                        principalColumn: "Bid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<int>(type: "int", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Address_id);
                    table.ForeignKey(
                        name: "FK_Address_User_Uid",
                        column: x => x.Uid,
                        principalTable: "User",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Oder_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<int>(type: "int", nullable: false),
                    Address_id = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Oder_id);
                    table.ForeignKey(
                        name: "FK_Order_Address_Address_id",
                        column: x => x.Address_id,
                        principalTable: "Address",
                        principalColumn: "Address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_User_Uid",
                        column: x => x.Uid,
                        principalTable: "User",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Order_detail",
                columns: table => new
                {
                    Order_items_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_detail", x => x.Order_items_id);
                    table.ForeignKey(
                        name: "FK_Order_detail_Order_Order_id",
                        column: x => x.Order_id,
                        principalTable: "Order",
                        principalColumn: "Oder_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_detail_Product_Pid",
                        column: x => x.Pid,
                        principalTable: "Product",
                        principalColumn: "Pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_Uid",
                table: "Address",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Address_id",
                table: "Order",
                column: "Address_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Uid",
                table: "Order",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_detail_Order_id",
                table: "Order_detail",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_detail_Pid",
                table: "Order_detail",
                column: "Pid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Bid",
                table: "Product",
                column: "Bid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_detail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
