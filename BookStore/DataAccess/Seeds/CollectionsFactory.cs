using BookStore.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public static class CollectionsFactory
    {
        public static IEnumerable<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "39fed276-428e-443b-87bc-2189a12d0a48",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "df15def4-d452-4e9c-906e-8d135705cf75"
                },
                new IdentityRole
                {
                    Id = "84536517-dbb4-46b1-96a0-0b4225090c5",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "39640d3c-b761-4bf0-93a2-fa69fd8235bb"
                }
            };
        }

        public static IEnumerable<IdentityUser> GetUsers()
        {
            return new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = "4fbbea10-a2e2-4849-bc5a-c2b3cbec400a",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEON7jD3hW2E6XqqX2QsqnWWtGcIaUw+7z9cMfmVFwzcZwUO4ybLncUMXkTip6bDr+w==", //Password7
                    SecurityStamp = "5OIXSUF7WQ3OJQDKL6EDICRGYPX2V2HP",
                    ConcurrencyStamp = "d9d4cf88-76f6-4f1b-957e-12399f67458a",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new IdentityUser
                {
                    Id = "cce03b17-69d6-4af2-bb36-f730d1a04ba3",
                    UserName = "User",
                    NormalizedUserName = "USER",
                    Email = "user@mail.com",
                    NormalizedEmail = "USER@MAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEON7jD3hW2E6XqqX2QsqnWWtGcIaUw+7z9cMfmVFwzcZwUO4ybLncUMXkTip6bDr+w==", //Password7
                    SecurityStamp = "CE3LFFFIVOUMY4QVEVKSVVSC57WCIPUF",
                    ConcurrencyStamp = "145a115e-413f-4477-882b-6649f7f99ab6",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            };
        }

        public static IEnumerable<IdentityUserRole<string>> GetUserRole()
        {
            return new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = "4fbbea10-a2e2-4849-bc5a-c2b3cbec400a",
                    RoleId = "84536517-dbb4-46b1-96a0-0b4225090c5"
                },
                new IdentityUserRole<string>
                {
                    UserId = "cce03b17-69d6-4af2-bb36-f730d1a04ba3",
                    RoleId = "39fed276-428e-443b-87bc-2189a12d0a48"
                }
            };
        }

        public static IEnumerable<Author> GetAuthorsCollection()
        {
            return new List<Author>
            {
                new Author
                {
                    Id = new Guid("df48360c-7d42-4056-6d4a-08d6c0d61f8d"),
                    Name = "Александр Шевчук",
                    CreatedAT = DateTime.Now
                },
                new Author
                {
                    Id = new Guid("9cf9b72c-b838-40c7-afc4-aa4eb62f4000"),
                    Name = "Дмитрий Охрименко",
                    CreatedAT = DateTime.Now
                },
                new Author
                {
                    Id = new Guid("a4bbafda-e5b8-4c19-a5e4-907e84fc0715"),
                    Name = "Андрей Касьянов",
                    CreatedAT = DateTime.Now
                },
                new Author
                {
                    Id = new Guid("ebfcd428-f104-4db0-848f-05f61e9b7c49"),
                    Name = "Джеффри Рихтер",
                    CreatedAT = DateTime.Now
                },
            };
        }

        public static IEnumerable<Book> GetBooksCollection()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                    Title = "Design Patterns via C#",
                    Description = "Description",
                    PublishedOn = 2013,
                    Publisher = "ITVDN",
                    OrgPrice = 100,
                    ActualPrice = 100,
                    PromotionalText = "PromotionalText",
                    ImageUrl = "http://padabum.com/pics/175590.jpg",
                    SoftDeleted = false,
                    CreatedAT = DateTime.Now
                },
                new Book
                {
                    Id = new Guid("c4b6769f-2d3e-427f-a65d-5dc4a0f28cdf"),
                    Title = "CLR via C#",
                    Description = "Description",
                    PublishedOn = 2017,
                    Publisher = "Питер",
                    OrgPrice = 948,
                    ActualPrice = 777,
                    PromotionalText = "PromotionalText",
                    ImageUrl = "https://rozetka.com.ua/40100992/p40100992/",
                    SoftDeleted = false,
                    CreatedAT = DateTime.Now
                }
            };
        }

        public static IEnumerable<BookAuthor> GetBookAuthorsCollection()
        {
            return new List<BookAuthor>
            {
                new BookAuthor
                {
                    BookId = new Guid("c4b6769f-2d3e-427f-a65d-5dc4a0f28cdf"),
                    AuthorId = new Guid("ebfcd428-f104-4db0-848f-05f61e9b7c49"),
                    Order = 0
                },
                new BookAuthor
                {
                    BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                    AuthorId = new Guid("df48360c-7d42-4056-6d4a-08d6c0d61f8d"),
                    Order = 0
                },
                new BookAuthor
                {
                    BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                    AuthorId = new Guid("9cf9b72c-b838-40c7-afc4-aa4eb62f4000"),
                    Order = 1
                },
                new BookAuthor
                {
                    BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                    AuthorId = new Guid("a4bbafda-e5b8-4c19-a5e4-907e84fc0715"),
                    Order = 2
                },
            };
        }

        public static IEnumerable<Order> GetOrdersCollection()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = new Guid("cc32fdc5-1847-423d-85a3-3b7a8b6d19b0"),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(3),
                    CustomerName = "First Customer",
                    CreatedAT = DateTime.UtcNow
                },
                new Order
                {
                    Id = new Guid("2b14d5d3-4755-4d3b-993e-bbb1972c5d2e"),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(3),
                    CustomerName = "Second Customer",
                    CreatedAT = DateTime.UtcNow
                }
                
            };
        }

        public static IEnumerable<LineItem> GetLineItemsCollection()
        {
            return new List<LineItem>
            {
                new LineItem
                {
                    Id = new Guid("f5ea848c-7167-428e-9ba8-789cc22a4a6b"),
                    LineNum = "123456789",
                    NumBooks = 1,
                    BookPrice = 777,
                    BookId = new Guid("c4b6769f-2d3e-427f-a65d-5dc4a0f28cdf"),
                    OrderId = new Guid("2b14d5d3-4755-4d3b-993e-bbb1972c5d2e")
                },
                new LineItem
                {
                    Id = new Guid("78552fe9-dd69-4345-a681-d733737545b1"),
                    LineNum = "123456787",
                    NumBooks = 2,
                    BookPrice = 777,
                    BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                    OrderId = new Guid("cc32fdc5-1847-423d-85a3-3b7a8b6d19b0")
                },
                new LineItem
                {
                    Id = new Guid("2a4783fc-3f6a-4486-86ef-5a4f2a092d0b"),
                    LineNum = "123456797",
                    NumBooks = 1,
                    BookPrice = 777,
                    BookId = new Guid("c4b6769f-2d3e-427f-a65d-5dc4a0f28cdf"),
                    OrderId = new Guid("cc32fdc5-1847-423d-85a3-3b7a8b6d19b0")
                }
            };
        }

        public static IEnumerable<Review> GetReviewsCollection()
        {
            return new List<Review>
            {
                new Review
                {
                    Id = new Guid("14868caf-9434-49e8-8f22-8d71683d55f9"),
                    VoterName = "First Voter",
                    NumStars = 7,
                    Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                    CreatedAT = DateTime.Now
                },
                new Review
                {
                    Id = new Guid("2935d6bd-ace2-44a5-938e-3cbc2731b53e"),
                    VoterName = "Second Voter",
                    NumStars = 9,
                    Comment = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    BookId = new Guid("3f27f7e7-fe64-4add-bb48-6ed5377b618f"),
                    CreatedAT = DateTime.Now
                },
                new Review
                {
                    Id = new Guid("836be1f1-663d-422e-af2e-ca8fc2f41940"),
                    VoterName = "Second Voter",
                    NumStars = 9,
                    Comment = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    BookId = new Guid("c4b6769f-2d3e-427f-a65d-5dc4a0f28cdf"),
                    CreatedAT = DateTime.Now
                }
            };
        }

    }
}
