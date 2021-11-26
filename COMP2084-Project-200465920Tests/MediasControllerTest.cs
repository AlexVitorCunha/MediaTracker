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
    public class MediasControllerTest
    {
        private ApplicationDbContext _context;
        MediasController controller;
        List<Genre> genres = new List<Genre>();


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
            genres.Add(new Genre
            {
                GenreId = 20,
                Name = "Horror"
            });

            genres.Add(new Genre
            {
                GenreId = 55,
                Name = "Comedy"
            });

            //instantiate contoller w/db dependency
            controller = new MediasController(_context);
        }


        [TestMethod]
        public void CreateLoadsCreateView()
        {
            // arrange – done in TestInitialize instead

            // act – execute the method
            var result = (ViewResult)controller.Create();

            // assert – evaluate if we got the result we expected
            Assert.AreEqual("Create", result.ViewName);
            
        }

        [TestMethod]
        public void CreateLoadsGenres()
        {


            //Assert.AreEqual("something", result.ViewData["GenreId"]);
        }
        }
}
