using System;
using Data.Entities;
using Data.Options;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;

#nullable disable

namespace Data.Migrations
{
    public partial class CreateAdmin : Migration
    {
       
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.InsertData(
                "Users",
                new string[]
                {
                    "Id",
                    "Email",
                    "Password",
                    "Role",
                    "IsActive",
                    "IsDeleted",
                    "IsEmailConfirmed",
                    "CreateDateTime",
                    "UpdateDateTime",
                    
                },
                new object[]
                {
                    Guid.NewGuid(),
                    "admin@mail.ru",
                   "$2a$10$ntrhHyNh2tckZPClvP9she5i/0H8aKdghEewbb02Kfa3v1CRwACAa",
                    2,
                    true,
                    false,
                    true,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                    
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
