using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Controllers;
using KPWebApp.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KPWebApp.UnitTests
{
    //[TestClass]
    //public class UnitTest1
    //{
    //    [TestMethod]
    //    public void TestMethod1()
    //    {
    //        //Arrange
    //        Mock<IRepository> mock = new Mock<IRepository>();
    //        mock.Setup(m => m.PostsCollection).Returns(new Post[]
    //        {
    //            new Post {Category = PostCategory.News, Header = "New1", PostId = 1, Text = "SomeText1", User = new User{Role = Role.Administrator}, Time = DateTime.Now},
    //            new Post {Category = PostCategory.News, Header = "New2", PostId = 2, Text = "SomeText2", User = new User{Role = Role.Administrator}, Time = DateTime.Now},
    //            new Post {Category = PostCategory.News, Header = "New3", PostId = 3, Text = "SomeText3", User = new User{Role = Role.Administrator}, Time = DateTime.Now},
    //            new Post {Category = PostCategory.News, Header = "New4", PostId = 4, Text = "SomeText4", User = new User{Role = Role.Administrator}, Time = DateTime.Now},
    //            new Post {Category = PostCategory.News, Header = "New5", PostId = 5, Text = "SomeText5", User = new User{Role = Role.Administrator}, Time = DateTime.Now},
    //        }.AsQueryable());
    //        PostController controller = new PostController(mock.Object);
    //        controller.PageCapacity = 3;

    //        //Act
    //        PostsListViewModel result = (PostsListViewModel)controller.List("News", 2).Model;

    //        //Assert
    //        Post[] postArray = result.Posts.ToArray();
    //        Assert.IsTrue(postArray.Length == 2);
    //        Assert.AreEqual(postArray[0].Header, "New4");
    //        Assert.AreEqual(postArray[1].Header, "New5");
    //    }
    //}
}
