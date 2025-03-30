var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var stringDeConexao = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");

builder.Services.AddSingleton(new PessoaRepositorio(stringDeConexao));

var app = builder.Build();

app.MapControllers();
app.Run();