using MySql.Data.MySqlClient;

public class PessoaRepositorio
{
    //Atributo contendo a string de conexão
    private readonly string? _stringDeConexao;

    //Constructor
    public PessoaRepositorio(string stringDeConexao)
    {
        _stringDeConexao = stringDeConexao;
    }

    // Método para cadastrar
    public Pessoa CadastrarPessoa(Pessoa p)
    {
    // Configurar a conexão
    using var conexao = new MySqlConnection(_stringDeConexao);

    // Realizar a conexão
    conexao.Open();

    // SQL
    string comandoSQL = "INSERT INTO pessoa (nome, cidade, idade) VALUES (@nome, @cidade, @idade);";
    comandoSQL+="SELECT LAST_INSERT_ID();";

    // Variável contendo a ação SQL
    using var comando = new MySqlCommand(comandoSQL, conexao);

    // Informar os parâmetros (nome, cidade e idade)
    comando.Parameters.AddWithValue("@nome", p.Nome);
    comando.Parameters.AddWithValue("@cidade", p.Cidade);
    comando.Parameters.AddWithValue("@idade", p.Idade);

    // Executar comando e retornar o código gerado
    // ExecuteScalar -> Retorna a primeira linha/coluna
    int codigoGerado = Convert.ToInt32(comando.ExecuteScalar());

    // Especificar o código gerado no objeto p
    p.Id = codigoGerado;

    // Retorno
    return p;
    }

    // Método para selecionar
    public List<Pessoa> SelecionarPessoas()
    {
        // Cria uma variável chamada pessoas do tipo List<Pessoa>
        List<Pessoa> pessoas = [];

        // Configurar a conexão
        using var conexao = new MySqlConnection(_stringDeConexao);

        // Realizar a conexão
        conexao.Open();

        // Criar comando SQL para seleção de todas as pessoas
        using var comandoSQL = new MySqlCommand("SELECT * FROM pessoa", conexao);
        
        // Executar comando SQL e armazenar todos os registros
        using var registros = comandoSQL.ExecuteReader();
        
        // Laço de repetição
        while (registros.Read())
        {
            // Adicionar cada linha da tabela na variável pessoas
            pessoas.Add(new Pessoa
            {
                Id = registros.GetInt32("id"),
                Nome = registros.GetString("nome"),
                Cidade = registros.GetString("cidade"),
                Idade = registros.GetInt32("idade")
            });

        }

        // Retorno
        return pessoas;
    }
}