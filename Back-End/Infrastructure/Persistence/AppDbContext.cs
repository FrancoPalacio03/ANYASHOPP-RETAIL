using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }

        public AppDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.ProductId);
                entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description);
                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(p => p.Category).IsRequired();
                entity.Property(p => p.Discount);
                entity.Property(p => p.ImageUrl).IsRequired();

                entity.HasOne<Category>(ca => ca.category)
                .WithMany(ad => ad.products)
                .HasForeignKey(c => c.Category);
            }
            );

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.CategoryId).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).HasMaxLength(100);

                entity.HasMany(c => c.products)
                .WithOne(p => p.category);
            }
            );

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");
                entity.HasKey(s => s.SaleId);
                entity.Property(s => s.SaleId).ValueGeneratedOnAdd();
                entity.Property(s => s.TotalPay).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.Subtotal).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.TotalDiscount).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.Taxes).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.Date).IsRequired();

                entity.HasMany(s => s.SaleProducts)
                .WithOne(sp => sp.sale);
            }
            );

            modelBuilder.Entity<SaleProduct>(entity =>
            {
                entity.ToTable("SaleProduct");
                entity.HasKey(e => e.ShoppingCartId);
                entity.Property(t => t.ShoppingCartId).ValueGeneratedOnAdd();
                entity.Property(sp => sp.Sale).IsRequired();
                entity.Property(sp => sp.Product).IsRequired();
                entity.Property(sp => sp.Quantity).IsRequired();
                entity.Property(sp => sp.Price).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(sp => sp.Discount);


                entity
                    .HasOne<Sale>(sp => sp.sale)
                    .WithMany(s => s.SaleProducts)
                    .HasForeignKey(sp => sp.Sale);

                entity
                    .HasOne<Product>(sp => sp.product)
                    .WithMany(s => s.SaleProducts)
                    .HasForeignKey(sp => sp.Product);
            }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electrodomésticos" },
                new Category { CategoryId = 2, Name = "Tecnología y Electrónica" },
                new Category { CategoryId = 3, Name = "Moda y Accesorios" },
                new Category { CategoryId = 4, Name = "Hogar y Decoración" },
                new Category { CategoryId = 5, Name = "Salud y Belleza" },
                new Category { CategoryId = 6, Name = "Deportes y Ocio" },
                new Category { CategoryId = 7, Name = "Juguetes y Juegos" },
                new Category { CategoryId = 8, Name = "Alimentos y Bebidas" },
                new Category { CategoryId = 9, Name = "Libros y Material Educativo" },
                new Category { CategoryId = 10, Name = "Jardinería y Bricolaje" }

                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Aire Acondicionado Nex On Off 2752fn",
                    Description = "Climatizar tus espacios a lo largo del año es sin duda algo importante para tu comodidad y la de tus seres queridos. Contar con un aire acondicionado con función frío/calor es la mejor decisión. Con este aire Nex conseguí una mejor relación costo-beneficio.",
                    Price = 850000m,
                    Category = 1,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/365635-1200-1200?v=638350437287400000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Ventilador Industrial 18 Metal Pioneer",
                    Description = "El ventilador PIONEER es mas econmico ya que ahorra un 50% de energia electrica\r\nEs practico, facil de trasladar y adaptabilidad.\r\nSus aspas Metalicas lo hacen un producto de gran durabilidad.\r\nsu base de apoyo firme y su rejilla protectora metalica lo hacen un producto seguro",
                    Price = 54996m,
                    Category = 1,
                    Discount = 12,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/371560-1200-1200?v=638422437614700000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Heladera Eslabon de Lujo Erd29Ab 273 L",
                    Description = "Esta heladera cuenta con vidrio templado en todos los estantes, ofreciendo calidad y resistencia. Los estantes son flexibles con altura regulable y cómodos anaqueles desmontables en la puerta.Los estantes se pueden mover en altura para optimizar el espacio, en función de las necesidades de guardado.",
                    Price = 530000m,
                    Category = 1,
                    Discount = 16,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/369370-1200-1200?v=638392835522730000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Lavarropas Drean Eco Next 6Kg Blanco",
                    Description = "Marca lider del mercado - 6 KG - 800 RPM - Eficiencia energetica A+. Color blanco.",
                    Price = 54996m,
                    Category = 1,
                    Discount = 5,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/372022-1200-1200?v=638428189685100000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Smartphone Samsung Galaxy S22 Ultra",
                    Description = "Potente smartphone con cámara de 108 MP, pantalla Dynamic AMOLED 2X de 6.8 pulgadas, 12 GB de RAM y 256 GB de almacenamiento.",
                    Price = 1499999m,
                    Category = 2,
                    Discount = 8,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTJfPPESvNHvqeb6FpNHdloDc-rMakxCHqu8fExoC6pMNepveNoUEgbeoX1S5fy8wFOZ82lWK6WeNrsGpS1KPXOTbh3mZ1qsyCuK5Qrq27HSFcUdT3Ugu30&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Laptop ASUS ROG Zephyrus G14",
                    Description = "Portátil gaming ultraligero con procesador AMD Ryzen 9 5900HS, pantalla QHD de 14 pulgadas, 16 GB de RAM y SSD de 1 TB.",
                    Price = 1899999m,
                    Category = 2,
                    Discount = 10,
                    ImageUrl = "https://acdn.mitiendanube.com/stores/001/907/418/products/1-0db142b8c6c121127217116552781441-1024-1024.webp"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Consola PlayStation 5",
                    Description = "Consola de última generación con gráficos en 4K, SSD ultra rápido, control DualSense y compatibilidad con juegos de PS4.",
                    Price = 699999m,
                    Category = 2,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcS_zCNBX8XjIOhO1odOQXnZKgvSBsL8x3m9uxc0tusdkgw3dsS0krgtsPjkSB00LBSxPZu1dcauTWc4ZO590-OMQJnNPBiC-ov39wJp7Yz5ixT7Eu0HoXV6&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Monitor Curvo Samsung Odyssey G9",
                    Description = "Monitor gaming curvo de 49 pulgadas, resolución Dual QHD (5120x1440), tasa de refresco de 240 Hz y tecnología Quantum Dot.",
                    Price = 2599999m,
                    Category = 2,
                    Discount = 15,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcQFRRNd74eCI_FcSpOQ33Q0B2NjYsEfCbj92xXSxooati04Di_eJnhR9-HOdKqvfkCkTeMBiRxSVQjKhXnxQ_qXGUtr3hJfNWvgctX7KqVKYc8z2XvzvJwzFg&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Vestido de Noche Elegante",
                    Description = "Vestido largo de noche con encaje y detalles bordados, ideal para eventos formales.",
                    Price = 89999m,
                    Category = 3,
                    Discount = 20,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRyd53rEy27sYraa0s7ieQuUdokjcWii6xsaDy6ZxIimYVBEVhtJqS4f8ND__YYk04NfprorgGZ4b54HHSHVVHmDY8DeFmg9kfQZFzZLIVg5F_18Jy4EVl2&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Bolso de Cuero Genuino",
                    Description = "Bolso de mano elegante fabricado en cuero genuino con compartimentos internos y correa ajustable.",
                    Price = 149999m,
                    Category = 3,
                    Discount = 10,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRzG6a3hup9Hr0Dc0l8AIlWdqv9Me8SAYq8JGlLznBm8Sq2FYzlffiN5A2aL71zhkItqqj5RV8MNlSFZqgmrnI_WsFfsNbhUjOLkPgWZSD8ExRxl5mf1ZxO&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Reloj de Pulsera Fossil Gen 6",
                    Description = "Reloj inteligente con pantalla táctil AMOLED, monitorización de salud y seguimiento de actividad física.",
                    Price = 349999m,
                    Category = 3,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSAinXkzzhkGSJUZEq6RU6rU9wiHOj5cwT-BemDcqUHkZK9V07SPOgWp4OGqeICP2bYZopvqt8DUZ6qwZWQm663NMT64UpwF2k_QaVQnh-UPa49XJPNr2kIfQ&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Zapatos de Vestir Clásicos",
                    Description = "Zapatos de cuero clásicos ideales para uso diario y ocasiones formales, diseño cómodo y duradero.",
                    Price = 119999m,
                    Category = 3,
                    Discount = 15,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcTS3foCp5SlYK36hyjerTtBdw4pvT2mOz06laSuSiq6-h7hKH3ke7KtVbSnE3W9koQYufrG1jNprhO7Mrcx2K3mRy5VGXqOPDGaf92qTnEatdbI9wDbOZoc&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Set de Muebles de Sala Modulares",
                    Description = "Set de muebles modulares para sala de estar, incluye sofá, mesa de centro y estantes.",
                    Price = 249999m,
                    Category = 4,
                    Discount = 10,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcQk9uD1VT7W-Ve4ecjwoY5qUJRM2Sgyozg-0SYWtm43CUWI58PLf9U-ADKm_DHielQ9zqr6PFsMazIOFyQrsAXtZ0ZBNd8E9L68W8jhpFk-&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Lámpara de Techo LED Moderna",
                    Description = "Lámpara de techo LED con diseño moderno, iluminación ajustable y control remoto.",
                    Price = 89999m,
                    Category = 4,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRBtGaNJn94HnEqFuXWDPw0U1OCb4-CAmV4U38x0922x3iQv8g_yzFNfZefJehExLk9G6zlTlYuTH4k37l0uvHxm5-UwytOQPJdMILIBixuWIhThQnwomtA&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Juego de Sábanas de Algodón Egipcio",
                    Description = "Juego de sábanas de 600 hilos de algodón egipcio, suaves y resistentes, incluye sábana plana, ajustable y fundas.",
                    Price = 119999m,
                    Category = 4,
                    Discount = 8,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTLnuaC_mjKtepU9CwRsJIkMUQD6QKuMNOqg82zT7TUT08nmAuAS7qb0yQsZSUbjFIOMxyo1RkeQmkvZMPlZLpc236mWitN&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Espejo Decorativo con Marco de Madera",
                    Description = "Espejo decorativo con marco de madera tallada a mano, ideal para espacios de estilo clásico y elegante.",
                    Price = 69999m,
                    Category = 4,
                    Discount = 12,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcT82RXmIwA_9cjxcRJgmG2QyLd4sotsAol1BrhdfR_aDza9fYnBZUZ57NN8OGq29GmH8uY8H3J0zyK30ZvFuZ5k16ICbuaWxgAsYndmtUOzmUej7Wu4cmxi3w&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Kit de Maquillaje Profesional",
                    Description = "Kit completo de maquillaje profesional con paletas de sombras, labiales, bases y brochas de alta calidad.",
                    Price = 149999m,
                    Category = 5,
                    Discount = 15,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcQQaGJjKviM7Ix4i9wmFiBptdH6mDaiHWVuXGAezYydWvNKnGYN8-jfhx5EyZpIWH-aZSwfpgZNk4g7d_Ijr5KkM6N4eKUmAd05lC7xZ8Y&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Cepillo Eléctrico Dental Sonicare",
                    Description = "Cepillo eléctrico dental con tecnología sónica para una limpieza profunda y efectiva, incluye varios modos de cepillado.",
                    Price = 79999m,
                    Category = 5,
                    Discount = 10,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcQZsrFjq8-ZGuWFMljBqUwFTaWUeqqnWOffmtXr5gLFpKDAPiAkKrj67wUjkpLrVgCwiVH3l9ElvRSk-lO-4_fuZovhkiMGkTrAVWjG9H0v3YD1cgK_QsQAa3Q&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Set de Perfumes Variados",
                    Description = "Set de perfumes variados para hombre y mujer, incluye fragancias de marcas reconocidas.",
                    Price = 299999m,
                    Category = 5,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSUilTJbRGExoWS-vXrgwTVPHZK_gTtyTX6Nu9kc7RG7uXiY_iOEGoOxxnqOK2rbSweoMvR9uK2PL9MY73U9Y1ouAMlUtRWO7oXwOZXkjdUahe33JytR-3E1wdI&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Mascarilla Facial de Colágeno",
                    Description = "Mascarilla facial de colágeno para hidratar y revitalizar la piel, ideal para todo tipo de piel.",
                    Price = 19999m,
                    Category = 5,
                    Discount = 20,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTuwBJcj7gHfHd0NAb38Cny1Stq-RUSI8cdt16G0s96iwbyzvSAuAsIpXTR9BeVIvKEfy6BYlmRW33_kYIkL-Ol2npovN6rV3ot5iMWinkijy9_YqhMjkh-Ug&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Bicicleta de Montaña Trek",
                    Description = "Bicicleta de montaña ligera y resistente, cuadro de aluminio, frenos de disco hidráulicos y cambios Shimano.",
                    Price = 699999m,
                    Category = 6,
                    Discount = 10,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcR5FzXwYigKlk2I3thb1_YtDN-9CIhxsoYxTm4Y0jpNJoBDaTWt48GgoS_u4yYL963-SOseMqKcSpvQO8sJL1O2OabXzoz8q71kDalp3ktvaoyucBYwG8c4&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Pelota de Fútbol Profesional Adidas",
                    Description = "Pelota de fútbol profesional con diseño aerodinámico, ideal para partidos y entrenamientos intensivos.",
                    Price = 79999m,
                    Category = 6,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcRC_Yj3UbmHWb1cw6KbXosRPDLOehaY8q3WCvMFLovsEqjFaKgIWO0kNt2BwbKf4PbtSyijiOu7IhYUJO4Y6mrqqxhfKr4gD6Mi9Fai0qZfEwVaCoZBB_Q4&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Raqueta de Tenis Wilson Pro Staff",
                    Description = "Raqueta de tenis de alta gama, diseño ligero y ergonómico, ideal para jugadores avanzados.",
                    Price = 249999m,
                    Category = 6,
                    Discount = 8,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTzrAH75B_-BkzkxMZF3nrQjqqmH0RV2v_Q-99UYUu-hgy1UIIai2elImG_5jw04HmlR-yDhA0va5Oh7KU8-yijhuQ5oGgujn7TElIRNuBCoYvs9muu9Nvq&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Patineta Eléctrica Hoverboard",
                    Description = "Patineta eléctrica con tecnología de autoequilibrio, velocidad ajustable y autonomía de hasta 15 km.",
                    Price = 349999m,
                    Category = 6,
                    Discount = 12,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcSRZ0eRUcN2jqBkAxDNUXnfjvRPtxuiog3wuBRm-DxiAXC-SIPyDsSXSJIsTV7naj4xNAzLgLNA0ZsuVfyLMzovatdRZaaLiDDEu0LignXGsMa0RXE_bgkE&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Set de Construcción LEGO Technic",
                    Description = "Set de construcción LEGO Technic con más de 1500 piezas, incluye instrucciones para construir varios modelos de vehículos.",
                    Price = 79999m,
                    Category = 7,
                    Discount = 10,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcScoyPHobmhmIp5mjYSgG84PDoCsJsx2vulaBH4YF9wiBux5Fl4uu2UKIiyssyahUfvtB_hwZydoo0GNEiiDs4RSu8ITS7GMi9XI4ZH-bKqjlXI9nsbblCe&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Muñeca Barbie Dreamhouse",
                    Description = "Muñeca Barbie con casa de ensueño, incluye accesorios como muebles y ropa para la muñeca.",
                    Price = 149999m,
                    Category = 7,
                    Discount = 8,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSBnw46A4ymOzK5aVghSFeea4r1dW1jHf1QOAjtBOQ6DeePlJbuFP0lRkgsPbVgGn_mWscgaE8xhcHo0emExkaARt0XiezNeFAI3KXQPEXU0gIkezDygnc1&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Juego de Mesa Monopoly Edición Especial",
                    Description = "Juego de mesa Monopoly edición especial con temática de ciudades famosas del mundo, ideal para toda la familia.",
                    Price = 89999m,
                    Category = 7,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcSlRADA0enIzx3OKJuRYmzciBB5LyMbhRV-QToOhdMEeP5vu3b927IL9EtbBvm7j1NV1-jaYyPxbcbAclB3NuoaEYlTUFjxAXJXrIJRWip0q7HfK2RYgbnf&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Pista de Carreras Hot Wheels",
                    Description = "Pista de carreras Hot Wheels con looping y lanzador de coches, emocionantes carreras para niños y niñas.",
                    Price = 59999m,
                    Category = 7,
                    Discount = 12,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTkDdm8ofVxeHEZg1GdFnwqJNkd0k7kQhnUIiW0MVUraZBBWVEv195ttrZMUi9J7STpj73wJKz20TX8TtFDkdVzAXsFNXR-WU1UhHvlf1wb716l776DnQwu&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Caja de Vinos Varietales",
                    Description = "Caja de vinos varietales seleccionados, incluye diferentes cepas y marcas reconocidas.",
                    Price = 99999m,
                    Category = 8,
                    Discount = 15,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcRaULa9aL3suKEjZHcfF15L4YT38sLJuGRf692HUZLwpPPB9CT73Jb3IsJ0CDSxfhg1er4HcYIEbxR_hW09Ag-d2m6xQJmvuieGugdT_4n49RkmnXxfHQum&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Caja de Chocolates Artesanales",
                    Description = "Caja de chocolates artesanales variados, perfectos para regalo o disfrute personal.",
                    Price = 49999m,
                    Category = 8,
                    Discount = 10,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcSkLOlVkRm_7EkAxE19fIIEkxbjUFZ8cp-gsAGGJWr2YI90Sw4tkTOyOoYomThiSjZsJ9k2YtMjIVaAm_0rvt26Sa8UayxUy7KpCqnur0U&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Pack de Cervezas Artesanales",
                    Description = "Pack de cervezas artesanales de diferentes estilos y sabores, ideal para aficionados a la cerveza.",
                    Price = 69999m,
                    Category = 8,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRxThpx-03Ko-i1ZLbjSo_c5_hoA0E7qlbELoA4jCTb0D3LPHOfWXwQTUzhF4Vm4wFPDpBpBMGVn7kxCZbq8EIcJQn_1GEGekKWA9Pv6Gc&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Caja de Tés Gourmet",
                    Description = "Caja de tés gourmet con variedad de infusiones y sabores exóticos, ideal para momentos de relax.",
                    Price = 34999m,
                    Category = 8,
                    Discount = 12,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTsGbPeustN-h1tglmXjbyxJwo9atcnhZ1oWk98kX_BDv79l2bGJZxxJ1aw3MFYmgIyjpENsWmVzraIPF7OPkzCDyf1BSjvZSCw_RBbIZ9ubggodOdVzsP-pw&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Colección Completa Harry Potter",
                    Description = "Colección completa de libros de Harry Potter, incluye todos los libros de la saga escrita por J.K. Rowling.",
                    Price = 199999m,
                    Category = 9,
                    Discount = 20,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcQY6AhaXhwNP2Doj3pV4bKW87gMCegyTI5efNROST6WodZa1Z0MMlQiNuk_N4T5BpK6-teJs8asTb9rdRzB1TjsGbQMDV7W1hWjbZ0x1UP2HUBK50YhLl8f&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Enciclopedia Ilustrada de Ciencias",
                    Description = "Enciclopedia ilustrada de ciencias con información detallada y imágenes a todo color, ideal para estudiantes y curiosos.",
                    Price = 89999m,
                    Category = 9,
                    Discount = 8,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTJb12if7DCZK2_ebm14dtyGUKkf7XxgmQljk160OEi_a_L-XP_LBg-h9sMLaAh51UjKM8vSGUFkfZ76AjmVsfi8ZdqOrti6NzQFLy3EWw&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Guía Completo de Fotografía",
                    Description = "Guía completo de fotografía en formato libro, desde principiantes hasta técnicas avanzadas de composición y edición.",
                    Price = 64999m,
                    Category = 9,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcSSFkQuWBxhZKlMqHJd2t1tONI6j-eExxQ4BS00JEsUGypVByDutUw42mL6e8wcMH-6xubvLu4vI6uleIzu5kK5V76bqa92t7pTKkqWfFMvKp3gnWTxstNIVw&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Diccionario de Lengua Española",
                    Description = "Diccionario completo de la lengua española con definiciones actualizadas y ejemplos de uso.",
                    Price = 29999m,
                    Category = 9,
                    Discount = 12,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcRy-bX6YBY0IMEiE-qTv562WsRfROuEcVlHOdNg4Ijxw14Frvz_a__4Y6xeQera_xxT7_A3Z-4LLJHXD9iL2nTSbFARyhabDFmpGFVYKdBuCrC350TrTsgC&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Set de Herramientas de Jardinería",
                    Description = "Set completo de herramientas de jardinería, incluye palas, rastrillos, tijeras de podar y guantes protectores.",
                    Price = 79999m,
                    Category = 10,
                    Discount = 10,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcQin4JRIPkEPsIwbCMEN8ncsMlFDWirEpHd2db_NcPhHBNfeWlIBD8Ocf2ZXMt5fQlrgjFWeGLbXpIXCFlvLOSzUbPGpGs8JyjFTD7qRjGFk4FysdZBKAYR&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Mesa de Trabajo Plegable",
                    Description = "Mesa de trabajo plegable para bricolaje, superficie resistente y ajustable, fácil de transportar y almacenar.",
                    Price = 49999m,
                    Category = 10,
                    Discount = 8,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcTwCwcb9QWfOGKeSChOhBYEMYsq2gh8C4lOH511rM0FQXaV6Y1X3PehT9S-PqrVPE09I-IPMZNgKhmDsuCbvf4rurGY6s9cPWE07_Ing6TvLMg8EDdTk2f4&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Set X10 de Iluminación Solar para Jardín",
                    Description = "Kit de iluminación solar para jardín, incluye lámparas LED, paneles solares y baterías recargables.",
                    Price = 129999m,
                    Category = 10,
                    Discount = 5,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTPkMHrIbwKIyl_E5iB30B22OqVvkp5u39OCmp5YTCkqZJJXnLUekLgesCcYz8VfS0gkLc4FXSxSfbvNe9UAuv79OZ_ud2KFEH8Wt6Xlp5hraCUTqoPmykDNw&usqp=CAE"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Invernadero Portátil",
                    Description = "Invernadero portátil para cultivo de plantas, estructura resistente y cubierta transparente para maximizar la luz solar.",
                    Price = 89999m,
                    Category = 10,
                    Discount = 12,
                    ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcRdSRPrD53aV_8AogJf0A4fM__7kbenu3frR26-Qy3P8ctE1Np2tUF9jdsqvXiDTtPRgHaraCQgVF9V4dCF6Yp9yprIRQlq-5W4E2B2Ams--yFH9_VDwa1G&usqp=CAE"
                });



        }

    }
}
