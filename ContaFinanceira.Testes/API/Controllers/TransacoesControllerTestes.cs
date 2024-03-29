﻿using ContaFinanceira.API.Controllers;
using ContaFinanceira.Domain.Interfaces.Services;
using ContaFinanceira.Domain.Requests;
using ContaFinanceira.Domain.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContaFinanceira.Testes.API.Controllers
{
    public class TransacoesControllerTestes
    {
        private readonly Mock<ITransacaoService> _transacaoService;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<ILogger<TransacoesController>> _logger;

        public TransacoesControllerTestes()
        {
            _transacaoService = new Mock<ITransacaoService>();
            _httpContext = new Mock<IHttpContextAccessor>();
            _logger = new Mock<ILogger<TransacoesController>>();
        }

        [Fact]
        public async Task Adicionar_Sucesso()
        {
            //Arrange
            var request = new TransacaoRequest() { Valor = 10M };
            var response = new List<TransacaoResponse>() { new TransacaoResponse() { Id = 1, Data = DateTime.Now, Valor = 10M } };

            _transacaoService
                .Setup(x => x.Adicionar(request))
                .ReturnsAsync(response);

            _httpContext
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(new List<Claim>() { new Claim(ClaimTypes.Sid, "1") });

            var controller = new TransacoesController(_transacaoService.Object, _httpContext.Object, _logger.Object);

            //Act
            var result = await controller.Adicionar(request);

            //Assert
            var model = Assert.IsAssignableFrom<CreatedResult>(result);
            Assert.Equal(201, model.StatusCode);
            var lista = Assert.IsAssignableFrom<List<TransacaoResponse>>(model.Value);
            Assert.Equal(response.First().Id, lista.First().Id);
            Assert.Equal(response.First().Data, lista.First().Data);
            Assert.Equal(response.First().Valor, lista.First().Valor);
        }

        [Fact]
        public async Task Adicionar_Erro_Validacao()
        {
            //Arrange
            var request = new TransacaoRequest() { Valor = 10M };

            _transacaoService
                .Setup(x => x.Adicionar(request))
                .ThrowsAsync(new ValidationException("Conta inválida."));

            _httpContext
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(new List<Claim>() { new Claim(ClaimTypes.Sid, "1") });

            var controller = new TransacoesController(_transacaoService.Object, _httpContext.Object, _logger.Object);

            //Act
            var result = await controller.Adicionar(request);

            //Assert
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.Equal(400, model.StatusCode);
            Assert.Equal("Conta inválida.", model.Value);
        }

        [Fact]
        public async Task Adicionar_Erro()
        {
            //Arrange
            var request = new TransacaoRequest() { Valor = 10M };
            var response = new List<TransacaoResponse>() { new TransacaoResponse() { Id = 1, Data = DateTime.Now, Valor = 10M } };

            _transacaoService
                .Setup(x => x.Adicionar(request))
                .ThrowsAsync(new Exception("Erro ao comunicar-se com banco de dados."));

            _httpContext
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(new List<Claim>() { new Claim(ClaimTypes.Sid, "1") });

            var controller = new TransacoesController(_transacaoService.Object, _httpContext.Object, _logger.Object);

            //Act
            var result = await controller.Adicionar(request);

            //Assert
            var model = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(500, model.StatusCode);
            Assert.Equal("Erro ao comunicar-se com banco de dados.", model.Value);
        }
    }
}
