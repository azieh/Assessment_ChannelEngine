using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Assessment_ChannelEngine.Core.Wrapper;
using Assessment_ChannelEngine.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Assessment_ChannelEngine.UnitTest
{
    public class Tests
    {
        private IGenericRestClient _sut;
        private Mock<IHttpClientWrapper> _httpClientWrapperMock;

        [SetUp]
        public void Setup()
        {
            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _sut = new GenericRestClient(_httpClientWrapperMock.Object);
        }
        [TearDown]
        public void TearDown()
        {
            _httpClientWrapperMock = null;
            _sut = null;
        }

        [Test]
        public void GenericRestClient_throw_exception_when_httpClientWrapper_isNull()
        {
            Assert.Throws<ArgumentNullException>(() => new GenericRestClient(null));
        }

        [Test]
        public void GenericRestClient_PostAsync_throw_exception_when_Configure_method_was_not_called_before()
        {
            //Act && Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.PostAsync<string, object>("http://someUrl.com", new object()));
        }

        [Test]
        public void GenericRestClient_PostAsync_throw_exception_when_apiUrl_isNull()
        {
            //Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.PostAsync<string, object>(null, new object()));
        }

        [Test]
        public void GenericRestClient_PostAsync_throw_exception_when_body_isNull()
        {
            //Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.PostAsync<string, object>("http://someUrl.com", null));
        }

        [Test]
        public void GenericRestClient_GetAsync_throw_exception_when_Configure_method_was_not_called_before()
        {
            //Act && Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.GetAsync<string>("http://someUrl.com"));
        }

        [Test]
        public void GenericRestClient_GetAsync_throw_exception_when_apiUrl_isNull()
        {
            //Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.GetAsync<string>(null));
        }

        [Test]
        public void GenericRestClient_Configure_throw_exception_when_Url_isNull()
        {
            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => _sut.Configure(null, "1234"));
        }

        [Test]
        public void GenericRestClient_Configure_throw_exception_when_ApiKey_isNull()
        {
            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => _sut.Configure("http://someUrl.com", null));
        }

        [Test]
        public async System.Threading.Tasks.Task GenericRestClient_GetAsync_return_ok()
        {
            //Arrange
            var dataAsString = JsonSerializer.Serialize("Result");
            _httpClientWrapperMock.Setup(_ => _.GetStreamAsync(It.IsAny<string>())).ReturnsAsync(() => GenerateStreamFromString(dataAsString));
            _sut.Configure("http://someUrl.com/", "1234");

            //Act
            var result = await _sut.GetAsync<string>("api");

            //Assert
            Assert.AreEqual("Result", result);
        }

        [Test]
        public async System.Threading.Tasks.Task GenericRestClient_PostAsync_return_ok()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            var dataAsString = JsonSerializer.Serialize("Result");
            httpResponseMessage.Content = new StringContent(dataAsString);
            _httpClientWrapperMock.Setup(_ => _.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(() => httpResponseMessage);
            _sut.Configure("http://someUrl.com/", "1234");

            //Act
            var result = await _sut.PostAsync<string, object>("api", new object());

            //Assert
            Assert.AreEqual("Result", result);
        }

        private  Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}