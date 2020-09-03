using Moq;
using Motoroal.CodingExercise.Repository.Interfaces;
using Motoroal.CodingExercise.Repository.Models;
using Motorola.CodingExercise.Service.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Motorola.CodingExercise.Service.Test
{   
    public class MarsRoverPhotoControllerTest
    {
        private Mock<IMarsRoverPhotoRepo> _marsRaverPhotoRepoMock;
        private MarsRoverPhotoController _marsRoverPhotoController;

        [SetUp]
        public void Setup()
        {
            _marsRaverPhotoRepoMock = new Mock<IMarsRoverPhotoRepo>();
        }

        [Test]
        public async Task GetMarsRoverPhotos_Return_Results()
        {           
            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate("2017-02-27")).Returns(Task.FromResult(MarsRoverPhotos("2017-02-27")));
            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate(It.IsNotIn<string>(new[] { "2017-02-27" }) )).Returns(Task.FromResult(new List<MarsRoverPhoto>()));
            _marsRoverPhotoController = new MarsRoverPhotoController(_marsRaverPhotoRepoMock.Object);

            var photos = await _marsRoverPhotoController.GetMarsRoverPhotos();

            var retList = (List<MarsRoverPhoto>)(((OkObjectResult)photos).Value);
            Assert.True(retList.Count == 2);
        }

        [Test]
        public async Task GetMarsRoverPhotos_Return_Results_Download_Photos_Exists()
        {
            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate("2017-02-27")).Returns(Task.FromResult(MarsRoverPhotos("2017-02-27")));
            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate(It.IsNotIn<string>(new[] { "2017-02-27" }))).Returns(Task.FromResult(new List<MarsRoverPhoto>()));
            _marsRoverPhotoController = new MarsRoverPhotoController(_marsRaverPhotoRepoMock.Object);

            var photos = await _marsRoverPhotoController.GetMarsRoverPhotos();

            string earthDate = "2017-02-27";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}");
            bool directoryExists = Directory.Exists(path);
            
            Assert.True(directoryExists);

            string imgFilePath = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}\1.JPG");
            Assert.True(System.IO.File.Exists(imgFilePath));

            imgFilePath = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}\2.JPG");
            Assert.True(System.IO.File.Exists(imgFilePath));
        }

        [Test]
        public async Task GetMarsRoverPhotos_Return_Results_Download_Photos_NotExists()
        {
            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate("2017-02-27")).Returns(Task.FromResult(MarsRoverPhotos("2017-02-27")));
            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate("2018-06-02")).Returns(Task.FromResult(MarsRoverPhotos("2018-06-02")));

            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate(It.IsNotIn<string>(new[] { "2017-02-27", "2018-06-02" }))).Returns(Task.FromResult(new List<MarsRoverPhoto>()));
            _marsRoverPhotoController = new MarsRoverPhotoController(_marsRaverPhotoRepoMock.Object);

            var photos = await _marsRoverPhotoController.GetMarsRoverPhotos();

            string earthDate = "2017-02-27";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}");
            bool directoryExists = Directory.Exists(path);

            Assert.True(directoryExists);

            string imgFilePath = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}\3.JPG");
            Assert.False(System.IO.File.Exists(imgFilePath));

            earthDate = "2018-06-02";
            path = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}");
            Assert.True(Directory.Exists(path));

            imgFilePath = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}\3.JPG");
            Assert.True(System.IO.File.Exists(imgFilePath));

            imgFilePath = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}\1.JPG");
            Assert.False(System.IO.File.Exists(imgFilePath));
        }

        [Test]
        public async Task GetMarsRoverPhotos_Return_Results_Download_Photos_InRightFoldres()
        {
           _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate("2017-02-27")).Returns(Task.FromResult(MarsRoverPhotos("2017-02-27")));
            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate("2018-06-02")).Returns(Task.FromResult(MarsRoverPhotos("2018-06-02")));

            _marsRaverPhotoRepoMock.Setup(x => x.GetMarsRoverPhotosByDate(It.IsNotIn<string>(new[] { "2017-02-27", "2018-06-02" }))).Returns(Task.FromResult(new List<MarsRoverPhoto>()));
            _marsRoverPhotoController = new MarsRoverPhotoController(_marsRaverPhotoRepoMock.Object);

            var photos = await _marsRoverPhotoController.GetMarsRoverPhotos();

            string earthDate = "2016-07-13";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $@"Downloads\{earthDate}");
            bool directoryExists = Directory.Exists(path);

            Assert.False(directoryExists);
        }

        private List<MarsRoverPhoto> MarsRoverPhotos(string formattedDate)
        {
            var photos = new List<MarsRoverPhoto>();

            if(formattedDate == "2017-02-27")
            {
                photos.Add(new MarsRoverPhoto { earth_date = "2017-02-27", id = 1, img_src = "http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01004/opgs/edr/fcam/FLB_486615455EDR_F0481570FHAZ00323M_.JPG" });
                photos.Add(new MarsRoverPhoto { earth_date = "2017-02-27", id = 2, img_src = "http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01004/opgs/edr/fcam/FRB_486615455EDR_F0481570FHAZ00323M_.JPG" });
            }
            else if(formattedDate == "2018-06-02")
            {
                photos.Add(new MarsRoverPhoto { earth_date = "2018-06-02", id = 3, img_src = "http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01004/opgs/edr/fcam/FRB_486615455EDR_F0481570FHAZ00323M_.JPG" });
            }

            return photos;
        }
    }
}
