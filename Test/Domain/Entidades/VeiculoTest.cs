using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void TestarGetSet()
    {
        var veiculo = new Veiculo();

        veiculo.Id = 1;
        veiculo.Nome = "Fiesta 2.0";
        veiculo.Marca = "Ford";
        veiculo.Ano = 2019;

        Assert.AreEqual(1,  veiculo.Id);
        Assert.AreEqual("Fiesta 2.0",  veiculo.Nome);
        Assert.AreEqual("Ford",  veiculo.Marca);
        Assert.AreEqual(2019,  veiculo.Ano);
    }
}