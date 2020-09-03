using Motoroal.CodingExercise.Repository.Interfaces;
using Motoroal.CodingExercise.Repository.Repos;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Motorola.CodingExercise.Repository.Test.Repos
{
    
    public class MarsRoverPhotoRepoTest
    {
        private IMarsRoverPhotoRepo _marsRaverPhotoRepo;

        [SetUp]
        public void Setup()
        {
            _marsRaverPhotoRepo = new MarsRoverPhotoRepo();
        }

        [Test]
        public async Task GetMarsRoverPhotosByDate_ValidDate_ReturnPhotos_GreaterThan0()
        {
           var photoList = await _marsRaverPhotoRepo.GetMarsRoverPhotosByDate("2016-05-13");
           Assert.IsNotNull(photoList);
           Assert.Greater(photoList.Count, 0);
        }

        [Test]
        public async Task GetMarsRoverPhotosByDate_ValidDate_ReturnPhotos_Zero()
        {
            var photoList = await _marsRaverPhotoRepo.GetMarsRoverPhotosByDate("2020-09-02");
            Assert.IsNotNull(photoList);
            Assert.AreEqual(0, photoList.Count);
        }

        [Test]               
        public void GetMarsRoverPhotosByDate_InValidDate()
        {
             Assert.ThrowsAsync<Exception>(() => _marsRaverPhotoRepo.GetMarsRoverPhotosByDate("2020-02-31"));            
        }

        [Test]
        public void GetMarsRoverPhotosByDate_InValidDate_RandomString()
        {
            Assert.ThrowsAsync<Exception>(() => _marsRaverPhotoRepo.GetMarsRoverPhotosByDate("TestWithNonDateValue"));
        }
    }
}
