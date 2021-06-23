using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Volvo.Controllers;
using Volvo.Models;
using Xunit;

namespace VolvoXUnitTestProject
{
    public class UnitTest1
    {


        [Fact]
        public async void Test_IndexGetAsync()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // Act
            string erro = null;
            var result = await caminhao.Index(erro) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_DetailsGet_Id_null()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int? id = null;
            // assert
            await Assert.ThrowsAsync<Exception>(() => caminhao.Details(id));
        }

        [Fact]
        public async void Test_DetailsGet_Id_False()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int id = -1;
            // assert
            await Assert.ThrowsAsync<Exception>(() => caminhao.Details(id));

        }

        [Fact]
        public void Test_CreateGet()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // Act
            string erro = null;
            var result = caminhao.Create(erro) as ViewResult;
            // Assert
            Assert.NotNull(result);
        }       

        [Fact]
        public async void Test_CreatePostModeloInvalido()
        {
            // Act
            CaminhaoController caminhao = new CaminhaoController();
            Caminhao c = new Caminhao();
            c.Id = 1;
            c.Modelo = null;
            c.Ano = 1111;
            c.AnoModelo = 2222;
            c.Chassi = "";            
            
            // Act
            var result = (RedirectToActionResult) await caminhao.Create(c);
            //var result = (RedirectToActionResult)controller.Index();
            //var testeResult = result.RouteValues.Values.ToString();
            // Assert
           
            Assert.Equal("Create", result.ActionName);
           // Assert.NotNull(result);

        }

        [Fact]
        public async void Test_CreatePostAnoModeloInvalido()
        {
            // Act
            CaminhaoController caminhao = new CaminhaoController();
            Caminhao c = new Caminhao();
            c.Id = 1;
            c.Modelo = "FH";
            c.Ano = 1111;
            c.AnoModelo = 0;
            c.Chassi = "";

            var result = (RedirectToActionResult)await caminhao.Create(c);
            //var result = (RedirectToActionResult)controller.Index();
            //var testeResult = result.RouteValues.Values.ToString();
            // Assert

            Assert.Equal("Create", result.ActionName);

        }         

        [Fact]
        public async void Test_EditGet_Id_False()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int? id = -1;
            string erro = null;
            // assert
            
            var result = (RedirectToActionResult)await caminhao.Edit(id, erro);           

            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async void Test_EditGet_Id_Null()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int? id = 0;
            string erro = null;
            // assert

            var result = (RedirectToActionResult)await caminhao.Edit(id, erro);

            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async void Test_EditPost_Id_False()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int id = -1;
            Caminhao c = new Caminhao();
            c.Id = 1;
            c.Modelo = null;
            c.Ano = 1111;
            c.AnoModelo = 2222;
            c.Chassi = "";
            string erro = null;
            // assert
            var result = (RedirectToActionResult)await caminhao.Edit(id, c);

            Assert.Equal("Edit", result.ActionName);
        }

        [Fact]
        public async void Test_EditPost_Id_False2()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int id = 2;
            Caminhao c = new Caminhao();
            c.Id = -1;
            c.Modelo = null;
            c.Ano = 1111;
            c.AnoModelo = 2222;
            c.Chassi = "";
            string erro = null;
            // assert
            var result = (RedirectToActionResult)await caminhao.Edit(id, c);

            Assert.Equal("Edit", result.ActionName);

        }

        [Fact]
        public async void Test_EditPost_ModeloFalse()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int id = 2;
            Caminhao c = new Caminhao();
            c.Id = 0;
            c.Modelo = null;
            c.Ano = 1111;
            c.AnoModelo = DateTime.Now.Year;
            c.Chassi = "";
            string erro = null;
            // assert
            var result = (RedirectToActionResult)await caminhao.Edit(id, c);

            Assert.Equal("Edit", result.ActionName);

        }

        [Fact]
        public async void Test_EditPost_AnoModeloFalse()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int id = 2;
            Caminhao c = new Caminhao();
            c.Id = 0;
            c.Modelo = "FH";
            c.Ano = 0;
            c.AnoModelo = 2222;
            c.Chassi = "";
            string erro = null;
            // assert
            var result = (RedirectToActionResult)await caminhao.Edit(id, c);

            Assert.Equal("Edit", result.ActionName);

        }

        [Fact]
        public async void Test_DeleteGet()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int? id = -1;

            // assert
            await Assert.ThrowsAsync<Exception>(() => caminhao.Delete(id));
        }

        [Fact]
        public async void Test_DeleteGet_Id_Null()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int? id = null;

            // assert
            await Assert.ThrowsAsync<Exception>(() => caminhao.Delete(id));

        }

        [Fact]
        public async void Test_DeleteGet_Id_False()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int id = -1;

            // assert
            await Assert.ThrowsAsync<Exception>(() => caminhao.DeleteConfirmed(id));

        }

        [Fact]
        public void Test_CaminhaoExists()
        {
            CaminhaoController caminhao = new CaminhaoController();
            // act
            int id = -1;

            // assert
            Assert.False(caminhao.CaminhaoExists(id));

        }

        [Fact]
        public void Test_GetAnosModelos()
        {
            //act
            AnoModeloCaminhao anoModeloCaminhao = new AnoModeloCaminhao();

            // assert
            Assert.Equal(typeof(List<AnoModeloCaminhao>), anoModeloCaminhao.GetAnosModelos().GetType());

        }

        [Fact]
        public void Test_GetModelos()
        {
            //act
            ModeloCaminhao modeloCaminhao = new ModeloCaminhao();

            // assert
            Assert.Equal(typeof(List<ModeloCaminhao>), modeloCaminhao.GetModelos().GetType());

        }

        //[Fact]
        //public void Test_Error()
        //{
        //    CaminhaoController caminhao = new CaminhaoController();
        //    // Act
        //    var result = caminhao.Error() as ViewResult;
        //    // Assert
        //    Assert.NotNull(result);
        //}


    }
}
