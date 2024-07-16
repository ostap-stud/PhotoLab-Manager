using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PhotoLab.Data;
using PhotoLab.Models;
using static NuGet.Packaging.PackagingConstants;

namespace PhotoLab.Areas.Identity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PhotoLabContext context)
        {
            if (context.Orders.Any())
            {
                return;
            }

            var hasher = new PasswordHasher<PhotoLabUser>();
            var users = new List<PhotoLabUser>() 
            {
                new PhotoLabUser()
                {
                    Id="c36efb17-1b3d-47fc-8e0d-bd9f2c9c5495",
                    Name="Остап",
                    Surname="Гриб",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    NormalizedUserName = "admin@gmail.com".ToUpper(),
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Admin-1234"),
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var services = new List<Service>()
            {
                new Service
                {
                    ServiceName="Оцифрування",
                    Description="Цифрове сканування та проявлення фотоплівки 35мм",
                    Price=85.0M,
                    ImageURI="https://odessa-life.od.ua/wp-content/uploads/2023/03/proyavlennaya-plenka-1280x720.jpg"
                },
                new Service
                {
                    ServiceName="Базова редакція фото",
                    Description="Накладання ефектів, рамок, фільтрів.\nПоліпшення якості.\nКорекція яскравості/контрасту/гамми.\nЗмінення фону",
                    Price=90.0M,
                    ImageURI="https://gdm-catalog-fmapi-prod.imgix.net/ProductScreenshot/85ffb34f-3c0b-4ffa-ba00-d8270bc67706.png"
                },
                new Service
                {
                    ServiceName="Реставрація старих фото",
                    Description="Відновлення старих пошкоджених фотографій.",
                    Price=95.5M,
                    ImageURI="https://imgv3.fotor.com/images/side/The-before-and-after-result-of-removing-scratches-on-old-damaged-photo-using-Fotors-AI-photo-restoration-tool.jpg"
                },
                new Service
                {
                    ServiceName="Колір",
                    Description="Кольоровий принт форматів А1-6",
                    Price=35.5M,
                    ImageURI="https://www.planetware.com/wpimages/2020/02/france-in-pictures-beautiful-places-to-photograph-eiffel-tower.jpg"
                },
                new Service
                {
                    ServiceName="Картина",
                    Description="Картина клієнтського зразку з дерев'яним обрамленням",
                    Price=55.5M,
                    ImageURI="https://salon-baget.ua/wp-content/uploads/2018/10/starinnaya-kartina-v-reznoy-derevyannoy-rame.jpg"
                },
                new Service
                {
                    ServiceName="Ч/Б",
                    Description="Чорно-білий принт форматів А1-6",
                    Price=30.5M,
                    ImageURI="https://i.etsystatic.com/38075410/r/il/18819c/5308777728/il_fullxfull.5308777728_3psy.jpg"
                },
                new Service
                {
                    ServiceName="Стікери",
                    Description="А4 сторінка стікерів для будь-якої поверхні",
                    Price=25.5M,
                    ImageURI="https://i5.walmartimages.com/asr/db769599-25d0-4d46-8d36-65b23cb9dfb1.4332e8e142a11423903b4a0bf95987d9.jpeg?odnHeight=612&odnWidth=612&odnBg=FFFFFF"
                },
            };

            context.Services.AddRange(services);
            context.SaveChanges();

            var orders = new List<Order>()
            {
                new Order
                {
                    Client_Name="Тимур",
                    Client_Surname="Козуб",
                    Status=Status.Processing,
                    Date=new DateTime(2023, 12, 04, 23, 42, 0),
                    Worker=users.FirstOrDefault().Email,
                    ServiceId=services.FirstOrDefault(s => s.ServiceName.Equals("Оцифрування"), services[0]).ServiceId,
                    Count=2,
                },
                new Order
                {
                    Client_Name="Ігор",
                    Client_Surname="Гнатевич",
                    Status=Status.Success,
                    Date=new DateTime(2023, 12, 11, 14, 23, 0),
                    Worker=users.FirstOrDefault().Email,
                    ServiceId=services.FirstOrDefault(s => s.ServiceName.Equals("Картина"), services[0]).ServiceId,
                    Count=1,
                },
                new Order
                {
                    Client_Name="Яна",
                    Client_Surname="Худченко",
                    Status=Status.Processing,
                    Date=new DateTime(2023, 12, 12, 16, 12, 0),
                    Worker=users.FirstOrDefault().Email,
                    ServiceId=services.FirstOrDefault(s => s.ServiceName.Equals("Стікери"), services[0]).ServiceId,
                    Count=3,
                },
                new Order
                {
                    Client_Name="Дарина",
                    Client_Surname="Бенеш",
                    Status=Status.Cancelled,
                    Date=new DateTime(2023, 12, 06, 18, 20, 0),
                    Worker=users.FirstOrDefault().Email,
                    ServiceId=services.FirstOrDefault(s => s.ServiceName.Equals("Ч/Б"), services[0]).ServiceId,
                    Count=5,
                },
                new Order
                {
                    Client_Name="Юрій",
                    Client_Surname="Наріжний",
                    Status=Status.Success,
                    Date=new DateTime(2023, 12, 07, 12, 30, 0),
                    Worker=users.FirstOrDefault().Email,
                    ServiceId=services.FirstOrDefault(s => s.ServiceName.Equals("Базова редакція фото"), services[0]).ServiceId,
                    Count=2,
                },
                new Order
                {
                    Client_Name="Христина",
                    Client_Surname="Карпінська",
                    Status=Status.Success,
                    Date=new DateTime(2023, 12, 08, 14, 30, 0),
                    Worker=users.FirstOrDefault().Email,
                    ServiceId=services.FirstOrDefault(s => s.ServiceName.Equals("Реставрація старих фото"), services[0]).ServiceId,
                    Count=3,
                },
            };

            orders.ForEach(o =>
            {
                o.Service = services[o.ServiceId - 1];
                o.Total = o.Service.Price * o.Count;
            });

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
