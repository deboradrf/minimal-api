using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Servicos;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        
        var configuration = builder.Build();

        return new DbContexto(configuration);
    }

    [TestMethod]
    public void TestarIncluirVeiculo()
    {
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo
        {
            Nome = "Veículo Teste",
            Marca = "Marca Teste"
        };

        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo);

        Assert.AreEqual(1, veiculoServico.Todos(1).Count());
    }

    [TestMethod]
    public void TestarBuscaPorId()
    {
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo
        {
            Nome = "Veículo Teste",
            Marca = "Marca Teste"
        };

        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo);

        var veiculoBanco = veiculoServico.BuscaPorId(veiculo.Id);
        Assert.IsNotNull(veiculoBanco);
        Assert.AreEqual(veiculo.Nome, veiculoBanco.Nome);
    }

    [TestMethod]
    public void TestarAtualizarVeiculo()
    {
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo
        {
            Nome = "Veículo Teste",
            Marca = "Marca Teste"
        };

        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo);

        veiculo.Nome = "Veículo Atualizado";
        veiculoServico.Atualizar(veiculo);

        var veiculoBanco = veiculoServico.BuscaPorId(veiculo.Id);
        Assert.AreEqual("Veículo Atualizado", veiculoBanco.Nome);
    }

    [TestMethod]
    public void TestarApagarVeiculo()
    {
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo
        {
            Nome = "Veículo Teste",
            Marca = "Marca Teste"
        };

        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo);

        veiculoServico.Apagar(veiculo);
        var veiculoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        Assert.IsNull(veiculoBanco);
    }

    [TestMethod]
    public void TestarListarVeiculos()
    {
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo1 = new Veiculo { Nome = "Veículo 1", Marca = "Marca 1" };
        var veiculo2 = new Veiculo { Nome = "Veículo 2", Marca = "Marca 2" };

        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo1);
        veiculoServico.Incluir(veiculo2);

        var veiculos = veiculoServico.Todos(1);
        Assert.AreEqual(2, veiculos.Count());
    }
}