using COMP2084_Project_200465920.Controllers;
using COMP2084_Project_200465920.Data;
using COMP2084_Project_200465920.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

            _context.Medias.Add(new Media
            {
                MediaId = 6,
                Title = "Jumanji",
                MediaType = "Movie",
                Season = 0,
                Episode = 0,
                ReleaseDate = new DateTime(2015, 12, 20, 10, 20, 30),
                GenreId = 20
            });
            _context.Medias.Add(new Media
            {
                MediaId = 7,
                Title = "Despicable me",
                MediaType = "Movie",
                Season = 0,
                Episode = 0,
                ReleaseDate = new DateTime(2019, 1, 15, 10, 20, 30),
                GenreId = 20
            });
            _context.Medias.Add(new Media
            {
                MediaId = 8,
                Title = "Quantum mania",
                MediaType = "Movie",
                Season = 0,
                Episode = 0,
                ReleaseDate = new DateTime(2017, 12, 20, 10, 20, 30),
                GenreId = 20
            });
            _context.Medias.Add(new Media
            {
                MediaId = 9,
                Title = "Disaster movie",
                MediaType = "Movie",
                Season = 0,
                Episode = 0,
                ReleaseDate = new DateTime(2020, 07, 22, 10, 20, 30),
                GenreId = 20
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

        #region Create (POST)

        [TestMethod]
        public void CreateWithInvalidModelLoadsViewData()
        {
            //arrange
            Media media = new Media
            {
                MediaId = 7888,
                Title = "something",
                MediaType = "Movie",
                Season = 0,
                Episode = 0,
                ReleaseDate = new DateTime(2015, 12, 20, 10, 20, 30),
                GenreId = 20
            };
            IFormFile poster = null;
            // act
            controller.ModelState.AddModelError("InvalidKey", "Invalid Value");
            var result = (ViewResult)controller.Create(media, poster).Result;
            var viewData = result.ViewData;

            // assert
            Assert.IsNotNull(viewData["GenreId"]);
        }

        [TestMethod]
        public void CreateWithInvalidModelLoadsView()
        {
            //arrange
            Media media = new Media
            {
                MediaId = 7888,
                Title = "something",
                MediaType = "Movie",
                Season = 0,
                Episode = 0,
                ReleaseDate = new DateTime(2015, 12, 20, 10, 20, 30),
                GenreId = 20
            };
            IFormFile poster = null;
            // act
            controller.ModelState.AddModelError("InvalidKey", "Invalid Value");
            var result = (ViewResult)controller.Create(media, poster).Result;

            // assert
            Assert.AreEqual("Create",result.ViewName);
        }

        [TestMethod]
        public void CreateWithValidMediaLoadsView()
        {
            //arrange
            Media media = new Media
            {
                MediaId = 7888,
                Title = "something",
                MediaType = "Movie",
                Season = 0,
                Episode = 0,
                ReleaseDate = new DateTime(2015, 12, 20, 10, 20, 30),
                GenreId = 20
            };
            IFormFile poster = null;
            //act
            var result = (RedirectToActionResult)controller.Create(media,poster).Result;
            //assert
            Assert.AreEqual("Index", result.ActionName);
        }



        #endregion
        #region Edit (GET)
        [TestMethod]
        public void EditInvalidMediaIdLoads404()
        {
            // arrange new done in TestInitialize instead

            // act            
            var result = (ViewResult)controller.Edit(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidIdLoads404()
        {

            // act            
            var result = (ViewResult)controller.Edit(1000).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsView()
        {
            //act
            var result = (ViewResult)controller.Edit(6).Result;

            //assert
            Assert.AreEqual("Edit", result.ViewName);

        }

        [TestMethod]
        public void EditValidIdLoadsMedia()
        {
            //act
            var result = (ViewResult)controller.Edit(6).Result;

            //assert
            Assert.AreEqual(_context.Medias.Find(6), result.Model);
        }


            #endregion
        #region Edit (POST)
        [TestMethod]
        public void EditPostInvalidMediaIdLoads404()
        {
            // arrange new done in TestInitialize instead
            Media media = _context.Medias.First();
            IFormFile newPoster = null;
            String currentPoster = "";

            // act            
            var result = (ViewResult)controller.Edit(9999999, media, newPoster, currentPoster).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        #endregion
        #region Delete (GET)
        [TestMethod]
        public void DeleteNullIdLoads404()
        {
            // act            
            var result = (ViewResult)controller.Delete(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidIdLoads404()
        {
            // act            
            var result = (ViewResult)controller.Delete(1000).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            // act            
            var result = (ViewResult)controller.Delete(6).Result;

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsData()
        {


            // act            
            var result = (ViewResult)controller.Delete(6).Result;

            // assert
            Assert.AreEqual(_context.Medias.Find(6), result.Model);
        }

        #endregion
        #region DeleteConfirmed (POST)
        [TestMethod]
        public void DeleteConfirmedLoadsView()
        {
            // act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(6).Result;

            // assert
            Assert.AreEqual("Index", result.ActionName);
        }


            #endregion
        }

}
