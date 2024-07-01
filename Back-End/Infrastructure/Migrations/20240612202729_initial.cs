using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPay = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_Category",
                        column: x => x.Category,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleProduct",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProduct", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Product_Product",
                        column: x => x.Product,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Sale_Sale",
                        column: x => x.Sale,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Electrodomésticos" },
                    { 2, "Tecnología y Electrónica" },
                    { 3, "Moda y Accesorios" },
                    { 4, "Hogar y Decoración" },
                    { 5, "Salud y Belleza" },
                    { 6, "Deportes y Ocio" },
                    { 7, "Juguetes y Juegos" },
                    { 8, "Alimentos y Bebidas" },
                    { 9, "Libros y Material Educativo" },
                    { 10, "Jardinería y Bricolaje" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Description", "Discount", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0f5ba235-5ea3-4bf4-a042-22ece88128fb"), 3, "Vestido largo de noche con encaje y detalles bordados, ideal para eventos formales.", 20, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRyd53rEy27sYraa0s7ieQuUdokjcWii6xsaDy6ZxIimYVBEVhtJqS4f8ND__YYk04NfprorgGZ4b54HHSHVVHmDY8DeFmg9kfQZFzZLIVg5F_18Jy4EVl2&usqp=CAE", "Vestido de Noche Elegante", 89999m },
                    { new Guid("16d2a07c-4aa6-4b42-a4f3-a36c63f97149"), 4, "Espejo decorativo con marco de madera tallada a mano, ideal para espacios de estilo clásico y elegante.", 12, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcT82RXmIwA_9cjxcRJgmG2QyLd4sotsAol1BrhdfR_aDza9fYnBZUZ57NN8OGq29GmH8uY8H3J0zyK30ZvFuZ5k16ICbuaWxgAsYndmtUOzmUej7Wu4cmxi3w&usqp=CAE", "Espejo Decorativo con Marco de Madera", 69999m },
                    { new Guid("1a09579e-699c-43a0-b8d2-5f0973d7b89c"), 3, "Reloj inteligente con pantalla táctil AMOLED, monitorización de salud y seguimiento de actividad física.", 5, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSAinXkzzhkGSJUZEq6RU6rU9wiHOj5cwT-BemDcqUHkZK9V07SPOgWp4OGqeICP2bYZopvqt8DUZ6qwZWQm663NMT64UpwF2k_QaVQnh-UPa49XJPNr2kIfQ&usqp=CAE", "Reloj de Pulsera Fossil Gen 6", 349999m },
                    { new Guid("2ada394c-3a37-4543-bc4a-e152b62c8535"), 10, "Mesa de trabajo plegable para bricolaje, superficie resistente y ajustable, fácil de transportar y almacenar.", 8, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTwCwcb9QWfOGKeSChOhBYEMYsq2gh8C4lOH511rM0FQXaV6Y1X3PehT9S-PqrVPE09I-IPMZNgKhmDsuCbvf4rurGY6s9cPWE07_Ing6TvLMg8EDdTk2f4&usqp=CAE", "Mesa de Trabajo Plegable", 49999m },
                    { new Guid("34686da4-6c82-4ba3-9c31-ee183a807d5d"), 6, "Bicicleta de montaña ligera y resistente, cuadro de aluminio, frenos de disco hidráulicos y cambios Shimano.", 10, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcR5FzXwYigKlk2I3thb1_YtDN-9CIhxsoYxTm4Y0jpNJoBDaTWt48GgoS_u4yYL963-SOseMqKcSpvQO8sJL1O2OabXzoz8q71kDalp3ktvaoyucBYwG8c4&usqp=CAE", "Bicicleta de Montaña Trek", 699999m },
                    { new Guid("352a55c4-1942-4bc1-b972-601707a54d18"), 10, "Kit de iluminación solar para jardín, incluye lámparas LED, paneles solares y baterías recargables.", 5, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTPkMHrIbwKIyl_E5iB30B22OqVvkp5u39OCmp5YTCkqZJJXnLUekLgesCcYz8VfS0gkLc4FXSxSfbvNe9UAuv79OZ_ud2KFEH8Wt6Xlp5hraCUTqoPmykDNw&usqp=CAE", "Set X10 de Iluminación Solar para Jardín", 129999m },
                    { new Guid("35dfcdce-a6be-4794-b887-fc82d6df6987"), 2, "Portátil gaming ultraligero con procesador AMD Ryzen 9 5900HS, pantalla QHD de 14 pulgadas, 16 GB de RAM y SSD de 1 TB.", 10, "https://acdn.mitiendanube.com/stores/001/907/418/products/1-0db142b8c6c121127217116552781441-1024-1024.webp", "Laptop ASUS ROG Zephyrus G14", 1899999m },
                    { new Guid("42b2d88a-57b7-49c4-9179-c71023282306"), 10, "Set completo de herramientas de jardinería, incluye palas, rastrillos, tijeras de podar y guantes protectores.", 10, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcQin4JRIPkEPsIwbCMEN8ncsMlFDWirEpHd2db_NcPhHBNfeWlIBD8Ocf2ZXMt5fQlrgjFWeGLbXpIXCFlvLOSzUbPGpGs8JyjFTD7qRjGFk4FysdZBKAYR&usqp=CAE", "Set de Herramientas de Jardinería", 79999m },
                    { new Guid("437686b0-7481-4bac-afd0-6d5634eb261e"), 3, "Zapatos de cuero clásicos ideales para uso diario y ocasiones formales, diseño cómodo y duradero.", 15, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcTS3foCp5SlYK36hyjerTtBdw4pvT2mOz06laSuSiq6-h7hKH3ke7KtVbSnE3W9koQYufrG1jNprhO7Mrcx2K3mRy5VGXqOPDGaf92qTnEatdbI9wDbOZoc&usqp=CAE", "Zapatos de Vestir Clásicos", 119999m },
                    { new Guid("489cebf7-244a-4b2c-9a83-c1deee47f599"), 9, "Guía completo de fotografía en formato libro, desde principiantes hasta técnicas avanzadas de composición y edición.", 5, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcSSFkQuWBxhZKlMqHJd2t1tONI6j-eExxQ4BS00JEsUGypVByDutUw42mL6e8wcMH-6xubvLu4vI6uleIzu5kK5V76bqa92t7pTKkqWfFMvKp3gnWTxstNIVw&usqp=CAE", "Guía Completo de Fotografía", 64999m },
                    { new Guid("48b153b7-7c08-4cdc-8f0b-1463a516db8e"), 3, "Bolso de mano elegante fabricado en cuero genuino con compartimentos internos y correa ajustable.", 10, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRzG6a3hup9Hr0Dc0l8AIlWdqv9Me8SAYq8JGlLznBm8Sq2FYzlffiN5A2aL71zhkItqqj5RV8MNlSFZqgmrnI_WsFfsNbhUjOLkPgWZSD8ExRxl5mf1ZxO&usqp=CAE", "Bolso de Cuero Genuino", 149999m },
                    { new Guid("4f738766-1988-472c-a834-8418685bfd04"), 6, "Patineta eléctrica con tecnología de autoequilibrio, velocidad ajustable y autonomía de hasta 15 km.", 12, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcSRZ0eRUcN2jqBkAxDNUXnfjvRPtxuiog3wuBRm-DxiAXC-SIPyDsSXSJIsTV7naj4xNAzLgLNA0ZsuVfyLMzovatdRZaaLiDDEu0LignXGsMa0RXE_bgkE&usqp=CAE", "Patineta Eléctrica Hoverboard", 349999m },
                    { new Guid("6972577d-9864-4747-b055-6f743152a49e"), 6, "Pelota de fútbol profesional con diseño aerodinámico, ideal para partidos y entrenamientos intensivos.", 5, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcRC_Yj3UbmHWb1cw6KbXosRPDLOehaY8q3WCvMFLovsEqjFaKgIWO0kNt2BwbKf4PbtSyijiOu7IhYUJO4Y6mrqqxhfKr4gD6Mi9Fai0qZfEwVaCoZBB_Q4&usqp=CAE", "Pelota de Fútbol Profesional Adidas", 79999m },
                    { new Guid("699e2cee-06d1-4d37-9da2-c49eec6daf23"), 8, "Pack de cervezas artesanales de diferentes estilos y sabores, ideal para aficionados a la cerveza.", 5, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRxThpx-03Ko-i1ZLbjSo_c5_hoA0E7qlbELoA4jCTb0D3LPHOfWXwQTUzhF4Vm4wFPDpBpBMGVn7kxCZbq8EIcJQn_1GEGekKWA9Pv6Gc&usqp=CAE", "Pack de Cervezas Artesanales", 69999m },
                    { new Guid("6a0f28e0-a314-4dae-83e7-c83094cd1fdf"), 2, "Potente smartphone con cámara de 108 MP, pantalla Dynamic AMOLED 2X de 6.8 pulgadas, 12 GB de RAM y 256 GB de almacenamiento.", 8, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTJfPPESvNHvqeb6FpNHdloDc-rMakxCHqu8fExoC6pMNepveNoUEgbeoX1S5fy8wFOZ82lWK6WeNrsGpS1KPXOTbh3mZ1qsyCuK5Qrq27HSFcUdT3Ugu30&usqp=CAE", "Smartphone Samsung Galaxy S22 Ultra", 1499999m },
                    { new Guid("6d29f1e5-c4de-46d1-ad22-38f013f52951"), 8, "Caja de chocolates artesanales variados, perfectos para regalo o disfrute personal.", 10, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcSkLOlVkRm_7EkAxE19fIIEkxbjUFZ8cp-gsAGGJWr2YI90Sw4tkTOyOoYomThiSjZsJ9k2YtMjIVaAm_0rvt26Sa8UayxUy7KpCqnur0U&usqp=CAE", "Caja de Chocolates Artesanales", 49999m },
                    { new Guid("723efde1-fe5d-4d59-a942-9f6d1ebfe1c2"), 7, "Pista de carreras Hot Wheels con looping y lanzador de coches, emocionantes carreras para niños y niñas.", 12, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTkDdm8ofVxeHEZg1GdFnwqJNkd0k7kQhnUIiW0MVUraZBBWVEv195ttrZMUi9J7STpj73wJKz20TX8TtFDkdVzAXsFNXR-WU1UhHvlf1wb716l776DnQwu&usqp=CAE", "Pista de Carreras Hot Wheels", 59999m },
                    { new Guid("76ebf61b-7543-4cfd-a570-290b00dd50e4"), 2, "Consola de última generación con gráficos en 4K, SSD ultra rápido, control DualSense y compatibilidad con juegos de PS4.", 5, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcS_zCNBX8XjIOhO1odOQXnZKgvSBsL8x3m9uxc0tusdkgw3dsS0krgtsPjkSB00LBSxPZu1dcauTWc4ZO590-OMQJnNPBiC-ov39wJp7Yz5ixT7Eu0HoXV6&usqp=CAE", "Consola PlayStation 5", 699999m },
                    { new Guid("7b486a22-ad72-4c94-8f5d-6bf48eec14c6"), 1, "El ventilador PIONEER es mas econmico ya que ahorra un 50% de energia electrica\r\nEs practico, facil de trasladar y adaptabilidad.\r\nSus aspas Metalicas lo hacen un producto de gran durabilidad.\r\nsu base de apoyo firme y su rejilla protectora metalica lo hacen un producto seguro", 12, "https://arcencohogar.vtexassets.com/arquivos/ids/371560-1200-1200?v=638422437614700000&width=1200&height=1200&aspect=true", "Ventilador Industrial 18 Metal Pioneer", 54996m },
                    { new Guid("82678766-e12b-49dc-b60c-ac11df9c4372"), 4, "Lámpara de techo LED con diseño moderno, iluminación ajustable y control remoto.", 5, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRBtGaNJn94HnEqFuXWDPw0U1OCb4-CAmV4U38x0922x3iQv8g_yzFNfZefJehExLk9G6zlTlYuTH4k37l0uvHxm5-UwytOQPJdMILIBixuWIhThQnwomtA&usqp=CAE", "Lámpara de Techo LED Moderna", 89999m },
                    { new Guid("878d866f-5530-4e82-a05a-a2bba39e6e33"), 4, "Juego de sábanas de 600 hilos de algodón egipcio, suaves y resistentes, incluye sábana plana, ajustable y fundas.", 8, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTLnuaC_mjKtepU9CwRsJIkMUQD6QKuMNOqg82zT7TUT08nmAuAS7qb0yQsZSUbjFIOMxyo1RkeQmkvZMPlZLpc236mWitN&usqp=CAE", "Juego de Sábanas de Algodón Egipcio", 119999m },
                    { new Guid("8969245a-fb86-437a-9431-2c2c5adba70a"), 7, "Set de construcción LEGO Technic con más de 1500 piezas, incluye instrucciones para construir varios modelos de vehículos.", 10, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcScoyPHobmhmIp5mjYSgG84PDoCsJsx2vulaBH4YF9wiBux5Fl4uu2UKIiyssyahUfvtB_hwZydoo0GNEiiDs4RSu8ITS7GMi9XI4ZH-bKqjlXI9nsbblCe&usqp=CAE", "Set de Construcción LEGO Technic", 79999m },
                    { new Guid("8a6dac6e-9b7b-494f-bd70-55f53f207551"), 5, "Kit completo de maquillaje profesional con paletas de sombras, labiales, bases y brochas de alta calidad.", 15, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcQQaGJjKviM7Ix4i9wmFiBptdH6mDaiHWVuXGAezYydWvNKnGYN8-jfhx5EyZpIWH-aZSwfpgZNk4g7d_Ijr5KkM6N4eKUmAd05lC7xZ8Y&usqp=CAE", "Kit de Maquillaje Profesional", 149999m },
                    { new Guid("90019186-740e-4398-9916-a738983131b1"), 5, "Set de perfumes variados para hombre y mujer, incluye fragancias de marcas reconocidas.", 5, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSUilTJbRGExoWS-vXrgwTVPHZK_gTtyTX6Nu9kc7RG7uXiY_iOEGoOxxnqOK2rbSweoMvR9uK2PL9MY73U9Y1ouAMlUtRWO7oXwOZXkjdUahe33JytR-3E1wdI&usqp=CAE", "Set de Perfumes Variados", 299999m },
                    { new Guid("a5a43d3b-8f82-49f4-83e4-d5ba2e291fab"), 7, "Juego de mesa Monopoly edición especial con temática de ciudades famosas del mundo, ideal para toda la familia.", 5, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcSlRADA0enIzx3OKJuRYmzciBB5LyMbhRV-QToOhdMEeP5vu3b927IL9EtbBvm7j1NV1-jaYyPxbcbAclB3NuoaEYlTUFjxAXJXrIJRWip0q7HfK2RYgbnf&usqp=CAE", "Juego de Mesa Monopoly Edición Especial", 89999m },
                    { new Guid("a6639346-560a-4e65-a78e-67174acba753"), 1, "Climatizar tus espacios a lo largo del año es sin duda algo importante para tu comodidad y la de tus seres queridos. Contar con un aire acondicionado con función frío/calor es la mejor decisión. Con este aire Nex conseguí una mejor relación costo-beneficio.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/365635-1200-1200?v=638350437287400000&width=1200&height=1200&aspect=true", "Aire Acondicionado Nex On Off 2752fn", 850000m },
                    { new Guid("a819291a-5b2c-4617-828e-81c5345be69b"), 8, "Caja de tés gourmet con variedad de infusiones y sabores exóticos, ideal para momentos de relax.", 12, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTsGbPeustN-h1tglmXjbyxJwo9atcnhZ1oWk98kX_BDv79l2bGJZxxJ1aw3MFYmgIyjpENsWmVzraIPF7OPkzCDyf1BSjvZSCw_RBbIZ9ubggodOdVzsP-pw&usqp=CAE", "Caja de Tés Gourmet", 34999m },
                    { new Guid("b1e78f5f-cb85-4b7f-9e81-7f7a648d0b85"), 4, "Set de muebles modulares para sala de estar, incluye sofá, mesa de centro y estantes.", 10, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcQk9uD1VT7W-Ve4ecjwoY5qUJRM2Sgyozg-0SYWtm43CUWI58PLf9U-ADKm_DHielQ9zqr6PFsMazIOFyQrsAXtZ0ZBNd8E9L68W8jhpFk-&usqp=CAE", "Set de Muebles de Sala Modulares", 249999m },
                    { new Guid("b52fc3a6-70cd-413a-a383-fb535dd23d25"), 10, "Invernadero portátil para cultivo de plantas, estructura resistente y cubierta transparente para maximizar la luz solar.", 12, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcRdSRPrD53aV_8AogJf0A4fM__7kbenu3frR26-Qy3P8ctE1Np2tUF9jdsqvXiDTtPRgHaraCQgVF9V4dCF6Yp9yprIRQlq-5W4E2B2Ams--yFH9_VDwa1G&usqp=CAE", "Invernadero Portátil", 89999m },
                    { new Guid("c57b8365-59ce-4ae5-94a7-f7eb922e7094"), 1, "Marca lider del mercado - 6 KG - 800 RPM - Eficiencia energetica A+. Color blanco.", 5, "https://arcencohogar.vtexassets.com/arquivos/ids/372022-1200-1200?v=638428189685100000&width=1200&height=1200&aspect=true", "Lavarropas Drean Eco Next 6Kg Blanco", 54996m },
                    { new Guid("cca31237-ed31-4d48-891a-e476be62a61b"), 9, "Diccionario completo de la lengua española con definiciones actualizadas y ejemplos de uso.", 12, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcRy-bX6YBY0IMEiE-qTv562WsRfROuEcVlHOdNg4Ijxw14Frvz_a__4Y6xeQera_xxT7_A3Z-4LLJHXD9iL2nTSbFARyhabDFmpGFVYKdBuCrC350TrTsgC&usqp=CAE", "Diccionario de Lengua Española", 29999m },
                    { new Guid("d28d83f3-c01a-4315-b738-64c4ca0decf0"), 9, "Enciclopedia ilustrada de ciencias con información detallada y imágenes a todo color, ideal para estudiantes y curiosos.", 8, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTJb12if7DCZK2_ebm14dtyGUKkf7XxgmQljk160OEi_a_L-XP_LBg-h9sMLaAh51UjKM8vSGUFkfZ76AjmVsfi8ZdqOrti6NzQFLy3EWw&usqp=CAE", "Enciclopedia Ilustrada de Ciencias", 89999m },
                    { new Guid("d717c228-c00b-4272-a01a-11f625a189f2"), 9, "Colección completa de libros de Harry Potter, incluye todos los libros de la saga escrita por J.K. Rowling.", 20, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcQY6AhaXhwNP2Doj3pV4bKW87gMCegyTI5efNROST6WodZa1Z0MMlQiNuk_N4T5BpK6-teJs8asTb9rdRzB1TjsGbQMDV7W1hWjbZ0x1UP2HUBK50YhLl8f&usqp=CAE", "Colección Completa Harry Potter", 199999m },
                    { new Guid("dc5b686f-50bd-49a8-a633-4c57f2c425ac"), 6, "Raqueta de tenis de alta gama, diseño ligero y ergonómico, ideal para jugadores avanzados.", 8, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTzrAH75B_-BkzkxMZF3nrQjqqmH0RV2v_Q-99UYUu-hgy1UIIai2elImG_5jw04HmlR-yDhA0va5Oh7KU8-yijhuQ5oGgujn7TElIRNuBCoYvs9muu9Nvq&usqp=CAE", "Raqueta de Tenis Wilson Pro Staff", 249999m },
                    { new Guid("e561be5c-130f-4276-a37f-89f7524bc182"), 5, "Cepillo eléctrico dental con tecnología sónica para una limpieza profunda y efectiva, incluye varios modos de cepillado.", 10, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcQZsrFjq8-ZGuWFMljBqUwFTaWUeqqnWOffmtXr5gLFpKDAPiAkKrj67wUjkpLrVgCwiVH3l9ElvRSk-lO-4_fuZovhkiMGkTrAVWjG9H0v3YD1cgK_QsQAa3Q&usqp=CAE", "Cepillo Eléctrico Dental Sonicare", 79999m },
                    { new Guid("e59a34d5-c217-4713-9194-ac77baf48989"), 7, "Muñeca Barbie con casa de ensueño, incluye accesorios como muebles y ropa para la muñeca.", 8, "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSBnw46A4ymOzK5aVghSFeea4r1dW1jHf1QOAjtBOQ6DeePlJbuFP0lRkgsPbVgGn_mWscgaE8xhcHo0emExkaARt0XiezNeFAI3KXQPEXU0gIkezDygnc1&usqp=CAE", "Muñeca Barbie Dreamhouse", 149999m },
                    { new Guid("ed465709-5395-4dd5-8a6c-479501729324"), 8, "Caja de vinos varietales seleccionados, incluye diferentes cepas y marcas reconocidas.", 15, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcRaULa9aL3suKEjZHcfF15L4YT38sLJuGRf692HUZLwpPPB9CT73Jb3IsJ0CDSxfhg1er4HcYIEbxR_hW09Ag-d2m6xQJmvuieGugdT_4n49RkmnXxfHQum&usqp=CAE", "Caja de Vinos Varietales", 99999m },
                    { new Guid("f1bd874f-4fc8-44d9-bff9-50f6cddc23a3"), 2, "Monitor gaming curvo de 49 pulgadas, resolución Dual QHD (5120x1440), tasa de refresco de 240 Hz y tecnología Quantum Dot.", 15, "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcQFRRNd74eCI_FcSpOQ33Q0B2NjYsEfCbj92xXSxooati04Di_eJnhR9-HOdKqvfkCkTeMBiRxSVQjKhXnxQ_qXGUtr3hJfNWvgctX7KqVKYc8z2XvzvJwzFg&usqp=CAE", "Monitor Curvo Samsung Odyssey G9", 2599999m },
                    { new Guid("fb4e928c-9b7e-48ac-98e6-2926574a7550"), 5, "Mascarilla facial de colágeno para hidratar y revitalizar la piel, ideal para todo tipo de piel.", 20, "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTuwBJcj7gHfHd0NAb38Cny1Stq-RUSI8cdt16G0s96iwbyzvSAuAsIpXTR9BeVIvKEfy6BYlmRW33_kYIkL-Ol2npovN6rV3ot5iMWinkijy9_YqhMjkh-Ug&usqp=CAE", "Mascarilla Facial de Colágeno", 19999m },
                    { new Guid("fc87702b-7273-4e07-a6f6-6eaaf5474b19"), 1, "Esta heladera cuenta con vidrio templado en todos los estantes, ofreciendo calidad y resistencia. Los estantes son flexibles con altura regulable y cómodos anaqueles desmontables en la puerta.Los estantes se pueden mover en altura para optimizar el espacio, en función de las necesidades de guardado.", 16, "https://arcencohogar.vtexassets.com/arquivos/ids/369370-1200-1200?v=638392835522730000&width=1200&height=1200&aspect=true", "Heladera Eslabon de Lujo Erd29Ab 273 L", 530000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Category",
                table: "Product",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Product",
                table: "SaleProduct",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Sale",
                table: "SaleProduct",
                column: "Sale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
