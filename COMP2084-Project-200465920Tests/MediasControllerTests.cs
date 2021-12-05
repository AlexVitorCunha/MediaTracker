using COMP2084_Project_200465920.Controllers;
using COMP2084_Project_200465920.Data;
using COMP2084_Project_200465920.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace COMP2084_Project_200465920Tests
{
    [TestClass]
    public class MediasControllerTests
    {
        private ApplicationDbContext _context;
        MediasController controller;


        // runs automatically before each test
        [TestInitialize]
        public void TestInitialize()
        {
            // creat in-memory db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // populate db w/mock data
            _context.Genres.Add(new Genre
            {
                GenreId = 20,
                Name = "Horror"
            });

            _context.Genres.Add(new Genre
            {
                GenreId = 55,
                Name = "Comedy"
            });
            _context.Genres.Add(new Genre
            {
                GenreId = 33,
                Name = "Drama"
            });
            _context.SaveChanges();

            //instantiate contoller w/db dependency
            controller = new MediasController(_context);
        }

        #region Create(GET)
        // Create (GET)
        [TestMethod]
        public void CreateLoadsCreateView()
        {
            
            // act – execute the method
            var result = (ViewResult)controller.Create();

            // assert – evaluate if we got the result we expected
            Assert.AreEqual("Create", result.ViewName);
            
        }

        [TestMethod]
        public void CreateLoadsGenres()
        {

            // act
            var result = (ViewResult)controller.Create();
            var viewData = result.ViewData;

            // assert
            Assert.IsNotNull(viewData["GenreId"]);
        }
        #endregion
    }

}
