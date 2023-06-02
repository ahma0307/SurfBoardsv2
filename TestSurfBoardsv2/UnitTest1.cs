using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SurfBoardsv2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using SurfBoardsv2.Data;
using SurfBoardsv2.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace TestSurfBoardsv2
{

    public class BoardControllerTest
    {
        [Fact]
        public async Task Create_WithValidBoard_SavesToDatabase()
        {
            // Options for InMemory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_WithValidBoard_SavesToDatabase")
                .Options;

            var mockEnvironment = new Mock<IWebHostEnvironment>();

            var board = new Board
            {
                Name = "TestBoard",
                Length = 5.5f,
                Width = 2.5f,
                Thickness = 0.5f,
                Volume = 27.5f,
                Type = "Longboard",
                Price = 299.99M,
                Equipment = "Wax",
                IsAvailable = true
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new BoardsController(context, mockEnvironment.Object, null);
                var result = await controller.Create(board);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.True(context.Boards.Any());
                Assert.Equal(1, context.Boards.Count());
                Assert.Equal(board.Name, context.Boards.Single().Name);
            }
        }
    }

}
