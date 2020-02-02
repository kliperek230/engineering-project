using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodMateApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(maxLength: 20, nullable: true),
                    product_name_eng = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    kcal = table.Column<double>(nullable: true),
                    protein = table.Column<double>(nullable: true),
                    carbs = table.Column<double>(nullable: true),
                    fats = table.Column<double>(nullable: true),
                    product_name_pl = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__products__47027DF5F1DA5EBE", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(maxLength: 20, nullable: false),
                    last_name = table.Column<string>(maxLength: 20, nullable: false),
                    sex = table.Column<string>(maxLength: 1, nullable: false),
                    birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    u_height = table.Column<int>(nullable: false),
                    u_weight = table.Column<int>(nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    pswd = table.Column<string>(maxLength: 100, nullable: true),
                    nickname = table.Column<string>(maxLength: 20, nullable: false),
                    kcal = table.Column<double>(nullable: true),
                    protein = table.Column<double>(nullable: true),
                    carbs = table.Column<double>(nullable: true),
                    fats = table.Column<double>(nullable: true),
                    bench = table.Column<int>(nullable: true),
                    ohp = table.Column<int>(nullable: true),
                    squat = table.Column<int>(nullable: true),
                    deadlift = table.Column<int>(nullable: true),
                    measurements = table.Column<int>(nullable: true),
                    u_type = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__B9BE370F38AA58BF", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Lifts",
                columns: table => new
                {
                    lift_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    m_date = table.Column<DateTime>(type: "date", nullable: false),
                    lift_name = table.Column<string>(maxLength: 8, nullable: false),
                    value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lifts__D9C8A289337D7016", x => x.lift_id);
                    table.ForeignKey(
                        name: "FK__lifts__user_id__5441852A",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    meal_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    m_date = table.Column<DateTime>(type: "date", nullable: true),
                    product_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__meals__2910B00F78701AE6", x => x.meal_id);
                    table.ForeignKey(
                        name: "FK__meals__product_i__6A30C649",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__meals__user_id__693CA210",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    measurement_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    m_date = table.Column<DateTime>(type: "date", nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    l_calve = table.Column<int>(nullable: true),
                    r_calve = table.Column<int>(nullable: true),
                    l_thigh = table.Column<int>(nullable: true),
                    r_thigh = table.Column<int>(nullable: true),
                    butt = table.Column<int>(nullable: true),
                    waist = table.Column<int>(nullable: true),
                    chest = table.Column<int>(nullable: true),
                    l_arm = table.Column<int>(nullable: true),
                    r_arm = table.Column<int>(nullable: true),
                    l_forearm = table.Column<int>(nullable: true),
                    r_forearm = table.Column<int>(nullable: true),
                    u_weight = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__measurem__E3D1E1C1D6C54877", x => x.measurement_id);
                    table.ForeignKey(
                        name: "FK__measureme__user___4D94879B",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lifts_user_id",
                table: "Lifts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_product_id",
                table: "Meals",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_user_id",
                table: "Meals",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_user_id",
                table: "Measurements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_bench",
                table: "Users",
                column: "bench");

            migrationBuilder.CreateIndex(
                name: "IX_Users_deadlift",
                table: "Users",
                column: "deadlift");

            migrationBuilder.CreateIndex(
                name: "IX_Users_measurements",
                table: "Users",
                column: "measurements");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ohp",
                table: "Users",
                column: "ohp");

            migrationBuilder.CreateIndex(
                name: "IX_Users_squat",
                table: "Users",
                column: "squat");

            migrationBuilder.AddForeignKey(
                name: "FK__users__bench__5629CD9C",
                table: "Users",
                column: "bench",
                principalTable: "Lifts",
                principalColumn: "lift_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__users__deadlift__59063A47",
                table: "Users",
                column: "deadlift",
                principalTable: "Lifts",
                principalColumn: "lift_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__users__ohp__571DF1D5",
                table: "Users",
                column: "ohp",
                principalTable: "Lifts",
                principalColumn: "lift_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__users__squat__5812160E",
                table: "Users",
                column: "squat",
                principalTable: "Lifts",
                principalColumn: "lift_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__users__measureme__5535A963",
                table: "Users",
                column: "measurements",
                principalTable: "Measurements",
                principalColumn: "measurement_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__lifts__user_id__5441852A",
                table: "Lifts");

            migrationBuilder.DropForeignKey(
                name: "FK__measureme__user___4D94879B",
                table: "Measurements");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Lifts");

            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
