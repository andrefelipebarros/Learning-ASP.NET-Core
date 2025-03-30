using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("pessoa")]
public class PessoaControle : ControllerBase
{

    //Atributo Pessoa Repositorio
    private readonly PessoaRepositorio _pessoaRepositorio;

    public PessoaControle(PessoaRepositorio pessoaRepositorio)
    {
        _pessoaRepositorio = pessoaRepositorio;
    }

    //Rota de Cadastro
    [HttpPost]
    public Pessoa Cadastrar([FromBody] Pessoa p)
    {
        var obj = _pessoaRepositorio.CadastrarPessoa(p);
        return obj;
    }

    [HttpGet]
    public List<Pessoa> Selecionar()
    {
        var lista = _pessoaRepositorio.SelecionarPessoas();
        return lista;
    }
}