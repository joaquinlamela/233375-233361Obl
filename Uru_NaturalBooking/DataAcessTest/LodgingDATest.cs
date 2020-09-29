﻿using Castle.Core.Internal;
using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcessTest
{
    [TestClass]
    public class LodgingDATest
    {
        TouristSpot touristSpot; 
        Lodging lodging; 
        

        [TestInitialize]
        public void SetUp()
        {
            touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Maldonado",
                Description = "Departamento donde la naturaleza y la tranquilidad desborda."
            };

            lodging = new Lodging()
            {
                Id = Guid.NewGuid(), 
                Name = "Hotel Las Cumbres",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150,
                TouristSpot = touristSpot,
            };
        }

        [TestMethod]
        public void TestAddLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            lodgingRepository.Add(lodging);

            List<Lodging> listOfLodging = lodgingRepository.GetAll().ToList();

            Assert.AreEqual(lodging, listOfLodging[0]);
        }

        [TestMethod]
        public void TestGetLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            lodgingRepository.Add(lodging);
            Lodging lodgingOfDb = lodgingRepository.Get(lodging.Id);

            Assert.AreEqual(lodgingOfDb, lodgingOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void GetLodgingDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            Lodging lodgingOfDb = lodgingRepository.Get(lodging.Id);
        }

        [TestMethod]
        public void TestRemoveLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            lodgingRepository.Add(lodging);
            List<Lodging> listOfLodginfOfDbBeforeRemove = lodgingRepository.GetAll().ToList(); 
            lodgingRepository.Remove(lodging);

            List<Lodging> listOfLodgingOfDbAfterRemove = lodgingRepository.GetAll().ToList();

            CollectionAssert.AreNotEqual(listOfLodginfOfDbBeforeRemove, listOfLodgingOfDbAfterRemove); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestRemoveLodgingDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            lodgingRepository.Remove(lodging);
        }

        [TestMethod]
        public void TestUpdateLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            lodgingRepository.Add(lodging);

            lodging.Name = "Hotel Enjoy Conrad";

            lodgingRepository.Update(lodging);

            List<Lodging> listOfLodgings = lodgingRepository.GetAll().ToList();

            Assert.AreNotEqual("Hotel Las Cumbres", listOfLodgings[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestUpdateLodgingInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            lodgingRepository.Update(lodging);
        }

        [TestMethod]
        public void TestGetAllLodgingsOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);

            Lodging lodgingOfConrad = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Enjoy Conrad",
                QuantityOfStars = 5,
                Address = "Parada 4 Playa Mansa, Rambla Claudio Williman",
                PricePerNight = 1500,
                TouristSpot = touristSpot,
            };

            lodgingRepository.Add(lodging);
            lodgingRepository.Add(lodgingOfConrad);

            List<Lodging> listWithOriginalsLodgings = new List<Lodging>();
            listWithOriginalsLodgings.Add(lodging);
            listWithOriginalsLodgings.Add(lodgingOfConrad);

            List<Lodging> listOfLodgingOfDb = lodgingRepository.GetAll().ToList();

            CollectionAssert.AreEqual(listWithOriginalsLodgings, listOfLodgingOfDb);
        }

    }
}
